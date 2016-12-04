﻿using System;
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
            List<Dictionary<String, String>> bookingData; 
            String[] bookings = Directory.GetFiles(dataDirectory);
            bool wasSuccessful = false;
            int i = 0;
            String value;

            while (!wasSuccessful && i < bookings.Length)
            {
                if (dataReader.ReadBooking(bookings[i], out bookingData))
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
                i++;
            }

            customerData = result;
            return wasSuccessful;
        }

        /*
         * Returns a list of booking numbers, all of which were made by
         * a given customer.
         */
        public List<int> GetAllBookingNbs(int customerNb)
        {
            List<int> bookingNbs = new List<int>();
            String[] bookingFiles = Directory.GetFiles(dataDirectory);
            List<Dictionary<String, String>> bookingData;
            String value;
            int start;
            int end;

            foreach (String fileName in bookingFiles)
            {
                if (dataReader.ReadBooking(fileName, out bookingData))
                {
                    foreach (Dictionary<String, String> d in bookingData)
                    {
                        if (d.TryGetValue(CustomerField.CUSTOMER_NUMBER
                                                       .ToString(),
                                          out value)
                         && Int32.Parse(value) == customerNb)
                        {
                            start = fileName.LastIndexOf("\\") + 1;
                            end = fileName.IndexOf(".") - start;
                            if (end > 0)
                            {
                                bookingNbs.Add(Int32.Parse(
                                            fileName.Substring(start, end)));
                            }
                        }
                    }
                }
            }

            return bookingNbs;
        }
    }
}
