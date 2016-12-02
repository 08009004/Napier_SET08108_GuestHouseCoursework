using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Utility class, facade for the Data Persistence implementation
     * details.
     */
    class DataPersistenceFacade
    {
        // Property: the data persistence directory.
        private String dataDirectory = @"data";

        /*
         * Persists the BookingComponent to {dataDirectory}/{bookingNb}.csv; 
         * returns trus if data was persisted successfuly or false if not.
         */
        public bool Persist(BookingComponent booking)
        {
            String filePath = (String.Format(@"{0}/{1}.csv", 
                                             dataDirectory, 
                                             booking.GetBookingNb()));

            return CSVWriter.Persist(booking, filePath);
        }

    }
}
