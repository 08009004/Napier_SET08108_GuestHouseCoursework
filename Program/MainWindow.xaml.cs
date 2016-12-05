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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Properties
        // reference to the PersonFactory instance:
        private PersonFactory pf = PersonFactory.Instance;
        // reference to the BookingFactory instance:
        private BookingFactory bf = BookingFactory.Instance;
        // reference to a DataPersistenceFacade instance:
        private DataPersistenceFacade dpf = new DataPersistenceFacade();
        // reference to the current booking:
        private BookingComponent currentBooking;

        public MainWindow()
        {
            InitializeComponent();
        }

        /*
         * Loads and displays the booking referenced by txtBookingRef.Text.
         */
        private void btnLoadBooking_Click(object sender, RoutedEventArgs e)
        {
            int bookingNb;
            if (!Int32.TryParse(txtBookingRef.Text, out bookingNb))
            {
                MessageBox.Show("Please enter a valid booking number.");
            }
            else 
            {
                List<Dictionary<String, String>> bookingData;
                if (!dpf.Read(bookingNb, out bookingData)) 
                {
                    MessageBox.Show("Can't find booking number "
                                    + txtBookingRef.Text + ".\r\n"
                                    + "Please enter a valid"
                                    + " booking number.");
                }
                else 
                {
                    currentBooking = bf.Restore(bookingData);
                    displayCurrentBooking();
                }
            }
        }

        /*
         * Displays the fields of currentBooking in the window.
         */
        private void displayCurrentBooking()
        {
            if (currentBooking == null)
            {
                MessageBox.Show("Please load or create a booking.");
            }
            else
            {
                clearDisplay();

                txtBookingRef.Text = currentBooking.GetBookingNb().ToString();

                PersonComponent c = currentBooking.GetCustomer();
                txtCustNumber.Text = c.CustomerNb.ToString();
                txtCustName.Text = c.Name;

                if (c.IsGuest())
                {
                    lstGuests.Items.Add(c.Name);
                }
                foreach (PersonComponent g in currentBooking.GetGuests())
                {
                    lstGuests.Items.Add(g.Name);
                }

                DateTime arrival;
                DateTime departure;
                currentBooking.GetDates(out arrival, out departure);
                txtArrival.Text = arrival.ToString().Substring(0, 10);
                txtDeparture.Text = departure.ToString().Substring(0, 10);
            }
        }

        /*
         * Executed upon clicking the 'Clear' button.
         */
        private void btnClearWindow_Click(object sender, RoutedEventArgs e)
        {
            clearDisplay();
        }

        /*
         * Empties all boxes displayed in the MainWindow.
         */
        private void clearDisplay()
        {
            txtBookingRef.Text = String.Empty;
            txtCustNumber.Text = String.Empty;
            txtCustName.Text = String.Empty;
            txtArrival.Text = String.Empty;
            txtDeparture.Text = String.Empty;
            lstGuests.Items.Clear();
        }

        /*
         * Executed upon clicking the 'Create/Amend' button.
         */
        private void btnNewBooking_Click(object sender, RoutedEventArgs e)
        {
            new NewBooking(dpf, currentBooking).ShowDialog();
        }

    }
}
