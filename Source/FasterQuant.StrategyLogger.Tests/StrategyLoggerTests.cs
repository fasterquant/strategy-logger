using System;
using System.IO;
using Serilog;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FasterQuant.StrategyLogger.Tests
{
    [TestClass]
    public class StrategyLoggerTests
    {
        [TestMethod]
        public void StrategyModeIsLiveAndUsingConfigfile_WorksCorrectly()
        {
            var logConfig = GetTestLogConfiguration();
            var logConfigFileName = "LoggerConfig.json";

            var loggerFactory = new LoggerFactory(StrategyMode.Live, logConfigFileName, RollingInterval.Month);
            Log.Logger = loggerFactory.GetLogger();
            var signalEventData = new SignalEventData(8898, "Long Short Portfolio", 339393, "Buy The Dip", "Long",
                "Signal Encountered", EventType.Execution.ToString(), EventSubType.Signal.ToString(), DateTime.Now,
                "ABC");
            var signalEventDataSerialized =  JsonConvert.SerializeObject(signalEventData);

            Log.Information(signalEventDataSerialized);
            Log.CloseAndFlush();

            var logFileName = logConfig.LiveTradingLogFile.Replace(".",
                DateTime.Now.ToString("yyyyMM") + ".");
            var logFileContents = LogFileHandler.Read(logConfig.Path, logFileName);

            LogFileHandler.Delete(logConfig.Path, logFileName);

            Assert.IsTrue(logFileContents.Contains(signalEventDataSerialized));
        }

        [TestMethod]
        public void StrategyModeIsBacktestAndUsingConfigfile_WorksCorrectly()
        {
            var logConfig = GetTestLogConfiguration();
            var logConfigFileName = "LoggerConfig.json";
            var loggerFactory = new LoggerFactory(StrategyMode.Backtest, logConfigFileName, RollingInterval.Month);
            Log.Logger = loggerFactory.GetLogger();
            var signalEventData = new SignalEventData(8898, "Long Short Portfolio", 339393, "Buy The Dip", "Long",
                "Signal Encountered", EventType.Execution.ToString(), EventSubType.Signal.ToString(), DateTime.Now,
                "ABC");
            var signalEventDataSerialized = JsonConvert.SerializeObject(signalEventData);

            Log.Information(signalEventDataSerialized);
            Log.CloseAndFlush();

            var logFileName = logConfig.BacktestLogFile.Replace(".",
                DateTime.Now.ToString("yyyyMM") + ".");
            var logFileContents = LogFileHandler.Read(logConfig.Path, logFileName);

            LogFileHandler.Delete(logConfig.Path, logFileName);

            Assert.IsTrue(logFileContents.Contains(signalEventDataSerialized));
        }

        [TestMethod]
        public void StrategyModeIsLiveAndNotUsingConfigfile_WorksCorrectly()
        {
            var logConfig = GetTestLogConfiguration();
            var loggerFactory = new LoggerFactory(StrategyMode.Live, logConfig, RollingInterval.Month);
            Log.Logger = loggerFactory.GetLogger();
            var signalEventData = new SignalEventData(8898, "Long Short Portfolio", 339393, "Buy The Dip", "Long",
                "Signal Encountered", EventType.Execution.ToString(), EventSubType.Signal.ToString(), DateTime.Now,
                "ABC");
            var signalEventDataSerialized = JsonConvert.SerializeObject(signalEventData);

            Log.Information(signalEventDataSerialized);
            Log.CloseAndFlush();

            var logFileName = logConfig.LiveTradingLogFile.Replace(".",
                DateTime.Now.ToString("yyyyMM") + ".");
            var logFileContents = LogFileHandler.Read(logConfig.Path, logFileName);

            LogFileHandler.Delete(logConfig.Path, logFileName);

            Assert.IsTrue(logFileContents.Contains(signalEventDataSerialized));
        }

        [TestMethod]
        public void StrategyModeIsBacktestAndNotUsingConfigfile_WorksCorrectly()
        {
            var logConfig = GetTestLogConfiguration();
            var loggerFactory = new LoggerFactory(StrategyMode.Backtest, logConfig, RollingInterval.Month);
            Log.Logger = loggerFactory.GetLogger();
            var signalEventData = new SignalEventData(8898, "Long Short Portfolio", 339393, "Buy The Dip", "Long",
                "Signal Encountered", EventType.Execution.ToString(), EventSubType.Signal.ToString(), DateTime.Now,
                "ABC");
            var signalEventDataSerialized = JsonConvert.SerializeObject(signalEventData);

            Log.Information(signalEventDataSerialized);
            Log.CloseAndFlush();

            var logFileName = logConfig.BacktestLogFile.Replace(".",
                DateTime.Now.ToString("yyyyMM") + ".");
            var logFileContents = LogFileHandler.Read(logConfig.Path, logFileName);

            LogFileHandler.Delete(logConfig.Path, logFileName);

            Assert.IsTrue(logFileContents.Contains(signalEventDataSerialized));
        }

        private LogConfiguration GetTestLogConfiguration()
        {
            return new LogConfiguration("c:\\Temp\\", "AlgoTerminalLive.log", "AlgoTerminalBacktest.log");
        }

    }
}
