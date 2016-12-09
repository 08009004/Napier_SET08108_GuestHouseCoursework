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
     * Interaction logic for WindowCustomerDetails.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-09
     */
    public partial class WindowCustomerDetails : Window
    {
        //Property:
        // reference to calling window's ModelFacade instance:
        private ModelFacade mFacade;

        // GENERIC WINDOW METHODS:

        /*
         * The window constructor.
         */
        public WindowCustomerDetails(ModelFacade modelFacade)
        {
            this.mFacade = modelFacade;

            InitializeComponent();
        }


    }
}
