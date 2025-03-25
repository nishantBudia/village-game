namespace VillageGame.Core.Interfaces
{
    /// <summary>
    /// Provides access to application configuration settings
    /// </summary>
    public interface IAppConfig
    {
        /// <summary>
        /// Gets a configuration value
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="key">The key of the configuration value</param>
        /// <param name="defaultValue">The default value to return if the key is not found</param>
        /// <returns>The configuration value, or defaultValue if the key is not found</returns>
        T GetValue<T>(string key, T defaultValue = default);
        
        /// <summary>
        /// Sets a configuration value
        /// </summary>
        /// <typeparam name="T">The type of the value to set</typeparam>
        /// <param name="key">The key of the configuration value</param>
        /// <param name="value">The value to set</param>
        /// <returns>True if the value was set successfully</returns>
        bool SetValue<T>(string key, T value);
        
        /// <summary>
        /// Removes a configuration value
        /// </summary>
        /// <param name="key">The key of the configuration value to remove</param>
        /// <returns>True if the value was removed successfully or did not exist</returns>
        bool RemoveValue(string key);
        
        /// <summary>
        /// Loads configuration from a file
        /// </summary>
        /// <param name="filePath">The path to the configuration file</param>
        /// <returns>True if the configuration was loaded successfully</returns>
        bool LoadFromFile(string filePath);
        
        /// <summary>
        /// Saves configuration to a file
        /// </summary>
        /// <param name="filePath">The path to the configuration file</param>
        /// <returns>True if the configuration was saved successfully</returns>
        bool SaveToFile(string filePath);
        
        /// <summary>
        /// Gets the current environment (e.g., Development, Production)
        /// </summary>
        string Environment { get; }
        
        /// <summary>
        /// Gets the application version
        /// </summary>
        string Version { get; }
    }
} 