using AutoMapper;
using MeetingRoomBooking.Common.Models;
using MeetingRoomBooking.Repository.Models;
using MeetingRoomBooking.Service.DTOs;
using prjMeetingRoomBooking.ViewModels;

namespace prjMeetingRoomBooking.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            this.CreateMap<UserModel, CUserViewModel>();

            this.CreateMap<UserModel, UserDto>();

            this.CreateMap<MeetingRoomModel, MeetingRoomDto>();

            this.CreateMap<MeetingRoomDto, MeetingRoomModel > ();

            this.CreateMap<Manager2RoomDto, Manager2RoomModel>();

            this.CreateMap<Manager2RoomModel, Manager2RoomDto>();

            this.CreateMap<MeetingBookingModel, BookingDto>();

            this.CreateMap<BookingDto, MeetingBookingModel>();

            this.CreateMap<BookingDto, BookingViewModel>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))                
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember
                (
                    dest => dest.StartDate,
                    opt => opt.MapFrom(
                        src => ((DateTime)src.StartTime).ToString("yyyy-MM-dd")
                    )
                )
                .ForMember
                (
                    dest => dest.StartTime,
                    opt => opt.MapFrom(
                        src => ((DateTime)src.StartTime).ToString("HH:mm")
                    )
                )
                .ForMember
                (
                    dest => dest.EndDate,
                    opt => opt.MapFrom(
                        src => ((DateTime)src.EndTime).ToString("yyyy-MM-dd")
                    )
                )
                .ForMember
                (
                    dest => dest.EndTime,
                    opt => opt.MapFrom(
                        src => ((DateTime)src.EndTime).ToString("HH:mm")
                    )
                );

            this.CreateMap<BookingViewModel, BookingDto>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember
                (
                    dest => dest.StartTime,
                    opt => opt.MapFrom(
                        src => Convert.ToDateTime($"{src.StartDate}T{src.StartTime}:00")
                    )
                )
                .ForMember
                (
                    dest => dest.EndTime,
                    opt => opt.MapFrom(
                        src => Convert.ToDateTime($"{src.EndDate}T{src.EndTime}:00")
                    )
                );
        }
    }
}
