using System;
using System.Collections.Generic;

namespace MeetingRoomBooking.Repository.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Manager2RoomModel
    {
        /// <summary>
        /// Gets or sets the name of the manager.
        /// </summary>
        /// <value>
        /// The name of the manager.
        /// </value>
        public string? ManagerName { get; set; }
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
