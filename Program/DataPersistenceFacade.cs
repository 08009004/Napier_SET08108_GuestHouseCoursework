using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// custom imports:
using System.IO;

namespace Program
{
    /*
     * Utility class, facade for the Data Persistence implementation
     * details.
     */
    class DataPersistenceFacade
    {
        // Properties: 
        // the data persistence directory.
        private String dataDirectory = @"data";
        // the data writer instance:
        private CSVWriter dataWriter = CSVWriter.Instance;
        // the data reader instance:
        private CSVReader dataReader = CSVReader.Instance;

        /*
         * Persists the BookingComponent to {dataDirectory}/{bookingNb}.csv; 
         * returns true if data was persisted successfuly or false if not.
         */
        public bool Persist(BookingComponent booking)
        {
            String filePath = (String.Format(@"{0}/{1}.csv", 
                                             dataDirectory, 
                                             booking.GetBookingNb()));

            return dataWriter.Persist(booking, filePath);
        }

        /*
         * Builds a List<Dictionary<attribute, value>>, each representing
         * an entity of a given BookingComponent (the dictonary keys are named
         * as defined in the *Field.cs enumerations);
         * returns true if data was read successfuly, otherwise false.
         */
        public bool Read(int bookingNb,
                         out List<Dictionary<String, String>> bookingData)
        {
            String filePath = (String.Format(@"{0}/{1}.csv", 
                                             dataDirectory,
                                             bookingNb));

            List<Dictionary<String, String>> results;
            bool wasSuccessful = dataReader.ReadBooking(filePath, out results);
            
            bookingData = results;
            return wasSuccessful;
        }

        /*
         * Returns a Dictionary<attribute, value>, representing a Customer.cs 
         * instance (the dictonary keys are named as defined in the *Field.cs 
         * enumerations);
         * returns true if data was read successfuly otherwise false.
         */
        public bool Read(int customerNb,
                         out Dictionary<String, String> customerData)
        {
            Dictionary<String, String> result = null;
            List<Dictionary<String, String>> booking; 
            String[] bookings = Directory.GetFiles(dataDirectory);
            bool wasSuccessful = false;
            int i = 0;
            String value;

            while (!wasSuccessful && i < bookings.Length)
            {
                if (dataReader.ReadBooking(bookings[i], out booking))
                {
                    foreach (Dictionary<String, String> d in booking)
                    {
                        if (d.TryGetValue(CustomerField.CUSTOMER_NUMBER
                                                       .ToString(), 
                                          out value)
                         && Int32.Parse(value) == customerNb)
                        {
                            result = d;
                            wasSuccessful = true;
                        }
                    }
                }
                i++;
            }

            customerData = result;
            return wasSuccessful;
        }

    }
}
