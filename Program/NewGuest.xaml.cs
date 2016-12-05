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
     * last modified: 2016-12-05
     */
    public partial class NewGuest : Window
    {
        // Properties: 
        // points to the list of guests for current booking.
        List<PersonComponent> guests;
        // points to the customer for current booking.
        PersonComponent customer;

        /*
         * Constructor 1.
         */
        public NewGuest(List<PersonComponent> guests)
        {
            this.guests = guests;
            InitializeComponent();
        }

        /*
         * Constructor 2.
         */
        public NewGuest(List<PersonComponent> guests, 
                        PersonComponent customer)
        {
            this.guests = guests;
            this.customer = customer;
            InitializeComponent();
            lblName.Visibility = Visibility.Hidden;
            txtName.Visibility = Visibility.Hidden;
        }

        /*
         * Executed upon clicking 'Add guest' button.
         */
        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            PersonComponent g = null;

            if (validateValues())
            {
                // Instantiates a new guest
                if (this.customer == null)
                {
                    g = PersonFactory.Instance
                                     .GetNewGuest(txtName.Text,
                                                  txtPassportNb.Text,
                                                  Int32.Parse(txtAge.Text));
                    this.guests.Add(g);
                    this.Close();
                }
                // Or adds the current customer to the list of guests
                else
                {
                    g = PersonFactory.Instance
                                     .GetNewGuest(this.customer,
                                                  txtPassportNb.Text,
                                                  Int32.Parse(txtAge.Text));
                    this.guests.Add(g);
                    this.Close();
                }
            }
        }

        /*
         * True if all the window fields are valid to create a guest, 
         * otherwise false.
         * Displays error message windows.
         */
        private bool validateValues()
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
            
            if (customer == null
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
