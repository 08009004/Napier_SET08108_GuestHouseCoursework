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
     * last modified: 2016-12-09
     */
    public partial class MainWindow : Window
    {
        // PROPERTIES:

        // reference to a ModelFacade instance:
        private ModelFacade mFacade;

        // METHODS:

        /*
         * Constructor.
         */
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
            refreshDisplay();
        }

        /*
         * Refreshes all fields displayed in the window according to 
         * current system objects states.
         */
        private void refreshDisplay()
        {
            if (mFacade.IsABookingLoaded())
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
            DateTime start;
            DateTime end;
            mFacade.GetCurrentBookDates(out start, out end);

            // update labels content:
            lblBookingNbValue.Content = mFacade.GetCurrentBookNb().ToString();
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
            // update labels content:
            lblCustomerNameValue.Content = mFacade.GetCurrentCustName();
            lblCustomerNbValue.Content = mFacade.GetCurrentCustNb().ToString();

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
            foreach (String g in mFacade.GetGuestNames())
            {
                lstGuests.Items.Add(g);
            }

            // make label & list box visible:
            lblGuests.Visibility = Visibility.Visible;
            lstGuests.Visibility = Visibility.Visible;
        }

        /*
         * Empties all boxes displayed in the MainWindow.
         */
        private void clearDisplay()
        {
            // hide booking data display:
            lblBookingNbValue.Visibility = Visibility.Hidden;
            lblArrivalValue.Visibility = Visibility.Hidden;
            lblDepartureValue.Visibility = Visibility.Hidden;

            // hide customer data display:
            lblCustomerNbValue.Visibility = Visibility.Hidden;
            lblCustomerNameValue.Visibility = Visibility.Hidden;

            // hide guests data display:
            lstGuests.Items.Clear();
        }

        /*
         * Closes the current booking.
         */
        private void btnCloseBooking_Click(object sender, RoutedEventArgs e)
        {
            mFacade.CurrentBookingClose();
            clearDisplay();
        }
        
        /*
         * Opens a WindowCreateEdit dialog to view & edit current booking
         * or create a new one.
         */
        private void btnNewEdit_Click(object sender, RoutedEventArgs e)
        {
            new WindowCreateEdit(mFacade).ShowDialog();
            refreshDisplay();
        }

        /*
         * Opens a WindowInvoice dialog to view current booking invoice.
         */
        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (!mFacade.IsABookingLoaded())
            {
                MessageBox.Show("There is no booking loaded in the system"
                                + " yet.\r\n"
                                + "Load a booking to view it's invoice.");
            }
            else
            {
                new WindowInvoice(mFacade).ShowDialog();
            }
        }

        /*
         * Saves the current system state when the program is closed.
         */
        private void Window_Closing(object sender, 
                                    System.ComponentModel.CancelEventArgs e)
        {
            mFacade.PersistSystemState();
            MessageBox.Show("Cheerio");
        }

        /*
         * Deletes the booking curently loaded in the system.
         */
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int bookingNb = mFacade.GetCurrentBookNb();
            mFacade.CurrentBookingClose();
            mFacade.DeleteBooking(bookingNb);
            clearDisplay();
        }
    }
}
