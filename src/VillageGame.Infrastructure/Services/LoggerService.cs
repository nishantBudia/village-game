using System;
using System.Collections.Generic;
using System.IO;
using Serilog;
using Serilog.Events;
using VillageGame.Core.Interfaces;

namespace VillageGame.Infrastructure.Services
{
    /// <summary>
    /// Implements the ILoggerService interface to provide logging capabilities.
    /// This implementation uses Serilog for system logging and a custom mechanism for user events.
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private Serilog.Core.Logger _logger;
        private List<UserEvent> _userEvents;
        private readonly string _logDirectory = "Logs";
        private readonly string _logFileName = "village_game.log";
        
        /// <summary>
        /// Initializes a new instance of the LoggerService class
        /// </summary>
        public LoggerService()
        {
            _userEvents = new List<UserEvent>();
        }
        
        /// <summary>
        /// Initializes the logger service
        /// </summary>
        /// <returns>True if initialization was successful</returns>
        public bool Initialize()
        {
            try
            {
                // Create log directory if it doesn't exist
                if (!Directory.Exists(_logDirectory))
                {
                    Directory.CreateDirectory(_logDirectory);
                }
                
                // Configure Serilog
                _logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(Path.Combine(_logDirectory, _logFileName), 
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
                    .CreateLogger();
                
                _logger.Information("Logger initialized");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize logger: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">Optional format arguments</param>
        public void LogDebug(string message, params object[] args)
        {
            _logger?.Debug(message, args);
        }
        
        /// <summary>
        /// Logs an informational message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">Optional format arguments</param>
        public void LogInformation(string message, params object[] args)
        {
            _logger?.Information(message, args);
        }
        
        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">Optional format arguments</param>
        public void LogWarning(string message, params object[] args)
        {
            _logger?.Warning(message, args);
        }
        
        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">Optional format arguments</param>
        public void LogError(string message, params object[] args)
        {
            _logger?.Error(message, args);
        }
        
        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="message">Optional additional message</param>
        public void LogException(Exception exception, string message = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                _logger?.Error(exception, "An exception occurred");
            }
            else
            {
                _logger?.Error(exception, message);
            }
        }
        
        /// <summary>
        /// Logs a user event for analytics purposes
        /// </summary>
        /// <param name="eventName">The name of the event</param>
        /// <param name="properties">Optional event properties</param>
        public void LogUserEvent(string eventName, object properties = null)
        {
            var userEvent = new UserEvent
            {
                EventName = eventName,
                Properties = properties,
                Timestamp = DateTime.UtcNow
            };
            
            _userEvents.Add(userEvent);
            _logger?.Information("User event: {EventName} - {Properties}", eventName, properties);
        }
        
        /// <summary>
        /// Gets all logged user events
        /// </summary>
        /// <returns>A list of user events</returns>
        public List<UserEvent> GetUserEvents()
        {
            return _userEvents;
        }
        
        /// <summary>
        /// Clears the user event log
        /// </summary>
        public void ClearUserEvents()
        {
            _userEvents.Clear();
        }
    }
    
    /// <summary>
    /// Represents a user event for analytics purposes
    /// </summary>
    public class UserEvent
    {
        /// <summary>
        /// Gets or sets the name of the event
        /// </summary>
        public string EventName { get; set; }
        
        /// <summary>
        /// Gets or sets the properties associated with the event
        /// </summary>
        public object Properties { get; set; }
        
        /// <summary>
        /// Gets or sets the timestamp when the event occurred
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
} 