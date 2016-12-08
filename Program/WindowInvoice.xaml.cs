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
        // METHODS:

        /*
         * Constructor.
         */
        public WindowInvoice(ModelFacade mFacade)
        {
            InitializeComponent();

            lblBookingNb.Content += " " + mFacade.GetCurrentBookingNb()
                                                 .ToString();
        }
    }
}
