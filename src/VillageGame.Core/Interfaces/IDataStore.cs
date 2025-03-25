using System.Threading.Tasks;

namespace VillageGame.Core.Interfaces
{
    /// <summary>
    /// Provides an abstraction for data storage operations.
    /// This interface can be implemented by different storage providers (local JSON, SQLite, cloud, etc.)
    /// </summary>
    public interface IDataStore
    {
        /// <summary>
        /// Initializes the data store
        /// </summary>
        /// <returns>True if initialization was successful</returns>
        Task<bool> InitializeAsync();
        
        /// <summary>
        /// Saves data to the store
        /// </summary>
        /// <typeparam name="T">The type of data to save</typeparam>
        /// <param name="key">The key to store the data under</param>
        /// <param name="data">The data to store</param>
        /// <returns>True if the save operation was successful</returns>
        Task<bool> SaveDataAsync<T>(string key, T data);
        
        /// <summary>
        /// Loads data from the store
        /// </summary>
        /// <typeparam name="T">The type of data to load</typeparam>
        /// <param name="key">The key to retrieve the data from</param>
        /// <returns>The loaded data, or default(T) if the key was not found</returns>
        Task<T> LoadDataAsync<T>(string key);
        
        /// <summary>
        /// Deletes data from the store
        /// </summary>
        /// <param name="key">The key to delete</param>
        /// <returns>True if the delete operation was successful or the key didn't exist</returns>
        Task<bool> DeleteDataAsync(string key);
        
        /// <summary>
        /// Checks if a key exists in the store
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key exists</returns>
        Task<bool> KeyExistsAsync(string key);
    }
} 