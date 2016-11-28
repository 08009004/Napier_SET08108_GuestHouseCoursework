using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /* Abstract class defining the minimum implementation of 
     * a concrete PersonDecorator.
     */
    abstract class PersonDecorator : PersonComponent
    {
        // Property: the decorator component
            /*
             * NOTE: Ideally this property should be 'protected' hence  
             * visible only from children classes Customer.cs and Guest.cs  
             * but VisualStudio genereates a compile time error if it is.
             * 
             * Simon and I looked into it together and could not figure out 
             * why.
             */
        public PersonComponent DecoratedComponent { get; set; }

        /*
         * Returns a textual representation of the decorated 
         * PersonComponent in order to persist it to a CSV file.
         */
        public override String ToCSV()
        {
            String csv = String.Empty;

            if (DecoratedComponent != null)
            {
                csv = DecoratedComponent.ToCSV();
            }

            return csv;
        }

        /*
         * Returns true if the PersonComponent is a Customer.
         */
        public override bool IsCustomer()
        {
            bool isCustomer = false;

            if (DecoratedComponent != null)
            {
                isCustomer = DecoratedComponent.IsCustomer();
            }

            return isCustomer;
        }
    }
}
