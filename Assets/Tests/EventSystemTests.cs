using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;
using System;

namespace WordDetective.Tests
{
    public class EventSystemTests
    {
        private GameObject gameObject;
        private EventSystem eventSystem;
        
        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
            eventSystem = gameObject.AddComponent<EventSystem>();
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }
        
        [Test]
        public void Subscribe_AddsCallback()
        {
            // Arrange
            string eventName = "TestEvent";
            bool callbackCalled = false;
            Action<object> callback = (data) => callbackCalled = true;
            
            // Act
            eventSystem.Subscribe(eventName, callback);
            eventSystem.TriggerEvent(eventName);
            
            // Assert
            Assert.IsTrue(callbackCalled);
        }
        
        [Test]
        public void Unsubscribe_RemovesCallback()
        {
            // Arrange
            string eventName = "TestEvent";
            bool callbackCalled = false;
            Action<object> callback = (data) => callbackCalled = true;
            
            eventSystem.Subscribe(eventName, callback);
            eventSystem.Unsubscribe(eventName, callback);
            
            // Act
            eventSystem.TriggerEvent(eventName);
            
            // Assert
            Assert.IsFalse(callbackCalled);
        }
        
        [Test]
        public void TriggerEvent_WithData_PassesDataToCallback()
        {
            // Arrange
            string eventName = "TestEvent";
            object testData = new { value = 42 };
            object receivedData = null;
            Action<object> callback = (data) => receivedData = data;
            
            eventSystem.Subscribe(eventName, callback);
            
            // Act
            eventSystem.TriggerEvent(eventName, testData);
            
            // Assert
            Assert.AreEqual(testData, receivedData);
        }
        
        [Test]
        public void ClearAllEvents_RemovesAllCallbacks()
        {
            // Arrange
            string eventName1 = "TestEvent1";
            string eventName2 = "TestEvent2";
            bool callback1Called = false;
            bool callback2Called = false;
            Action<object> callback1 = (data) => callback1Called = true;
            Action<object> callback2 = (data) => callback2Called = true;
            
            eventSystem.Subscribe(eventName1, callback1);
            eventSystem.Subscribe(eventName2, callback2);
            eventSystem.ClearAllEvents();
            
            // Act
            eventSystem.TriggerEvent(eventName1);
            eventSystem.TriggerEvent(eventName2);
            
            // Assert
            Assert.IsFalse(callback1Called);
            Assert.IsFalse(callback2Called);
        }
    }
} 