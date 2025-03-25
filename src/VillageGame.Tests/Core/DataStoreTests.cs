using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using VillageGame.Core.Interfaces;
using VillageGame.Infrastructure.Services;

namespace VillageGame.Tests.Core
{
    [TestFixture]
    public class DataStoreTests
    {
        private IDataStore _dataStore;
        private string _testDirectory;
        
        [SetUp]
        public async Task Setup()
        {
            // Create a temporary test directory
            _testDirectory = Path.Combine(Path.GetTempPath(), "VillageGameTests", Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testDirectory);
            
            // Initialize data store with the test directory
            _dataStore = new JsonDataStore();
            await _dataStore.InitializeAsync();
        }
        
        [TearDown]
        public void TearDown()
        {
            // Clean up test directory
            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
        }
        
        [Test]
        public async Task SaveAndLoadData_StringValue_ReturnsCorrectValue()
        {
            // Arrange
            var key = "test_string";
            var value = "Hello, World!";
            
            // Act
            var saveResult = await _dataStore.SaveDataAsync(key, value);
            var loadedValue = await _dataStore.LoadDataAsync<string>(key);
            
            // Assert
            Assert.That(saveResult, Is.True, "Save operation should succeed");
            Assert.That(loadedValue, Is.EqualTo(value), "Loaded value should match saved value");
        }
        
        [Test]
        public async Task SaveAndLoadData_ObjectValue_ReturnsCorrectValue()
        {
            // Arrange
            var key = "test_object";
            var value = new TestData
            {
                Id = 1,
                Name = "Test Object",
                CreatedAt = DateTime.UtcNow
            };
            
            // Act
            var saveResult = await _dataStore.SaveDataAsync(key, value);
            var loadedValue = await _dataStore.LoadDataAsync<TestData>(key);
            
            // Assert
            Assert.That(saveResult, Is.True, "Save operation should succeed");
            Assert.That(loadedValue, Is.Not.Null, "Loaded value should not be null");
            Assert.That(loadedValue.Id, Is.EqualTo(value.Id), "Loaded Id should match saved Id");
            Assert.That(loadedValue.Name, Is.EqualTo(value.Name), "Loaded Name should match saved Name");
        }
        
        [Test]
        public async Task KeyExists_AfterSavingData_ReturnsTrue()
        {
            // Arrange
            var key = "test_exists";
            var value = "Test Value";
            
            // Act
            await _dataStore.SaveDataAsync(key, value);
            var exists = await _dataStore.KeyExistsAsync(key);
            
            // Assert
            Assert.That(exists, Is.True, "Key should exist after saving data");
        }
        
        [Test]
        public async Task KeyExists_WithNonexistentKey_ReturnsFalse()
        {
            // Arrange
            var key = "nonexistent_key";
            
            // Act
            var exists = await _dataStore.KeyExistsAsync(key);
            
            // Assert
            Assert.That(exists, Is.False, "Key should not exist if not saved");
        }
        
        [Test]
        public async Task DeleteData_ExistingKey_RemovesData()
        {
            // Arrange
            var key = "test_delete";
            var value = "Delete Me";
            await _dataStore.SaveDataAsync(key, value);
            
            // Act
            var deleteResult = await _dataStore.DeleteDataAsync(key);
            var exists = await _dataStore.KeyExistsAsync(key);
            
            // Assert
            Assert.That(deleteResult, Is.True, "Delete operation should succeed");
            Assert.That(exists, Is.False, "Key should not exist after deletion");
        }
        
        private class TestData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
} 