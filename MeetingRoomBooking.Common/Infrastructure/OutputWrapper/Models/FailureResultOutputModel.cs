using Newtonsoft.Json;

namespace MeetingRoomBooking.Common.Infrastructure.OutputWrapper.Models
{
    /// <summary>
    /// 執行失敗的 OutputModel.
    /// </summary>
    public class FailureResultOutputModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailureResultOutputModel"/> class.
        /// </summary>
        public FailureResultOutputModel()
        {
            Errors = new List<FailureInformation>();
        }

        /// <summary>
        /// The CorrelationId
        /// </summary>
        [JsonProperty(PropertyName = "id", Order = 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// The Api Version
        /// </summary>
        [JsonProperty(PropertyName = "apiVersion", Order = 2)]
        public string ApiVersion { get; set; }

        /// <summary>
        /// The Method
        /// </summary>
        [JsonProperty(PropertyName = "method", Order = 3)]
        public string Method { get; set; }

        /// <summary>
        /// The status
        /// </summary>
        [JsonProperty(PropertyName = "status", Order = 4)]
        public string Status { get; set; }

        /// <summary>
        /// The errors
        /// </summary>
        [JsonProperty(PropertyName = "errors", Order = 5)]
        public List<FailureInformation> Errors { get; set; }
    }
}
