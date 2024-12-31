using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Service.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class MeetingRoomDto
    {
        /// <summary>
        /// Gets or sets the room identifier.
        /// </summary>
        /// <value>
        /// The room identifier.
        /// </value>
        public string RoomId { get; set; } = null!;
        /// <summary>
        /// Gets or sets the name of the room.
        /// </summary>
        /// <value>
        /// The name of the room.
        /// </value>
        public string? RoomName { get; set; }
        /// <summary>
        /// Gets or sets the manager identifier.
        /// </summary>
        /// <value>
        /// The manager identifier.
        /// </value>
        public string? ManagerId { get; set; }
        /// <summary>
        /// Gets or sets the room size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public string? Size { get; set; }
    }
}
