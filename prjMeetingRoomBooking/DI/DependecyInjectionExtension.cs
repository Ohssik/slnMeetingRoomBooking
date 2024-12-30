using MapsterMapper;
using MeetingRoomBooking.Repository.Implements;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Service.Implements;
using MeetingRoomBooking.Service.Interfaces;

namespace prjMeetingRoomBooking.DI
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
            #endregion

            #region Repository
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

        }
    }
}
