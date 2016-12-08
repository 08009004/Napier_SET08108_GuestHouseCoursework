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
     * Interaction logic for WindowCarHireDetails.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-08
     */
    public partial class WindowCarHireDetails : Window
    {
        //PROPERTIES:

        // reference to calling window's ModelFacade instance:
        private ModelFacade mFacade;

        // METHODS:

        /*
         * Constructor, the index passed must be the index in the current
         * booking's decoration stack index as returned by 
         * ModelFacade.GetCurrentExtras()) or -1 for a new extra.
         */
        public WindowCarHireDetails(ModelFacade mFacade, int index)
        {
            this.mFacade = mFacade;
            InitializeComponent();

            if (index >= 0)
            {
                // txtDietRequirements.Text = "display current value at index: " + index;
            }
            else
            {
                // txtDietRequirements.Text = String.Empty;
            }
        }
    }
}
