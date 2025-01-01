using Mapster;
using MapsterMapper;
using MeetingRoomBooking.Web.MappingProfile;

namespace Yungching.Web.Knowhow.Application.Test
{
    public class TestHook
    {
        public static Mapper getConfig()
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MapperConfig).Assembly);
            return new Mapper(config);
        }
    }
}
