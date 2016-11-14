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

using System.ComponentModel;
using System.Reflection;

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

            CSVWriter.Persist<Person>(new Person("John", 54));

            XMLWriter.Persist<Person>(new Person("John", 54));
            XMLWriter.Persist<Person>(new Person("James", 65));
            /*
            List<Person> l = new List<Person>();
            l.Add(new Person("John", 65));
            l.Add(new Person("Joe", 65));
            XMLWriter.Persist<List<Person>>(l);   
            */

            MessageBox.Show("XMLReader " + XMLReader.RecoverAll<Person>().ElementAt(0).ToString());
        }
    }
}
