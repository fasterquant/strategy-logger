using System.IO;
using System;
using Serilog;
using System.Reflection;
using Newtonsoft.Json;

namespace FasterQuant.StrategyLogger
{
    public class LoggerFactory
    {
        private string _logConfigFile = "LoggerConfig.json";
        private LogConfiguration _logConfig;
        private string _logPath;

        public LoggerFactory(string strategyMode)
        {
            var configPath = Path.Combine(getConfigPath(), _logConfigFile);
            this._logConfig = JsonConvert.DeserializeObject<LogConfiguration>(readConfig(configPath));
            this._logPath = getLogPath(strategyMode, this._logConfig);
        }

        public LoggerFactory(string strategyMode, LogConfiguration logConfig)
        {
            this._logConfig = logConfig;
            this._logPath = getLogPath(strategyMode, logConfig); 
        }

        public ILogger GetLogger()
        {
            return new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(this._logPath, rollingInterval: RollingInterval.Month)
                    .CreateLogger();
        }
        
        private string getLogPath(string strategyMode, LogConfiguration logConfig)
        {
            var logFile = strategyMode.ToUpper() == "LIVE" ? this._logConfig.LiveTradingLogFile : this._logConfig.BacktestLogFile;
            return Path.Combine(this._logConfig.Path, logFile);
        }

        private string getConfigPath()
        {
            var ass = Assembly.GetExecutingAssembly();
            return new Uri(ass.CodeBase).LocalPath.ToLower().Replace((Assembly.GetExecutingAssembly().GetName().Name).ToLower() + ".dll", "");
        }

        private string readConfig(string path)
        {
            var config = "";
            var s = "";
            if (!File.Exists(path))
            {
                throw new Exception("The following path does not exist: " + path + ".  Please make sure " + _logConfigFile + " is deployed with " + Assembly.GetExecutingAssembly().GetName() + ".");
            }

            using (StreamReader sr = File.OpenText(path))
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
