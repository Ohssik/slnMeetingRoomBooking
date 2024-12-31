using Evertrust.Core.Logging.Abstractions;

namespace MeetingRoomBooking.Common.Infrastructure.Logging
{
    public class CustomLogHelperFactory : ILogHelperFactory
    {
        public ILogHelper Create<T>() where T : class
        {
            return new CustomLogHelper();
        }

        public ILogHelper Create(Type type)
        {
            return new CustomLogHelper();

        }

        public ILogHelper Create(string loggerName)
        {
            return new CustomLogHelper();
        }
    }
}
