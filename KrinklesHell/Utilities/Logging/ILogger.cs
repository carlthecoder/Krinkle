using System;

namespace Utilities.Logging
{
    public interface ILogger
    {
        /// <summary>
        /// Logs a message directly, without formatting.
        /// </summary>
        /// <param name="message"></param>
        void LogMessage(string message);

        /// <summary>
        /// Formats and logs an error message.
        /// </summary>
        /// <param name="fatalrMessage"></param>
        /// <param name="exception">exception is optional</param>
        void Fatal(string fatalMessage, Exception exception = null);

        /// <summary>
        /// Formats and logs an error message.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="exception">exception is optional</param>
        void Error(string errorMessage, Exception exception = null);

        /// <summary>
        /// Formats and logs an error message.
        /// </summary>
        /// <param name="warningMessage"></param>
        /// <param name="exception">exception is optional</param>
        void Warn(string warningMessage, Exception exception = null);

        /// <summary>
        /// Formats and logs an error message.
        /// </summary>
        /// <param name="infoMessage"></param>
        /// <param name="exception">exception is optional</param>
        void Info(string infoMessage, Exception exception = null);

        /// <summary>
        /// Formats and logs an error message.
        /// </summary>
        /// <param name="traceMessage"></param>
        /// <param name="exception">exception is optional</param>
        void Trace(string traceMessage, Exception exception = null);
    }
}