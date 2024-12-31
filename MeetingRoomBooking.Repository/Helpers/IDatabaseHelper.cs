using System.Data;

namespace MeetingRoomBooking.Repository.Helpers
{
    /// <summary>
    /// interface IDatabaseHelper
    /// </summary>
    public interface IDatabaseHelper
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
    }
}
