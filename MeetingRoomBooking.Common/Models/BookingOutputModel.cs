namespace MeetingRoomBooking.Common.Models
{
    /// <summary>
    /// class BookingOutputModel
    /// </summary>
    public class BookingOutputModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingOutputModel"/> class.
        /// </summary>
        public BookingOutputModel()
        {
            this.Success = false;
            this.Result = string.Empty;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BookingOutputModel"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// The result.
        /// </summary>
        public string Result { get; set; }
    }
}
