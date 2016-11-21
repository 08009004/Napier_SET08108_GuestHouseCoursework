using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Static utility class, facade for the Data Persistence implementation
     * details.
     */
    static class DataPersistenceFacade
    {
        private static string personFile = @"person.csv";
        /*
         * Writes the data concerning all instances of Person in the  
         * list passed as a parameter to the respective CSV file.
         */
        public static bool Persist(List<Person> persons)
        {
            bool wasPersisted = false;
            foreach (Person p in persons) CSVWriter.WriteData(personFile, p);

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
