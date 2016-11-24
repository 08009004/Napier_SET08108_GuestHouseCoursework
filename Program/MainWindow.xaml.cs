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
            Person p1 = new Person("pierre", "ruiz");
            Customer cust1 = new Customer("this address", 1);
            cust1.SetComponent(p1);

            Person p2 = new Person("caroline", "gallacher");
            Guest guest1 = new Guest("08855NL", 36);
            guest1.SetComponent(p2);

            CSVWriter.Persist(cust1);
            CSVWriter.Persist(guest1);

        
// works:   List<Dictionary<string, string>> ld = CSVReader.ReadData("person.csv");
          
            InitializeComponent();

        }
    }
}
