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
     * Interaction logic for NewBooking.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public partial class NewBooking : Window
    {
        //Property:
        // reference to calling window's ModelFacade instance:
        private ModelFacade mFacade;

        /*
         * The window constructor.
         */
        public NewBooking(ModelFacade modelFacade)
        {
            this.mFacade = modelFacade;

            InitializeComponent();
            lblBookingRef.Content = "Booking number\r\n";

            if (mFacade.CurrentBook == null)
            {
                lblBookingRef.Visibility = Visibility.Hidden;
            }
            else
            {
                lblBookingRef.Content += mFacade.CurrentBook.GetBookingNb().ToString();
                refreshDisplay();
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
         * Refreshes all fields displayed in the window according to  
         * current system objects states.
         */
        private void refreshDisplay()
        {
            clearDisplay();

            refreshBookingDisplay();
            refreshCustomerDisplay();
            refreshGuestsDisplay();
            
        }

        /*
         * Refreshes the booking fields displayed in the window.
         */
        private void refreshBookingDisplay()
        {
            if (mFacade.CurrentBook != null)
            {
                DateTime arrival;
                DateTime departure;
                mFacade.CurrentBook.GetDates(out arrival, out departure);
                dtpArrival.SelectedDate = arrival;
                dtpDeparture.SelectedDate = departure;

                lblBookingRef.Content += mFacade.CurrentBook.GetBookingNb().ToString();
                lblBookingRef.Visibility = Visibility.Visible;
            }
        }

        /*
         * Refreshes the customer fields displayed in the window.
         */
        private void refreshCustomerDisplay()
        {
            if (mFacade.CurrentCust != null)
            {
                txtCustNumber.Text = mFacade.CurrentCust.GetCustNb().ToString();
                txtCustName.Text = mFacade.CurrentCust.Name;
                txtCustAddress.Text = mFacade.CurrentCust.GetAddress();
            }
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
         * Loads & displays the customer referenced by txtCustNumber.Text
         */
        private void btnLoadCust_Click(object sender, RoutedEventArgs e)
        {
            foreach (PersonComponent g in mFacade.CurrentBook.GetGuests())
            {
                lstGuests.Items.Add(g.Name);
            }
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
                mFacade.CurrentCust = mFacade.PFact.GetNewCustomer(txtCustName.Text,
                                                txtCustAddress.Text);
                refreshDisplay();
            }
        }

        /*
         * Routine triggered upon clicking the 'Save' button.
         */
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (areAllValuesValid())
            {
                mFacade.CurrentBook = BookingFactory.Instance.GetNewBooking(
                                        mFacade.CurrentCust, 
                                        (DateTime) dtpArrival.SelectedDate,
                                        (DateTime) dtpDeparture.SelectedDate);

                MessageBox.Show("Booking saved.");

                if (lblBookingRef.Visibility == Visibility.Hidden)
                {
                    lblBookingRef.Content += mFacade.CurrentBook.GetBookingNb().ToString();
                    lblBookingRef.Visibility = Visibility.Visible;
                }
            }
        }

        /*
         * True if all the window fields are valid to create or amend a 
         * booking, otherwise false.
         * Displays error message windows.
         */
        private bool areAllValuesValid()
        {
            bool areValidValues = true;

            if (mFacade.CurrentCust == null)
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
                new NewGuest(mFacade.CurrentBook.GetGuests()).ShowDialog();
                refreshDisplay();
            }
        }

        /*
         * Routine executed upon clicking the 'Add to guests' button.
         */
        private void btnAddToGuest_Click(object sender, RoutedEventArgs e)
        {
            bool isCustAGuest = false;

            if (mFacade.CurrentBook != null)
            {
                foreach (PersonComponent g in mFacade.CurrentBook.GetGuests())
                {
                    if (g.GetCustNb() > 0)
                    {
                        isCustAGuest = true;
                        MessageBox.Show("The customer is already in the list"
                                        + " of guests.");
                    }
                }
            }

            if (canAddGuest() && !isCustAGuest)
            {
                new NewGuest(mFacade.CurrentBook.GetGuests(),
                             mFacade.CurrentCust).ShowDialog();

                refreshDisplay();
            }
        }

        /*
         * True if it is possible to add a guest to the booking,
         * otherwise false.
         * Displays error message windows.
         */
        private bool canAddGuest()
        {
            bool canAddGuest = true;

            if (mFacade.CurrentBook == null)
            {
                canAddGuest = false;
                MessageBox.Show("Please save the booking before adding"
                                + " guests.");
            }
            else if (mFacade.CurrentBook.GetGuests().Count >= 4)
            {
                canAddGuest = false;
                MessageBox.Show("This booking is already full, the"
                                + " maximum number of guests per booking"
                                + " is 4.");
            }
            
            return canAddGuest;
        }

        /*
         * Routine executed upon clicking the 'Delete' button.
         */
        private void btnDeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            if (lstGuests.SelectedIndex < 0 || lstGuests.SelectedIndex > 3)
            {
                MessageBox.Show("Please select the guest"
                                + " that you want to delete.");
            }
            else
            {
                mFacade.CurrentBook.GetGuests().RemoveAt(lstGuests.SelectedIndex);
                refreshDisplay();
            }
            
        }

        /*
         * Routine executed upon clicking the 'Close' button.
         */
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
