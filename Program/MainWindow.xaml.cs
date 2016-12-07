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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Program
{
    /*
     * Interaction logic for MainWindow.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-07
     */
    public partial class MainWindow : Window
    {
        // Properties
        // reference to a ModelFacade instance:
        private ModelFacade mFacade;

        public MainWindow()
        {
            this.mFacade = new ModelFacade();
            InitializeComponent();
            clearDisplay();
        }

        /*
         * Loads and displays the booking referenced by txtBookingRef.Text.
         */
        private void btnLoadBooking_Click(object sender, RoutedEventArgs e)
        {
            new WindowLoadBooking(mFacade).ShowDialog();
            
            /*
            else if (!Int32.TryParse(txtBookingRef.Text, out bookingNb))
            {
                MessageBox.Show("Please enter a valid booking number.");
            }
            
            if (!mFacade.RestoreBooking(bookingNb))
            {
                MessageBox.Show("Can't find booking number "
                                + txtBookingRef.Text + ".\r\n"
                                + "Please enter a valid"
                                + " booking number.");
            }
             */

            refreshDisplay();
        }

        /*
         * Refreshes all fields displayed in the window according to 
         * current system objects states.
         */
        private void refreshDisplay()
        {
            if (mFacade.CurrentBook != null)
            {
                clearDisplay();
                refreshBookingDisplay();
                refreshCustomerDisplay();
                refreshGuestsDisplay();
            }
        }

        /*
         * Refreshes the booking fields displayed in the window.
         */
        private void refreshBookingDisplay()
        {
            BookingComponent b = mFacade.CurrentBook;
            DateTime start;
            DateTime end;
            b.GetDates(out start, out end);

            // update labels content:
            lblBookingNbValue.Content = b.GetBookingNb().ToString();
            lblArrivalValue.Content = start.ToString().Substring(0, 10);
            lblDepartureValue.Content = end.ToString().Substring(0, 10);
            
            // make labels visible:
            lblBooking.Visibility = Visibility.Visible;
            lblBookingNb.Visibility = Visibility.Visible;
            lblBookingNbValue.Visibility = Visibility.Visible;
            lblArrival.Visibility = Visibility.Visible;
            lblArrivalValue.Visibility = Visibility.Visible;
            lblDeparture.Visibility = Visibility.Visible;
            lblDepartureValue.Visibility = Visibility.Visible;
        }

        /*
         * Refreshes the customer fields displayed in the window.
         */
        private void refreshCustomerDisplay()
        {
            PersonComponent c = mFacade.CurrentCust;

            // update labels content:
            lblCustomerNameValue.Content = c.Name;
            lblCustomerNbValue.Content = c.GetCustNb().ToString();

            // make labels visible:
            lblCustomer.Visibility = Visibility.Visible;
            lblCustomerNb.Visibility = Visibility.Visible;
            lblCustomerNbValue.Visibility = Visibility.Visible;
            lblCustomerName.Visibility = Visibility.Visible;
            lblCustomerNameValue.Visibility = Visibility.Visible;
        }

        /*
         * Refreshes the guests fields displayed in the window.
         */
        private void refreshGuestsDisplay()
        {
            BookingComponent b = mFacade.CurrentBook;
            PersonComponent c = mFacade.CurrentCust;

            // update listbox content:
            if (c.IsGuest())
            {
                lstGuests.Items.Add(c.Name);
            }
            foreach (PersonComponent g in b.GetGuests())
            {
                lstGuests.Items.Add(g.Name);
            }

            // make label & list box visible:
            lblGuests.Visibility = Visibility.Visible;
            lstGuests.Visibility = Visibility.Visible;
        }

        /*
         * Executed upon clicking the 'Clear' button.
         */
        private void btnCloseBooking_Click(object sender, RoutedEventArgs e)
        {
            this.mFacade.CurrentBook = null;
            clearDisplay();
        }

        /*
         * Empties all boxes displayed in the MainWindow.
         */
        private void clearDisplay()
        {
            // hide booking data display:
            //lblBooking.Visibility = Visibility.Hidden;
            //lblBookingNb.Visibility = Visibility.Hidden;
            lblBookingNbValue.Visibility = Visibility.Hidden;
            //lblArrival.Visibility = Visibility.Hidden;
            lblArrivalValue.Visibility = Visibility.Hidden;
            //lblDeparture.Visibility = Visibility.Hidden;
            lblDepartureValue.Visibility = Visibility.Hidden;

            // hide customer data display:
            //lblCustomer.Visibility = Visibility.Hidden;
            //lblCustomerNb.Visibility = Visibility.Hidden;
            lblCustomerNbValue.Visibility = Visibility.Hidden;
            //lblCustomerName.Visibility = Visibility.Hidden;
            lblCustomerNameValue.Visibility = Visibility.Hidden;

            // hide guests data display:
            lstGuests.Items.Clear();
        }
        
        /*
         * Executed upon clicking the 'Create/Amend' button.
         */
        private void btnCreateAmend_Click(object sender, RoutedEventArgs e)
        {
            new NewBooking(this.mFacade).ShowDialog();
            refreshDisplay();
        }

        /*
         * Executed upon clicking the 'Save & Exit' button.
         */
        private void btnExitProgram_Click(object sender, RoutedEventArgs e)
        {
            mFacade.PersistSystemState();
            this.Close();
        }

    }
}
