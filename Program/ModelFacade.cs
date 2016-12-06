using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /*
     * Facade providing an interface to the system.
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public class ModelFacade
    {
        // PROPERTIES:

        // points to the BookingFactory instance:
        private BookingFactory bFact;
        public BookingFactory BFact { get { return bFact; } }

        // ponts to the PersonFactory instance:
        private PersonFactory pFact;
        public PersonFactory PFact { get { return pFact; } }

        // points to a DataPersistenceFacade object:
        private DataPersistenceFacade dpFacade;
        public DataPersistenceFacade DPFacade { get { return dpFacade; } }

        // points to the BookingComponent current working instance:
        public BookingComponent CurrentBook { get; set; }

        // points to the PersonComponent current working instance:
        public PersonComponent CurrentCust { get; set; }

        // METHODS:

        /*
         * Constructor.
         */
        public ModelFacade()
        {
            bFact = BookingFactory.Instance;
            pFact = PersonFactory.Instance;
            dpFacade = new DataPersistenceFacade();
        }

        /*
         * Recovers last system state persisted to file.
         */
        public bool RestoreSystemSavedState()
        {
            return false;
        }

        /*
         * Restores system state from file.
         */
        public bool PersistSystemState()
        {
            return dpFacade.PersistSystemState();
        }

        /*
         * Loads the booking matching given booking number into the system
         * (from persisted data).
         * Returns true if the booking was found & loaded successfully,
         * otherwise false.
         */
        public bool RestoreBooking(int bookingNb)
        {
            bool wasRestored = true;
            List<Dictionary<String, String>> bookingData;
            if (!dpFacade.Read(bookingNb, out bookingData))
            {
                wasRestored = false;
            }
            else
            {
                CurrentBook = bFact.Restore(bookingData);
                CurrentCust = CurrentBook.GetCustomer();
            }
            return wasRestored;
        }

        /*
         * Loads the customer matching given customer number into the system
         * (from persisted data).
         * Returns true if the customer was found & loaded successfully,
         * otherwise false.
         */
        public bool RestoreCustomer(int customerNb)
        {
            bool wasRestored = true;
            Dictionary<String, String> customerData;
            if (!dpFacade.Read(customerNb, out customerData))
            {
                wasRestored = false;
            }
            else
            {
                CurrentCust = pFact.RestoreCustomer(customerData);
                /*
                if (CurrentBook != null)
                {
                    foreach (PersonComponent g in CurrentBook.GetGuests())
                    {
                        if (g.IsCustomer())
                        {
                            CurrentBook.GetGuests().Remove(g);
                        }
                    }
                }
                 */
            }
            return wasRestored;
        }

        /*
         * Instanciates a new customer.
         */
        public void CreateCustomer(String name, String address) 
        {
            CurrentCust = pFact.GetNewCustomer(name, address);
        }

        /*
         * Instanciates a new booking for the current customer.
         */
        public void CreateBooking(DateTime arrival, DateTime departure)
        {
            CurrentBook = bFact.GetNewBooking(CurrentCust, arrival, departure);
        }

        /*
         * Persists the current booking of the system.
         * Returns true if the booking was saved successfully, otherwise false.
         */
        public bool PersistCurrentBooking()
        {
            return dpFacade.Persist(CurrentBook);
        }

        /*
         * Adds a new person to current booking's list of guests.
         */
        public void AddGuest(String name, String passportNb, int age)
        {
            CurrentBook.AddGuest(pFact.GetNewGuest(name, passportNb, age));
        }

        /*
         * Adds current customer to current booking's list of guests.
         */
        public void AddCustomerToGuests(String passportNb, int age)
        {
            CurrentBook.AddGuest(pFact.GetNewGuest(CurrentCust, 
                                                   passportNb, 
                                                   age));
        }

        /*
         * Deletes the guest at given index in list of guests for the
         * current booking.
         */
        public void DeleteGuest(int index)
        {
            CurrentBook.GetGuests().RemoveAt(index);
        }
    }
}
