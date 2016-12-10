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
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public class BookingFactory
    {
        //PROPERTIES:

        // the next booking unique number:
        private int nxtBookingNb;

        // points to the PersonFactory instance.
        private PersonFactory personFactory = PersonFactory.Instance;

        // the singleton instance porperty
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

        // METHODS

        /*
         * Private constructor, to prevent class instantiation from
         * external classes (singleton class).
         */
        private BookingFactory() 
        {
            this.nxtBookingNb = 1;
        }

        /*
         * Restores the factory state according to sysData values.
         * Returns true if restore was successful, otherwise false.
         */
        public bool Restore(Dictionary<String, String> sysData)
        {
            String nxtBookNbString;
            bool outcome; 
            outcome = sysData.TryGetValue(
                            PersonFactoryField.NEXT_CUST_NB.ToString(), 
                            out nxtBookNbString);
            outcome = Int32.TryParse(nxtBookNbString, out this.nxtBookingNb);
            return outcome;
        }

        /*
         * Generates a new booking instance on the basis of the data 
         * passed as parameters.
         */
        public BookingComponent GetNewBooking(PersonComponent customer, 
                                              DateTime arrival, 
                                              DateTime departure)
        {
            return new Booking(
                            this.nxtBookingNb++, customer, arrival, departure);
        }

        /*
         * Returns a reference to the updated booking instance.
         */
        public BookingComponent UpdateBooking(int bookingNb, 
                                              PersonComponent newCustomer, 
                                              DateTime newArrival,
                                              DateTime newDeparture)
        {
            return new Booking(
                            bookingNb, newCustomer, newArrival, newDeparture);
        }

        /*
         * Generates a new booking instance on the basis of the data 
         * passed as parameters.
         */
        private BookingComponent getNewBooking(int bookingNb,
                                               PersonComponent customer,
                                               DateTime arrival,
                                               DateTime departure)
        {
            return new Booking(bookingNb, customer, arrival, departure);
        }

        /*
         * Decorates a BookingComponent, wrapping it inside a Breakfast 
         * decorator.
         */
        public Breakfast AddBreakfast(BookingComponent booking, 
                                      String dietRequirements)
        {
            Breakfast bf = new Breakfast(dietRequirements);
            bf.Setcomponent(booking);
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
            em.Setcomponent(booking);
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
            ch.Setcomponent(booking);
            return ch;
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
        public BookingComponent Restore(
                                    List<Dictionary<String, String>> booking)
        {
            BookingComponent result = null; 

            Dictionary<String, String> bData;
            Dictionary<String, String> cData;
            List<Dictionary<String, String>> guestsData;
            extract(booking, out bData, out cData, out guestsData);

            String bookingNb;
            String csvArrival;
            String csvDeparture;

            if (bData != null
             && cData != null
             && bData.TryGetValue(BookingField.BOOKING_NUMBER.ToString(), 
                                  out bookingNb)
             && bData.TryGetValue(BookingField.ARRIVAL.ToString(),
                                  out csvArrival)
             && bData.TryGetValue(BookingField.DEPARTURE.ToString(), 
                                  out csvDeparture))
            {
                result = getNewBooking(Int32.Parse(bookingNb),
                                       personFactory.RestoreCustomer(cData),
                                       Convert.ToDateTime(csvArrival), 
                                       Convert.ToDateTime(csvDeparture));

                if (result.GetCustomer().IsGuest())
                {
                    result.AddGuest(result.GetCustomer());
                }
            }

            result = addGuestsData(result, guestsData);
            result = addExtrasData(result, bData);
            return result;
        }

        /*
         * Extracts the BookingComponent data, the Customer data and the
         * Guests data from contents of a dictonary<attribute, values>, 
         * presumably recovered from persisteddata (the dictonary keys 
         * should follow the naming implemented in the *Field.cs 
         * enumerations)
         */
        private void extract(
                        List<Dictionary<String, String>> booking,
                        out Dictionary<String, String> bookingData,
                        out Dictionary<String, String> customerData,
                        out List<Dictionary<String, String>> guestsData) 
        {
            Dictionary<String, String> bd = null;
            Dictionary<String, String> cd = null;
            List<Dictionary<String, String>> gd 
                                    = new List<Dictionary<String, String>>();

            foreach (Dictionary<String, String> d in booking)
            {
                if (d.ContainsKey(BookingField.BOOKING_NUMBER.ToString()))
                {
                    bd = d;
                }
                else 
                if (d.ContainsKey(CustomerField.CUSTOMER_NUMBER.ToString()))
                {
                    cd = d;
                }
                else
                {
                    gd.Add(d);
                }
            }
            bookingData = bd;
            customerData = cd;
            guestsData = gd;
        }

        /*
         * Adds Guests to a BookingComponent according to data contents of a 
         * dictonary<attribute, values>, presumably recovered from persisted 
         * data (the dictonary keys should follow the naming implemented in 
         * the *Field.cs enumerations)
         */
        private BookingComponent addGuestsData(
                                    BookingComponent b, 
                                    List<Dictionary<String, String>> gData) 
        {
            foreach (Dictionary<String, String> d in gData)
            {
                    b.AddGuest(personFactory.RestoreGuest(d));
            }
            return b;
        }

        /*
         * Decorates a BookingComponent according to data contents of a 
         * dictonary<attribute, values>, presumably recovered from persisted 
         * data (the dictonary keys should follow the naming implemented in 
         * the *Field.cs enumerations)
         */
        private BookingComponent addExtrasData(
                                    BookingComponent b,
                                    Dictionary<String, String> bData)
        {
            if (b != null
             && bData != null) 
            {
                String s1;
                String s2;
                String s3;

                if (bData.TryGetValue(
                                BreakfastField.DIET_REQUIREMENT_BREAKFAST
                                              .ToString(),
                                out s1))
                {
                    b = AddBreakfast(b, s1);
                }

                if (bData.TryGetValue(
                                EveningMealField.DIET_REQUIREMENT_EVENING
                                                .ToString(),
                                out s1))
                {
                    b = AddEveningMeal(b, s1);
                }

                if (bData.TryGetValue(CarHireField.DRIVER_NAME.ToString(),
                                      out s1)
                 && bData.TryGetValue(CarHireField.START.ToString(),
                                      out s2)
                 && bData.TryGetValue(CarHireField.END.ToString(),
                                      out s3))
                {
                    b = AddCarHire(b, 
                                   s1, 
                                   Convert.ToDateTime(s2), 
                                   Convert.ToDateTime(s3));
                }
            }
            return b;
        }

        /*
         * Returns a textual representation of the BookingFactory object
         * in order to persist it to a CSV file.
         */
        public String ToCSV()
        {
            return "#BOOKING_FACTORY\r\n" + nxtBookingNb + "\r\n";
        }
    }
}
