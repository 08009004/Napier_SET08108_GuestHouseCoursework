using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Booking : BookingComponent
    {
        private int bookingNb;
        private Customer cust;
        private DateTime arrival;
        private DateTime departure;
        private List<Guest> guests = new List<Guest>();

        /*
         * Constructor.
         * 
         * throws ArgumentException if bookingNb is not strictly greater 
         * than 0; or if departure day is not strictly later than arrival
         * day
         */
        public Booking(int bookingNb, 
                       Customer customer, 
                       DateTime arrival, 
                       DateTime departure)
        {
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
        public void AddGuest(Guest guest)
        {
            if (guests.Count >= 4)
            {
                throw new ArgumentException("this booking already has 4 guests");
            }
            this.guests.Add(guest);
        }

        /*
         * Returns the number of guests included in this Booking.
         */
        public int GetNbGuests()
        {
            return guests.Count;
        }

        /*
         * Returns the number of nights booked.
         */
        public int GetNbNights()
        {
            return (departure - arrival).Days;
        }

        /*
         * Returns the basic cost (extras excluded) of the booking.
         */
        public double GetCost()
        {
            double cost = 0;
            int nights = this.GetNbNights();

            foreach (Guest g in guests)
            {
                if (g.Age < 18)
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
    }
}
