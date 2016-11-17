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
            InitializeComponent();

            
            List<Person> l = new List<Person>();
            l.Add(new Person("John", 65));
            l.Add(new Person("Joe", 65));
            l.Add(new Person("Jane", 26));
            l.Add(new Person("Nina", 40));

    //        foreach (Person p in l) CSVWriter.Persist(p);

            CSVReader.Read(@"person.csv");
        }
    }
}
