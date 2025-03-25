using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using VillageGame.Core.Interfaces;

namespace VillageGame.Infrastructure.Services
{
    /// <summary>
    /// Implements the IAppConfig interface to provide application configuration settings.
    /// This class uses a JSON file for persistence.
    /// </summary>
    public class AppConfig : IAppConfig
    {
        private readonly Dictionary<string, object> _configValues;
        
        /// <summary>
        /// Gets the current environment (e.g., Development, Production)
        /// </summary>
        public string Environment { get; private set; }
        
        /// <summary>
        /// Gets the application version
        /// </summary>
        public string Version { get; private set; }
        
        /// <summary>
        /// Initializes a new instance of the AppConfig class
        /// </summary>
        public AppConfig()
        {
            _configValues = new Dictionary<string, object>();
            
            // Set default values
            Environment = "Development";
            Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";
            
            // Additional default configuration values
            SetValue("LogLevel", "Information");
            SetValue("ScreenWidth", 1280);
            SetValue("ScreenHeight", 720);
            SetValue("MusicVolume", 0.7f);
            SetValue("SoundVolume", 1.0f);
            SetValue("AnalyticsEnabled", true);
        }
        
        /// <summary>
        /// Gets a configuration value
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="key">The key of the configuration value</param>
        /// <param name="defaultValue">The default value to return if the key is not found</param>
        /// <returns>The configuration value, or defaultValue if the key is not found</returns>
        public T GetValue<T>(string key, T defaultValue = default)
        {
            if (_configValues.TryGetValue(key, out var value))
            {
                try
                {
                    if (value is T typedValue)
                    {
                        return typedValue;
                    }
                    
                    // Try to convert the value to the requested type
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
            
            return defaultValue;
        }
        
        /// <summary>
        /// Sets a configuration value
        /// </summary>
        /// <typeparam name="T">The type of the value to set</typeparam>
        /// <param name="key">The key of the configuration value</param>
        /// <param name="value">The value to set</param>
        /// <returns>True if the value was set successfully</returns>
        public bool SetValue<T>(string key, T value)
        {
            try
            {
                _configValues[key] = value;
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Removes a configuration value
        /// </summary>
        /// <param name="key">The key of the configuration value to remove</param>
        /// <returns>True if the value was removed successfully or did not exist</returns>
        public bool RemoveValue(string key)
        {
            return !_configValues.ContainsKey(key) || _configValues.Remove(key);
        }
        
        /// <summary>
        /// Loads configuration from a file
        /// </summary>
        /// <param name="filePath">The path to the configuration file</param>
        /// <returns>True if the configuration was loaded successfully</returns>
        public bool LoadFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    // If the file doesn't exist, create it with default values
                    SaveToFile(filePath);
                    return true;
                }
                
                var json = File.ReadAllText(filePath);
                var config = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                
                if (config == null)
                {
                    return false;
                }
                
                // Update all configuration values
                foreach (var kvp in config)
                {
                    _configValues[kvp.Key] = kvp.Value;
                }
                
                // Update special properties
                if (config.TryGetValue("Environment", out var env) && env is string envStr)
                {
                    Environment = envStr;
                }
                
                if (config.TryGetValue("Version", out var ver) && ver is string verStr)
                {
                    Version = verStr;
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Saves configuration to a file
        /// </summary>
        /// <param name="filePath">The path to the configuration file</param>
        /// <returns>True if the configuration was saved successfully</returns>
        public bool SaveToFile(string filePath)
        {
            try
            {
                // Create directory if it doesn't exist
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                // Update special properties
                _configValues["Environment"] = Environment;
                _configValues["Version"] = Version;
                
                // Serialize to JSON
                var json = JsonConvert.SerializeObject(_configValues, Formatting.Indented);
                File.WriteAllText(filePath, json);
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 