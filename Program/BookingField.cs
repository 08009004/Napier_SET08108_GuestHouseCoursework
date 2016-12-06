using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /* 
     * Enumeration of Booking.cs fields in the order they appear in 
     * Booking.ToCSV() and the persisted file, excluding the fields
     * defined in PersonField.cs, CustomerField.cs & GuestField.cs
     * 
     * author: Pierre Ruiz (matriculation number 08009004)
     * last modified: 2016-12-06
     */
    public enum BookingField { BOOKING_NUMBER, ARRIVAL, DEPARTURE  }
}
