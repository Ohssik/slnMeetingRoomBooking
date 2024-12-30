using MapsterMapper;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.ParameterModels;
using MeetingRoomBooking.Service.Interfaces;
using MeetingRoomBooking.Service.ParameterDtos;

namespace MeetingRoomBooking.Service.Implements
{
    public class BookingService: IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task AddBookingAsync(AddBookingParameterDto parameter)
        {
            await _bookingRepository.AddBookingAsync(_mapper.Map<AddBookingParameterModel>(parameter));
        }
    }
}
