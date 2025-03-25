using System.Collections.Generic;
using System.Threading.Tasks;

namespace VillageGame.Core.Interfaces
{
    /// <summary>
    /// Provides an abstraction for analytics services.
    /// This interface can be implemented by different analytics providers.
    /// </summary>
    public interface IAnalyticsService
    {
        /// <summary>
        /// Initializes the analytics service
        /// </summary>
        /// <returns>True if initialization was successful</returns>
        Task<bool> InitializeAsync();
        
        /// <summary>
        /// Tracks a custom event
        /// </summary>
        /// <param name="eventName">The name of the event</param>
        /// <param name="properties">Optional properties associated with the event</param>
        /// <returns>True if the event was tracked successfully</returns>
        Task<bool> TrackEventAsync(string eventName, Dictionary<string, object> properties = null);
        
        /// <summary>
        /// Tracks a screen view
        /// </summary>
        /// <param name="screenName">The name of the screen being viewed</param>
        /// <param name="properties">Optional properties associated with the screen view</param>
        /// <returns>True if the screen view was tracked successfully</returns>
        Task<bool> TrackScreenViewAsync(string screenName, Dictionary<string, object> properties = null);
        
        /// <summary>
        /// Sets a user property
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="propertyValue">The value of the property</param>
        /// <returns>True if the user property was set successfully</returns>
        Task<bool> SetUserPropertyAsync(string propertyName, object propertyValue);
        
        /// <summary>
        /// Sets the user ID for the current user
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <returns>True if the user ID was set successfully</returns>
        Task<bool> SetUserIdAsync(string userId);
        
        /// <summary>
        /// Flushes any queued events to the analytics service
        /// </summary>
        /// <returns>True if the flush was successful</returns>
        Task<bool> FlushAsync();
    }
} 