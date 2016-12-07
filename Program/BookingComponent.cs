﻿using System;
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
    public abstract class BookingComponent
    {
        /*
         * Must add a guest to a given BookingComponent.
         */
        public abstract void AddGuest(PersonComponent guest);

        /*
         * Must return the Booking number.
         */
        public abstract int GetBookingNb();

        /*
         * Must return the Booking's cutomer.
         */
        public abstract PersonComponent GetCustomer();

        /*
         * Must return the Booking's list of guests.
         */
        public abstract List<PersonComponent> GetGuests();

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
         * Must return a new BookingComponent, decorated with all the
         * same BookingDecorators except the one passed as a parameter, 
         * or the to the Booking itself if it is not decorated.
         */
        public virtual BookingComponent Undecorate(BookingComponent decorator)
        {
            return this;
        }

        /*
         * Returns true if the BookingDecorator wraps another 
         * BookingDecorator, otherwise false.
         */
        public virtual bool isDecorator()
        {
            return false;
        }

        /*
         * Must return a textual representation of the  
         * BookingComponent in order to persist it to a CSV file.
         */
        public abstract String ToCSV();
    }
}
