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
     * Interaction logic for WindowCreateEdit.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-07
     */
    public partial class WindowCreateEdit : Window
    {
        //Property:
        // reference to calling window's ModelFacade instance:
        private ModelFacade mFacade;

        /*
         * The window constructor.
         */
        public WindowCreateEdit(ModelFacade modelFacade)
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
            lstExtras.Items.Clear();
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
            refreshExtrasDisplay();
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
            if (mFacade.CurrentBook != null)
            {
                BookingComponent b = mFacade.CurrentBook;
                PersonComponent c = mFacade.CurrentCust;

                lblGuest.Visibility = Visibility.Visible;
                lstGuests.Visibility = Visibility.Visible;

                lstGuests.Items.Clear();
                foreach (PersonComponent g in b.GetGuests())
                {
                    lstGuests.Items.Add(g.Name);
                }
            }
        }

        /*
         * Refreshes the extras fields displayed in the window.
         */
        private void refreshExtrasDisplay()
        {
            if (mFacade.CurrentBook != null)
            {
                lstExtras.Items.Clear();
                foreach (String s in mFacade.getCurrentExtras())
                {
                    lstExtras.Items.Add(s);
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
            else if (!mFacade.RestoreCustomer(customerNb))
            {
                MessageBox.Show("Can't find customer number "
                                + txtCustNumber.Text + ".\r\n"
                                + "Please enter a valid"
                                + " booking number.");
            }

            refreshCustomerDisplay();
        }

        /*
         * Routine triggered upon clicking the 'New Customer' button.
         */
        private void btnNewCust_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCustName.Text))
            {
                MessageBox.Show("Please enter a valid customer name");
            }
            else if (String.IsNullOrWhiteSpace(txtCustAddress.Text))
            {
                MessageBox.Show("Please enter a valid customer address");
            }
            else
            {
                mFacade.CreateCustomer(txtCustName.Text, txtCustAddress.Text);
                refreshCustomerDisplay();
            }
        }

        /*
         * Checks if the booking can be saved or updated on the basis of
         * window field values, and acts accordingly (closes the window, 
         * except if the booking was a new one).
         */
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (areAllValuesValid())
            {
                if (mFacade.CurrentBook == null)
                {
                    mFacade.CreateBooking((DateTime)dtpArrival.SelectedDate,
                                          (DateTime)dtpDeparture.SelectedDate);
                    mFacade.PersistCurrentBooking();
                    refreshDisplay();
                }
                else
                {
                    mFacade.UpdateBooking((DateTime)dtpArrival.SelectedDate,
                                          (DateTime)dtpDeparture.SelectedDate);
                    mFacade.PersistCurrentBooking();
                    this.Close();
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

            if (dtpArrival.SelectedDate == null)
            {
                areValidValues = false;
                MessageBox.Show("Please select a valid arrival date"
                                + " before saving the booking.");
            }
            else if (dtpDeparture.SelectedDate == null)
            {
                areValidValues = false;
                MessageBox.Show("Please select a valid departure date"
                                + " before saving the booking.");
            }
            else if (dtpDeparture.SelectedDate <= dtpArrival.SelectedDate)
            {
                areValidValues = false;
                MessageBox.Show("Departure date must be strictly"
                                + " later than arrival date.");
            }

            return areValidValues;
        }

        /*
         * Opens an empty WindowGuestDetail window to input new guest details (if
         * there is still less than 4 guests booked).
         */
        private void btnNewGuest_Click(object sender, RoutedEventArgs e)
        {
            if (canAddGuest())
            {
                new WindowGuestDetails(mFacade, false).ShowDialog();
                refreshDisplay();
            }
        }

        /*
         * Routine executed upon clicking the 'Add to guests' button.
         */
        private void btnAddCustToGuests_Click(object sender, RoutedEventArgs e)
        {
            if (canAddGuest() && !isCustInGuests())
            {
                new WindowGuestDetails(mFacade, true).ShowDialog();
                refreshGuestsDisplay();
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
                                + " the guests.");
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
         * Scans the list of guests to check if there already is a customer
         * in it.
         */
        private bool isCustInGuests()
        {
            bool isCustInGuests = false;

            if (mFacade.CurrentBook != null)
            {
                foreach (PersonComponent g in mFacade.CurrentBook.GetGuests())
                {
                    isCustInGuests = isCustInGuests || g.IsCustomer();
                }
            }

            if (isCustInGuests)
            {
                MessageBox.Show("There is already a customer in the list"
                                + " of guests.");
            }

            return isCustInGuests;
        }

        /*
         * Opens a WindowGuestDetail to edit selected guest details.
         */
        private void lstGuests_MouseDoubleClick(object sender, 
                                                MouseButtonEventArgs e)
        {
            List<int> l = (List<int>) lstGuests.ItemsSource;
            int i = lstGuests.SelectedIndex;

            if (i < 0) 
            {
                MessageBox.Show("Please /*double click on a booking*/ from the"
                            + " list to load it into the system.");
            }
            else 
            {
                new WindowGuestDetails(mFacade, false, i).ShowDialog();
                refreshGuestsDisplay();
            }
        }

        /*
         * Deletes selected guest from the booking.
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
                mFacade.DeleteGuest(lstGuests.SelectedIndex);
                refreshGuestsDisplay();
            }
        }

        /*
         * Routine executed upon clicking the 'Close' button.
         */
        private void btnDiscard_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
