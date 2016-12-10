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
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public class PersonFactory
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
         * Restores the factory state according to sysData values.
         * Returns true if restore was successful, otherwise false.
         */
        public bool Restore(Dictionary<String, String> sysData)
        {
            String nxtBookNbString;
            bool outcome;
            outcome = sysData.TryGetValue(
                            BookingFactoryField.NEXT_BOOKING_NB.ToString(), 
                            out nxtBookNbString);
            outcome = Int32.TryParse(nxtBookNbString, out this.nextCustNb);
            return outcome;
        }

        /*
         * Generates a new customer instance on the basis of the data 
         * passed as parameters.
         */
        public PersonComponent GetNewCustomer(String name, String address)
        {
            PersonComponent p = new Person(name);
            PersonDecorator c = new Customer(address, this.nextCustNb);
            this.nextCustNb++;
            c.SetComponent(p);
            return c;
        }

        /*
         * Returns a reference to the updated PersonComponent instance.
         */
        public PersonComponent UpdateCustomer(PersonComponent customer,
                                              String newName,
                                              String newAddress)
        {
            // create a fresh customer with the new values but same customerNb
            PersonComponent p = new Person(newName);
            PersonDecorator c = new Customer(newAddress, customer.GetCustNb());
            c.SetComponent(p);

            // get all decorators of customer being updated
            List<PersonDecorator> decorators;
            customer.Unwrap(out decorators);

            // decorate the new customer with those decorators except those 
            // of type 'Customer'
            foreach (PersonDecorator decorator in decorators)
            {
                if (decorator.GetType() != typeof(Customer))
                {
                    decorator.SetComponent(c);
                    c = decorator;
                }
            }

            return c;
        }

        /*
         * Instantiates a Customer from a dictonary<attribute, values>,
         * presumably recovered from persisted data (the dictonary keys 
         * should follow the naming implemented in the *Field.cs enumerations)
         * 
         * Thows Argument exceptions if there is a problem with the contents
         * of the dictionary passed as a parameter.
         */
        public PersonDecorator RestoreCustomer(
                                    Dictionary<String, String> attributes)
        {
            String name;
            String address;
            String custRef;
            int custRefNb;
            String passportNb;
            String ageString;
            int age;

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

            PersonDecorator c = new Customer(address, custRefNb);
            c.SetComponent(new Person(name));
            PersonDecorator g;

            if (attributes.TryGetValue("PASSPORT_NUMBER", out passportNb)
             && attributes.TryGetValue("AGE", out ageString))
            {
                if (!Int32.TryParse(ageString, out age))
                {
                    throw new ArgumentException("impossible to parse "
                                                + "[key: AGE, " 
                                                + " value: "+ ageString 
                                                + "] to type Int32.");
                }
                g = new Guest(passportNb, age);
                g.SetComponent(c);
            }
            else
            {
                g = c;
            }

            return g;
        }

        /*
         * Generates a new guest instance on the basis of the data 
         * passed as parameters.
         */
        public PersonComponent GetNewGuest(String name, 
                                           String passportNb, 
                                           int age)
        {
            Person p = new Person(name);
            Guest g = new Guest(passportNb, age);
            g.SetComponent(p);
            return g;
        }

        /*
         * Generates a new guest instance on the basis of the data 
         * passed as parameters.
         */
        public PersonComponent GetNewGuest(
                                    PersonComponent customer, 
                                    String passportNb, 
                                    int age)
        {
            Guest g = new Guest(passportNb, age);
            g.SetComponent(customer);
            return g;
        }

        /*
         * Instantiates a Guest from a dictonary<attribute, values>,
         * presumably recovered from persisted data (the dictonary keys 
         * should follow the naming implemented in the *Field.cs enumerations)
         * 
         * Thows Argument exceptions if there is a problem with the contents
         * of the dictionary passed as a parameter.
         */
        public PersonComponent RestoreGuest(
                                    Dictionary<String, String> attributes)
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
            g.SetComponent(new Person(name));

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
