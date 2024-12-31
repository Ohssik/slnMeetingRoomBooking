using AutoMapper;
using MeetingRoomBooking.Common.Models;
using MeetingRoomBooking.Repository.Models;
using MeetingRoomBooking.Service.DTOs;

namespace MeetingRoomBooking.WebAPI.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {           
            this.CreateMap<MeetingBookingModel, BookingDto>();

            this.CreateMap<BookingViewModel, BookingDto>()
                .ForMember(dest=>dest.RoomId,opt=>opt.MapFrom(s=>s.RoomId))
                .ForMember(dest=>dest.Id,opt=>opt.MapFrom(s=>s.Id))
                .ForMember(dest=>dest.BookingUserId, opt=>opt.MapFrom(s=>s.BookingUserId))
                .ForMember(dest=>dest.Subject, opt=>opt.MapFrom(s=>s.Subject))
                .ForMember(
                    dest=>dest.StartTime, 
                    opt=>opt.MapFrom(s=>$"{s.StartDate} {s.StartTime.Substring(0,2)}:{s.StartTime.Substring(2, 2)}:00")
                )
                .ForMember(
                    dest => dest.EndTime,
                    opt => opt.MapFrom(s => $"{s.EndDate} {s.EndTime.Substring(0, 2)}:{s.EndTime.Substring(2, 2)}:00")
                )
                ;

        }
    }
}
