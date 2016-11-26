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
        private PersonComponent person;

        /*
         * Sets the component that this decorator decorates.
         */
        public void SetComponent(PersonComponent component)
        {
            this.person = component;
        }

        /*
         * Returns a textual representation of the decorated 
         * PersonComponent in order to persist it to a CSV file.
         */
        public override String ToCSV()
        {
            String csv = String.Empty;

            if (person != null)
            {
                csv = person.ToCSV();
            }

            return csv;
        }
    }
}
