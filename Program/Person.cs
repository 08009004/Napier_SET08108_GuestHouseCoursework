using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Person : PersonComponent
    {
        // Property:
        private String name;
        public String Name
        {
            get
            {
                return this.name;
            }
        }

        /*
         * Constructor.
         * 
         * throws ArgumentException if firstName or secondName is null or
         * equals String.Empty
         */
        public Person(String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("ConcretePerson.Name must not"
                                            + " be null nor String.Empty");
            }

            this.name = name;
        }

        /*
         * Returns a textual representation of the Person object
         * in order to persist it to a CSV file.
         */
        public override String ToCSV()
        {
            return "#PERSON\r\n" + Name + "\r\n";
        }
    }
}
