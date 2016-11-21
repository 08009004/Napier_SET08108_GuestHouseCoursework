using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Custom imports:
using System.Runtime.Serialization;
using System.Xml;
using System.Collections;

namespace Program
{
    public class Person : CSVWritable
    {
        public String Name { get; set; }
        public int UniqueID { get; set; }
        public int Age { get; set; }
        private int Test { get; set; }
        private List<int> list = new List<int>();

        public Person()
        {
            // parameterless constructor required for XML serialization.
        }
        

        public Person(String name, int age)
        {
            Name = name;
            Age = age;

            list.Add(0);
            list.Add(56);
            list.Add(8546);
        }

        public String ToCSV()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in list) sb.Append(i + ";");
            sb.Length--;
            return "#PERSON\r\n" + UniqueID + ","
                                        + Name + ","
                                        + Age + ","
                                        + sb.ToString() + "\r\n";
        }
    }
}
