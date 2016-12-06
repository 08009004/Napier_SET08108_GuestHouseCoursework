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
     * Interaction logic for NewGuest.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public partial class NewGuest : Window
    {
        // Property: 
        // reference to calling window's ModelFacade instance:
        private ModelFacade mFacade;
        // true if the new guest to create is the booking's customer.
        private bool isCustomer;

        /*
         * Constructor 1.
         */
        public NewGuest(ModelFacade mFacade, bool isCustomer)
        {
            this.mFacade = mFacade;
            this.isCustomer = isCustomer;
            InitializeComponent();

            if (isCustomer)
            {
                lblName.Visibility = Visibility.Hidden;
                txtName.Visibility = Visibility.Hidden;
            }
        }

        /*
         * Executed upon clicking 'Add guest' button.
         */
        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            if (areAllValuesValid())
            {
                if (!this.isCustomer)
                {
                    mFacade.AddGuest(txtName.Text,
                                     txtPassportNb.Text,
                                     Int32.Parse(txtAge.Text));
                }
                else
                {
                    mFacade.AddCustomerToGuests(txtPassportNb.Text,
                                                Int32.Parse(txtAge.Text));
                }
                this.Close();
            }
        }

        /*
         * True if all the window fields are valid to create a guest, 
         * otherwise false.
         * Displays error message windows.
         */
        private bool areAllValuesValid()
        {
            bool areValidValues = true;
            int tmp;

            if (!Int32.TryParse(txtAge.Text, out tmp))
            {
                MessageBox.Show("Please enter a valid number in age box.");
                areValidValues = false;
            }
            else if (tmp <= 0)
            {
                MessageBox.Show("The guest's age must be strictly greater"
                                + " than zero.");
                areValidValues = false;
            }
            
            if (mFacade.CurrentCust == null
             && String.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please fill in the guest's name");
                areValidValues = false;
            }

            if (String.IsNullOrWhiteSpace(txtPassportNb.Text))
            {
                MessageBox.Show("Please fill in the guest's passport number");
                areValidValues = false;
            }

            return areValidValues;
        }
    }
}
