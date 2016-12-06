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
     * Interaction logic for Bookings.xaml
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public partial class Bookings : Window
    {
        // Property:
        // points to a ModelFacade instance.
        private ModelFacade mFacade;

        /*
         * Constructor.
         */
        public Bookings(ModelFacade mFacade)
        {
            this.mFacade = mFacade;
            InitializeComponent();
        }

        /*
         * Closes the window.
         */
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
