using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    abstract class PersonDecorator : PersonComponent
    {
        private PersonComponent person;

        /*
         * Sets the component of that will be decorated by this decorator.
         */
        public void SetComponent(PersonComponent component)
        {
            this.person = component;
        }

        /*
         * Returns a textual representation of the PersonDecorator 
         * in order to persist it to a CSV file.
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
