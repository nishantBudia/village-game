namespace VillageGame.Core.Interfaces
{
    /// <summary>
    /// Provides a global context for accessing shared services and configurations
    /// throughout the application.
    /// </summary>
    public interface IGlobalContext
    {
        /// <summary>
        /// Gets the logger service
        /// </summary>
        ILoggerService Logger { get; }
        
        /// <summary>
        /// Gets the data storage service
        /// </summary>
        IDataStore DataStore { get; }
        
        /// <summary>
        /// Gets the analytics service
        /// </summary>
        IAnalyticsService Analytics { get; }
        
        /// <summary>
        /// Gets the monetization service
        /// </summary>
        IMonetizationService Monetization { get; }
        
        /// <summary>
        /// Gets application configuration settings
        /// </summary>
        IAppConfig Config { get; }
        
        /// <summary>
        /// Initializes the global context and all of its services
        /// </summary>
        void Initialize();
    }
} 