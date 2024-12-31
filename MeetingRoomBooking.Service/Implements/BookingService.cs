using MapsterMapper;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.ParameterModels;
using MeetingRoomBooking.Service.Dtos;
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

        public async Task<BookingDto> GetBookingAsync(GetBookingParameterDto parameter)
        {
            var result = await _bookingRepository.GetBookingAsync(_mapper.Map<GetBookingParameterModel>(parameter));
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

        public async Task<bool> IsBooked(GetBookingParameterDto parameter)
        {
            var targetDate = parameter.FirstDay.GetValueOrDefault();
            var startTime = parameter.StartTime.Split(':');
            var endTime = parameter.EndTime.Split(':');

            var targetSt = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day, Convert.ToInt32(startTime[0]), Convert.ToInt32(startTime[1]),0);
            var targetEt = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day, Convert.ToInt32(endTime[0]), Convert.ToInt32(endTime[1]), 0);
            var id = parameter.Id;

            var records =  await _bookingRepository.GetBookingsForCheckAsync(_mapper.Map<GetBookingParameterModel>(parameter));
           
            if(records.Any(b=>b.StartTime < targetEt && targetSt < b.EndTime))
            {
                return true;
            }
            
            return false;
        }

        public async Task<IEnumerable<BookingDto>> GetBookingListAsync(GetBookingParameterDto parameter)
        {
            var result = await _bookingRepository.GetBookingListAsync(_mapper.Map<GetBookingParameterModel>(parameter));
            return _mapper.Map<IEnumerable<BookingDto>>(result);
        }
    }
}
