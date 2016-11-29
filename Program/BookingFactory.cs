using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Singleton utility class, instantiates and decorates
     * BookingComponents.
     */
    class BookingFactory
    {
        //Properties:
        private int nxtBookingNb;
        private static BookingFactory instance;
        public static BookingFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingFactory();
                }
                return instance;
            }
        }

        /*
         * Private constructor, to prevent class instantiation from
         * external classes (singleton class).
         */
        private BookingFactory() 
        {
            this.nxtBookingNb = 1;
        }

        /*
         * Generates a new booking instance on the basis of the data 
         * passed as parameters.
         */
        public Booking GetNewBooking(Customer cust, 
                                     DateTime arrival, 
                                     DateTime departure)
        {
            return new Booking(this.nxtBookingNb++, cust, arrival, departure);
        }

        /*
         * Decorates a BookingComponent, wrapping it inside a Breakfast 
         * decorator.
         */
        public Breakfast AddBreakfast(BookingComponent booking, 
                                      String dietRequirements)
        {
            Breakfast bf = new Breakfast(dietRequirements);
            bf.DecoratedComponent = booking;
            return bf;
        }

        /*
         * Decorates a BookingComponent, wrapping it inside an EveningMeal 
         * decorator. 
         */
        public EveningMeal AddEveningMeal(BookingComponent booking, 
                                          String dietRequirements)
        {
            EveningMeal em = new EveningMeal(dietRequirements);
            em.DecoratedComponent = booking;
            return em;
        }

        /*
         * Decorates a BookingComponent, wrapping it inside a CarHire 
         * decorator. 
         */
        public CarHire AddCarHire(BookingComponent booking, 
                                  String driverName, 
                                  DateTime start, 
                                  DateTime end)
        {
            CarHire ch = new CarHire(driverName, start, end);
            ch.DecoratedComponent = booking;
            return ch;
        }

        /*
         * Returns a textual representation of the BookingFactory object
         * in order to persist it to a CSV file.
         */
        public String ToCSV()
        {
            return "#BOOKING_FACTORY\r\n" + nxtBookingNb + "\r\n";
        }

        public void RestoreBooking(int bookingNb)
        {

        }

        private void RestoreBooking(Dictionary<String, String> attributes)
        {

        }
    }
}
