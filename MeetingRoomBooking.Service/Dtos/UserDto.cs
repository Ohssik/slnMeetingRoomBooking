namespace MeetingRoomBooking.Service.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; } = null!;
        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        /// <value>
        /// The user password.
        /// </value>
        public string UserPwd { get; set; } = null!;
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string? UserName { get; set; }
    }
}
