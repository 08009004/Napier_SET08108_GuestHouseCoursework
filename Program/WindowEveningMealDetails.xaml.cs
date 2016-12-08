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
     * Interaction logic for WindowEveningMealDetails.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-08
     */
    public partial class WindowEveningMealDetails : Window
    {
        //PROPERTIES:

        // reference to calling window's ModelFacade instance:
        private ModelFacade mFacade;

        // points to the EveningMeal edited (is null when creating new one).
        private BookingDecorator eveningMeal;

        // METHODS:

        /*
         * Constructor, the index passed must be the index in the current
         * booking's decoration stack index as returned by 
         * ModelFacade.GetCurrentExtras()) or -1 for a new extra.
         */
        public WindowEveningMealDetails(ModelFacade mFacade,
                                        BookingDecorator instance)
        {
            this.mFacade = mFacade;
            this.eveningMeal = instance;
            InitializeComponent();

            if (this.eveningMeal != null)
            {
                txtDietRequirements.Text
                            = ((EveningMeal)instance).GetDietRequirements();
            }
            else
            {
                txtDietRequirements.Text = String.Empty;
            }
        }

        /*
         * Creates or updates the extra.
         */
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.eveningMeal != null)
            {
                mFacade.UpdateEveningMeal(this.eveningMeal, 
                                          txtDietRequirements.Text);
            }
            else
            {
                mFacade.AddEveningMeal(txtDietRequirements.Text);
            }

            this.Close();
        }
    }
}
