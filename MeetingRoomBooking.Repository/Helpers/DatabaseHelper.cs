using CoreProfiler;
using CoreProfiler.Data;
using System.Data;
using System.Data.SqlClient;

namespace MeetingRoomBooking.Repository.Helpers
{
    /// <summary>
    /// class DatabaseHelper
    /// </summary>
    /// <seealso cref="MeetingRoomBooking.Repository.Helpers.IDatabaseHelper" />
    public class DatabaseHelper:IDatabaseHelper
    {
        private readonly string _connectionString;
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseHelper"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DatabaseHelper(string connectionString)
        {
            this._connectionString = connectionString;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            var connection = new ProfiledDbConnection
            (
                new SqlConnection(this._connectionString),
                ()=>ProfilingSession.Current==null
                ?null
                :new DbProfiler(ProfilingSession.Current.Profiler)
            );
               
            return connection;
        }
    }
}
