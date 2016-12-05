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
    /// <summary>
    /// Interaction logic for NewBooking.xaml
    /// </summary>
    public partial class NewBooking : Window
    {
        private PersonFactory pf = PersonFactory.Instance;
        private DataPersistenceFacade dpf;
        private BookingComponent currentBooking;
        private PersonComponent currentCustomer;

        /*
         * The window constructor.
         */
        public NewBooking(DataPersistenceFacade dpf, BookingComponent currentBooking)
        {
            this.dpf = dpf;
            this.currentBooking = currentBooking;

            InitializeComponent();
            lblBookingRef.Content += "\r\n";

            if (currentBooking == null)
            {
                lblBookingRef.Visibility = Visibility.Hidden;
            }
            else
            {
                lblBookingRef.Content += currentBooking.GetBookingNb().ToString();

                this.currentCustomer = currentBooking.GetCustomer();
                displayCustomer(this.currentCustomer);
            }
        }

        /*
         * Clears all fields displayed in the window.
         */
        private void clearDisplay()
        {
            lblBookingRef.Content = "Booking number:\r\n";
            lblBookingRef.Visibility = Visibility.Hidden;

            dtpArrival.SelectedDate = null;
            dtpDeparture.SelectedDate = null;

            txtCustNumber.Text = String.Empty;
            txtCustName.Text = String.Empty;
            txtCustAddress.Text = String.Empty;

            lstGuests.Items.Clear();
        }

        /*
         * Refreshes window display according to current objects states.
         */
        private void refreshDisplay()
        {
            clearDisplay();

            if (currentBooking != null)
            {
                DateTime arrival;
                DateTime departure;
                currentBooking.GetDates(out arrival, out departure);
                dtpArrival.SelectedDate = arrival;
                dtpDeparture.SelectedDate = departure;

                lblBookingRef.Content += currentBooking.GetBookingNb().ToString();
                lblBookingRef.Visibility = Visibility.Visible;
                
                txtCustNumber.Text = currentBooking.GetCustomer().CustomerNb.ToString();
                txtCustName.Text = currentBooking.GetCustomer().Name;
                txtCustAddress.Text = ((Customer) currentBooking.GetCustomer()).Address;

                foreach (PersonComponent g in currentBooking.GetGuests())
                {
                    lstGuests.Items.Add(g.Name);
                }
            }
        }

        /*
         * Loads & displays the customer referenced by txtCustNumber.Text
         */
        private void btnLoadCust_Click(object sender, RoutedEventArgs e)
        {
            int customerNb;

            if (!Int32.TryParse(txtCustNumber.Text, out customerNb))
            {
                MessageBox.Show("Please enter a valid customer number.");
            }
            else
            {
                Dictionary<String, String> customerData;
                if (!dpf.Read(customerNb, out customerData))
                {
                    MessageBox.Show("Can't find customer number "
                                    + txtCustNumber.Text + ".\r\n"
                                    + "Please enter a valid"
                                    + " customer number.");
                }
                else
                {
                    currentCustomer = pf.RestoreCustomer(customerData);
                    displayCustomer(currentCustomer);
                }
            }
        }

        /*
         * Displays customer data in the window fields.
         */
        private void displayCustomer(PersonComponent customer) 
        {
            clearCustDisplay();
            txtCustNumber.Text = customer.CustomerNb.ToString();
            txtCustName.Text = customer.Name;
            txtCustAddress.Text = ((Customer)customer).Address;
        }

        /*
         * Clears all fields in the window.
         */
        private void clearCustDisplay()
        {
            txtCustNumber.Text = String.Empty;
            txtCustName.Text = String.Empty;
        }

        /*
         * Routine triggered upon clicking the 'New Customer' button.
         */
        private void btnNewCust_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCustName.Text)
             || String.IsNullOrWhiteSpace(txtCustAddress.Text))
            {
                MessageBox.Show("Please enter the customer's name"
                                + " and address.");
            }
            else
            {
                currentCustomer = pf.GetNewCustomer(txtCustName.Text,
                                                txtCustAddress.Text);
                displayCustomer(currentCustomer);
            }
        }

        /*
         * Routine triggered upon clicking the 'Save' button.
         */
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (validateValues())
            {
                currentBooking = BookingFactory.Instance.GetNewBooking(
                                        currentCustomer, 
                                        (DateTime) dtpArrival.SelectedDate,
                                        (DateTime) dtpDeparture.SelectedDate);

                MessageBox.Show("Booking saved.");

                if (lblBookingRef.Visibility == Visibility.Hidden)
                {
                    lblBookingRef.Content += currentBooking.GetBookingNb().ToString();
                    lblBookingRef.Visibility = Visibility.Visible;
                }
            }
        }

        /*
         * True if all the window fields are valid to create or amend a 
         * booking, otherwise false.
         * Displays error message windows.
         */
        private bool validateValues()
        {
            bool areValidValues = true;
            
            if (currentCustomer == null)
            {
                areValidValues = false;
                MessageBox.Show("Please create or load a customer for"
                                + " the booking before saving.");
            }

            if (dtpArrival.SelectedDate == null
             || dtpDeparture.SelectedDate == null)
            {
                areValidValues = false;
                MessageBox.Show("Please select arrival and depature dates"
                                + " for the booking before saving.");
            }

            if (dtpDeparture.SelectedDate <= dtpArrival.SelectedDate)
            {
                areValidValues = false;
                MessageBox.Show("Departure date must be strictly"
                                + " later than arrival date.");
            }

            return areValidValues;
        }

        /*
         * Routine executed upon clicking the 'Add Guest' button.
         */
        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            if (canAddGuest())
            {
                new NewGuest(currentBooking.GetGuests()).ShowDialog();
            }

            refreshDisplay();
        }

        /*
         * Routine executed upon clicking the 'Add to guests' button.
         */
        private void btnAddToGuest_Click(object sender, RoutedEventArgs e)
        {
            bool isCustAGuest = false;

            foreach (PersonComponent g in currentBooking.GetGuests())
            {
                if (g.CustomerNb > 0)
                {
                    isCustAGuest = true;
                    MessageBox.Show("The customer is already in the list"
                                    + " of guests.");
                }
            }

            if (canAddGuest() && !isCustAGuest)
            {
                new NewGuest(currentBooking.GetGuests(), 
                             currentCustomer).ShowDialog();
            }

            refreshDisplay();
        }

        private bool canAddGuest()
        {
            bool canAddGuest = true;

            if (currentBooking == null)
            {
                canAddGuest = false;
                MessageBox.Show("Please save the booking before adding"
                                + " guests.");
            }
            else if (currentBooking.GetGuests().Count >= 4)
            {
                canAddGuest = false;
                MessageBox.Show("This booking is already full, the"
                                + " maximum number of guests per booking"
                                + " is 4.");
            }
            
            return canAddGuest;
        }
    }
}
