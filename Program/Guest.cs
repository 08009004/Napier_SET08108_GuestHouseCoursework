using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Represents a guest.
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public class Guest : PersonDecorator
    {
        // PROPERTIES:

        private String passportNb;
        public override String GetPassportNb()
        {
            return this.passportNb;
        }

        private int age;
        public override int GetAge()
        {
            return this.age;
        }

        // METHODS:

        /*
        * Constructor.
        * 
        * throws ArgumentException if age is lesser than zero, or if 
        * passportNb equals String.Empty or null
        */
        public Guest(String passportNb, int age)
        {
            if (String.IsNullOrEmpty(passportNb))
            {
                throw new ArgumentException("ConcreteDecoratorGuest.passportNb"
                                            + " must not be null nor"
                                            + " String.Empty");
            }

            if (age < 0)
            {
                throw new ArgumentException("ConcreteDecoratorGuest.age must"
                                            + " not be lesser than 0");
            }

            this.passportNb = passportNb;
            this.age = age;
        }

        /*
         * Returns a textual representation of the decorated ConcretePerson  
         * (post Guest decoration) in order to persist it to a CSV file;
         * fields must come in the same order as enumerated in GuestFields.cs
         */
        public override String ToCSV()
        {
            return base.ToCSV() + "#GUEST\r\n"
                                + passportNb
                                + ","
                                + age
                                + "\r\n";
        }

        /*
         * Returns true if the Guest is also a Customer, otherwise flase
         */
        public override bool IsCustomer()
        {
            return base.IsCustomer();
        }

        /*
         * Returns true.
         */
        public override bool IsGuest()
        {
            return true;
        }
    }
}
