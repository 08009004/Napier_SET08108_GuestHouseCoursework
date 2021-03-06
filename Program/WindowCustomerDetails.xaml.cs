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
     * Interaction logic for WindowCustomerDetails.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-09
     */
    public partial class WindowCustomerDetails : Window
    {
        //PROPERTIES:

        // reference to calling window's ModelFacade instance:
        private ModelFacade mFacade;

        // METHODS:

        /*
         * The window constructor.
         */
        public WindowCustomerDetails(ModelFacade modelFacade)
        {
            this.mFacade = modelFacade;
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
            lstCustomers.ItemsSource = mFacade.GetAllCustomerNbs();
            if (mFacade.IsACustomerLoaded())
            {
                lblCustNumberValue.Content = mFacade.GetCurrentCustNb();
                txtCustName.Text = mFacade.GetCurrentCustName();
                txtCustAddress.Text = mFacade.GetCurrentCustAdress();
            }
        }

        /*
         * Refreshes all customer detail fields displayed in the window 
         * according to currently loaded customer data.
         */
        private void refreshCustDetailDisplay()
        {
            if (mFacade.IsACustomerLoaded())
            {
                lblCustNumberValue.Content = mFacade.GetCurrentCustNb();
                txtCustName.Text = mFacade.GetCurrentCustName();
                txtCustAddress.Text = mFacade.GetCurrentCustAdress();
            }
        }

        /*
         * Empties all boxes displayed in the MainWindow.
         */
        private void clearDisplay()
        {
            clearCustomerDetails();

            if (lstCustomers.Items != null)
            {
                lstCustomers.Items.Clear();
            }
        }

        /*
         * Hide empty all customer detail fields from the window.
         */
        private void clearCustomerDetails()
        {
            lblCustNumberValue.Content = String.Empty;
            txtCustName.Text = String.Empty;
            txtCustAddress.Text = String.Empty;
        }

        /*
         * loads selected customer into the system when double clicking on it.
         */
        private void lstCustomers_MouseDoubleClick(object sender,
                                                  MouseButtonEventArgs e)
        {
            List<int> l = (List<int>)lstCustomers.ItemsSource;
            int i = lstCustomers.SelectedIndex;

            if (i < 0 || !mFacade.RestoreCustomer(l.ElementAt(i)))
            {
                MessageBox.Show("Please double click on a customer from the"
                            + " list to load it into the system, or create"
                            + " a new one.");
            }

            refreshCustDetailDisplay();
        }

        
        /*
         * Creates or updates customer, and closes dialog.
         */
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (areAllValuesValid())
            {
                if (mFacade.IsACustomerLoaded()) // update current customer
                {
                    mFacade.UpdateCurrentCustomer(txtCustName.Text,
                                                  txtCustAddress.Text);
                }
                else // create a new customer
                {
                    mFacade.CreateCustomer(txtCustName.Text, 
                                           txtCustAddress.Text);
                }
                this.Close();
            }
        }

        /*
         * True if all fields contain valid values to create/update a
         * customer.
         * Displays error messages.
         */
        private bool areAllValuesValid()
        {
            bool areAllValid = true;

            if (String.IsNullOrWhiteSpace(txtCustName.Text))
            {
                areAllValid = false;
                MessageBox.Show("Please enter a valid customer name");
            }
            else if (String.IsNullOrWhiteSpace(txtCustAddress.Text))
            {
                areAllValid = false;
                MessageBox.Show("Please enter a valid customer address");
            }

            return areAllValid;
        }
    }
}
