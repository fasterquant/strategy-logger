using Serilog;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FasterQuant.StrategyLogger.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var lf = new LoggerFactory("LIVE");
            Log.Logger = lf.GetLogger();
            var sed = new StrategyEventData(8898, "This is a test", 339393, "kdkdkd", "Long", "test here", "Execution", "fun");

            Log.Information("{@Strategy}", sed);
        }
    }
}
