namespace MeetingRoomBooking.Common.Infrastructure.OutputWrapper.Models
{
    public class RequestValidateResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestValidateResult"/> class.
        /// </summary>
        public RequestValidateResult()
        {
            this.Success = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestValidateResult"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        public RequestValidateResult(bool success = false)
        {
            this.Success = success;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestValidateResult"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public RequestValidateResult(bool success = false,
                                     string parameterName = "",
                                     string parameterValue = "")
            : this(success)
        {
            this.ParameterName = parameterName;
            this.ParameterValue = parameterValue;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RequestValidateResult"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }

        /// <summary>
        /// ParameterName.
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// ParameterValue.
        /// </summary>
        public object ParameterValue { get; set; }

        /// <summary>
        /// ErrorMessage.
        /// </summary>
        public ErrorMessage Error { get; set; }
    }
}
