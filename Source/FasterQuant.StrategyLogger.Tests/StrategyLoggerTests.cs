using System;
using Serilog;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            var signalEventData = new SignalEventData(8898, "Long Short Portfolio", 123, "Buy The Dip", "Long",
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
        public void StrategyModeIsBacktestForMultipleLogFilesAndNotUsingConfigfile_WorksCorrectly()
        {
            var logConfig = GetTestLogConfigurationForMultipleLogFiles();
            var identifierPlaceHolder = "{strategyId}";
            var strategyId1 = 123;
            var strategyId2 = 456;
            var identifierValues = new List<string>() { strategyId1.ToString(), strategyId2.ToString() };
            var searchStringTemplate = ":" + identifierPlaceHolder + ",";

            var loggerFactory = new LoggerFactory(StrategyMode.Backtest, logConfig, RollingInterval.Month, identifierPlaceHolder, searchStringTemplate, identifierValues);
            Log.Logger = loggerFactory.GetLogger();
            var signalEventData = new SignalEventData(8898, "Long Short Portfolio", strategyId1, "Buy The Dip", "Long",
                "Signal Encountered", EventType.Execution.ToString(), EventSubType.Signal.ToString(), DateTime.Now,
                "ABC");
            var signalEventData2 = new SignalEventData(8898, "Long Short Portfolio", strategyId2, "Buy The Dip", "Long",
               "Signal Encountered", EventType.Execution.ToString(), EventSubType.Signal.ToString(), DateTime.Now,
               "ABC");
            var signalEventDataSerialized = JsonConvert.SerializeObject(signalEventData);
            var signalEventDataSerialized2 = JsonConvert.SerializeObject(signalEventData2);

            Log.Information(signalEventDataSerialized);
            Log.Information(signalEventDataSerialized2);
            Log.CloseAndFlush();

            var logFileName = logConfig.BacktestLogFile.Replace(".",
                DateTime.Now.ToString("yyyyMM") + ".").Replace(identifierPlaceHolder, strategyId1.ToString());
            var logFileContents = LogFileHandler.Read(logConfig.Path, logFileName);
            LogFileHandler.Delete(logConfig.Path, logFileName);

            var logFileName2 = logConfig.BacktestLogFile.Replace(".",
                DateTime.Now.ToString("yyyyMM") + ".").Replace(identifierPlaceHolder, strategyId2.ToString());
            var logFileContents2 = LogFileHandler.Read(logConfig.Path, logFileName2);
            LogFileHandler.Delete(logConfig.Path, logFileName2);

            Assert.IsTrue(logFileContents.Contains(signalEventDataSerialized));
            Assert.IsTrue(logFileContents2.Contains(signalEventDataSerialized2));
        }

        private LogConfiguration GetTestLogConfiguration()
        {
            return new LogConfiguration("c:\\Temp\\", "AlgoTerminalLive.log", "AlgoTerminalBacktest.log");
        }

        private LogConfiguration GetTestLogConfigurationForMultipleLogFiles()
        {
            return new LogConfiguration("c:\\Temp\\", "AlgoTerminalLive{strategyId}.log", "AlgoTerminalBacktest{strategyId}.log");
        }

    }
}
