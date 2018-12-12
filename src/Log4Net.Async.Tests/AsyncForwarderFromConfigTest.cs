﻿using System.Linq;
using System.Reflection;
using log4net;
using log4net.Config;
using NUnit.Framework;

namespace Log4Net.Async.Tests
{
    [TestFixture]
    public class AsyncForwarderFromConfigTest
    {
        [Test]
        public void BufferSizeIsCorrectlyApplied()
        {
            // Arrange
            XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetExecutingAssembly()));
            var log = LogManager.GetLogger(typeof(AsyncForwarderFromConfigTest));
            
            // Act
            var appenders = log.Logger.Repository.GetAppenders();

            // Assert
            var asyncForwarder = appenders.FirstOrDefault(x => x is AsyncForwardingAppender) as AsyncForwardingAppender;
            Assert.That(asyncForwarder, Is.Not.Null);
            const int bufferSizeInConfigFile = 2000;
            Assert.That(asyncForwarder.BufferSize, Is.EqualTo(bufferSizeInConfigFile));

            LogManager.Shutdown();
        }
    }
}
