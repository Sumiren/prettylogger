using NUnit.Framework;

namespace PrettyLogger.Facade.Test
{
    public class LoggerBuilderFactoryTest
    {
        [Test]
        public void TestCreateRawLoggerBuilder()
        {
            var builder = LoggerBuilderFactory.Instance.CreateRawLoggerBuilder();
            LoggerBuilderTest.AssertCanBuildUpCorrectly(builder);
        }

        [Test]
        public void TestCreateLoggerBuilder()
        {
            var builder = LoggerBuilderFactory.Instance.CreateLoggerBuilder();
            LoggerBuilderWorkflowAdapterTest.AssertCanBuildUpCorrectly(builder);
        }
    }
}