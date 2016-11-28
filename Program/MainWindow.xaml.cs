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
            PersonFactory pf = PersonFactory.Instance;
            Customer c = PersonFactory.Instance.GetNewCustomer("pierre", "someplace");

            Booking b = new Booking(56789,
                                    c, 
                                    new DateTime(2016, 1, 18), 
                                    new DateTime(2016, 1, 20));
            b.AddGuest(PersonFactory.Instance.GetNewGuest("caroline g", "080055GW", 35));
            b.AddGuest(PersonFactory.Instance.GetNewGuest(c, "564HGFR", 31));

            CSVWriter.Persist(b);

            InitializeComponent();

        }
    }
}
