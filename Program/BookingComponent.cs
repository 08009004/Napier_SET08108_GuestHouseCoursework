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
     */
    abstract class BookingComponent
    {
        /*
         * Must add a guest to a given BookingComponent.
         */
        public abstract void AddGuest(Guest guest);

        /*
         * Must return the Booking number.
         */
        public abstract int GetBookingNb();

        /*
         * Must return the number of guests included in this 
         * BookingComponent.
         */
        public abstract int GetNbGuests();

        /*
         * Must return the Booking start and end dates.
         */
        public abstract void GetDates(out DateTime arrival, out DateTime departure);

        /*
         * Must return the number of nights booked.
         */
        public abstract int GetNbNights();

        /*
         * Must return the cost of the BookingComponent
         */
        public abstract double GetCost();

        /*
         * Must return a textual representation of the  
         * BookingComponent in order to persist it to a CSV file.
         */
        public abstract String ToCSV();
    }
}
