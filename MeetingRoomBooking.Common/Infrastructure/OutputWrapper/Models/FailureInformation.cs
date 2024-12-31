using Newtonsoft.Json;

namespace MeetingRoomBooking.Common.Infrastructure.OutputWrapper.Models
{
    public class FailureInformation
    {
        /// <summary>
        /// The error code.
        /// </summary>
        [JsonProperty(PropertyName = "code", Order = 1)]
        public int ErrorCode { get; set; }

        /// <summary>
        /// The message.
        /// </summary>
        [JsonProperty(PropertyName = "message", Order = 2)]
        public string Message { get; set; }

        /// <summary>
        /// The description.
        /// </summary>
        [JsonProperty(PropertyName = "description", Order = 3)]
        public string Description { get; set; }
        public string Domain { get; set; }
        public string PropertyName { get; set; }
    }
}
