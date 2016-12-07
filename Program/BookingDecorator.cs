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
         * Returns a new BookingComponent, decorated with all the
         * same BookingDecorators except the one passed as a parameter, 
         * or the to the Booking itself if it is not decorated.
         */
        public override BookingComponent Undecorate(BookingComponent decorator)
        {
       //     if (this == decorator) return DecoratedComponent;
                /* Short circuit method if the decorator to remove is the
                 * last one added.
                 */

            BookingComponent result = decorator;
            BookingComponent component = this;
            List<BookingDecorator> references = GetDecorators();

            // redecorate the BookingComponent:
            result = component;
            MessageBox.Show("result =\r\n\r\n" + result.ToCSV());

            foreach (BookingDecorator bComp in references)
            {
                MessageBox.Show("reference =\r\n\r\n" + bComp.ToCSV());
                bComp.DecoratedComponent = result;
                result = bComp;
            }

            MessageBox.Show("result =\r\n\r\n" + result.ToCSV());
            return result;
        }

        /*
         * Returns references to all the BookingDecorator instances in the
         * decoration stack.
         */
        public override List<BookingDecorator> GetDecorators()
        {
            BookingComponent component = this;
            List<BookingDecorator> references = new List<BookingDecorator>();

            while (component.isDecorator())
            {
                references.Add((BookingDecorator)component);
                component = ((BookingDecorator)component).DecoratedComponent;
            }

            return references;
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
