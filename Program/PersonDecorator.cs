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
    public abstract class PersonDecorator : PersonComponent
    {
        // Properties:
        // the decorator component
            /*
             * NOTE: Ideally this property should be 'protected' hence  
             * visible only from children classes Customer.cs and Guest.cs  
             * but VisualStudio genereates a compile time error if it is.
             * 
             * Simon and I looked into it together and could not figure out 
             * why.
             */
        public PersonComponent DecoratedComponent { get; set; }
        // the PersonDecorator's name (is null if the root component is not 
        // a concrete Person)
        public override string Name
        {
            get 
            {
                String name = null;

                if (DecoratedComponent != null)
                {
                    name = DecoratedComponent.Name;
                }

                return name; 
            }
        }

        // the PersonDecorator's customer number (-1 if the root 
        // component is not a concrete Person)
        public override int CustomerNb
        {
            get
            {
                int custNb = -1;

                if (DecoratedComponent != null)
                {
                    custNb = DecoratedComponent.CustomerNb;
                }

                return custNb;
            }
        }

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
         * Returns true if the PersonComponent is a Customer, otherwise false.
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

        /*
         * Returns true if the PersonComponent is a Guest, otherwise false.
         */
        public override bool IsGuest()
        {
            bool isGuest = false;

            if (DecoratedComponent != null)
            {
                isGuest = DecoratedComponent.IsGuest();
            }

            return isGuest;
        }
    }
}
