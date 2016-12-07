using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Represents a booking.
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public class Booking : BookingComponent
    {
        // PROPERTIES:

        // the booking reference number.
        private int bookingNb;
        public override int GetBookingNb()
        {
            return this.bookingNb;
        }

        // the booking's customer.
        private PersonComponent cust;
        public override PersonComponent GetCustomer()
        {
            return this.cust;
        }

        // the booking start and end dates.
        private DateTime arrival;
        private DateTime departure;
        public override void GetDates(out DateTime arrival,
                                      out DateTime departure)
        {
            arrival = this.arrival;
            departure = this.departure;
        }

        //the booking's list of guests.
        private List<PersonComponent> guests = new List<PersonComponent>();
        public override List<PersonComponent> GetGuests()
        {
            return this.guests;
        }

        // METHODS:

        /*
         * Constructor.
         * 
         * throws ArgumentException if bookingNb is not strictly greater 
         * than 0, if departure day is not strictly later than arrival
         * day, or if customer is not decorated as a customer.
         */
        public Booking(int bookingNb, 
                       PersonComponent customer, 
                       DateTime arrival, 
                       DateTime departure)
        {
            if (customer.GetCustNb() <= 0)
            {
                throw new ArgumentException("Booking.cust must be"
                                            + "  decorated as a Customer");
            }

            if (bookingNb <= 0)
            {
                throw new ArgumentException("Booking.bookingNb must be"
                                            + " strictly greater than 0");
            }

            if (departure.DayOfYear <= arrival.DayOfYear
             && departure.Year <= arrival.Year)
            {
                throw new ArgumentException("departure day must be strictly"
                                            + " later than arrival day");
            }

            this.bookingNb = bookingNb;
            this.cust = customer;
            this.arrival = arrival;
            this.departure = departure;
        }

        /*
         * Adds a guest to the booking.
         * 
         * Throws ArgumentException if there is already 4 guests added to
         * the booking.
         */
        public override void AddGuest(PersonComponent guest)
        {
            //  Arcane compile error - need to solve
            /*
            if (!guest.isGuest())
            {
                throw new ArgumentException("this PersonComponent is not a guest");
            }
             */
             
            if (guests.Count >= 4)
            {
                throw new ArgumentException("this booking already has 4 guests");
            }
            this.guests.Add(guest);
        }

        /*
         * Returns the number of guests included in this Booking.
         */
        public override int GetNbGuests()
        {
            return guests.Count;
        }

        /*
         * Returns the number of nights booked.
         */
        public override int GetNbNights()
        {
            return (departure - arrival).Days;
        }

        /*
         * Returns the basic cost (extras excluded) of the booking.
         */
        public override double GetCost()
        {
            double cost = 0;
            int nights = this.GetNbNights();

            foreach (Guest g in guests)
            {
                if (g.GetAge() < 18)
                {
                    cost += 30 * nights;
                }
                else
                {
                    cost += 50 * nights;
                }
            }

            return cost;
        }

        /*
         * Returns a reference to the Booking itself it is not decorated
         * at all.
         */
        public override BookingComponent Undecorate(BookingComponent decorator)
        {
            return this;
        }

        /*
         * Returns a textual representation of the Booking in order 
         * to persist it to a CSV file.
         */
        public override String ToCSV()
        {
            int custIndex = indexOfCustomer();

            StringBuilder csvBooking = new StringBuilder("#BOOKING\r\n");
            csvBooking.Append(bookingNb + ",");
            csvBooking.Append(arrival.ToString().Substring(0, 10) + ",");
            csvBooking.Append(departure.ToString().Substring(0, 10) + "\r\n");

            if (custIndex >= 0)
            {
                csvBooking.Append(guests.ElementAt(custIndex).ToCSV());
            }
            else
            {
                csvBooking.Append(cust.ToCSV());
            }

            if (GetNbGuests() > 0)
            {
                foreach (Guest g in guests)
                {
                    if (guests.IndexOf(g) != custIndex)
                    {
                        csvBooking.Append(g.ToCSV());
                    }
                    
                }
            }

            return csvBooking.ToString();
        }

        /*
         * Returns the index of the element that is also a customer in 
         * list of guests, or -1 if none is.
         */
        private int indexOfCustomer()
        {
            int i = -1;

            foreach (Guest g in guests)
            {
                if (g.IsCustomer())
                {
                    i = guests.IndexOf(g);
                }
            }

            return i;
        }
    }
}
