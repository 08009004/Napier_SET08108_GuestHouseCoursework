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
        public PersonComponent DecoratedComponent { get; set; }
            /*
             * Ideally this property should be 'protected' hence visible only 
             * from children classes Customer.cs and Guest.cs but VisualStudio
             * genereates a compile time error if it is.
             * Simon and I looked into it together and could not figure out why.
             */

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
    }
}
