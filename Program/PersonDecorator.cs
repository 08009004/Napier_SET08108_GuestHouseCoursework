using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /* Abstract class defining the minimum implementation of 
     * a concrete PersonDecorator.
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public abstract class PersonDecorator : PersonComponent
    {
        // Properties:
        // the decorator component
        protected PersonComponent decoratedComponent;
        public void SetComponent(PersonComponent p)
        {
            decoratedComponent = p;
        }

        // the PersonDecorator's name (is null if the root component is not 
        // a concrete Person)
        public override string Name
        {
            get 
            {
                String name = null;

                if (decoratedComponent != null)
                {
                    name = decoratedComponent.Name;
                }

                return name; 
            }
        }

        /*
         * Returns the PersonDecorator's Address (is null if the root 
         * component is not a concrete Person)
         */
        public override string GetAddress()
        {
            String address = null;

            if (decoratedComponent != null)
                // && decoratedComponent.GetType() != typeof(Person))
            {
                address = decoratedComponent.GetAddress();
            }

            return address;
        }

        /* 
         * Returns the PersonDecorator's customer number (-1 if the root 
         * component is not a concrete Person)
         */
        public override int GetCustNb()
        {
            int custNb = -1;

            if (decoratedComponent != null)
            {
                custNb = decoratedComponent.GetCustNb();
            }

            return custNb;
        }

        /*
         * Returns the PersonDecorator's passport number (is null if the root 
         * component is not a concrete Person)
         */
        public override string GetPassportNb()
        {
            String passportNb = null;

            if (decoratedComponent != null)
            // && decoratedComponent.GetType() != typeof(Person))
            {
                passportNb = decoratedComponent.GetAddress();
            }

            return passportNb;
        }

        /* 
         * Returns the PersonDecorator's age (-1 if the root 
         * component is not a concrete Person)
         */
        public override int GetAge()
        {
            int age = -1;

            if (decoratedComponent != null)
            {
                age = decoratedComponent.GetCustNb();
            }

            return age;
        }

        /* 
         * Returns a reference to the PersonDecorator's decorated component, 
         * or the to the PersonDecorator itself if it is not decorated.
         */
        public override PersonComponent Undecorate()
        {
            PersonComponent p = this;

            if (decoratedComponent != null)
            {
                p = decoratedComponent;
            }
            return p;
        }


        /*
         * Returns a textual representation of the decorated 
         * PersonComponent in order to persist it to a CSV file.
         */
        public override String ToCSV()
        {
            String csv = String.Empty;

            if (decoratedComponent != null)
            {
                csv = decoratedComponent.ToCSV();
            }

            return csv;
        }

        /*
         * Returns true if the PersonComponent is a Customer, otherwise false.
         */
        public override bool IsCustomer()
        {
            bool isCustomer = false;

            if (decoratedComponent != null)
            {
                isCustomer = decoratedComponent.IsCustomer();
            }

            return isCustomer;
        }

        /*
         * Returns true if the PersonComponent is a Guest, otherwise false.
         */
        public override bool IsGuest()
        {
            bool isGuest = false;

            if (decoratedComponent != null)
            {
                isGuest = decoratedComponent.IsGuest();
            }

            return isGuest;
        }
    }
}
