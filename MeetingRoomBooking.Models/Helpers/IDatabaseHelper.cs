using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
