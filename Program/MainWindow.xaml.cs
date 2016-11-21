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

            foreach (Person p in l) CSVWriter.Persist(p);

/*
            Dictionary<String, String> d = CSVReader.index("Name,Age,list,Jane,26,0,56,8546");
            String v;
            foreach (String k in d.Keys)
            {
                if (d.TryGetValue(k, out v)) MessageBox.Show("key: " + k + "; value: " + v);
            }
                

    //        CSVReader.Read<Person>(@"person.csv");
 */
       //     XmlWriter.Persist<List<Person>>(l);
            List<String> temp = new List<string>();
            temp.Add("#PERSON\r\n");
            temp.Add("0,John,65,0;56;8546\r\n");
            temp.Add("#CUSTOMER\r\n");
            temp.Add("0123568\r\n");
            temp.Add("#PERSON\r\n");
            temp.Add("0,Joe,65,0;56;8546\r\n");
        
            List<String> li = CSVReader.aggregateInstance(temp);
            foreach (String s in li) MessageBox.Show(s);
        }
    }
}
