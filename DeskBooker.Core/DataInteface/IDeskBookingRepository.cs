using DeskBooker.Domain;

namespace DeskBooker.Core.DataInteface
{
    public interface IDeskBookingRepository
    {
        void Save(DeskBooking deskBooking);
    }
}
