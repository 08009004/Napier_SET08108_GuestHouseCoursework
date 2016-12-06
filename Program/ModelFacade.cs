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
         * Loads the booking matching given booking number from persisted data
         * into the system.
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
    }
}
