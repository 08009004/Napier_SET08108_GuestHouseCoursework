using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Represents a customer.
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public class Customer : PersonDecorator
    {
        // Properties:
        private String address;
        public override String GetAddress()
        {
            return this.address;
        }

        private int customerNb;
        public override int GetCustNb()
        {
            return this.customerNb;
        }

        /*
         * Constructor.
         * 
         * throws ArgumentException if customerRefNb is not strictly greater 
         * than 0, or if address equals either String.Empty or null
         */
        public Customer(String address, int customerRefNb)
        {
            if (String.IsNullOrEmpty(address))
            {
                throw new ArgumentException("Customer.address"
                                            + " must not be null nor String.Empty");
            }

            if (customerRefNb <= 0)
            {
                throw new ArgumentException("Customer.customerRefNb must be"
                                            + " strictly greater than 0");
            }

            this.address = address;
            this.customerNb = customerRefNb;
        }

        /*
         * Returns a textual representation of the decorated Person  
         * (post Guest decoration) in order to persist it to a CSV file;
         * fields must come in the same order as enumerated in CustomerFields.cs
         */
        public override String ToCSV()
        {
            return base.ToCSV() + "#CUSTOMER\r\n"
                                + customerNb
                                + ","
                                + address
                                + "\r\n";
        }

        /*
         * Returns true.
         */
        public override bool IsCustomer()
        {
            return true;
        }

        /*
         * Returns true if the Customer is also a Guest, otherwise flase
         */
        public override bool IsGuest()
        {
            return base.IsGuest();
        }
    }
}
