using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Utility class, facade for the Data Persistence implementation
     * details.
     */
    class DataPersistenceFacade
    {
        // Property: the path to the csv file.
        private String csvFilePath = @"holiday_data.csv";

        /*
         * Returns true if a guest with the name passed as a parameter
         * was found in the persisted data, otherwise false.
         * 
         * When method returns true, guests will be a list of all dictionaries
         * representing the instances where a guest with such a name was found.
         */
        public bool Exists(String guestName, 
                           out List<Dictionary<string, string>> foundInstances)
        // REFERENCE: 
        // https://www.tutorialspoint.com/csharp/csharp_output_parameters.htm
        {
            String name;
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
                              //      = CSVReader.ReadBooking(csvFilePath);
            List<Dictionary<string, string>> found
                                    = new List<Dictionary<string, string>>();

            foreach (Dictionary<string, string> d in data)
            {
                if (d.TryGetValue("NAME", out name) && name.Equals(guestName)) 
                {
                    found.Add(d);
                }
            }

            foundInstances = found;

            return foundInstances != null && foundInstances.Count > 0;
        }


        /*
         * Writes the data concerning all instances of Person in the  
         * list passed as a parameter to the respective CSV file.
         */
        public static bool Persist(List<Person> persons)
        {
            bool wasPersisted = false;
 //           foreach (Person p in persons) CSVWriter.WriteData(@"person.csv", p);

            return wasPersisted;
        }

        /*
         * Selects and reads the data about all the persisted Person instances
         * from the respective CSV file, and  returns a list of CSV instances 
         * (as strings).
         */
        public static List<Person> RecoverPersons()
        {
          //  return CSVReader.ReadData<Person>(@"person.csv");
            return new List<Person>();
        }

    }
}
