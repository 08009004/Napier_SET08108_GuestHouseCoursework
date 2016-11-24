using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class ConcretePerson : PersonComponent
    {
        // Properties:
        private String firstName;
        public String FirstName 
        {
            get
            {
                return this.firstName;
            }
        }

        private String secondName;
        public String SecondName
        {
            get
            {
                return this.secondName;
            }
        }

        /*
         * Constructor.
         * 
         * throws ArgumentException if firstName or secondName is null or
         * equals String.Empty
         */
        public ConcretePerson(String firstName, String secondName)
        {
            if (String.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("ConcretePerson.firstName must"
                                            + " not be null nor String.Empty");
            }

            if (String.IsNullOrEmpty(secondName))
            {
                throw new ArgumentException("ConcretePerson.secondName must"
                                            + " not be null nor String.Empty");
            }

            this.firstName = firstName;
            this.secondName = secondName;
        }

        /*
         * Returns a textual representation of the ConcretePerson object
         * in order to persist it to a CSV file.
         */
        public override String ToCSV()
        {
            return "#PERSON\r\n" + FirstName + "," + SecondName + "\r\n";
        }
    }
}
