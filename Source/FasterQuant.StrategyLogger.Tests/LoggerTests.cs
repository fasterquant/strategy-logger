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

            Log.Information("This is a test");
        }
    }
}
