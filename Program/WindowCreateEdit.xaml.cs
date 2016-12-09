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
     * last modified: 2016-12-08
     */
    public partial class WindowCreateEdit : Window
    {
        //PROPERTIES:

        // reference to calling window's ModelFacade instance:
        private ModelFacade mFacade;

        // METHODS:

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
                lblBookingRef.Content += mFacade.GetCurrentBookNb()
                                                .ToString();
                refreshDisplay();
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
                if (!mFacade.IsABookingLoaded())
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

            if (!mFacade.IsACustomerLoaded())
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

        // METHODS RELATED TO CURRENT BOOKING:

        /*
         * Refreshes the booking fields displayed in the window.
         */
        private void refreshBookingDisplay()
        {
            if (mFacade.IsABookingLoaded())
            {
                DateTime arrival;
                DateTime departure;
                mFacade.GetCurrentBookDates(out arrival, out departure);
                dtpArrival.SelectedDate = arrival;
                dtpDeparture.SelectedDate = departure;

                lblBookingRef.Content += mFacade.GetCurrentBookNb()
                                                .ToString();
                lblBookingRef.Visibility = Visibility.Visible;
            }
        }

        // METHODS RELATED TO CURRENT CUSTOMER:

        /*
         * Refreshes the customer fields displayed in the window.
         */
        private void refreshCustomerDisplay()
        {
            if (mFacade.IsACustomerLoaded())
            {
                txtCustNumber.Text = mFacade.GetCurrentCustNb().ToString();
                txtCustName.Text = mFacade.GetCurrentCustName();
                txtCustAddress.Text = mFacade.GetCurrentCustAdress();
            }
        }

        /*
         * Loads & displays the customer referenced by txtCustNumber.Text
         */
        private void btnCreateLoadCust_Click(object sender, RoutedEventArgs e)
        {
            new WindowCustomerDetails(this.mFacade).ShowDialog();
            refreshCustomerDisplay();
        }

        
        

        // METHODS RELATED TO GUESTS:

        /*
         * Refreshes the guests fields displayed in the window.
         */
        private void refreshGuestsDisplay()
        {
            if (mFacade.IsABookingLoaded())
            {
                lblGuest.Visibility = Visibility.Visible;
                lstGuests.Visibility = Visibility.Visible;

                lstGuests.Items.Clear();
                lstGuests.ItemsSource = mFacade.GetGuestNames();
            }
        }

        /*
         * Opens an empty WindowGuestDetail dialog to input new guest details
         * (if there is still less than 4 guests booked).
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
         * True if it is possible to add a guest to the booking,
         * otherwise false.
         * Displays error message windows.
         */
        private bool canAddGuest()
        {
            bool canAddGuest = true;

            if (!mFacade.IsABookingLoaded())
            {
                canAddGuest = false;
                MessageBox.Show("Please save the booking before adding"
                                + " the guests.");
            }
            else if (mFacade.GetCurrentNbGuests() >= 4)
            {
                canAddGuest = false;
                MessageBox.Show("This booking is already full, the"
                                + " maximum number of guests per booking"
                                + " is 4.");
            }

            return canAddGuest;
        }

        /*
         * Opens a WindowGuestDetails dialog to enter guest details for 
         * current customer entity.
         */
        private void btnAddCustToGuests_Click(object sender, RoutedEventArgs e)
        {
            if (canAddGuest())
            {
                if (mFacade.IsGuestACustomer(lstGuests.SelectedIndex))
                {
                    MessageBox.Show("The customer is already booked as"
                                    + " a guest.");
                }
                else
                {
                    new WindowGuestDetails(mFacade, true).ShowDialog();
                    refreshGuestsDisplay();
                }
            }
        }

        /*
         * Opens a WindowGuestDetail to amend selected guest's details.
         */
        private void lstGuests_MouseDoubleClick(object sender, 
                                                MouseButtonEventArgs e)
        {
            List<int> l = (List<int>) lstGuests.ItemsSource;
            int i = lstGuests.SelectedIndex;

            if (mFacade.GetCurrentNbGuests() == 0)
            {
                MessageBox.Show("There is currently no guests booked.");
            }
            else if(i < 0) 
            {
                MessageBox.Show("Please double click on the guest that"
                            + " you want to edit");
            }
            else if (mFacade.IsGuestACustomer(i))
            {
                new WindowGuestDetails(mFacade, true, i).ShowDialog();
                refreshGuestsDisplay();
            }
            else
            {
                new WindowGuestDetails(mFacade, false, i).ShowDialog();
                refreshGuestsDisplay();
            }
        }

        /*
         * Deletes selected guest from current booking.
         */
        private void btnDeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            if (lstGuests.SelectedIndex < 0 || lstGuests.SelectedIndex > 3)
            {
                MessageBox.Show("Please first select the guest"
                                + " that you want to delete.");
            }
            else
            {
                mFacade.DeleteGuest(lstGuests.SelectedIndex);
                refreshGuestsDisplay();
            }
        }

        // METHODS RELATED TO EXTRAS:

        /*
         * Refreshes the extras fields displayed in the window.
         */
        private void refreshExtrasDisplay()
        {
            if (mFacade.IsABookingLoaded())
            {
                lstExtras.Items.Clear();
                foreach (String s in mFacade.GetCurrentExtrasNames())
                {
                    lstExtras.Items.Add(s);
                }
            }
        }

        /*
         * Opens a dialog to add a breakfasts extra to the current booking.
         */
        private void btnAddBreakfast_Click(object sender, RoutedEventArgs e)
        {
            if (!mFacade.IsABookingLoaded())
            {
                MessageBox.Show("Please save your new booking before adding"
                                + " a breakfast extra.");
            }
            else
            {
                new WindowBreakfastDetails(mFacade, null).ShowDialog();
                refreshExtrasDisplay();
            }
        }

        /*
         * Opens a dialog to add an evening meals extra to the current booking.
         */
        private void btnAddEveningMeal_Click(object sender, RoutedEventArgs e)
        {
            if (!mFacade.IsABookingLoaded())
            {
                MessageBox.Show("Please save your new booking before adding"
                                + " an evening meals extra.");
            }
            else
            {
                new WindowEveningMealDetails(mFacade, null).ShowDialog();
                refreshExtrasDisplay();
            }
        }

        /*
         * Opens a dialog to add a car hire extra to the current booking.
         */
        private void btnAddCarHire_Click(object sender, RoutedEventArgs e)
        {
            if (!mFacade.IsABookingLoaded())
            {
                MessageBox.Show("Please save your new booking before adding"
                                + " a car hire extra.");
            }
            else
            {
                new WindowCarHireDetails(mFacade, null).ShowDialog();
                refreshExtrasDisplay();
            }
        }

        /*
         * Opens a Window{ExtraType}Details to amend selected extra.
         */
        private void lstExtras_MouseDoubleClick(object sender, 
                                                MouseButtonEventArgs e)
        {
            List<BookingDecorator> extras = mFacade.GetCurrentExtras();
            int i = lstExtras.SelectedIndex;

            if (extras != null && extras.Count == 0)
            {
                MessageBox.Show("There is no extra for this booking"
                                + " at present.");
            }
            else if (i < 0) 
            {
                MessageBox.Show("Please double click on the extra that"
                            + " you want to edit");
            }
            else 
            {
                if (extras.ElementAt(i).GetType() == typeof(Breakfast))
                {
                    new WindowBreakfastDetails(mFacade, extras.ElementAt(i)).ShowDialog();
                    
                }
                else if (extras.ElementAt(i).GetType() == typeof(EveningMeal)) 
                {
                    new WindowEveningMealDetails(mFacade, extras.ElementAt(i)).ShowDialog();
                }
                else if (extras.ElementAt(i).GetType() == typeof(CarHire))
                {
                    new WindowCarHireDetails(mFacade, extras.ElementAt(i)).ShowDialog();
                }

                refreshExtrasDisplay();
            }
        }

        /*
         * Deletes the selected extra from the currwent booking.
         */
        private void btnExtraDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstExtras.SelectedIndex >= 0)
            {
                mFacade.RemoveExtra(lstExtras.SelectedIndex);
                refreshExtrasDisplay();
            }
            else
            {
                MessageBox.Show("Please select the extra that you want to"
                                + " delete from the booking");
            }
        }

        private void btnNewCust_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
