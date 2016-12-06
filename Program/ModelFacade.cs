using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class ModelFacade
    {
        // PROPERTIES:

        // points to the BookingFactory instance:
        private BookingFactory bFact;

        // ponts to the PersonFactory instance:
        private PersonFactory pFact;

        // points to a DataPersistenceFacade object:
        private DataPersistenceFacade dpFacade;

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
    }
}
