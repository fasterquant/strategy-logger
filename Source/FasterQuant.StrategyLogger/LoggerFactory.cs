using System.IO;
using System;
using Serilog;
using System.Reflection;
using Newtonsoft.Json;

namespace FasterQuant.StrategyLogger
{
    public class LoggerFactory
    {
        private readonly LogConfiguration _logConfig;
        private readonly string _logPath;
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

        public ILogger GetLogger()
        {
            return new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(this._logPath, rollingInterval: this._rollingInterval)
                    .CreateLogger();
        }
        
        private string GetLogPath(StrategyMode strategyMode, LogConfiguration logConfig)
        {
            var logFile = strategyMode == StrategyMode.Live ? this._logConfig.LiveTradingLogFile : this._logConfig.BacktestLogFile;
            return Path.Combine(this._logConfig.Path, logFile);
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
