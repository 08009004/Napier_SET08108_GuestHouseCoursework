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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Properties
        // points to a ModelFacade instance:
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
            int bookingNb;
            
            if (!Int32.TryParse(txtBookingRef.Text, out bookingNb))
            {
                MessageBox.Show("Please enter a valid booking number.");
            }
            else if (!mFacade.RestoreBooking(bookingNb))
            {
                MessageBox.Show("Can't find booking number "
                                + txtBookingRef.Text + ".\r\n"
                                + "Please enter a valid"
                                + " booking number.");
            }

            refreshDisplay();
        }

        /*
         * Refreshes all fields displayed in the window.
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

            txtBookingRef.Text = b.GetBookingNb().ToString();
            
            lblArrival.Visibility = Visibility.Visible;
            lblDeparture.Visibility = Visibility.Visible;
            b.GetDates(out start, out end);
            lblArrivalDate.Content = start.ToString().Substring(0, 10);
            lblArrivalDate.Visibility = Visibility.Visible;
            lblDepartureDate.Content = end.ToString().Substring(0, 10);
            lblDepartureDate.Visibility = Visibility.Visible;
        }

        /*
         * Refreshes the customer fields displayed in the window.
         */
        private void refreshCustomerDisplay()
        {
            PersonComponent c = mFacade.CurrentCust;

            lblCustomer.Visibility = Visibility.Visible;
            lblCustNumber.Content = "Number: " + c.GetCustNb().ToString();
            lblCustNumber.Visibility = Visibility.Visible;
            lblCustName.Content = "Name: " + c.Name;
            lblCustName.Visibility = Visibility.Visible;
        }

        /*
         * Refreshes the guests fields displayed in the window.
         */
        private void refreshGuestsDisplay()
        {
            BookingComponent b = mFacade.CurrentBook;
            PersonComponent c = mFacade.CurrentCust;

            lblGuest.Visibility = Visibility.Visible;
            lstGuests.Visibility = Visibility.Visible;
            if (c.IsGuest())
            {
                lstGuests.Items.Add(c.Name);
            }
            foreach (PersonComponent g in b.GetGuests())
            {
                lstGuests.Items.Add(g.Name);
            }
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
            txtBookingRef.Text = String.Empty;
            lblArrival.Visibility = Visibility.Hidden;
            lblDeparture.Visibility = Visibility.Hidden;
            lblArrivalDate.Visibility = Visibility.Hidden;
            lblDepartureDate.Visibility = Visibility.Hidden;

            lblCustomer.Visibility = Visibility.Hidden;
            lblCustNumber.Visibility = Visibility.Hidden;
            lblCustName.Visibility = Visibility.Hidden;

            lblGuest.Visibility = Visibility.Hidden;
            lstGuests.Items.Clear();
            lstGuests.Visibility = Visibility.Hidden;
        }
        
        /*
         * Executed upon clicking the 'Create/Amend' button.
         */
        private void btnCreateAmend_Click(object sender, RoutedEventArgs e)
        {
            new NewBooking(this.mFacade).ShowDialog();
            refreshDisplay();
        }

    }
}
