using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Represents a breakfast extra.
     */
    class Breakfast : BookingDecorator
    {
        // Property:
        private String dietRequirement;

        /*
         * Constructor.
         */
        public Breakfast(String dietRequirement)
        {
            this.dietRequirement = dietRequirement;
        }

        /*
         * Returns the cost of the decorated BookingComponent, 
         * breakfast included.
         */
        public override double GetCost()
        {
            return base.GetCost() + (5 * base.DecoratedComponent.GetNbGuests()
                                       * base.DecoratedComponent.GetNbNights());
        }

        /*
         * Returns a textual representation of the decorated 
         * BookingDecorator in order to persist it to a CSV file.
         */
        public override String ToCSV()
        {
            return base.ToCSV() + "#BREAKFAST\r\n"
                                + dietRequirement
                                + "\r\n";
        }
    }
}
