﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Customer : PersonDecorator
    {
        // Properties:
        private String address;
        public String Address
        {
            get
            {
                return this.address;
            }
        }

        private int customerRefNb;
        public int CustomerRefNb
        {
            get
            {
                return this.customerRefNb;
            }
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
            this.customerRefNb = customerRefNb;
        }

        /*
         * Returns a textual representation of the decorated Person  
         * (post Guest decoration) in order to persist it to a CSV file;
         * fields must come in the same order as enumerated in CustomerFields.cs
         */
        public override String ToCSV()
        {
            return base.ToCSV() + "#CUSTOMER\r\n"
                                + customerRefNb
                                + ","
                                + address
                                + "\r\n";
        }
    }
}
