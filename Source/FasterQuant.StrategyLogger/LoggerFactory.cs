using System.IO;
using System;
using Serilog;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FasterQuant.StrategyLogger
{
    public class LoggerFactory
    {
        private readonly LogConfiguration _logConfig;
        private readonly string _logPath;
        private readonly string _identifierPlaceHolder;
        private readonly List<string> _indentifierValues;
        private readonly string _searchStringTemplate;
        private readonly RollingInterval _rollingInterval;

        public LoggerFactory(StrategyMode strategyMode, string logConfigFileName, RollingInterval rollingInterval)
        {
            var config = ReadConfig(GetConfigPath(), logConfigFileName);
            this._logConfig = JsonConvert.DeserializeObject<LogConfiguration>(config);
            this._logPath = GetLogPath(strategyMode, this._logConfig);
            this._rollingInterval = rollingInterval;
        }


        public LoggerFactory(StrategyMode strategyMode, LogConfiguration logConfig, RollingInterval rollingInterval)
        {
            this._logConfig = logConfig;
            this._logPath = GetLogPath(strategyMode, logConfig);
            this._rollingInterval = rollingInterval;
        }

        public LoggerFactory(StrategyMode strategyMode, string logConfigFileName, RollingInterval rollingInterval, string identifierPlaceHolder, string searchStringTemplate, List<string> indentifierValues)
        {
            var config = ReadConfig(GetConfigPath(), logConfigFileName);
            this._logConfig = JsonConvert.DeserializeObject<LogConfiguration>(config);
            this._logPath = GetLogPath(strategyMode, this._logConfig);
            this._rollingInterval = rollingInterval;

            this._identifierPlaceHolder = identifierPlaceHolder;
            this._searchStringTemplate = searchStringTemplate;
            this._indentifierValues = indentifierValues;
        }

        public LoggerFactory(StrategyMode strategyMode, LogConfiguration logConfig, RollingInterval rollingInterval, string identifierPlaceHolder, string searchStringTemplate, List<string> indentifierValues)
        {
            this._logConfig = logConfig;
            this._logPath = GetLogPath(strategyMode, logConfig);
            this._rollingInterval = rollingInterval;

            this._identifierPlaceHolder = identifierPlaceHolder;
            this._searchStringTemplate = searchStringTemplate;
            this._indentifierValues = indentifierValues;
        }

        public ILogger GetLogger()
        {

            if (string.IsNullOrEmpty(this._identifierPlaceHolder))
            {
                return GetLoggerWithASingleFileSync();
            }

            return GetLoggerWithMultipleFilesSync();
        }

        private ILogger GetLoggerWithASingleFileSync()
        {
            return new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(this._logPath, rollingInterval: this._rollingInterval)
                    .CreateLogger();
        }

        private ILogger GetLoggerWithMultipleFilesSync()
        {
            var lc = new LoggerConfiguration();
            foreach (var v in this._indentifierValues)
            {

                var searchString = this._searchStringTemplate.Replace(this._identifierPlaceHolder, v);
                var path = this._logPath.Replace(this._identifierPlaceHolder, v);
                lc.WriteTo.Logger(x =>
                {
                    x.WriteTo.File(path, rollingInterval: this._rollingInterval);
                    x.Filter.ByIncludingOnly(e => e.RenderMessage().Contains(searchString));
                });
            }

            return lc.MinimumLevel.Debug()
                   .CreateLogger();
        }


        private string GetLogPath(StrategyMode strategyMode, LogConfiguration logConfig)
        {
            var logFile = strategyMode == StrategyMode.Live ? logConfig.LiveTradingLogFile :logConfig.BacktestLogFile;
            return Path.Combine(logConfig.Path, logFile);
        }

        private string GetConfigPath()
        {
            var ass = Assembly.GetExecutingAssembly();
            return new Uri(ass.CodeBase).LocalPath.ToLower().Replace((Assembly.GetExecutingAssembly().GetName().Name).ToLower() + ".dll", "");
        }

        private string ReadConfig(string path, string logConfigFileName)
        {
            var config = "";
            var s = "";
            var fullPath = Path.Combine(path, logConfigFileName);
            if (!File.Exists(fullPath))
            {
                throw new Exception("The following path does not exist: " + fullPath + ".  Please make sure " + logConfigFileName + " is deployed with " + Assembly.GetExecutingAssembly().GetName() + ".");
            }

            using (StreamReader sr = File.OpenText(fullPath))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    config += s;
                }
            }

            return config;
        }
    }
}
