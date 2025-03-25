using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VillageGame.Core.Interfaces
{
    /// <summary>
    /// Provides an abstraction for monetization services including in-app purchases and ad integration.
    /// </summary>
    public interface IMonetizationService
    {
        /// <summary>
        /// Event raised when a purchase is completed
        /// </summary>
        event EventHandler<PurchaseEventArgs> PurchaseCompleted;
        
        /// <summary>
        /// Event raised when an ad is displayed
        /// </summary>
        event EventHandler<AdEventArgs> AdDisplayed;
        
        /// <summary>
        /// Event raised when an ad is closed
        /// </summary>
        event EventHandler<AdEventArgs> AdClosed;
        
        /// <summary>
        /// Initializes the monetization service
        /// </summary>
        /// <returns>True if initialization was successful</returns>
        Task<bool> InitializeAsync();
        
        /// <summary>
        /// Gets available products
        /// </summary>
        /// <returns>A list of available products</returns>
        Task<IEnumerable<Product>> GetProductsAsync();
        
        /// <summary>
        /// Initiates a purchase
        /// </summary>
        /// <param name="productId">The ID of the product to purchase</param>
        /// <returns>True if the purchase was initiated successfully</returns>
        Task<bool> PurchaseAsync(string productId);
        
        /// <summary>
        /// Restores previous purchases
        /// </summary>
        /// <returns>A list of restored purchases</returns>
        Task<IEnumerable<Purchase>> RestorePurchasesAsync();
        
        /// <summary>
        /// Checks if a product is owned
        /// </summary>
        /// <param name="productId">The ID of the product to check</param>
        /// <returns>True if the product is owned</returns>
        Task<bool> IsProductOwnedAsync(string productId);
        
        /// <summary>
        /// Checks if ads are ready to be displayed
        /// </summary>
        /// <param name="adType">The type of ad to check</param>
        /// <returns>True if ads are ready</returns>
        Task<bool> AreAdsReadyAsync(AdType adType);
        
        /// <summary>
        /// Shows an ad
        /// </summary>
        /// <param name="adType">The type of ad to show</param>
        /// <returns>True if the ad was shown successfully</returns>
        Task<bool> ShowAdAsync(AdType adType);
    }
    
    /// <summary>
    /// Represents a product that can be purchased
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product ID
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the product description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the product price
        /// </summary>
        public decimal Price { get; set; }
        
        /// <summary>
        /// Gets or sets the product price currency
        /// </summary>
        public string Currency { get; set; }
    }
    
    /// <summary>
    /// Represents a completed purchase
    /// </summary>
    public class Purchase
    {
        /// <summary>
        /// Gets or sets the product ID
        /// </summary>
        public string ProductId { get; set; }
        
        /// <summary>
        /// Gets or sets the purchase timestamp
        /// </summary>
        public DateTime PurchaseTime { get; set; }
        
        /// <summary>
        /// Gets or sets the purchase transaction ID
        /// </summary>
        public string TransactionId { get; set; }
    }
    
    /// <summary>
    /// Enumerates the types of ads
    /// </summary>
    public enum AdType
    {
        /// <summary>
        /// Banner ad
        /// </summary>
        Banner,
        
        /// <summary>
        /// Interstitial ad
        /// </summary>
        Interstitial,
        
        /// <summary>
        /// Rewarded ad
        /// </summary>
        Rewarded
    }
    
    /// <summary>
    /// Provides data for purchase events
    /// </summary>
    public class PurchaseEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the purchase
        /// </summary>
        public Purchase Purchase { get; set; }
    }
    
    /// <summary>
    /// Provides data for ad events
    /// </summary>
    public class AdEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the ad type
        /// </summary>
        public AdType AdType { get; set; }
        
        /// <summary>
        /// Gets or sets whether the ad was rewarded
        /// </summary>
        public bool Rewarded { get; set; }
    }
} 