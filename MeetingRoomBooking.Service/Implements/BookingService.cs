using MapsterMapper;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.ParameterModels;
using MeetingRoomBooking.Service.Dtos;
using MeetingRoomBooking.Service.Interfaces;
using MeetingRoomBooking.Service.ParameterDtos;
using System.Reflection.Metadata;

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

        public async Task<BookingDto> GetBooking(GetBookingParameterDto parameter)
        {
            var result = await _bookingRepository.GetBookingById(_mapper.Map<GetBookingParameterModel>(parameter));
            return _mapper.Map<BookingDto>(result);
        }

        public async Task ModifyBookingAsync(ModifyBookingParameterDto parameter)
        {
            await _bookingRepository.ModifyBookingAsync(_mapper.Map<ModifyBookingParameterModel>(parameter));
        }

        public async Task RemoveBookingAsync(int id)
        {
            await _bookingRepository.RemoveBookingAsync(id);
        }
    }
}
