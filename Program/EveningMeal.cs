using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Represents an evening meals extra.
     */
    public class EveningMeal : BookingDecorator
    {
        // Property:
        private String dietRequirement;

        /*
         * Constructor.
         */
        public EveningMeal(String dietRequirement)
        {
            this.dietRequirement = dietRequirement;
        }

        /*
         * Returns the cost of the decorated BookingComponent, 
         * evening meals included.
         */
        public override double GetCost()
        {
            return base.GetCost() + (15 * base.DecoratedComponent.GetNbGuests()
                                        * base.DecoratedComponent.GetNbNights());
        }

        /*
         * Returns a textual representation of the decorated 
         * BookingDecorator in order to persist it to a CSV file.
         */
        public override String ToCSV()
        {
            return base.ToCSV() + "#EVENING_MEAL\r\n"
                                + dietRequirement
                                + "\r\n";
        }
    }
}
