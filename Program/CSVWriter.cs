using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Reflection;
using System.Windows;

namespace Program
{
    static class CSVWriter
    {

        public static void Persist<T>(T obj)
        {
            generateCSVData<T>(obj);
        }

        private static String generateCSVData<T>(T obj)
        {
            StringBuilder csv = new StringBuilder();
            listProperties<T>(obj);
//            csv.Append("flight code, " + data.FlightCode + ", ");
//            csv.Append("airline, " + data.Airline + ", ");
//            csv.Append("departure, " + data.Departure + ", ");
//            csv.Append("arrival time, " + data.ArrivalTime);
            return csv.ToString();
        }

        private static void writeCVSFile(String code, String csv)
        {
            System.IO.File.AppendAllText(code + ".csv", csv);
        }

        private static List<String> listProperties<T>(T obj)
        {
            // http://stackoverflow.com/questions/737151/how-to-get-the-list-of-properties-of-a-class
            /* foreach (Object o in p.GetType().GetProperties(BindingFlags.Public 
                                                            | BindingFlags.NonPublic 
                                                            | BindingFlags.Instance)) 
             {
                 MessageBox.Show(o.ToString());
             }
            */                                                                                              
            // using System.ComponentModel;
            // using  System.Reflection;

            List<string> l = new List<string>();

            foreach (Object o in typeof(T).GetType().GetProperties(BindingFlags.Public
                                                            | BindingFlags.NonPublic
                                                            | BindingFlags.Instance))
            {
                l.Add(o.ToString());
            }

            foreach (string s in l) MessageBox.Show(s);

            return l;
        }

    }
}
