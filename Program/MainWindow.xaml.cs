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
        public MainWindow()
        {
            PersonFactory personFactory = PersonFactory.Instance;
            Customer c1 = PersonFactory.Instance.GetNewCustomer("pierre ruiz", "my address");
            Customer c2 = PersonFactory.Instance.GetNewCustomer("lola", "her address");
            Guest g1 = PersonFactory.Instance.GetNewGuest("caroline Gallacher", "08855NL", 36);
            Guest g2 = PersonFactory.Instance.GetNewGuest("leslie", "ABABC", 99);

            CSVWriter.Persist(c1);
            CSVWriter.Persist(g1);
            CSVWriter.Persist(g2);
            CSVWriter.Persist(c2);

            List<Dictionary<string, string>> csvPersons = CSVReader.ReadData(@"person.csv");
            List<Dictionary<string, string>> csvCustomers = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> csvGuests = new List<Dictionary<string, string>>();

            foreach (Dictionary<string, string> d in csvPersons)
            {
                if (d.ContainsKey("CUSTOMER_NUMBER")) csvCustomers.Add(d);
                if (d.ContainsKey("PASSPORT_NUMBER")) csvGuests.Add(d);
            }

            List<PersonComponent> customers = new List<PersonComponent>();
            List<PersonComponent> guests = new List<PersonComponent>();

            foreach (Dictionary<string, string> d in csvCustomers)
            {
                customers.Add(PersonFactory.Instance.RestoreCustomer(d));
            }
            
            foreach (Dictionary<string, string> d in csvGuests)
            {
                guests.Add(PersonFactory.Instance.RestoreGuest(d));
            }

            foreach (PersonComponent p in customers)
            {
                MessageBox.Show("Customer: " + p.ToCSV());
            }

            foreach (PersonComponent p in guests)
            {
                MessageBox.Show("Guest: " + p.ToCSV());
            }


        
// works:   List<Dictionary<string, string>> ld = CSVReader.ReadData("person.csv");
          
            InitializeComponent();

        }
    }
}
