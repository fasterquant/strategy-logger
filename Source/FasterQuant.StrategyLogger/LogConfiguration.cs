
namespace FasterQuant.StrategyLogger
{
    public class LogConfiguration
    {
        public string Path;
        public string LiveTradingLogFile;
        public string BacktestLogFile;

        public LogConfiguration(string path, string liveTradingLogFile, string backtestLogFile)
        {
            this.Path = path;
            this.LiveTradingLogFile = liveTradingLogFile;
            this.BacktestLogFile = backtestLogFile;
        }
    }
}
