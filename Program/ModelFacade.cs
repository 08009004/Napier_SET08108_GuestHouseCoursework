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
     * last modified: 2016-12-08
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

            Dictionary<String, String> sysData;
            if (dpFacade.ReadSystemState(out sysData)) 
            {
                bFact.Restore(sysData);
                pFact.Restore(sysData);
            }
        }

        // METHODS RELATED TO CURRENT BOOKING:

        /*
         * Returns the current booking's booking number, or -1 if no booking
         * is currently loaded.
         */
        public int GetCurrentBookNb()
        {
            int currentBookingNb = -1;
            if (CurrentBook != null)
            {
                currentBookingNb = CurrentBook.GetBookingNb();
            }
            return currentBookingNb;
        }

        /*
         * Returns the current booking's number of nights, or -1 if no 
         * booking is currently loaded.
         */
        public int GetCurrentNbNights()
        {
            int nbNights = -1;
            if (CurrentBook != null)
            {
                List<BookingDecorator> extras;
                nbNights = CurrentBook.Unwrap(out extras).GetNbNights();
            }
            return nbNights;
        }

        /*
         * Returns the current booking's cost per night, or -1 if no booking
         * is currently loaded.
         */
        public float GetCurrentCostPerNight()
        {
            float costPerNight = -1;
            if (CurrentBook != null)
            {
                List<BookingDecorator> extras;
                costPerNight = CurrentBook.Unwrap(out extras).GetCostPerNight();
            }
            return costPerNight;
        }

        /*
         * Returns the current booking's cost for, breakfasts or -1 if no 
         * booking is currently loaded.
         */
        public float GetCurrentBreakfastCost()
        {
            float breakfastsCost = -1;

            if (CurrentBook != null)
            {
                List<BookingDecorator> extras;
                CurrentBook.Unwrap(out extras);

                breakfastsCost = 0;

                if (extras != null)
                {
                    foreach (BookingDecorator e in extras)
                    {
                        if (e.GetType() == typeof(Breakfast))
                        {
                            breakfastsCost += e.GetCost();
                        }
                    }
                }
            }
            return breakfastsCost;
        }

        /*
         * Returns the current booking's cost for, evening meals or -1 if no 
         * booking is currently loaded.
         */
        public float GetCurrentEveningMealsCost()
        {
            float eveningMealsCost = -1;

            if (CurrentBook != null)
            {
                List<BookingDecorator> extras;
                CurrentBook.Unwrap(out extras);

                eveningMealsCost = 0;

                if (extras != null)
                {
                    foreach (BookingDecorator e in extras)
                    {
                        if (e.GetType() == typeof(EveningMeal))
                        {
                            eveningMealsCost += e.GetCost();
                        }
                    }
                }
            }
            return eveningMealsCost;
        }

        /*
         * Returns the current booking's cost for, evening meals or -1 if no 
         * booking is currently loaded.
         */
        public float GetCurrentCarHireCost()
        {
            float carHiresCost = -1;

            if (CurrentBook != null)
            {
                List<BookingDecorator> extras;
                CurrentBook.Unwrap(out extras);

                carHiresCost = 0;

                if (extras != null)
                {
                    foreach (BookingDecorator e in extras)
                    {
                        if (e.GetType() == typeof(CarHire))
                        {
                            carHiresCost += e.GetCost();
                        }
                    }
                }
            }
            return carHiresCost;
        }

        // METHODS RELATED TO THE CURRENT CUSTOMER:

        /*
         * Returns the current booking's customer number, or -1 if no booking
         * is currently loaded.
         */
        public int GetCurrentCustNb()
        {
            int customerNb = -1;
            if (CurrentBook != null)
            {
                customerNb = CurrentCust.GetCustNb();
            }
            return customerNb;
        }

        /*
         * Returns the current booking's customer name, or null if no booking
         * is currently loaded.
         */
        public String GetCurrentCustName()
        {
            String customerName = null;
            if (CurrentBook != null)
            {
                customerName = CurrentCust.Name;
            }
            return customerName;
        }

        /*
         * Returns the current booking's customer address, or null if no 
         * booking is currently loaded.
         */
        public String GetCurrentCustAdress()
        {
            String customerAddress = null;
            if (CurrentBook != null)
            {
                customerAddress = CurrentCust.GetAddress();
            }
            return customerAddress;
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
         * 
         */
        public void UpdateBooking(DateTime arrival, DateTime departure)
        {
            List<PersonComponent> savedGuests = CurrentBook.GetGuests();
            List<BookingDecorator> decorationStack;
            BookingComponent booking = CurrentBook.Unwrap(out decorationStack);

            booking = bFact.UpdateBooking(booking.GetBookingNb(), 
                                          CurrentCust, 
                                          arrival, 
                                          departure);

            if (decorationStack != null)
            {
                foreach (BookingDecorator reference in decorationStack)
                {
                    reference.DecoratedComponent = booking;
                    booking = reference;
                }
            }

            CurrentBook = booking;

            foreach (PersonComponent g in savedGuests)
            {
                CurrentBook.AddGuest(g);
            }
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
         * Updates details of guest at given index in current booking's
         * list of guests.
         */
        public void EditGuest(int index, 
                               String name, 
                               String passportNb, 
                               int age)
        {
            CurrentBook.GetGuests().RemoveAt(index);
            CurrentBook.GetGuests().Insert(index, 
                                           pFact.GetNewGuest(name,
                                                             passportNb,
                                                             age));
        }

        /*
         * Updates guest details of current customer at given index in current 
         * booking's list of guests.
         */
        public void EditCustomerGuestDetails(int index, 
                                             String passportNb,
                                             int age)
        {
            CurrentBook.GetGuests().RemoveAt(index);
            CurrentBook.GetGuests().Insert(index,
                                           pFact.GetNewGuest(CurrentCust.Undecorate(),
                                                             passportNb,
                                                             age));
        }

        /*
         * Deletes the guest at given index in list of guests for the
         * current booking.
         * Undecorates that guest if if it is also a 
         * customer and updates CurrentCustomer with the correct
         * memory reference.
         */
        public void DeleteGuest(int index)
        {
            if (CurrentBook.GetGuests().ElementAt(index).IsCustomer())
            {
                CurrentCust = CurrentCust.Undecorate();
            }
            CurrentBook.GetGuests().RemoveAt(index);
        }

        /*
         * Returns a list of all the booking numbers in the system.
         */
        public List<int> GetAllBookingNbs()
        {
            return dpFacade.GetAllBookingNbs();
        }

        /*
         * Returns a list of all the booking numbers of bookings made 
         * by a given customer.
         */
        public List<int> GetAllBookingNbs(PersonComponent customer)
        {
            return dpFacade.GetAllBookingNbs(customer.GetCustNb());
        }

        /*
         * Decorates the current booking with a Breakfast extra.
         */
        public void AddBreakFast(String dietRequirements)
        {
            CurrentBook = bFact.AddBreakfast(CurrentBook, dietRequirements);
        }

        /*
         * Decorates the current booking with an EveningMeal extra.
         */
        public void AddEveningMeal(String dietRequirements)
        {
            CurrentBook = bFact.AddEveningMeal(CurrentBook, dietRequirements);
        }

        /*
         * Decorates the current booking with a CarHire extra.
         */
        public void AddCarHire(String driverName, 
                               DateTime start, 
                               DateTime end)
        {
            CurrentBook = bFact.AddCarHire(CurrentBook, 
                                           driverName, 
                                           start, 
                                           end);
        }

        /*
         * Returns a list of textual representations of each of the 
         * CurrentBooking's decorators (= extra).
         */
        public List<String> GetCurrentExtras()
        {

            List<BookingDecorator> references;
            CurrentBook.Unwrap(out references);
            List<String> extras = new List<String>();
            if (references != null)
            {
                foreach (BookingDecorator bd in references)
                {
                    if (bd.GetType() == typeof(EveningMeal))
                    {
                        extras.Add("Evening meal");
                    }
                    else if (bd.GetType() == typeof(Breakfast))
                    {
                        extras.Add("Breakfast");
                    }
                    else if (bd.GetType() == typeof(CarHire))
                    {
                        extras.Add("CarHire");
                    }
                }
            }
            return  extras;
        }

        /*
         * Removes the selected extra from the booking.
         */
        public void RemoveExtra(int index) 
        {
            List<BookingDecorator> references;
            CurrentBook.Unwrap(out references);
            //CurrentBook = CurrentBook.Unwrap(out references);
            if (references != null)
            {
                CurrentBook = CurrentBook.Undecorate(references.ElementAt(index));
            }
        }
    }
}
