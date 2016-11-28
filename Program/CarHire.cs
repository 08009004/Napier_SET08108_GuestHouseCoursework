using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class CarHire : BookingDecorator
    {
        // Properties:
        private String driverName;
        private DateTime start;
        private DateTime end;

        /*
         * Constructor.
         * 
         * throws ArgumentException if (String.IsNullOrWhiteSpace(driverName)  
         * or if end day is not strictly later than start day
         */
        public CarHire(String driverName, DateTime start, DateTime end)
        {
            if (String.IsNullOrWhiteSpace(driverName))
            {
                throw new ArgumentException("CarHire.driverName must not be"
                                            + " null, whitespace or an" 
                                            + " empty string.");
            }

            if (end.DayOfYear <= start.DayOfYear
             && end.Year <= start.Year)
            {
                throw new ArgumentException("end day must be strictly"
                                            + " later than start day");
            }

            this.driverName = driverName;
            this.start = start;
            this.end = end;
        }

        /*
         * Returns the cost of the decorated BookingComponent, 
         * car hire included.
         */
        public override double GetCost()
        {
            return ((end - start).Days) * 50;
        }

        /*
         * Returns a textual representation of the decorated 
         * BookingDecorator in order to persist it to a CSV file.
         */
        public override String ToCSV()
        {
            StringBuilder csvCarHire = new StringBuilder("##CAR_HIRE\r\n");

            csvCarHire.Append(driverName + ",");
            csvCarHire.Append(start.ToString().Substring(0, 10) + ",");
            csvCarHire.Append(end.ToString().Substring(0, 10) + "\r\n");

            return base.ToCSV() + csvCarHire.ToString();
        }
    }
}
