using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /* Abstract class defining the minimum implementation of 
     * a concrete BookingDecorator.
     */
    abstract class BookingDecorator : BookingComponent
    {
        // Property : the BookingDecorator component
        public BookingComponent DecoratedComponent { get; set; }

        /*
         * Returns the cost of the decorated BookingComponent
         * (or -1 if the root component is not a concrete Booking).
         */
        public override double GetCost()
        {
            double cost = -1;

            if (DecoratedComponent != null)
            {
                cost = DecoratedComponent.GetCost();
            }

            return cost;
        }

        /*
         * Returns the number of guests included in the decorated 
         * BookingComponent (or -1 if the root component is not a
         * concrete Booking).
         */
        public override int GetNbGuests()
        {
            int nbGuests = -1;

            if (DecoratedComponent != null)
            {
                nbGuests = DecoratedComponent.GetNbGuests();
            }

            return nbGuests;
        }

        /*
         * Returns the number of nights included in the decorated 
         * BookingComponent (or -1 if the root component is not a
         * concrete Booking).
         */
        public override int GetNbNights()
        {
            int nbNights = -1;

            if (DecoratedComponent != null)
            {
                nbNights = DecoratedComponent.GetNbGuests();
            }

            return nbNights;
        }

        /*
         * Returns a textual representation of the decorated 
         * BookingDecorator in order to persist it to a CSV file
         * (or String.Empty if the root component is not a 
         * concrete Booking).
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
