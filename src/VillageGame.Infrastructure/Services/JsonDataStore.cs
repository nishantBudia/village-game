using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VillageGame.Core.Interfaces;

namespace VillageGame.Infrastructure.Services
{
    /// <summary>
    /// Implements the IDataStore interface to provide data persistence using JSON files.
    /// This implementation is suitable for simple data storage needs.
    /// </summary>
    public class JsonDataStore : IDataStore
    {
        private readonly string _dataDirectory = "GameData";
        private readonly Dictionary<string, object> _cache;
        
        /// <summary>
        /// Initializes a new instance of the JsonDataStore class
        /// </summary>
        public JsonDataStore()
        {
            _cache = new Dictionary<string, object>();
        }
        
        /// <summary>
        /// Initializes the data store
        /// </summary>
        /// <returns>True if initialization was successful</returns>
        public async Task<bool> InitializeAsync()
        {
            try
            {
                // Create data directory if it doesn't exist
                if (!Directory.Exists(_dataDirectory))
                {
                    Directory.CreateDirectory(_dataDirectory);
                }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Saves data to the store
        /// </summary>
        /// <typeparam name="T">The type of data to save</typeparam>
        /// <param name="key">The key to store the data under</param>
        /// <param name="data">The data to store</param>
        /// <returns>True if the save operation was successful</returns>
        public async Task<bool> SaveDataAsync<T>(string key, T data)
        {
            try
            {
                // Update cache
                _cache[key] = data;
                
                // Serialize data to JSON
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                
                // Save to file
                var filePath = GetFilePath(key);
                await File.WriteAllTextAsync(filePath, json, Encoding.UTF8);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Loads data from the store
        /// </summary>
        /// <typeparam name="T">The type of data to load</typeparam>
        /// <param name="key">The key to retrieve the data from</param>
        /// <returns>The loaded data, or default(T) if the key was not found</returns>
        public async Task<T> LoadDataAsync<T>(string key)
        {
            try
            {
                // Check if data exists in cache
                if (_cache.TryGetValue(key, out var cachedData) && cachedData is T typedCachedData)
                {
                    return typedCachedData;
                }
                
                // Load from file
                var filePath = GetFilePath(key);
                if (!File.Exists(filePath))
                {
                    return default;
                }
                
                var json = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
                var data = JsonConvert.DeserializeObject<T>(json);
                
                // Update cache
                _cache[key] = data;
                
                return data;
            }
            catch (Exception)
            {
                return default;
            }
        }
        
        /// <summary>
        /// Deletes data from the store
        /// </summary>
        /// <param name="key">The key to delete</param>
        /// <returns>True if the delete operation was successful or the key didn't exist</returns>
        public async Task<bool> DeleteDataAsync(string key)
        {
            try
            {
                // Remove from cache
                _cache.Remove(key);
                
                // Delete file
                var filePath = GetFilePath(key);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Checks if a key exists in the store
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key exists</returns>
        public async Task<bool> KeyExistsAsync(string key)
        {
            // Check cache first
            if (_cache.ContainsKey(key))
            {
                return true;
            }
            
            // Check file system
            var filePath = GetFilePath(key);
            return File.Exists(filePath);
        }
        
        private string GetFilePath(string key)
        {
            // Sanitize key to create a valid filename
            var sanitizedKey = string.Join("_", key.Split(Path.GetInvalidFileNameChars()));
            return Path.Combine(_dataDirectory, $"{sanitizedKey}.json");
        }
    }
} 