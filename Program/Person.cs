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
    [DataContract(Name = "Person")]
    public class Person
    {
        [DataMember()]
        public String Name { get; set; }
        [DataMember()]
        public int UniqueID { get; set; }
        [DataMember()]
        public int Age { get; set; }
        [DataMember()]
        private int Test { get; set; }
        [DataMember()]
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

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in list) sb.Append(i + ",");
            sb.Length--;
            return "UID,Name,Age,list," + UniqueID + "," 
                                        + Name + "," 
                                        + Age + "," 
                                        + sb.ToString() + "\r\n";
        }
    }
}
