using System;
using VillageGame.Core.Interfaces;

namespace VillageGame.Infrastructure.Services
{
    /// <summary>
    /// Implements the IGlobalContext interface to provide a global context for the application.
    /// This class centralizes access to all shared services and configurations.
    /// </summary>
    public class GlobalContext : IGlobalContext
    {
        private bool _isInitialized;
        
        /// <summary>
        /// Gets the logger service
        /// </summary>
        public ILoggerService Logger { get; private set; }
        
        /// <summary>
        /// Gets the data storage service
        /// </summary>
        public IDataStore DataStore { get; private set; }
        
        /// <summary>
        /// Gets the analytics service
        /// </summary>
        public IAnalyticsService Analytics { get; private set; }
        
        /// <summary>
        /// Gets the monetization service
        /// </summary>
        public IMonetizationService Monetization { get; private set; }
        
        /// <summary>
        /// Gets application configuration settings
        /// </summary>
        public IAppConfig Config { get; private set; }
        
        /// <summary>
        /// Initializes a new instance of the GlobalContext class
        /// </summary>
        public GlobalContext()
        {
            _isInitialized = false;
        }
        
        /// <summary>
        /// Initializes the global context and all of its services
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }
            
            // Initialize services in the correct order
            InitializeConfig();
            InitializeLogger();
            InitializeDataStore();
            InitializeAnalytics();
            InitializeMonetization();
            
            _isInitialized = true;
        }
        
        private void InitializeConfig()
        {
            Config = new AppConfig();
            // Load default configuration
            Config.LoadFromFile("config.json");
        }
        
        private void InitializeLogger()
        {
            Logger = new LoggerService();
            Logger.Initialize();
        }
        
        private void InitializeDataStore()
        {
            DataStore = new JsonDataStore();
            // Initialize asynchronously but block until completed
            DataStore.InitializeAsync().GetAwaiter().GetResult();
        }
        
        private void InitializeAnalytics()
        {
            Analytics = new LocalAnalyticsService(Logger, DataStore);
            // Initialize asynchronously but block until completed
            Analytics.InitializeAsync().GetAwaiter().GetResult();
        }
        
        private void InitializeMonetization()
        {
            Monetization = new MockMonetizationService();
            // Initialize asynchronously but block until completed
            Monetization.InitializeAsync().GetAwaiter().GetResult();
        }
    }
} 