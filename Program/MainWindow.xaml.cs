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
            ConcretePerson p1 = new ConcretePerson("pierre", "ruiz");
            ConcreteDecoratorCustomer cust1 = new ConcreteDecoratorCustomer("this address", 1);
            cust1.SetComponent(p1);

            ConcretePerson p2 = new ConcretePerson("caroline", "gallacher");
            ConcreteDecoratorGuest guest1 = new ConcreteDecoratorGuest("08855NL", 36);
            guest1.SetComponent(p2);

            CSVWriter.Persist(cust1);
            CSVWriter.Persist(guest1);

        
// works:   List<Dictionary<string, string>> ld = CSVReader.ReadData("person.csv");
          
            InitializeComponent();

        }
    }
}
