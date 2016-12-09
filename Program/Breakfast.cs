using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Represents a breakfast extra.
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public class Breakfast : BookingDecorator
    {
        // PROPERTIES:

        private String dietRequirement;
        public String GetDietRequirements()
        {
            return this.dietRequirement;
        }

        // METHODS:

        /*
         * Constructor.
         */
        public Breakfast(String dietRequirement)
        {
            this.dietRequirement = dietRequirement;
        }

        /*
         * Returns the extra cost of the breakfasts.
         */
        public override float GetCost()
        {
            return 5 * base.DecoratedComponent.GetNbGuests()
                     * base.DecoratedComponent.GetNbNights();
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
