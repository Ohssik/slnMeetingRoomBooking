using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var connection = new SqlConnection(this._connectionString);
            return connection;
        }
    }
}
