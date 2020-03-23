using System.Collections.Generic;

namespace DeskBooker.Domain
{
    public class DeskBooking : DeskBookingBase
    {
        public int DeskId { get; set; }
        public int Id { get; set; }
    }
}