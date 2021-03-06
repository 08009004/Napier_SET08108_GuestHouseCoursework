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
     * Interaction logic for WindowCarHireDetails.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-08
     */
    public partial class WindowCarHireDetails : Window
    {
        //PROPERTIES:

        // reference to calling window's ModelFacade instance:
        private ModelFacade mFacade;

        // points to the CarHire extra edited (is null when creating new one).
        private BookingDecorator carHire;

        // METHODS:

        /*
         * Constructor, the index passed must be the index in the current
         * booking's decoration stack index as returned by 
         * ModelFacade.GetCurrentExtras()) or -1 for a new extra.
         */
        public WindowCarHireDetails(ModelFacade mFacade, 
                                    BookingDecorator instance)
        {
            this.mFacade = mFacade;
            this.carHire = instance;
            InitializeComponent();

            if (this.carHire != null)
            {
                DateTime start;
                DateTime end;
                ((CarHire)instance).GetHireDates(out start, out end);
                dtpStart.SelectedDate = start;
                dtpEnd.SelectedDate = end;

                txtDriverName.Text = ((CarHire)instance).GetDriverName();
            }
            else
            {
                txtDriverName.Text = String.Empty;
            }
        }

        /*
         * Creates or updates the extra.
         */
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (areAllValuesValid())
            {
                if (this.carHire != null)
                {
                    mFacade.UpdateCarHire(this.carHire, 
                                          txtDriverName.Text, 
                                          (DateTime)dtpStart.SelectedDate, 
                                          (DateTime)dtpEnd.SelectedDate);
                }
                else
                {
                    mFacade.AddCarHire(txtDriverName.Text,
                                       (DateTime)dtpStart.SelectedDate,
                                       (DateTime)dtpEnd.SelectedDate);
                }
                this.Close();
            }
        }

        /*
         * True if all the window fields are valid to create a car hire extra, 
         * otherwise false.
         * Displays error message windows.
         */
        private bool areAllValuesValid()
        {
            bool areValidValues = true;
            DateTime arrival;
            DateTime departure;
            mFacade.GetCurrentBookDates(out arrival, out departure);

            if (String.IsNullOrWhiteSpace(txtDriverName.Text))
            {
                areValidValues = false;
                MessageBox.Show("Please enter a driver name for the"
                                + "car hire.");
            }
            else if (dtpStart.SelectedDate == null)
            {
                areValidValues = false;
                MessageBox.Show("Please select a start date for the"
                                + " car hire.");
            }
            else if (dtpStart.SelectedDate < arrival
                  || dtpStart.SelectedDate >= departure)
            {
                areValidValues = false;
                MessageBox.Show("The selected start date is outwith "
                                + " the booking dates.\r\n"
                                + "Please select a start date between"
                                + " booking arrival and departure dates.");
            }
            else if (dtpEnd.SelectedDate == null)
            {
                areValidValues = false;
                MessageBox.Show("Please select an end date for the"
                                + " car hire.");
            }
            else if (dtpEnd.SelectedDate <= arrival
                  || dtpEnd.SelectedDate > departure)
            {
                areValidValues = false;
                MessageBox.Show("The selected end date is outwith "
                                + " the booking dates.\r\n"
                                + "Please select a start date between"
                                + " booking arrival and departure dates.");
            }

            return areValidValues;
        }
    }
}
