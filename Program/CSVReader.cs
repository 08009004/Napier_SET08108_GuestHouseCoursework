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
     * Static utility class, reads system objects data from CSV files.
     */
    class CSVReader
    {
        /*
         * Returns a Dictionary<string, string> representing a BookingComponent
         * as <attribute, value> (the dictonary keys follow the naming 
         * implemented in the *Field.cs enumerations).
         */
        public static List<Dictionary<String, String>> ReadBooking(String filename)
        {
            List<String> csvLines = readLines(filename);
            List<String[]> extractedEntities = extractClasses(csvLines);
            List<Dictionary<String, String>> indexedEntities = new List<Dictionary<String, String>>();

            foreach (String[] sArr in extractedEntities)
            {
                indexedEntities.Add(indexEntity(sArr));
            }
            
            return indexedEntities;
        }

        /*
         * Reads from a CSV file and returns a list of strings, each 
         * corresponding to a line from the file.
         * 
         * Throws ArgumentException if the number of lines in the file was not even,
         * as per the CSV formating done within classes.
         */
        // resource: https://msdn.microsoft.com/en-us/library/db5x7c0d(v=vs.110).aspx
        private static List<String> readLines(String filename)
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
                throw new ArgumentException("Invalid CSV file: should contain an"
                                            + " even number of text lines");
            }

            return csvLines;
        }

        /*
         * Extracts the different entities from a csv BookingComponent, 
         * each representing the data for a different class of that
         * BookingComponent.
         */
        private static List<String[]> extractClasses(List<String> csvBooking) 
        {
            List<String[]> csvEntities = new List<String[]>();
            String[] bookingComponent = null;
            String[] personComponent = null;
            int storeLength;
            String[] values;
            int i = 0;

            while (i < csvBooking.Count)
            {
                if (csvBooking.ElementAt(i).Equals("#BOOKING"))
                {
                    if (personComponent != null)
                    {
                        csvEntities.Add(personComponent);
                        personComponent = null;
                    }
                    bookingComponent = new String[1];
                    bookingComponent[0] = csvBooking.ElementAt(i);
                    values = csvBooking.ElementAt(i + 1).Split(',');
                    storeLength = bookingComponent.Length;
                    Array.Resize<String>(ref bookingComponent, bookingComponent.Length + values.Length);
                    Array.Copy(values, 0, bookingComponent, storeLength, values.Length);
                }
                else if (csvBooking.ElementAt(i).Equals("#PERSON"))
                {
                    if (personComponent != null)
                    {
                        csvEntities.Add(personComponent);
                        personComponent = null;
                    }
                    personComponent = new String[1];
                    personComponent[0] = csvBooking.ElementAt(i);
                    values = csvBooking.ElementAt(i + 1).Split(',');
                    storeLength = personComponent.Length;
                    Array.Resize<String>(ref personComponent, personComponent.Length + values.Length);
                    Array.Copy(values, 0, personComponent, storeLength, values.Length);
                }
                else if (csvBooking.ElementAt(i).Equals("#CUSTOMER")
                      || csvBooking.ElementAt(i).Equals("#GUEST"))
                {
                    Array.Resize<String>(ref personComponent, personComponent.Length + 1);
                    personComponent[personComponent.Length - 1] = csvBooking.ElementAt(i);
                    values = csvBooking.ElementAt(i + 1).Split(',');
                    storeLength = personComponent.Length;
                    Array.Resize<String>(ref personComponent, personComponent.Length + values.Length);
                    Array.Copy(values, 0, personComponent, storeLength, values.Length);
                }
                else if (csvBooking.ElementAt(i).Equals("#BREAKFAST")
                      || csvBooking.ElementAt(i).Equals("#EVENING_MEAL")
                      || csvBooking.ElementAt(i).Equals("#CAR_HIRE")) 
                {
                    Array.Resize<String>(ref bookingComponent, bookingComponent.Length + 1);
                    bookingComponent[bookingComponent.Length - 1] = csvBooking.ElementAt(i);
                    values = csvBooking.ElementAt(i + 1).Split(',');
                    storeLength = bookingComponent.Length;
                    Array.Resize<String>(ref bookingComponent, bookingComponent.Length + values.Length);
                    Array.Copy(values, 0, bookingComponent, storeLength, values.Length);
                }

                i = i + 2;
            }

            csvEntities.Add(bookingComponent);
            csvEntities.Add(personComponent);

            return csvEntities;
        }

        /*
         * Indexes a csv entity (String[]) into a dictionary<attribute, value> 
         */
        private static Dictionary<String, String> indexEntity(String[] entity)
        {
            List<String[]> dividedEntity = divideEntity(entity);
            Dictionary<String, String> indexedEntity = new Dictionary<String, String>();

            foreach (String[] sArr in dividedEntity)
            {
                switch (sArr[0])
                {
                    case "#BOOKING":
                        indexedEntity = index<BookingField>(sArr);
                        break;
                    case "#BREAKFAST":
                        indexedEntity = indexedEntity.Union(index<BreakfastField>(sArr))
                                                     .ToDictionary(k => k.Key, v => v.Value);
                        /*
                         * REFERENCE:
                         * http://stackoverflow.com/questions/59217/merging-two-arrays-in-net
                         */
                        break;
                    case "#EVENING_MEAL":
                        indexedEntity = indexedEntity.Union(index<EveningMealField>(sArr))
                                                     .ToDictionary(k => k.Key, v => v.Value);
                        break;
                    case "#CAR_HIRE":
                        indexedEntity = indexedEntity.Union(index<CarHireField>(sArr))
                                                     .ToDictionary(k => k.Key, v => v.Value);
                        break;
                    case "#PERSON":
                        indexedEntity = index<PersonField>(sArr);
                        break;
                    case "#CUSTOMER":
                        indexedEntity = indexedEntity.Union(index<CustomerField>(sArr))
                                                     .ToDictionary(k => k.Key, v => v.Value);
                        break;
                    case "#GUEST":
                        indexedEntity = indexedEntity.Union(index<GuestField>(sArr))
                                                     .ToDictionary(k => k.Key, v => v.Value);
                        break;

                    default:
                        throw new ArgumentException("Can't index entity, "
                                           + " content of sArr[0] must be"
                                           + " either #BOOKING or #PERSON");
                }
            }

            return indexedEntity;
        }

        private static List<String[]> divideEntity(String[] entity)
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

        private static Dictionary<String, String> index<T>(String[] entitySection)
        {
            Dictionary<String, String> indexedSection = new Dictionary<String, String>();
            T[] keysArr = (T[])Enum.GetValues(typeof(T));

            foreach (T k in keysArr)
            {
                indexedSection.Add(k.ToString(),
                                   entitySection[Array.IndexOf(keysArr, k) + 1]);
            }

            return indexedSection;
        }

        /*
         * Indexes a csv BookingComponent instance (string) into a 
         * Dictionary<attribute, value> representing the
         * BookingComponent instance.
         */
        private static Dictionary<String, Object> indexBooking(Dictionary<String, String> entities)
        {
            Dictionary<String, Object> indexedInstance 
                                        = new Dictionary<String, Object>();
            String entity;
           // String[] seperatedInstance = csvInstance.Split(',');

            foreach (String k in entities.Keys)
            {
                switch (k)
                {
                    case "#BOOKING":
                        if (entities.TryGetValue(k, out entity))
                        {
                            indexedInstance = index<BookingField>(entity);
                        }
                        /*
                         * RESOURCE:
                         * http://stackoverflow.com/questions/10559367/combine-multiple-dictionaries-into-a-single-dictionary
                         */
                        break;
                        /*
                    case "#BREAKFAST":
                        indexedInstance = index<BreakfastField>(csvInstance);
                        break;
                    case "#EVENING_MEAL":
                        indexedInstance = index<EveningMealField>(csvInstance);
                        break;
                    case "#CAR_HIRE":
                        indexedInstance = index<CarHireField>(csvInstance);
                        break;
                    case "#PERSON":
                        indexedInstance = index<PersonField>(csvInstance);
                        break;
                    case "#CUSTOMER":
                        indexedInstance = index<CustomerField>(csvInstance);
                        break;
                    case "#GUEST":
                        indexedInstance = index<GuestField>(csvInstance);
                        break;
                         */
                    default:
                        break;
                }
            
            }
            

            return indexedInstance;
        }

        private static Dictionary<String, Object> index<BookingField>(String csvEntity)
        {
            Dictionary<String, Object> indexedEntity
                                        = new Dictionary<String, Object>();

            String[] seperatedValues = csvEntity.Split(',');
            BookingField[] keysArray = (BookingField[])Enum.GetValues(typeof(BookingField));

            if (keysArray.Length != seperatedValues.Length)
            {
                throw new ArgumentException("invalid data");
            }


            for (int i = 0; i < keysArray.Length; i++ )
            {
                indexedEntity.Add(
                    keysArray[i].ToString(),
                    seperatedValues[i]);
            }

            return indexedEntity;
        }
    }
}
