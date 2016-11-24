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
    abstract class Person : CSVWritable
    {
        protected String FirstName { get; set; }
        protected String SecondName { get; set; }
//        public int CustomerRef { get; set; }
//        public String Address { get; set; }
//        public String PassportNumber { get; set; }
//        public int Age { get; set; }        

        public Person(String firstName, String secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }

        public String ToCSV()
        {
            return "#PERSON\r\n" + FirstName + "," + SecondName + "\r\n";
//                 + "#CUSTOMER\r\n" + CustomerRef + "," + Address + "\r\n"
//                 + "#GUEST\r\n" + PassportNumber + "," + Age + "\r\n";
        }
    }
}
