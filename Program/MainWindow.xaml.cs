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
            clearDisplay();
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
                    refreshDisplay();
                }
            }
        }

        /*
         * Displays the fields of currentBooking in the window.
         */
        private void refreshDisplay()
        {
            if (currentBooking != null)
            {
                clearDisplay();

                txtBookingRef.Text = currentBooking.GetBookingNb().ToString();

                lblCustomer.Visibility = Visibility.Visible;
                PersonComponent c = currentBooking.GetCustomer();
                lblCustNumber.Content = "Number: "
                                        + c.CustomerNb.ToString();
                lblCustNumber.Visibility = Visibility.Visible;
                lblCustName.Content = "Name: " + c.Name;
                lblCustName.Visibility = Visibility.Visible;

                lblGuest.Visibility = Visibility.Visible;
                lstGuests.Visibility = Visibility.Visible;
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
                lblArrival.Visibility = Visibility.Visible;
                lblDeparture.Visibility = Visibility.Visible;
                currentBooking.GetDates(out arrival, out departure);
                lblArrivalDate.Content = arrival.ToString()
                                                .Substring(0, 10);
                lblArrivalDate.Visibility = Visibility.Visible;
                lblDepartureDate.Content = departure.ToString()
                                                    .Substring(0, 10);
                lblDepartureDate.Visibility = Visibility.Visible;
            }
        }

        /*
         * Executed upon clicking the 'Clear' button.
         */
        private void btnClearWindow_Click(object sender, RoutedEventArgs e)
        {
            this.currentBooking = null;
            clearDisplay();
        }

        /*
         * Empties all boxes displayed in the MainWindow.
         */
        private void clearDisplay()
        {
            txtBookingRef.Text = String.Empty;
            lblArrival.Visibility = Visibility.Hidden;
            lblDeparture.Visibility = Visibility.Hidden;
            lblArrivalDate.Visibility = Visibility.Hidden;
            lblDepartureDate.Visibility = Visibility.Hidden;

            lblCustomer.Visibility = Visibility.Hidden;
            lblCustNumber.Visibility = Visibility.Hidden;
            lblCustName.Visibility = Visibility.Hidden;

            lblGuest.Visibility = Visibility.Hidden;
            lstGuests.Items.Clear();
            lstGuests.Visibility = Visibility.Hidden;
        }
        
        /*
         * Executed upon clicking the 'Create/Amend' button.
         */
        private void btnNewBooking_Click(object sender, RoutedEventArgs e)
        {
            new NewBooking(dpf, currentBooking).ShowDialog();
            refreshDisplay();
        }

    }
}
