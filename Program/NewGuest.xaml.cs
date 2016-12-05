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
    /// Interaction logic for NewGuest.xaml
    /// </summary>
    public partial class NewGuest : Window
    {
        // Property: points to the list of guests for current booking.
        List<PersonComponent> guests;

        /*
         * Constructor.
         */
        public NewGuest(List<PersonComponent> guests)
        {
            this.guests = guests;
            InitializeComponent();
        }

        /*
         * Executed upon clicking 'Add guest' button.
         */
        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            PersonComponent guest = null;

            if (validateValues())
            {
                guest = PersonFactory.Instance
                                     .GetNewGuest(txtName.Text,
                                                  txtPassportNb.Text,
                                                  Int32.Parse(txtAge.Text));
                this.guests.Add(guest);
                MessageBox.Show("Guest added successfuly");
                this.Close();
            }

            
        }

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
            
            if (String.IsNullOrWhiteSpace(txtName.Text))
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
