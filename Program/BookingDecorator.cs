using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    abstract class BookingDecorator : BookingComponent
    {
        // Property : the decorator component
        private BookingComponent booking;

        /*
         * Sets the component that this decorator decorates.
         */
        public void SetComponent(BookingComponent component)
        {
            this.booking = component;
        }

        /*
         * Returns a textual representation of the BookingDecorator 
         * in order to persist it to a CSV file.
         */
        public override String ToCSV()
        {
            String csv = String.Empty;

            if (booking != null)
            {
                csv = booking.ToCSV();
            }

            return csv;
        }
    }
}
