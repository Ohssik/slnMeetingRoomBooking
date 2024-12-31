using Evertrust.Core.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Runtime.CompilerServices;

namespace MeetingRoomBooking.Common.Infrastructure.Logging
{
    public class CustomLogHelper : ILogHelper
    {
        public void Log(LogLevel logLevel, string message)
        {
            return;
        }

        public void Log(LogLevel logLevel, string categoryName, string message)
        {
            return;
        }

        public void Log(LogLevel logLevel, string message, params string[] tags)
        {
            return;
        }

        public void Log(LogLevel logLevel, string categoryName, string message, params string[] tags)
        {
            return;
        }

        public void Log(LogLevel logLevel, string message, IDictionary properties, params string[] tags)
        {
            return;
        }

        public void Log(LogLevel logLevel, string categoryName, string message, IDictionary properties, params string[] tags)
        {
            return;
        }

        public void SubmitException(Exception exception)
        {
            return;
        }

        public void SubmitException(Exception exception, string message = null, IDictionary<string, object> extendedData = null)
        {
            return;
        }

        public void Write(LogLevel logLevel, LogContent logContent, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            return;
        }

        public void Write(LogLevel logLevel, string loggerName, LogContent logContent, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            return;
        }

        public void WriteException(LogLevel logLevel, Exception exception)
        {
            return;
        }

        public void WriteException(LogLevel logLevel, string categoryName, Exception exception)
        {
            return;
        }

        public void WriteException(LogLevel logLevel, Exception exception, params string[] tags)
        {
            return;
        }

        public void WriteException(LogLevel logLevel, string categoryName, Exception exception, params string[] tags)
        {
            return;
        }

        public void WriteException(LogLevel logLevel, Exception exception, IDictionary properties, params string[] tags)
        {
            return;
        }

        public void WriteException(LogLevel logLevel, string categoryName, Exception exception, IDictionary properties, params string[] tags)
        {
            return;
        }

        public void WriteException(LogLevel logLevel, Exception exception, string message, params string[] tags)
        {
            return;
        }

        public void WriteException(LogLevel logLevel, string categoryName, Exception exception, string message, params string[] tags)
        {
            return;
        }

        public void WriteException(LogLevel logLevel, Exception exception, string message, IDictionary properties, params string[] tags)
        {
            return;
        }

        public void WriteException(LogLevel logLevel, string categoryName, Exception exception, string message, IDictionary properties, params string[] tags)
        {
            return;
        }
    }
}
