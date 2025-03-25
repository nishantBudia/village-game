using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VillageGame.Core.Interfaces;

namespace VillageGame.Infrastructure.Services
{
    /// <summary>
    /// Implements the IMonetizationService interface to provide mock monetization services.
    /// This implementation is intended for development and testing.
    /// </summary>
    public class MockMonetizationService : IMonetizationService
    {
        private readonly List<Product> _availableProducts;
        private readonly List<Purchase> _purchases;
        private readonly Dictionary<AdType, bool> _adsReady;
        
        /// <summary>
        /// Event raised when a purchase is completed
        /// </summary>
        public event EventHandler<PurchaseEventArgs> PurchaseCompleted;
        
        /// <summary>
        /// Event raised when an ad is displayed
        /// </summary>
        public event EventHandler<AdEventArgs> AdDisplayed;
        
        /// <summary>
        /// Event raised when an ad is closed
        /// </summary>
        public event EventHandler<AdEventArgs> AdClosed;
        
        /// <summary>
        /// Initializes a new instance of the MockMonetizationService class
        /// </summary>
        public MockMonetizationService()
        {
            // Initialize available products
            _availableProducts = new List<Product>
            {
                new Product
                {
                    Id = "no_ads",
                    Name = "Remove Ads",
                    Description = "Remove all advertisements from the game",
                    Price = 2.99m,
                    Currency = "USD"
                },
                new Product
                {
                    Id = "premium_upgrade",
                    Name = "Premium Upgrade",
                    Description = "Unlock all premium features",
                    Price = 4.99m,
                    Currency = "USD"
                },
                new Product
                {
                    Id = "coins_100",
                    Name = "100 Coins",
                    Description = "Purchase 100 coins for in-game use",
                    Price = 0.99m,
                    Currency = "USD"
                },
                new Product
                {
                    Id = "coins_500",
                    Name = "500 Coins",
                    Description = "Purchase 500 coins for in-game use",
                    Price = 3.99m,
                    Currency = "USD"
                }
            };
            
            // Initialize purchases
            _purchases = new List<Purchase>();
            
            // Initialize ad ready state
            _adsReady = new Dictionary<AdType, bool>
            {
                { AdType.Banner, true },
                { AdType.Interstitial, true },
                { AdType.Rewarded, true }
            };
        }
        
        /// <summary>
        /// Initializes the monetization service
        /// </summary>
        /// <returns>True if initialization was successful</returns>
        public Task<bool> InitializeAsync()
        {
            // In a real implementation, this would initialize the SDK
            return Task.FromResult(true);
        }
        
        /// <summary>
        /// Gets available products
        /// </summary>
        /// <returns>A list of available products</returns>
        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            return Task.FromResult(_availableProducts.AsEnumerable());
        }
        
        /// <summary>
        /// Initiates a purchase
        /// </summary>
        /// <param name="productId">The ID of the product to purchase</param>
        /// <returns>True if the purchase was initiated successfully</returns>
        public async Task<bool> PurchaseAsync(string productId)
        {
            // Find the product
            var product = _availableProducts.FirstOrDefault(p => p.Id == productId);
            
            if (product == null)
            {
                return false;
            }
            
            // Simulate purchase processing
            await Task.Delay(1000);
            
            // Create a new purchase
            var purchase = new Purchase
            {
                ProductId = productId,
                PurchaseTime = DateTime.UtcNow,
                TransactionId = Guid.NewGuid().ToString()
            };
            
            // Add to purchases
            _purchases.Add(purchase);
            
            // Raise event
            PurchaseCompleted?.Invoke(this, new PurchaseEventArgs { Purchase = purchase });
            
            return true;
        }
        
        /// <summary>
        /// Restores previous purchases
        /// </summary>
        /// <returns>A list of restored purchases</returns>
        public Task<IEnumerable<Purchase>> RestorePurchasesAsync()
        {
            return Task.FromResult(_purchases.AsEnumerable());
        }
        
        /// <summary>
        /// Checks if a product is owned
        /// </summary>
        /// <param name="productId">The ID of the product to check</param>
        /// <returns>True if the product is owned</returns>
        public Task<bool> IsProductOwnedAsync(string productId)
        {
            var isOwned = _purchases.Any(p => p.ProductId == productId);
            return Task.FromResult(isOwned);
        }
        
        /// <summary>
        /// Checks if ads are ready to be displayed
        /// </summary>
        /// <param name="adType">The type of ad to check</param>
        /// <returns>True if ads are ready</returns>
        public Task<bool> AreAdsReadyAsync(AdType adType)
        {
            if (_adsReady.TryGetValue(adType, out var isReady))
            {
                return Task.FromResult(isReady);
            }
            
            return Task.FromResult(false);
        }
        
        /// <summary>
        /// Shows an ad
        /// </summary>
        /// <param name="adType">The type of ad to show</param>
        /// <returns>True if the ad was shown successfully</returns>
        public async Task<bool> ShowAdAsync(AdType adType)
        {
            if (!await AreAdsReadyAsync(adType))
            {
                return false;
            }
            
            // Simulate ad display
            await Task.Delay(1000);
            
            var adEventArgs = new AdEventArgs
            {
                AdType = adType,
                Rewarded = adType == AdType.Rewarded
            };
            
            // Raise displayed event
            AdDisplayed?.Invoke(this, adEventArgs);
            
            // Simulate ad viewing
            await Task.Delay(2000);
            
            // Raise closed event
            AdClosed?.Invoke(this, adEventArgs);
            
            // Reset ad ready state
            _adsReady[adType] = false;
            
            // Simulate ad refresh after a delay
            _ = RefreshAdAfterDelayAsync(adType);
            
            return true;
        }
        
        private async Task RefreshAdAfterDelayAsync(AdType adType)
        {
            // Simulate ad loading
            await Task.Delay(5000);
            
            // Mark ad as ready
            _adsReady[adType] = true;
        }
    }
} 