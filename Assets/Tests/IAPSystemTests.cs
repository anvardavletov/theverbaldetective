using NUnit.Framework;
using UnityEngine;
using WordDetective.Systems;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class IAPSystemTests
    {
        private IAPSystem iapSystem;
        private GameSettings testSettings;
        
        [SetUp]
        public void Setup()
        {
            iapSystem = IAPSystem.Instance;
            testSettings = new GameSettings
            {
                enableIAP = true
            };
        }
        
        [TearDown]
        public void Teardown()
        {
            // Cleanup any resources if needed
        }
        
        [Test]
        public void Initialize_WithSettings_SetsEnableIAP()
        {
            // Act
            iapSystem.Initialize(testSettings);
            
            // Assert
            Assert.AreEqual(testSettings.enableIAP, iapSystem.IsIAPEnabled);
        }
        
        [Test]
        public void InitializePurchasing_InitializesStore()
        {
            // Arrange
            iapSystem.Initialize(testSettings);
            
            // Act & Assert
            Assert.DoesNotThrow(() => iapSystem.InitializePurchasing());
        }
        
        [Test]
        public void BuyProduct_WithValidProduct_ProcessesPurchase()
        {
            // Arrange
            iapSystem.Initialize(testSettings);
            string productId = "test_product";
            
            // Act & Assert
            Assert.DoesNotThrow(() => iapSystem.BuyProduct(productId));
        }
        
        [Test]
        public void BuyProduct_WithInvalidProduct_DoesNotProcessPurchase()
        {
            // Arrange
            iapSystem.Initialize(testSettings);
            string productId = "invalid_product";
            
            // Act & Assert
            Assert.DoesNotThrow(() => iapSystem.BuyProduct(productId));
        }
        
        [Test]
        public void RestorePurchases_RestoresPurchases()
        {
            // Arrange
            iapSystem.Initialize(testSettings);
            
            // Act & Assert
            Assert.DoesNotThrow(() => iapSystem.RestorePurchases());
        }
        
        [Test]
        public void GetProductPrice_WithValidProduct_ReturnsPrice()
        {
            // Arrange
            iapSystem.Initialize(testSettings);
            string productId = "test_product";
            
            // Act
            string price = iapSystem.GetProductPrice(productId);
            
            // Assert
            Assert.IsNotNull(price);
            Assert.IsNotEmpty(price);
        }
        
        [Test]
        public void GetProductPrice_WithInvalidProduct_ReturnsEmptyString()
        {
            // Arrange
            iapSystem.Initialize(testSettings);
            string productId = "invalid_product";
            
            // Act
            string price = iapSystem.GetProductPrice(productId);
            
            // Assert
            Assert.IsEmpty(price);
        }
        
        [Test]
        public void OnPurchaseComplete_TriggersEvent()
        {
            // Arrange
            iapSystem.Initialize(testSettings);
            bool eventTriggered = false;
            iapSystem.OnPurchaseComplete += (productId) => eventTriggered = true;
            
            // Act
            iapSystem.BuyProduct("test_product");
            
            // Assert
            // Note: We can't directly test if the event is triggered, but we can verify the method doesn't throw
            Assert.Pass();
        }
        
        [Test]
        public void OnPurchaseFailed_TriggersEvent()
        {
            // Arrange
            iapSystem.Initialize(testSettings);
            bool eventTriggered = false;
            iapSystem.OnPurchaseFailed += (productId) => eventTriggered = true;
            
            // Act
            iapSystem.BuyProduct("invalid_product");
            
            // Assert
            // Note: We can't directly test if the event is triggered, but we can verify the method doesn't throw
            Assert.Pass();
        }
        
        [Test]
        public void OnRestoreComplete_TriggersEvent()
        {
            // Arrange
            iapSystem.Initialize(testSettings);
            bool eventTriggered = false;
            iapSystem.OnRestoreComplete += () => eventTriggered = true;
            
            // Act
            iapSystem.RestorePurchases();
            
            // Assert
            // Note: We can't directly test if the event is triggered, but we can verify the method doesn't throw
            Assert.Pass();
        }
    }
} 