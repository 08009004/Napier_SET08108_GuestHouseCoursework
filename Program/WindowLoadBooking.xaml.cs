using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Program
{
    /*
     * Interaction logic for WindowLoadBooking.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-07
     */
    public partial class WindowLoadBooking : Window
    {
        // Property:
        // points to a ModelFacade instance.
        private ModelFacade mFacade;

        /*
         * Constructor.
         */
        public WindowLoadBooking(ModelFacade mFacade)
        {
            this.mFacade = mFacade;
            InitializeComponent();
            refreshDisplay();
        }

        /*
         * Refreshes all fields displayed in the window according to 
         * current system objects states.
         */
        private void refreshDisplay()
        {
            clearDisplay();

            lstBookings.ItemsSource = mFacade.GetAllBookingNbs();
        }

        /*
         * Refreshes all booking detail fields displayed in the window 
         * according to current booking state.
         */
        private void refreshBookDetailDisplay()
        {
            BookingComponent b = mFacade.CurrentBook;
            DateTime start;
            DateTime end;

            if (b != null)
            {
                // update field contents:
                b.GetDates(out start, out end);
                lblArrivalValue.Content = start.ToString().Substring(0, 10);
                lblDepartureValue.Content = end.ToString().Substring(0, 10);
                lblCustNameValue.Content = b.GetCustomer().Name;
                lstGuests.Items.Clear();
                foreach (PersonComponent g in b.GetGuests())
                {
                    lstGuests.Items.Add(g.Name);
                }

                // make labels visible:
                lblArrivalValue.Visibility = Visibility.Visible;
                lblDepartureValue.Visibility = Visibility.Visible;
                lblCustNameValue.Visibility = Visibility.Visible;
            }
            else
            {
                clearBookingDetails();
            }
        }

        /*
         * Empties all boxes displayed in the MainWindow.
         */
        private void clearDisplay()
        {
            //lblBooking.Visibility = Visibility.Hidden;
            lstBookings.Items.Clear();
            lstGuests.Items.Clear();
        }
        /*
         * Hide all all booking detail fields from the MainWindow.
         */
        private void clearBookingDetails()
        {
            lblArrivalValue.Visibility = Visibility.Hidden;
            lblCustNameValue.Visibility = Visibility.Hidden;
            lblDepartureValue.Visibility = Visibility.Hidden;
            lstGuests.Items.Clear();
        }

        /*
         * Closes the window.
         */
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*
         * loads selected booking into the system when double clicking on it.
         */
        private void lstBookings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<int> l = (List<int>) lstBookings.ItemsSource;
            mFacade.RestoreBooking(l.ElementAt(lstBookings.SelectedIndex));
            refreshBookDetailDisplay();
        }
    }
}
