using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Program
{
    [Serializable]
    public class Person
    {
        public String Name { get; set; }

    //    [XmlIgnoreAttribute]
        public int Age { get; set; }

        private int Test { get; set; }
        
        private List<int> list = new List<int>();

        public Person()
        {
            // parameterless constructor required for serialization.
        }
        

        public Person(String name, int age)
        {
            Name = name;
            Age = age;

            list.Add(0);
            list.Add(56);
            list.Add(8546);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in list) sb.Append(i + ",");
            sb.Length--;
            return "Name,Age,list," + Name + "," + Age + "," + sb.ToString() + "\r\n";
        }
    }
}
