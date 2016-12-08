using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// temp
using System.Windows;

namespace Program
{
    /* Abstract class defining the minimum implementation of 
     * a concrete BookingDecorator.
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public abstract class BookingDecorator : BookingComponent
    {
        // Property : the BookingDecorator component
        /*
         * NOTE: Ideally this property should be 'protected' hence  
         * visible only from children classes Customer.cs and Guest.cs  
         * but VisualStudio genereates a compile time error if it is.
         * 
         * Simon and I looked into it together and could not figure out 
         * why.
         */
        public BookingComponent DecoratedComponent { get; set; }

        /*
         * Adds a guest to the decorated booking.
         */
        public override void AddGuest(PersonComponent guest)
        {
            if (DecoratedComponent != null)
            {
                DecoratedComponent.AddGuest(guest);
            }
        }

        /*
         * Returns the decorated BookingComponent's list of booking number
         * (or -1 if the root component is not a concrete Booking).
         */
        public override int GetBookingNb()
        {
            int bookingNb = -1;

            if (DecoratedComponent != null)
            {
                bookingNb = DecoratedComponent.GetBookingNb();
            }

            return bookingNb;
        }

        /*
         * Returns the decorated BookingComponent's customer 
         * (or null if the root component is not a concrete Booking).
         */
        public override PersonComponent GetCustomer()
        {
            PersonComponent customer = null;

            if (DecoratedComponent != null)
            {
                customer = DecoratedComponent.GetCustomer();
            }

            return customer;
        }

        /*
         * Returns the decorated BookingComponent's list of guests  
         * (or null if the root component is not a concrete Booking).
         */
        public override List<PersonComponent> GetGuests()
        {
            List<PersonComponent> guests = null;

            if (DecoratedComponent != null)
            {
                guests = DecoratedComponent.GetGuests();
            }

            return guests;
        }

        /*
         * Returns the root BookingComponent of the decoration stack; 
         * references outputs a list of pointers to all the BookingDecorator 
         * instances in the decoration stack.
         */
        public override BookingComponent Unwrap(out List<BookingDecorator> references)
        {
            BookingComponent component = this;
            List<BookingDecorator> decorators = new List<BookingDecorator>();

            while (component.isDecorator())
            {
                decorators.Add((BookingDecorator)component);
                component = ((BookingDecorator)component).DecoratedComponent;
            }

            references = decorators;
            return component;
        }

        /*
         * Returns true if the BookingDecorator wraps another 
         * BookingDecorator, otherwise false.
         */
        public override bool isDecorator()
        {
            return this.DecoratedComponent != null;
        }

        /*
         * Returns a reference to a new BookingComponent identical in content 
         * to calling instance, except unwraped from the BookingDecorator 
         * passed as a parameter (or the to the Booking itself if it is not 
         * decorated at all).
         */
        public override BookingComponent Undecorate(BookingDecorator reference)
        {
            if (this == reference) return DecoratedComponent;
                /* Short circuit method if the decorator to remove is the
                 * last one added.
                 */
            List<BookingDecorator> references;
            BookingComponent result = this.Unwrap(out references);
            
            foreach (BookingDecorator decorator in references)
            {
                if (decorator != reference)
                {
                    decorator.DecoratedComponent = result;
                    result = decorator;
                }
            }

            return result;
        }

        /*
         * Returns the booking start and end dates, or 01/01/1970 for both 
         * if an error occurs.
         */
        public override void GetDates(out DateTime arrival, 
                                      out DateTime departure)
        {
            DateTime a = new DateTime(1970, 1, 1);
            DateTime d = new DateTime(1970, 1, 1);

            if (DecoratedComponent != null)
            {
                DecoratedComponent.GetDates(out a, out d);
            }

            arrival = a;
            departure = d;
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
                nbNights = DecoratedComponent.GetNbNights();
            }

            return nbNights;
        }

        /*
         * Returns the cost for each individual night booked
         * (or -1 if the root component is not a concrete Booking).
         */
        public override float GetCostPerNight()
        {
            float costPerNight = -1;

            if (DecoratedComponent != null)
            {
                costPerNight = DecoratedComponent.GetCost();
            }

            return costPerNight;
        }

        /*
         * Returns the cost of the decorated BookingComponent
         * (or -1 if the root component is not a concrete Booking).
         */
        public override float GetCost()
        {
            float cost = -1;

            if (DecoratedComponent != null)
            {
                cost = DecoratedComponent.GetCost();
            }

            return cost;
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
