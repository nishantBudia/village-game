using System;

namespace VillageGame.Core.Interfaces
{
    /// <summary>
    /// Provides an abstraction for logging services.
    /// This interface can be used for both system logging and user behavior tracking.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Initializes the logger service
        /// </summary>
        /// <returns>True if initialization was successful</returns>
        bool Initialize();
        
        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">Optional format arguments</param>
        void LogDebug(string message, params object[] args);
        
        /// <summary>
        /// Logs an informational message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">Optional format arguments</param>
        void LogInformation(string message, params object[] args);
        
        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">Optional format arguments</param>
        void LogWarning(string message, params object[] args);
        
        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">Optional format arguments</param>
        void LogError(string message, params object[] args);
        
        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="message">Optional additional message</param>
        void LogException(Exception exception, string message = null);
        
        /// <summary>
        /// Logs a user event for analytics purposes
        /// </summary>
        /// <param name="eventName">The name of the event</param>
        /// <param name="properties">Optional event properties</param>
        void LogUserEvent(string eventName, object properties = null);
    }
} 