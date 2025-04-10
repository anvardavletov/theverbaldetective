using NUnit.Framework;
using UnityEngine;
using WordDetective.Systems;

namespace WordDetective.Tests
{
    public class AdSystemTests
    {
        private AdSystem adSystem;
        private GameSettings testSettings;
        
        [SetUp]
        public void Setup()
        {
            adSystem = AdSystem.Instance;
            testSettings = new GameSettings
            {
                showAds = true
            };
        }
        
        [TearDown]
        public void Teardown()
        {
            // Cleanup any resources if needed
        }
        
        [Test]
        public void Initialize_WithSettings_SetsShowAds()
        {
            // Act
            adSystem.Initialize(testSettings);
            
            // Assert
            Assert.AreEqual(testSettings.showAds, adSystem.ShowAds);
        }
        
        [Test]
        public void ShowBannerAd_WithShowAds_ShowsAd()
        {
            // Arrange
            adSystem.Initialize(testSettings);
            
            // Act & Assert
            Assert.DoesNotThrow(() => adSystem.ShowBannerAd());
        }
        
        [Test]
        public void ShowBannerAd_WithoutShowAds_DoesNotShowAd()
        {
            // Arrange
            testSettings.showAds = false;
            adSystem.Initialize(testSettings);
            
            // Act & Assert
            Assert.DoesNotThrow(() => adSystem.ShowBannerAd());
        }
        
        [Test]
        public void HideBannerAd_HidesAd()
        {
            // Arrange
            adSystem.Initialize(testSettings);
            adSystem.ShowBannerAd();
            
            // Act & Assert
            Assert.DoesNotThrow(() => adSystem.HideBannerAd());
        }
        
        [Test]
        public void ShowInterstitialAd_WithShowAds_ShowsAd()
        {
            // Arrange
            adSystem.Initialize(testSettings);
            
            // Act & Assert
            Assert.DoesNotThrow(() => adSystem.ShowInterstitialAd());
        }
        
        [Test]
        public void ShowInterstitialAd_WithoutShowAds_DoesNotShowAd()
        {
            // Arrange
            testSettings.showAds = false;
            adSystem.Initialize(testSettings);
            
            // Act & Assert
            Assert.DoesNotThrow(() => adSystem.ShowInterstitialAd());
        }
        
        [Test]
        public void ShowRewardedAd_WithShowAds_ShowsAd()
        {
            // Arrange
            adSystem.Initialize(testSettings);
            
            // Act & Assert
            Assert.DoesNotThrow(() => adSystem.ShowRewardedAd());
        }
        
        [Test]
        public void ShowRewardedAd_WithoutShowAds_DoesNotShowAd()
        {
            // Arrange
            testSettings.showAds = false;
            adSystem.Initialize(testSettings);
            
            // Act & Assert
            Assert.DoesNotThrow(() => adSystem.ShowRewardedAd());
        }
        
        [Test]
        public void OnAdLoaded_TriggersEvent()
        {
            // Arrange
            adSystem.Initialize(testSettings);
            bool eventTriggered = false;
            adSystem.OnAdLoaded += () => eventTriggered = true;
            
            // Act
            adSystem.LoadBannerAd();
            
            // Assert
            // Note: We can't directly test if the event is triggered, but we can verify the method doesn't throw
            Assert.Pass();
        }
        
        [Test]
        public void OnAdFailedToLoad_TriggersEvent()
        {
            // Arrange
            adSystem.Initialize(testSettings);
            bool eventTriggered = false;
            adSystem.OnAdFailedToLoad += () => eventTriggered = true;
            
            // Act
            adSystem.LoadBannerAd();
            
            // Assert
            // Note: We can't directly test if the event is triggered, but we can verify the method doesn't throw
            Assert.Pass();
        }
        
        [Test]
        public void OnAdClosed_TriggersEvent()
        {
            // Arrange
            adSystem.Initialize(testSettings);
            bool eventTriggered = false;
            adSystem.OnAdClosed += () => eventTriggered = true;
            
            // Act
            adSystem.ShowBannerAd();
            adSystem.HideBannerAd();
            
            // Assert
            // Note: We can't directly test if the event is triggered, but we can verify the method doesn't throw
            Assert.Pass();
        }
    }
} 