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
        // PROPERTIES:

        // points to a ModelFacade instance.
        private ModelFacade mFacade;

        // METHODS:
        
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
            if (mFacade.IsABookingLoaded())
            {
                // update field contents:
                DateTime start;
                DateTime end;
                mFacade.GetCurrentBookDates(out start, out end);
                lblArrivalValue.Content = start.ToString().Substring(0, 10);
                lblDepartureValue.Content = end.ToString().Substring(0, 10);
                lblCustNameValue.Content = mFacade.GetCurrentCustName();
                lstGuests.Items.Clear();

                foreach (String g in mFacade.GetGuestNames())
                {
                    lstGuests.Items.Add(g);
                }

                // make labels visible:
                lblArrivalValue.Visibility = Visibility.Visible;
                lblDepartureValue.Visibility = Visibility.Visible;
                lblCustNameValue.Visibility = Visibility.Visible;
            }
            else
            {
                refreshDisplay();
            }
        }

        /*
         * Empties all boxes displayed in the MainWindow.
         */
        private void clearDisplay()
        {
            clearBookingDetails();

            lstBookings.ItemsSource = null;
            lstGuests.Items.Clear();
        }

        /*
         * Hide all booking detail fields from the window.
         */
        private void clearBookingDetails()
        {
            lblArrivalValue.Visibility = Visibility.Hidden;
            lblCustNameValue.Visibility = Visibility.Hidden;
            lblDepartureValue.Visibility = Visibility.Hidden;
            lstGuests.Items.Clear();
        }

        /*
         * loads selected booking into the system when double clicking on it.
         */
        private void lstBookings_MouseDoubleClick(object sender, 
                                                  MouseButtonEventArgs e)
        {
            List<int> l = (List<int>) lstBookings.ItemsSource;
            int i = lstBookings.SelectedIndex;

            if (i < 0 || !mFacade.RestoreBooking(l.ElementAt(i))) 
            {
                MessageBox.Show("Please double click on a booking from the"
                            + " list to load it into the system.");
            }

            refreshBookDetailDisplay();
        }
    }
}
