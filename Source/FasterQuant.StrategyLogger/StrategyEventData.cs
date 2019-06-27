
namespace FasterQuant.StrategyLogger
{
    public class StrategyEventData
    {
        public long PortfolioId;
        public string PortfolioName;
        public long StrategyId;
        public string StrategyName;
        public string StrategyTradeType;
        public string Message;
        public string EventType;
        public string EventSubType;

        public StrategyEventData(long portfolioId, string portfolioName, long strategyId, string strategyName, string strategyTradeType, string message, string eventType, string eventSubType)
        {
            PortfolioId = portfolioId;
            PortfolioName = portfolioName;
            StrategyId = strategyId;
            StrategyName = strategyName;
            StrategyTradeType = strategyTradeType;
            Message = message; 
            EventType = eventType;
            EventSubType = eventSubType;
        }
    }
}
