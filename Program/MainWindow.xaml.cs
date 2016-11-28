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

            // -------------------

            Customer c1 = PersonFactory.Instance.GetNewCustomer("pierre", "someplace");

            Booking b1 = new Booking(56789,
                                    c1, 
                                    new DateTime(2016, 1, 18), 
                                    new DateTime(2016, 1, 20));
            b1.AddGuest(PersonFactory.Instance.GetNewGuest("caroline g", "080055GW", 35));
            b1.AddGuest(PersonFactory.Instance.GetNewGuest(c1, "564HGFR", 31));

            Breakfast br = new Breakfast("1 vegetarian");
            br.DecoratedComponent = b1;

            CSVWriter.Persist(br);

            // -------------------

            Customer c2 = PersonFactory.Instance.GetNewCustomer("sponge bob", "bikini bottom");

            Booking b2 = new Booking(9587,
                                    c2,
                                    new DateTime(2016, 1, 19),
                                    new DateTime(2016, 1, 27));
            b2.AddGuest(PersonFactory.Instance.GetNewGuest("patrick starfish", "***b**", 12));
            b2.AddGuest(PersonFactory.Instance.GetNewGuest("sandy squirrel", "TEXAS001", 13));

            EveningMeal em = new EveningMeal("crabbie patties");
            em.DecoratedComponent = b2;

            CSVWriter.Persist(em);

            // -------------------

            MessageBox.Show("b1 cost: £" + b1.GetCost()
                            + "\r\n TOTAL: £" + br.GetCost());
            MessageBox.Show("b2 cost: £" + b2.GetCost()
                            + "\r\n TOTAL: £" + em.GetCost());

            InitializeComponent();

        }
    }
}
