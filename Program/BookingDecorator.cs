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
        // PROPERTIES:
        
        // the BookingDecorator component
        protected BookingComponent decoratedComponent { get; set; }
        public void Setcomponent(BookingComponent booking)
        {
            decoratedComponent = booking;
        }

        // METHODS:

        /*
         * Adds a guest to the decorated booking.
         */
        public override void AddGuest(PersonComponent guest)
        {
            if (decoratedComponent != null)
            {
                decoratedComponent.AddGuest(guest);
            }
        }

        /*
         * Returns the decorated BookingComponent's list of booking number
         * (or -1 if the root component is not a concrete Booking).
         */
        public override int GetBookingNb()
        {
            int bookingNb = -1;

            if (decoratedComponent != null)
            {
                bookingNb = decoratedComponent.GetBookingNb();
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

            if (decoratedComponent != null)
            {
                customer = decoratedComponent.GetCustomer();
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

            if (decoratedComponent != null)
            {
                guests = decoratedComponent.GetGuests();
            }

            return guests;
        }

        /*
         * Returns the root BookingComponent of the decoration stack; 
         * references outputs a list of pointers to all the BookingDecorator 
         * instances in the decoration stack.
         */
        public override BookingComponent Unwrap(
                                          out List<BookingDecorator> references)
        {
            BookingComponent component = this;
            List<BookingDecorator> decorators = new List<BookingDecorator>();

            while (component.isDecorator())
            {
                decorators.Add((BookingDecorator)component);
                component = ((BookingDecorator)component).decoratedComponent;
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
            return this.decoratedComponent != null;
        }

        /*
         * Returns a reference to a new BookingComponent identical in content 
         * to calling instance, except unwraped from the BookingDecorator 
         * passed as a parameter (or the to the Booking itself if it is not 
         * decorated at all).
         */
        public override BookingComponent Undecorate(BookingDecorator reference)
        {
            if (this == reference) return decoratedComponent;
                /* Short circuit method if the decorator to remove is the
                 * last one added.
                 */
            List<BookingDecorator> references;
            BookingComponent result = this.Unwrap(out references);
            
            foreach (BookingDecorator decorator in references)
            {
                if (decorator != reference)
                {
                    decorator.decoratedComponent = result;
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

            if (decoratedComponent != null)
            {
                decoratedComponent.GetDates(out a, out d);
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

            if (decoratedComponent != null)
            {
                nbGuests = decoratedComponent.GetNbGuests();
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

            if (decoratedComponent != null)
            {
                nbNights = decoratedComponent.GetNbNights();
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

            if (decoratedComponent != null)
            {
                costPerNight = decoratedComponent.GetCostPerNight();
            }

            return costPerNight;
        }

        /*
         * Returns the cost of the decorated BookingComponent
         * (or -1 if the root component is not a concrete Booking).
         */
        public virtual float GetExtraCost()
        {
            float cost = -1;

            if (decoratedComponent != null
                && decoratedComponent.isDecorator())
            {
                cost = ((BookingDecorator)decoratedComponent).GetExtraCost();
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

            if (decoratedComponent != null)
            {
                csv = decoratedComponent.ToCSV();
            }

            return csv;
        }
    }
}
