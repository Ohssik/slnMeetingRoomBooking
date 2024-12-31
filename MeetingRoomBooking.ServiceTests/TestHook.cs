using AutoMapper;
using Castle.Core.Configuration;
using prjMeetingRoomBooking.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.ServiceTests
{
    public class TestHook
    {
        private static IConfigurationProvider _mapperConfigrationProvider;

        internal static IConfigurationProvider MapperConfigurationProvider
        {
            get
            {
                if(_mapperConfigrationProvider == null)
                {
                    _mapperConfigrationProvider = new MapperConfiguration(option =>
                    {
                        option.AddProfile<MappingProfile>();
                    });
                }

                return _mapperConfigrationProvider;
            }
        }
    }
}
