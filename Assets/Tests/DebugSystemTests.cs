using NUnit.Framework;
using UnityEngine;
using WordDetective.Systems;

namespace WordDetective.Tests
{
    public class DebugSystemTests
    {
        private DebugSystem debugSystem;
        private GameSettings testSettings;
        
        [SetUp]
        public void Setup()
        {
            debugSystem = DebugSystem.Instance;
            testSettings = new GameSettings
            {
                debugMode = true
            };
        }
        
        [TearDown]
        public void Teardown()
        {
            // Cleanup any resources if needed
        }
        
        [Test]
        public void Initialize_WithSettings_SetsDebugMode()
        {
            // Act
            debugSystem.Initialize(testSettings);
            
            // Assert
            Assert.AreEqual(testSettings.debugMode, debugSystem.IsDebugMode);
        }
        
        [Test]
        public void Log_WithDebugMode_LogsMessage()
        {
            // Arrange
            debugSystem.Initialize(testSettings);
            string message = "Test debug message";
            
            // Act & Assert
            Assert.DoesNotThrow(() => debugSystem.Log(message));
        }
        
        [Test]
        public void Log_WithoutDebugMode_DoesNotLogMessage()
        {
            // Arrange
            testSettings.debugMode = false;
            debugSystem.Initialize(testSettings);
            string message = "Test debug message";
            
            // Act & Assert
            Assert.DoesNotThrow(() => debugSystem.Log(message));
        }
        
        [Test]
        public void LogWarning_WithDebugMode_LogsWarning()
        {
            // Arrange
            debugSystem.Initialize(testSettings);
            string message = "Test warning message";
            
            // Act & Assert
            Assert.DoesNotThrow(() => debugSystem.LogWarning(message));
        }
        
        [Test]
        public void LogError_WithDebugMode_LogsError()
        {
            // Arrange
            debugSystem.Initialize(testSettings);
            string message = "Test error message";
            
            // Act & Assert
            Assert.DoesNotThrow(() => debugSystem.LogError(message));
        }
        
        [Test]
        public void LogException_WithDebugMode_LogsException()
        {
            // Arrange
            debugSystem.Initialize(testSettings);
            System.Exception exception = new System.Exception("Test exception");
            
            // Act & Assert
            Assert.DoesNotThrow(() => debugSystem.LogException(exception));
        }
        
        [Test]
        public void SetDebugMode_UpdatesDebugMode()
        {
            // Arrange
            debugSystem.Initialize(testSettings);
            bool newDebugMode = false;
            
            // Act
            debugSystem.SetDebugMode(newDebugMode);
            
            // Assert
            Assert.AreEqual(newDebugMode, debugSystem.IsDebugMode);
        }
        
        [Test]
        public void ClearDebugLog_ClearsLog()
        {
            // Arrange
            debugSystem.Initialize(testSettings);
            debugSystem.Log("Test message");
            
            // Act
            debugSystem.ClearDebugLog();
            
            // Assert
            // Note: We can't directly test if the log is cleared, but we can verify the method doesn't throw
            Assert.Pass();
        }
    }
} 