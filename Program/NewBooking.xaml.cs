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

        public NewBooking(DataPersistenceFacade dpf, BookingComponent currentBooking)
        {
            this.dpf = dpf;
            this.currentBooking = currentBooking;
            InitializeComponent();
        }

        private void btnLoadCust_Click(object sender, RoutedEventArgs e)
        {
            int customerNb;
            PersonComponent customer;


            if (!Int32.TryParse(txtCustNumber.Text, out customerNb))
            {
                MessageBox.Show("Please enter a valid booking number.");
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
                    customer = pf.RestoreCustomer(customerData);
                    displayCustomer(customer);
                }
            }
        }

        private void displayCustomer(PersonComponent customer) 
        {
            clearCustomer();
            txtCustName.Text = customer.Name;
            txtCustNumber.Text = customer.CustomerNb.ToString();
        }

        private void clearCustomer()
        {
            txtCustNumber.Text = String.Empty;
            txtCustName.Text = String.Empty;
        }
    }
}
