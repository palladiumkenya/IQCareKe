using System;

namespace Application.Logger
{
   public interface IEventLogger
    {
        /// <summary>
        /// Logs the debug.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogDebug(string message);
        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="ex">The executable.</param>
        void LogError(Exception ex);
        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="pageUrl">The page URL.</param>
        /// <param name="message">The message.</param>
        /// <param name="exceptionStackTrace">The exception stack trace.</param>
        void LogError(string pageUrl, string message, string exceptionStackTrace);
        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogInfo(string message);

    }
}
