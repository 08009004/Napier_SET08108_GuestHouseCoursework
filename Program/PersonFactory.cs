using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Singleton utility class, instantiates and decorates
     * PersonComponents.
     */
    class PersonFactory
    {
        //Properties:
        private int nextCustNb;
        private static PersonFactory instance;
        public static PersonFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PersonFactory();
                }
                return instance;
            }
        }

        /*
         * Private constructor, to prevent class instantiation from
         * external classes (singleton class).
         */
        private PersonFactory() 
        {
            this.nextCustNb = 1;
        }

        /*
         * Generates a new customer instance on the basis of the data 
         * passed as parameters.
         */
        public Customer GetNewCustomer(String name, String address)
        {
            Person p = new Person(name);
            Customer c = new Customer(address, this.nextCustNb);
            this.nextCustNb++;
            c.DecoratedComponent = p;
            return c;
        }

        /*
         * Instantiates a Customer from a dictonary storing attribute values,
         * presumably recovered from persisted data, the keys are expected to 
         * be named according to the PersonFields and CustomerFields enumerations.
         * 
         * Thows Argument exceptions if there is a problem with the contents
         * of the dictionary passed as a parameter.
         */
        public Customer RestoreCustomer(Dictionary<String, String> attributes)
        {
            String name;
            String address;
            String custRef;
            int custRefNb;

            if (!attributes.TryGetValue("NAME", out name)
             || !attributes.TryGetValue("ADDRESS", out address)
             || !attributes.TryGetValue("CUSTOMER_NUMBER", out custRef)) 
            {
                throw new ArgumentException("the attributes dictonary doesn't"
                                            + " have the expected keys.");
            }

            if (!Int32.TryParse(custRef, out custRefNb))
            {
                throw new ArgumentException("impossible to parse "
                                            + "[key: CUSTOMER_NUMBER, value: " 
                                            + custRef + "] to type Int32.");
            }

            Customer c = new Customer(address, custRefNb);
            c.DecoratedComponent = new Person(name);

            return c;
        }

        /*
         * Generates a new guest instance on the basis of the data 
         * passed as parameters.
         */
        public Guest GetNewGuest(String name, String passportNb, int age)
        {
            Person p = new Person(name);
            Guest g = new Guest(passportNb, age);
            g.DecoratedComponent = p;
            return g;
        }

        /*
         * Generates a new guest instance on the basis of the data 
         * passed as parameters.
         */
        public Guest GetNewGuest(Customer customer, String passportNb, int age)
        {
            Guest g = new Guest(passportNb, age);
            g.DecoratedComponent = customer;
            return g;
        }

        /*
         * Instantiates a Guest from a dictonary storing attribute values,
         * presumably recovered from persisted data, the keys are expected to 
         * be named according to the PersonFields and GuestFields enumerations.
         * 
         * Thows Argument exceptions if there is a problem with the contents
         * of the dictionary passed as a parameter.
         */
        public Guest RestoreGuest(Dictionary<String, String> attributes)
        {
            String name;
            String passportNb;
            String ageString;
            int age;

            if (!attributes.TryGetValue("NAME", out name)
             || !attributes.TryGetValue("AGE", out ageString)
             || !attributes.TryGetValue("PASSPORT_NUMBER", out passportNb))
            {
                throw new ArgumentException("the attributes dictonary doesn't"
                                            + " have the expected keys.");
            }

            if (!Int32.TryParse(ageString, out age))
            {
                throw new ArgumentException("impossible to parse "
                                            + "[key: AGE, value: " + ageString 
                                            + "] to type Int32.");
            }

            Guest g = new Guest(passportNb, age);
            g.DecoratedComponent = new Person(name);

            return g;
        }

        /*
         * Returns a textual representation of the PersonFactory object
         * in order to persist it to a CSV file.
         */
        public String ToCSV()
        {
            return "#PERSON_FACTORY\r\n" + nextCustNb + "\r\n";
        }
    }
}
