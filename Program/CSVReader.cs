using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Custom imports:
using System.IO;

// NOTE FOR GUI: open file dialogue
// http://www.wpf-tutorial.com/dialogs/the-openfiledialog/
// NOTE FOR STARTUP:
// http://stackoverflow.com/questions/6301529/open-a-text-file-with-wpf

/*
 * Static utility class, recovers objects data from CSV files.
 */
namespace Program
{
    /*
     * Singleton utility class, reads bookings data from CSV files.
     */
    class CSVReader
    {
        // Properties: 
        private static CSVReader instance;
        public static CSVReader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CSVReader();
                }
                return instance;
            }
        }

        /*
         * Private constructor, to prevent class instantiation from
         * external classes (singleton class).
         */
        private CSVReader() { }

        /*
         * Returns a Dictionary<string, string> representing a BookingComponent
         * as <attribute, value> (the dictonary keys follow the naming 
         * implemented in the *Field.cs enumerations).
         */
        public List<Dictionary<String, String>> ReadBooking(String filename)
        {
            List<String[]> extractedEntities 
                                    = extractClasses(readLines(filename));
            List<Dictionary<String, String>> indexedEntities 
                                    = new List<Dictionary<String, String>>();

            foreach (String[] sArr in extractedEntities)
            {
                indexedEntities.Add(index(sArr));
            }
            
            return indexedEntities;
        }

        /*
         * Reads from a CSV file and returns a list of strings, each 
         * corresponding to a line from the file.
         * 
         * Throws ArgumentException if the number of lines in the file was not 
         * even, as per the CSV formating done within classes.
         */
        private List<String> readLines(String filename)
            /*
             * RESOURCE:
             * https://msdn.microsoft.com/en-us/library/db5x7c0d(v=vs.110).aspx
             */
        {
            List<String> csvLines = new List<String>();
            String line;
            StreamReader sr = new StreamReader(filename);

            line = sr.ReadLine();
            while (line != null)
            {
                csvLines.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();

            if (csvLines.Count % 2 != 0)
            {
                throw new ArgumentException("Invalid CSV file: should contain"
                                            + " an even number of text"
                                            + " lines");
            }

            return csvLines;
        }

        /*
         * Extracts the different entities from a csv BookingComponent, 
         * each representing the data for a different class of that
         * BookingComponent.
         */
        private List<String[]> extractClasses(List<String> csvBooking) 
        {
            List<String[]> csvEntities = new List<String[]>();
            String[] booking = null;
            String[] person = null;

            for (int i = 0; i < csvBooking.Count; i = i+2 )
            {
                switch (csvBooking.ElementAt(i))
                {
                    case "#BOOKING":
                        if (person != null)
                        {
                            csvEntities.Add(person);
                            person = null;
                        }
                        booking = new String[1] { csvBooking.ElementAt(i) };
                        booking = append(booking, csvBooking.ElementAt(i + 1)
                                                            .Split(','));
                        break;
                    case "#BREAKFAST":
                    case "#EVENING_MEAL":
                    case "#CAR_HIRE":
                        Array.Resize<String>(ref booking, booking.Length + 1);
                        booking[booking.Length - 1] = csvBooking.ElementAt(i);
                        booking = append(booking, csvBooking.ElementAt(i + 1)
                                                            .Split(','));
                        break;
                    case "#PERSON":
                        if (person != null)
                        {
                            csvEntities.Add(person);
                            person = null;
                        }
                        person = new String[1] { csvBooking.ElementAt(i) };
                        person = append(person, csvBooking.ElementAt(i + 1)
                                                          .Split(','));
                        break;
                    case "#CUSTOMER":
                    case "#GUEST":
                        Array.Resize<String>(ref person, person.Length + 1);
                        person[person.Length - 1] = csvBooking.ElementAt(i);
                        person = append(person, csvBooking.ElementAt(i + 1)
                                                          .Split(','));
                        break;
                }
            }

            csvEntities.Add(booking);
            csvEntities.Add(person);

            return csvEntities;
        }

        /*
         * Appends arr2 at the end of arr1.
         */
        private String[] append(String[] arr1, String[] arr2)
        /*
         * RESOURCES:
         * http://stackoverflow.com/questions/59217/merging-two-arrays-in-net
         * https://msdn.microsoft.com/en-us/library/system.array.copy(v=vs.110).aspx
         */
        {
            int appendFrom = arr1.Length;
            Array.Resize<String>(ref arr1, arr1.Length + arr2.Length);
            Array.Copy(arr2, 0, arr1, appendFrom, arr2.Length);
            return arr1;
        }

        /*
         * Indexes a csv entity (String[]) into a 
         * dictionary<attribute, value>.
         */
        private Dictionary<String, String> index(String[] entity)
        {
            List<String[]> dividedEntity = divide(entity);
            Dictionary<String, String> indexedEntity = null;

            foreach (String[] sArr in dividedEntity)
            {
                switch (sArr[0])
                {
                    case "#BOOKING":
                        indexedEntity = index<BookingField>(sArr);
                        break;
                    case "#BREAKFAST":
                        indexedEntity = join(indexedEntity,
                                             index<BreakfastField>(sArr));
                        break;
                    case "#EVENING_MEAL":
                        indexedEntity = join(indexedEntity, 
                                             index<EveningMealField>(sArr));
                        break;
                    case "#CAR_HIRE":
                        indexedEntity = join(indexedEntity, 
                                             index<CarHireField>(sArr));
                        break;

                    case "#PERSON":
                        indexedEntity = index<PersonField>(sArr);
                        break;
                    case "#CUSTOMER":
                        indexedEntity = join(indexedEntity, 
                                             index<CustomerField>(sArr));
                        break;
                    case "#GUEST":
                        indexedEntity = join(indexedEntity, 
                                             index<GuestField>(sArr));
                        break;

                    default:
                        throw new ArgumentException("Can't index entity, "
                                           + " content of sArr[0] must be"
                                           + " either #BOOKING or #PERSON");
                }
            }
            return indexedEntity;
        }

        /*
         * Divides an entity (String[]) into a List<String[]>, each
         * element of which represents a Component class (from a
         * decorator pattern).
         */
        private List<String[]> divide(String[] entity)
        {
            List<String[]> dividedEntity = new List<String[]>();
            String[] section = new String[0];

            foreach (String s in entity)
            {
                if (s.StartsWith("#"))
                {
                    if (section.Length > 0)
                    {
                        dividedEntity.Add(section);
                    }
                    section = new String[1];
                    section[0] = s;
                }
                else
                {
                    Array.Resize<String>(ref section, section.Length + 1);
                    section[section.Length-1] = s;
                }
            }

            dividedEntity.Add(section);
            return dividedEntity;
        }

        /*
         * Indexes part of an entity's attributes (= section) into a
         * dictionary<attribute, value>.
         */
        private Dictionary<String, String> index<T>(String[] entitySection)
        {
            T[] keysArr = (T[])Enum.GetValues(typeof(T));
            Dictionary<String, String> indexedSection 
                                          = new Dictionary<String, String>();
            foreach (T k in keysArr)
            {
                indexedSection.Add(
                            k.ToString(),
                            entitySection[Array.IndexOf(keysArr, k) + 1]);
            }

            return indexedSection;
        }

        /*
         * Joins two Dictionary<String, String> (Union operation).
         */
        private Dictionary<String, String> 
            join(Dictionary<String, String> d1, Dictionary<String, String> d2)
                /*
                 * RESOURCE:
                 * http://stackoverflow.com/questions/59217/merging-two-arrays-in-net
                 */
        {
            return d1.Union(d2)
                     .ToDictionary(k => k.Key, v => v.Value);
        }
    }
}
