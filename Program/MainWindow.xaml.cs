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
        
            Person p = new Person("Jane", "Doe");
            p.CustomerRef = 256987;
            p.Address = "this or that address";
            p.PassportNumber = "08055NL";
            p.Age = 29;
            CSVWriter.Persist(p);

            Person q = new Person("John", "Doob");
            q.CustomerRef = 58;
            q.Address = "some place else";
            q.PassportNumber = "QQQ000";
            q.Age = 92;
            CSVWriter.Persist(q);

            Person r = new Person("Doug", "Star");
            r.CustomerRef = 943246;
            r.Address = "erewhon 90210";
            r.PassportNumber = "nfgYt2";
            r.Age = 7;
            CSVWriter.Persist(r);

            Person s = new Person("Lester", "Smooth");
            s.CustomerRef = 75632;
            s.Address = "jingle bells Strasse";
            s.PassportNumber = "UT567E";
            s.Age = 45;
            CSVWriter.Persist(s);
           

            List<Dictionary<string, string>> ld = CSVReader.ReadData("person.csv");
            String v;
            foreach (Dictionary<string, string> d in ld)
            {
                foreach (String k in d.Keys)
                {
                    if (d.TryGetValue(k, out v)) MessageBox.Show("key: " + k + "; value: " + v);
                }
            }
/*
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

       //     XmlWriter.Persist<List<Person>>(l);
            List<String> temp = new List<string>();
            temp.Add("#PERSON\r\n");
            temp.Add("0,John,65,0;56;8546\r\n");
            temp.Add("#CUSTOMER\r\n");
            temp.Add("0123568\r\n");
            temp.Add("#GUEST\r\n");
            temp.Add("tempo,TEMPI0");
            /*            temp.Add("#PERSON\r\n");
                        temp.Add("0,Joe,65,0;56;8546\r\n");
                        temp.Add("#GUEST\r\n");
                        temp.Add("tempo,TEMPI0");
                        temp.Add("#PERSON\r\n");
                        temp.Add("0,John,65,0;56;8546\r\n");

                        List<String> li = CSVReader.aggregateInstances(temp);
                        foreach (String s in li) MessageBox.Show(s);
             * 
             * enum PersonField {  FIRST_NAME, SECOND_NAME }
                enum CustomerField { CUSTOMER_NUMBER, ADDRESS }
                enum GuestField { PASSPORT_NUMBER, AGE }
             * 
             * 


            Dictionary<String, String> d = CSVReader.index("#PERSON\r\n"
                                                            + "John,Smith\r\n"
                                                            + "#CUSTOMER\r\n"
                                                            + "0123568,20 that street POSTCODE\r\n"
                                                            + "#GUEST\r\n"
                                                            + "08855NL,99");
            String v;
            foreach (String k in d.Keys)
            {
                if (d.TryGetValue(k, out v)) MessageBox.Show("key: " + k + "; value: " + v);
            }
 */
        }
    }
}
