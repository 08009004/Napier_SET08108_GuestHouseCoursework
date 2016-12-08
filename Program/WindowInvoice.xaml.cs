using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Program
{
    /*
     * Interaction logic for MainWindow.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-07
     */
    public partial class WindowInvoice : Window
    {
        // PROPERTIES:

        // reference to a ModelFacade instance:
        private ModelFacade mFacade;


        // METHODS:

        /*
         * Constructor.
         */
        public WindowInvoice(ModelFacade mFacade)
        {
            InitializeComponent();
            this.mFacade = mFacade;

            // get all the amounts:
            int nbNights = mFacade.GetCurrentNbNights();
            float costPerNight = mFacade.GetCurrentCostPerNight();
            float breakfastCost = mFacade.GetCurrentBreakfastCost();
            float eveningMealsCost = mFacade.GetCurrentEveningMealsCost();
            float carHireCost = mFacade.GetCurrentCarHireCost();

            printDetails();
            printBookingBreakdown(nbNights, costPerNight);
            printExtrasBreakdown(breakfastCost, eveningMealsCost, carHireCost);
            printTotal((nbNights * costPerNight) 
                            + breakfastCost
                            + eveningMealsCost
                            + carHireCost);
        }

        /*
         * Fills the labels of the invoice concerning the booking
         * details.
         */
        private void printDetails()
        {
            lblBookingNb.Content
                += " " + mFacade.GetCurrentBookNb().ToString();

            lblCustomerNb.Content
                += " " + mFacade.GetCurrentCustNb().ToString();

            lblCustomerDetails.Content
                += " " + mFacade.GetCurrentCustName();

            lblAddress.Content
                = mFacade.GetCurrentCustAdress();
        }

        /*
         * Fills the labels of the invoice concerning the booking
         * cost.
         */
        private void printBookingBreakdown(int nbNights, float costPerNight)
        {
            lblNbNightsValue.Content = nbNights;
            lblCostPerNightValue.Content = costPerNight;
            lblSubtotalValue.Content = (costPerNight * nbNights);
        }

        /*
         * Fills the labels of the invoice concerning the extras
         * costs.
         */
        private void printExtrasBreakdown(float breakfastCost,
                                          float eveningMealsCost,
                                          float carHireCost)
        {
            

            lblBreakfastCostValue.Content = breakfastCost;
            lblEveningMealsCostValue.Content = eveningMealsCost;
            lblCarHireCostValue.Content = carHireCost;

            lblExtrasTotalValue.Content = (breakfastCost 
                                           + eveningMealsCost 
                                           + carHireCost);
        }

        /*
         * Fills the labels of the invoice concerning the booking
         * total due.
         */
        private void printTotal(float grandTotal)
        {
            lblTotalDueValue.Content = grandTotal;
        }
    }
}
