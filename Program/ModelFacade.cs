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
     * last modified: 2016-12-09
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

        /*
         * Recovers last system state persisted to file.
         */
        public bool RestoreSystemSavedState()
        {
            return false;
        }

        /*
         * Persists current system state to file.
         */
        public bool PersistSystemState()
        {
            return dpFacade.PersistSystemState();
        }

        /*
         * Returns a list of all the booking numbers persisted on disc.
         */
        public List<int> GetAllBookingNbs()
        {
            return dpFacade.GetAllBookingNbs();
        }

        /*
         * Returns a list of all the customer numbers persisted on disc.
         */
        public List<int> GetAllCustomerNbs()
        {
            List<int> temp = dpFacade.GetAllCustomerNbs();
            return dpFacade.GetAllCustomerNbs();
        }

        /*
         * Returns a list of all the booking numbers persisted on disc made 
         * by a given customer.
         */
        public List<int> GetAllBookingNbs(PersonComponent customer)
        {
            return dpFacade.GetAllBookingNbs(customer.GetCustNb());
        }


        // METHODS RELATED TO CURRENT BOOKING:

        /*
         * True if there is a booking loaded in the system when method is 
         * called, otherwise false.
         */
        public bool IsABookingLoaded()
        {
            return CurrentBook != null;
        }

        /*
         * Instanciates a new booking for the current customer.
         */
        public void CreateBooking(DateTime arrival, DateTime departure)
        {
            CurrentBook = bFact.GetNewBooking(CurrentCust, arrival, departure);
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
         * Returns the current booking's departure and arrival dates.
         */
        public void GetCurrentBookDates(out DateTime arrival, 
                                        out DateTime departure) 
        {
            CurrentBook.GetDates(out arrival, out departure);
        }

        /*
         * Returns a list of the names of all the guests currently booked for 
         * the current booking.
         */
        public List<String> GetGuestNames()
        {
            List<String> guestNames = new List<String>();
            foreach (PersonComponent g in CurrentBook.GetGuests())
            {
                guestNames.Add(g.Name);
            }
            return guestNames;
        }

        /*
         * Returns the number of guests currently booked for the current 
         * booking(also 0 if no booking is currently loaded).
         */
        public int GetCurrentNbGuests()
        {
            int nbGuests = 0;
            if (CurrentBook != null)
            {
                nbGuests = CurrentBook.GetNbGuests();
            }
            return nbGuests;
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
                List<BookingDecorator> extras = GetCurrentExtras();
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
                List<BookingDecorator> extras = GetCurrentExtras();

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
                List<BookingDecorator> extras = GetCurrentExtras();

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
                List<BookingDecorator> extras = GetCurrentExtras();

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

        /*
         * Updates the BookingComponent instance currently loaded in the
         * system.
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
         * Persists the BookingComponent instance currently loaded in the 
         * system to file.
         * Returns true if the booking was saved successfully, otherwise false.
         */
        public bool PersistCurrentBooking()
        {
            return dpFacade.Persist(CurrentBook);
        }

        // METHODS RELATED TO THE CURRENT CUSTOMER:

        /*
         * 
         */
        public bool IsACustomerLoaded()
        {
            return CurrentCust != null;
        }

        /*
         * Instanciates a new customer.
         */
        public void CreateCustomer(String name, String address)
        {
            CurrentCust = pFact.GetNewCustomer(name, address);
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
            }
            return wasRestored;
        }

        /*
         * Returns the current booking's customer number, or -1 if no booking
         * is currently loaded.
         */
        public int GetCurrentCustNb()
        {
            int customerNb = -1;
            if (CurrentCust != null)
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
            if (CurrentCust != null)
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
            if (CurrentCust != null)
            {
                customerAddress = CurrentCust.GetAddress();
            }
            return customerAddress;
        }

        // METHOD RELATED TO CURRENT BOOKING'S GUESTS:

        /*
         * Adds a new person to current booking's list of guests.
         */
        public void AddGuest(String name, String passportNb, int age)
        {
            CurrentBook.AddGuest(pFact.GetNewGuest(name, passportNb, age));
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
         * True if the element at given index in list of guests is a
         * cutomer.
         */
        public bool IsGuestACustomer(int index)
        {
            return CurrentBook.GetGuests().ElementAt(index).IsCustomer();
        }

        /*
         * Adds current customer to current booking's list of guests.
         */
        public void AddCustomerToGuests(String passportNb, int age)
        {
            List<PersonComponent> savedGuests = CurrentBook.GetGuests();
            DateTime arrival;
            DateTime departure;
            CurrentBook.GetDates(out arrival, out departure); 
            
            CurrentCust = pFact.GetNewGuest(CurrentCust, 
                                            passportNb, 
                                            age);
            CurrentBook = bFact.UpdateBooking(CurrentBook.GetBookingNb(),
                                              CurrentCust,
                                              arrival,
                                              departure);

            foreach (PersonComponent g in savedGuests)
            {
                CurrentBook.GetGuests().Add(g);
            }

            CurrentBook.AddGuest(CurrentCust);
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
            // First unwrap guest decorator from current customer if they are
            // the guest being deleted:
            if (CurrentBook.GetGuests().ElementAt(index).IsCustomer())
            {
                List<PersonComponent> savedGuests = CurrentBook.GetGuests();
                DateTime arrival;
                DateTime departure;
                CurrentBook.GetDates(out arrival, out departure);

                CurrentCust = CurrentCust.Undecorate();
                CurrentBook = bFact.UpdateBooking(CurrentBook.GetBookingNb(), 
                                                  CurrentCust, 
                                                  arrival, 
                                                  departure);

                foreach (PersonComponent g in savedGuests)
                {
                    CurrentBook.GetGuests().Add(g);
                }


            }

            // Then delete selected guest reference from guests list:
            CurrentBook.GetGuests().RemoveAt(index);
        }

        // METHODS RELATED TO CURRENT BOOKING'S EXTRAS:

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
         * Updates properties of a given Breakfast.
         */
        public void UpdateBreakfast(BookingDecorator reference, 
                                    String newDietRequirements)
        {
            CurrentBook = CurrentBook.Undecorate(reference);
            CurrentBook = bFact.AddBreakfast(CurrentBook, 
                                             newDietRequirements);
        }

        /*
         * Updates properties of a given EveningMeal.
         */
        public void UpdateEveningMeal(BookingDecorator reference,
                                      String newDietRequirements)
        {
            CurrentBook = CurrentBook.Undecorate(reference);
            CurrentBook = bFact.AddEveningMeal(CurrentBook, 
                                               newDietRequirements);
        }

        /*
         * Updates properties of a given CarHire.
         */
        public void UpdateCarHire(BookingDecorator reference,
                                  String newDriverName,
                                  DateTime newStart,
                                  DateTime newEnd)
        {
            CurrentBook = CurrentBook.Undecorate(reference);
            CurrentBook = bFact.AddCarHire(CurrentBook, 
                                           newDriverName, 
                                           newStart, 
                                           newEnd);
        }

        /*
         * Returns a list of references to each of the CurrentBooking's 
         * decorators (= extras), or null if it is not decorated at all.
         */
        public List<BookingDecorator> GetCurrentExtras()
        {

            List<BookingDecorator> references;
            CurrentBook.Unwrap(out references);
            return references;
        }

        /*
         * Returns a list of textual representations of each of the 
         * CurrentBooking's decorators (= extra).
         */
        public List<String> GetCurrentExtrasNames()
        {

            List<BookingDecorator> references = GetCurrentExtras();
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
            List<BookingDecorator> references = GetCurrentExtras();
            if (references != null)
            {
                CurrentBook = CurrentBook.Undecorate(references.ElementAt(index));
            }
        }
    }
}
