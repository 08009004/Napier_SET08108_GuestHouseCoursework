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

        // index of the extra in the current booking's decoration stack
        // ( or -1 if new extra):
        private int index;

        // METHODS:

        /*
         * Constructor, the index passed must be the index in the current
         * booking's decoration stack index as returned by 
         * ModelFacade.GetCurrentExtras()) or -1 for a new extra.
         */
        public WindowEveningMealDetails(ModelFacade mFacade, int index)
        {
            this.mFacade = mFacade;
            this.index = index;
            InitializeComponent();

            if (index >= 0)
            {
                txtDietRequirements.Text = "display current value at index: " + index;
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
            if (index >= 0)
            {
                // update
            }
            else
            {
                mFacade.AddEveningMeal(txtDietRequirements.Text);
            }

            this.Close();
        }
    }
}
