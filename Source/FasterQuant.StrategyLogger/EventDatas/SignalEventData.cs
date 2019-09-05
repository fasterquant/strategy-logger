using System;

namespace FasterQuant.StrategyLogger
{
    public class SignalEventData : StrategyEventData
    {
        public DateTime SignalDateTime { get; }
        public string Symbol { get; }

        public SignalEventData(long portfolioId, string portfolioName, long strategyId, string strategyName, string strategyTradeType, string message, string eventType, string eventSubType,  DateTime signalDateTime, string symbol) : base(portfolioId, portfolioName, strategyId, strategyName, strategyTradeType, message, eventType, eventSubType)
        { 
            SignalDateTime = signalDateTime;
            Symbol = symbol;
        }
    }
}
