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
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public class DataPersistenceFacade
    {
        // PROPERTIES:
 
        // the bookings data persistence directory.
        private String dataDirectory = @"bookings";
        // the system data persistence directory.
        private String systemDirectory = @"system";
        // the data writer instance:
        private CSVWriter dataWriter = CSVWriter.Instance;
        // the data reader instance:
        private CSVReader dataReader = CSVReader.Instance;

        // METHODS RELATED TO BOOKINGS:

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
         * Returns a list of all booking numbers ever persisted.
         */
        public List<int> GetAllBookingNbs()
        {
            if (!Directory.Exists(dataDirectory))
            {
                dataWriter.CreateDir(dataDirectory);
            }

            String[] filePaths = Directory.GetFiles(dataDirectory);
            List<int> bookingNbs = new List<int>();

            foreach (String fp in filePaths)
            {
                bookingNbs.Add(extractBookingNb(fp));
            }

            return bookingNbs;
        }

        /*
         * Returns the booking number from given persisted booking file name.
         */
        private int extractBookingNb(String bookingFileName)
        {
            int start = bookingFileName.LastIndexOf("\\") + 1;
            int end = bookingFileName.IndexOf(".") - start;
            int bookingNb = -1;
            if (end > 0)
            {
                bookingNb = Int32.Parse(bookingFileName.Substring(start, end));
            }
            return bookingNb;
        }

        /*
         * Deletes the file storing given booking from disc if it
         * exists.
         */
        public void Delete(int bookingNb)
        {
            String filePath = (String.Format(@"{0}/{1}.csv",
                                             dataDirectory,
                                             bookingNb));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        // METHODS RELATED TO CUSTOMERS:

        /*
         * Returns a list of all booking numbers that were made by
         * a given customer.
         */
        public List<int> GetAllBookingNbs(int customerNb)
        {
            List<int> bookingNbs = new List<int>();
            String[] bookingFiles = new String[0];
            List<Dictionary<String, String>> bookingData;
            String value;

            if (Directory.Exists(dataDirectory))
            {
                bookingFiles = Directory.GetFiles(dataDirectory);
            }

            foreach (String fPath in bookingFiles)
            {
                if (dataReader.ReadBooking(fPath, out bookingData))
                {
                    foreach (Dictionary<String, String> d in bookingData)
                    {
                        if (d.TryGetValue(CustomerField.CUSTOMER_NUMBER
                                                       .ToString(),
                                          out value)
                         && Int32.Parse(value) == customerNb)
                        {
                            bookingNbs.Add(extractBookingNb(fPath));
                        }
                    }
                }
            }

            return bookingNbs;
        }

        /*
         * Returns a list of all the customer numbers persisted to file.
         */
        public List<int> GetAllCustomerNbs()
        {
            int customerNb;

            if (!Directory.Exists(dataDirectory))
            {
                dataWriter.CreateDir(dataDirectory);
            }

            List<int> customerNbs = new List<int>();

            foreach (String bookingFile in Directory.GetFiles(dataDirectory))
            {
                customerNb = extractCustomerNb(bookingFile);
                if (!customerNbs.Contains(customerNb)) 
                {
                    customerNbs.Add(customerNb);
                }
            }

            return customerNbs;
        }

        /*
         * Returns the customer number from given persisted booking file,
         * or -1 if the file cant be found.
         */
        private int extractCustomerNb(String bookingFile)
        {
            List<Dictionary<String, String>> bookingData;
            if (File.Exists(bookingFile)
             && dataReader.ReadBooking(bookingFile, out bookingData))
            {
                int customerNb;
                String customerNbString;

                foreach (Dictionary<String, String> entity in bookingData)
                {
                    if (entity.TryGetValue(CustomerField.CUSTOMER_NUMBER
                                                        .ToString(), 
                                           out customerNbString)
                     && Int32.TryParse(customerNbString, out customerNb))
                    {
                        return customerNb;
                        /* short circuit looping and exit method as soon
                         * as customer number was found.
                         */
                    }
                }
            }
            
            return -1; // or return -1
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
            List<int> bookings = GetAllBookingNbs(customerNb);
            bool wasSuccessful = false;

            if (bookings != null && bookings.Count > 0)
            {
                int bookingNb = bookings.ElementAt(0);
                List<Dictionary<String, String>> bookingData;
                String value;

                if (this.Read(bookingNb, out bookingData))
                {
                    foreach (Dictionary<String, String> d in bookingData)
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
            }
            
            customerData = result;
            return wasSuccessful;
        }

        // METHODS RELATED TO SYSTEM STATE:

        /*
         * Restores system state from file.
         */
        public bool PersistSystemState()
        {
            String personFactoryPath = (String.Format(@"{0}/{1}.csv",
                                             systemDirectory,
                                             "person-factory"));

            String bookingFactoryPath = (String.Format(@"{0}/{1}.csv",
                                             systemDirectory,
                                             "booking-factory"));

            return dataWriter.Persist(PersonFactory.Instance, 
                                      personFactoryPath)
                && dataWriter.Persist(BookingFactory.Instance, 
                                      bookingFactoryPath);
        }

        /*
         * Returns a Dictionary<attribute, value>, representing the last
         * system state saved (the dictonary keys are named as defined in the 
         * SystemField.cs enumeration);
         * returns true if data was read successfuly otherwise false.
         */
        public bool ReadSystemState(out Dictionary<String, String> sysData)
        {
            return dataReader.ReadSystemState(systemDirectory, out sysData);
        }
    }
}
