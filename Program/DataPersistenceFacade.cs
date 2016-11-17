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
        /*
         * Writes the data concerning all instances of Person in the  
         * list passed as a parameter to the respective CSV file.
         */
        public static bool Persist(List<Person> persons)
        {
            bool wasPersisted = false;
            foreach (Person p in persons) CSVWriter.WriteData<Person>(@"person.csv", p);

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

        private static String[] parsePerson(String personData)
        {
            String[] data = new String[9];
            
            return data;
        }

        public static List<String> seperate(String personData) 
        {
            int l;
            List<String> separated = new List<String>();

            while (personData.Length > 0)
            {
                int lenght = personData.Length;
                l = personData.IndexOf(",");
                separated.Add(personData.Substring(0, l));
                personData = personData.Substring(l+1, (personData.Length-l));
            }

            return separated;
        }
        
    }
}
