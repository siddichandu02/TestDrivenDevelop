using DeskBooker.Core.DataInteface;
using DeskBooker.Core.Domain;
using DeskBooker.Domain;
using System.Linq;

namespace DeskBooker.Processor
{
    public class DeskBookngRequestProcessor
    {
        private IDeskBookingRepository _deskBookingRepository;
        private IDeskRepository _deskRepository;
        public DeskBookngRequestProcessor(IDeskBookingRepository deskBookingRepository, IDeskRepository deskRepository)
        {
            _deskBookingRepository = deskBookingRepository;
            _deskRepository = deskRepository;
        }

        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null)
                throw new System.ArgumentNullException(nameof(request));
            var result = Create<DeskBookingResult>(request);
            var availableDesks = _deskRepository.GetAvailableDesks(request.Date);
            if (availableDesks.FirstOrDefault() is Desk availableDesk)
            {
              
                var deskBooking = Create<DeskBooking>(request);
                deskBooking.DeskId = availableDesk.Id;
                _deskBookingRepository.Save(deskBooking);
                result.DeskBookingId = deskBooking.Id;
                result.Code = DeskBookingResultCode.Success;
            }
            else
            {
                result.Code = DeskBookingResultCode.NoDeskAvailable;
            }
            return result;
        }

        private static T Create<T>(DeskBookingRequest request) where T:DeskBookingBase,new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date,

            };
        }
    }
}