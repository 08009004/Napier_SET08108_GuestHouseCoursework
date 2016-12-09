﻿using System;
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
        //Property:
        // reference to calling window's ModelFacade instance:
        private ModelFacade mFacade;

        // GENERIC WINDOW METHODS:

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
         * Closes the Create/Edit dialog without commiting changes.
         */
        private void btnDiscard_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        // METHODS RELATED TO CURRENT CUSTOMER:

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
         * Loads & displays the customer referenced by txtCustNumber.Text
         */
        private void btnCreateLoadCust_Click(object sender, RoutedEventArgs e)
        {
            /* OLD METHOD
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
             */
            new WindowCustomerDetails(this.mFacade).ShowDialog();
            refreshCustomerDisplay();
        }

        /*
         * Verifies fields validity and creates a new customer instance if
         * possible.
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
        

        // METHODS RELATED TO GUESTS:

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
         * Opens a WindowGuestDetails dialog to enter guest details for 
         * current customer entity.
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
                MessageBox.Show("Please select the guest"
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
            if (mFacade.CurrentBook != null)
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
            if (mFacade.CurrentBook == null)
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
            if (mFacade.CurrentBook == null)
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
            if (mFacade.CurrentBook == null)
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
            else if(i < 0) 
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
    }
}
