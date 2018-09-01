using NUnit.Framework;

namespace PrettyLogger.Facade.Test
{
    public class LoggerBuilderFactoryTest
    {
        [Test]
        public void TestCreateLoggerBuilderFactory()
        {
            var builder = LoggerBuilderFactory.Instance.CreateLoggerBuilder();
            LoggerBuilderTest.TestSimpleBuilder(builder);
        }
    }
}