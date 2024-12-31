using AutoMapper;
using Evertrust.Core.Common.AspNetCore.Misc;
using MeetingRoomBooking.Common.Models;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.Models;
using MeetingRoomBooking.Service.DTOs;
using MeetingRoomBooking.Service.Interfaces;

namespace MeetingRoomBooking.Service.Implements
{
    public class BookingService : IBookingService
    {
        private IRoomRepository _roomRepository;
        private IBookingRepository _bookingRepository;
        private IMapper _mapper;

        public BookingService(IRoomRepository roomRepository
            ,IBookingRepository bookingRepository
            ,IMapper mapper)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        /// <summary>
        ///   檢查欲預約時段是否已經被預約
        /// </summary>        
        public async Task<bool> IsBooked(CheckPeriodDto period)
        {
            DateOnly targetDate = period.TargetDay;
            string[] startTime = period.StartTime.Split(':');
            string[] endTime = period.EndTime.Split(':');
            DateTime targetSt = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day, Convert.ToInt32(startTime[0]), Convert.ToInt32(startTime[1]),0);
            DateTime targetEt = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day, Convert.ToInt32(endTime[0]), Convert.ToInt32(endTime[1]), 0);
            int id = period.Id.GetValueOrDefault();

            IEnumerable<MeetingBookingModel> records =  await _bookingRepository.GetBookingsForCheckAsync(targetDate, period.RoomId, id);
                
            if (records == null)
            {
                return false;
            }

            if(records.Any(b=>b.StartTime < targetEt && targetSt < b.EndTime))
            {
                return true;
            }
            
            return false;

            
        }

        /// <summary>
        ///   取得某時間區段的預約紀錄
        /// </summary>
        public async Task<IEnumerable<BookingDto>> GetBookingsByPeriodAsync(PeriodDto period)
        {
            DateTime periodStart = period.StartTime;
            DateTime periodEnd = period.EndTime.AddDays(1);

            IEnumerable<MeetingBookingModel> records = await _bookingRepository.GetAllAsync(periodStart, periodEnd);
            IEnumerable<BookingDto> result = _mapper.Map<IEnumerable<BookingDto>>(records);
            return result;
        }

        /// <summary>
        ///   回傳查詢時間的預約紀錄(API)
        /// </summary>
        public async Task<PagedOutputModel<BookingDto>> GetPagedResultAsync(BookingInputParamater booking)
        {
            SearchQueryCmdGenerator query = new SearchQueryCmdGenerator(booking);

            query.AddSqlCmd();
                        
            int totalCount = await _bookingRepository.GetRecordCountsAsync(query);

            query.AddSortBySqlCmd();

            IEnumerable<BookingViewModel> bookings = await _bookingRepository.SearchAsync(query);
            IEnumerable<BookingDto> result = _mapper.Map<IEnumerable<BookingDto>>(bookings);

            return new PagedOutputModel<BookingDto>()
            {
                PageNumber = booking.PageIndex,
                PageSize = booking.PageSize,
                TotalRecords = totalCount,
                Records = result
            };
        }

        public async Task<IEnumerable<BookingDto>> GetAllAsync(DateTime periodStart, DateTime periodEnd)
        {
            var bookings = await _bookingRepository.GetAllAsync(periodStart, periodEnd);
            var result = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return result;
        }

        public async Task<BookingDto> GetByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            var result = _mapper.Map<BookingDto>(booking);
            return result;
        }

        public async Task<IResult> CreateAsync(BookingDto booking)
        {
            var bookingModel=_mapper.Map<MeetingBookingModel>(booking);
            IResult result = await _bookingRepository.CreateAsync(bookingModel);
            return result;
        }

        public async Task<IResult> UpdateAsync(BookingDto booking)
        {
            var bookingModel = _mapper.Map<MeetingBookingModel>(booking);
            IResult result = await _bookingRepository.UpdateAsync(bookingModel);
            return result;
        }

        public async Task<IResult> DeleteAsync(int Id)
        {            
            IResult result = await _bookingRepository.DeleteAsync(Id);
            return result;
        }
    }
}
