using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class ConcreteDecoratorCustomer : PersonDecorator
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
       * throws ArgumentException if customerRefNb is lesser than zero, or if 
       * address equals String.Empty or null
       */
        public ConcreteDecoratorCustomer(String address, int customerRefNb) 
        {
            if (String.IsNullOrEmpty(address))
            {
                throw new ArgumentException("ConcreteDecoratorCustomer.address"
                                            + " must not be null nor String.Empty");
            }

            if (customerRefNb < 0)
            {
                throw new ArgumentException("ConcreteDecoratorCustomer.customerRefNb"
                                            + " must not be lesser than 0");
            }

            this.address = address;
            this.customerRefNb = customerRefNb;
        }

        /*
         * Returns a textual representation of the decorated ConcretePerson  
         * (post Guest decoration) in order to persist it to a CSV file.
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
