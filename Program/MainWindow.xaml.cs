﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            PersonFactory pf = PersonFactory.Instance;
            BookingFactory bf = BookingFactory.Instance;
            DataPersistenceFacade dpf = new DataPersistenceFacade();

            // -------------------
            
            Customer c1 = pf.GetNewCustomer("pierre", "someplace");

            BookingComponent b1 = bf.GetNewBooking(c1, 
                                          new DateTime(2016, 1, 18), 
                                          new DateTime(2016, 1, 20));

            b1.AddGuest(pf.GetNewGuest("caroline g", "080055GW", 35));
            b1.AddGuest(pf.GetNewGuest(c1, "564HGFR", 31));

            b1 = bf.AddBreakfast(b1, "1 vegetarian");

            dpf.Persist(b1);

            // -------------------

            Customer c2 = pf.GetNewCustomer("sponge bob", "bikini bottom");

            BookingComponent b2 = bf.GetNewBooking(c2,
                                          new DateTime(2016, 1, 19),
                                          new DateTime(2016, 1, 27));

            b2.AddGuest(pf.GetNewGuest("patrick starfish", "***b**", 12));
            b2.AddGuest(pf.GetNewGuest("sandy squirrel", "TEXAS001", 13));

            b2 = bf.AddEveningMeal(b2, "crabbie patties");
            b2 = bf.AddCarHire(b2, "sandy", new DateTime(2016, 1, 20), new DateTime(2016, 1, 23));

            dpf.Persist(b2);

            // -------------------
            /*
            MessageBox.Show("b1 cost: £" + b1.GetCost()
                            + "\r\n TOTAL: £" + b1.GetCost());
            MessageBox.Show("b2 cost: £" + b2.GetCost()
                            + "\r\n TOTAL: £" + b2.GetCost());
            
            */

            
            List<Dictionary<String, String>> ld1;
            List<Dictionary<String, String>> ld2;
            if (dpf.Read(1, out ld1) && dpf.Read(2, out ld2))
            {
                String v;
                int i = 0;
                /*
                foreach (Dictionary<String, String> d in ld1)
                {
                    i++;
                    foreach (String k in d.Keys)
                    {
                        if (d.TryGetValue(k, out v))
                        {
                            MessageBox.Show("list<dict> #1"
                                            + " - dict nb: " + i + "\r\n"
                                            + "KEY: " + k
                                            + "\r\n"
                                            + "VALUE: " + v);
                        }

                    }
                }
                 */

                i = 0;

                foreach (Dictionary<String, String> d in ld2)
                {
                    i++;
                    foreach (String k in d.Keys)
                    {
                        if (d.TryGetValue(k, out v))
                        {
                            MessageBox.Show("list<dict> #2"
                                            + " - dict nb: " + i + "\r\n"
                                            + "KEY: " + k
                                            + "\r\n"
                                            + "VALUE: " + v);
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("error reading files");
            }
            

            InitializeComponent();

        }
    }
}
