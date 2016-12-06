using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Abstract component class used to make sure that 
     * components and decorators share the same specification
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public abstract class PersonComponent
    {
        /*
         * Must return the PersonComponent's name.
         */
        public abstract String Name { get; }

        /*
         * Returns the PersonComponent's address (null if the 
         * PersonComponent is not a customer).
         */
        public virtual String GetAddress()
        {
            return null;
        }

        /*
         * Returns the PersonComponent's customer number (-1 if the 
         * PersonComponent is not a customer).
         */
        public virtual int GetCustNb() 
        {
            return -1;
        }

        /*
         * Must return a textual representation of the  
         * PersonComponent in order to persist it to a CSV file.
         */
        public abstract String ToCSV();

        /*
         * Returns false.
         */
        public virtual bool IsCustomer()
        {
            return false;
        }

        /*
         * Returns false.
         */
        public virtual bool IsGuest()
        {
            return false;
        }
    }
}
