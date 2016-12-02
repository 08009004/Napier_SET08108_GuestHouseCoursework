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
        private PersonFactory personFactory = PersonFactory.Instance;
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
         * 
         * Throws ArgumentException if either  hire start or hire end
         * date falls without the booking dates range.
         */
        public CarHire AddCarHire(BookingComponent booking, 
                                  String driverName, 
                                  DateTime start, 
                                  DateTime end)
        {
            DateTime arrival;
            DateTime departure;
            booking.GetDates(out arrival, out departure);

            if (start < arrival || end > departure)
            {
                throw new ArgumentException("CarHire start & end dates must"
                                        + " be within the booking dates" 
                                        + " range.");
            }

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

        /*
         * Instantiates a BookingComponent from a 
         * List<Dictonary<attribute, values>>, presumably recovered from 
         * persisted data (the dictonaries keys should follow the naming 
         * implemented in the *Field.cs enumerations)
         * 
         * Thows Argument exception if there is a problem with the contents
         * of the dictionary passed as a parameter.
         */
        public BookingComponent Restore(List<Dictionary<String, String>> booking)
        {
            Dictionary<String, String> bookingData = null;
            Dictionary<String, String> customerData = null;
            List<Dictionary<String, String>> guestsData = null;
            List<PersonComponent> guests = new List<PersonComponent>();
            Customer c = null;
            String csvArrival;
            String csvDeparture;
            BookingComponent result = null;

            foreach (Dictionary<String, String> d in booking)
            {
                if (d.ContainsKey(BookingField.BOOKING_NUMBER.ToString()))
                {
                    bookingData = d;
                }
                else if (d.ContainsKey(CustomerField.CUSTOMER_NUMBER.ToString()))
                {
                    customerData = d;
                }
                else
                {
                    guestsData.Add(d);
                }
            }

            if (customerData != null)
            {
                c = personFactory.RestoreCustomer(customerData);
            }

            if (bookingData != null 
             && c != null
             && bookingData.TryGetValue(BookingField.ARRIVAL.ToString(), out csvArrival)
             && bookingData.TryGetValue(BookingField.DEPARTURE.ToString(), out csvDeparture))
            {
                result = GetNewBooking(c, 
                                       Convert.ToDateTime(csvArrival), 
                                       Convert.ToDateTime(csvDeparture));
            }

            if (guestsData != null)
            {
                foreach (Dictionary<String, String> d in guestsData)
                {
                    result.AddGuest(personFactory.RestoreGuest(d));
                }
            }

            return result;
        }
    }
}
