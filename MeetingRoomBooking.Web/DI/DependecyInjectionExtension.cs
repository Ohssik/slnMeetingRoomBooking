using MapsterMapper;
using MeetingRoomBooking.Repository.Implements;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Service.Implements;
using MeetingRoomBooking.Service.Interfaces;

namespace MeetingRoomBooking.Web.DI
{
    public static class DependecyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IDatabaseHelper>(x => new DatabaseHelper
            //    (configuration.GetConnectionString("iProcurement")));

            services.AddScoped<IMapper, ServiceMapper>();

            #region Service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBookingService, BookingService>();
            #endregion

            #region Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            #endregion

        }
    }
}
