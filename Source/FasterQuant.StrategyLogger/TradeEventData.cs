using System;

namespace FasterQuant.StrategyLogger
{
    public class TradeEventData : StrategyEventData
    {
        public long TradeId { get; }
        public int TradeIndex { get; }
        public string Status { get; }
        public int Quantity { get; }
        public DateTime EntryDateTime { get; }
        public double EntryPrice { get; }
        public DateTime ExitDateTime { get; }
        public double ExitPrice { get; }
        public double TradeProfitLoss { get; }
        public double TradeProfitLossPercent { get; }
        public string Symbol { get; }

        public TradeEventData(long portfolioId, string portfolioName, long strategyId, string strategyName, string strategyTradeType, string message, string eventType, string eventSubType, long tradeId, int tradeIndex, string status, int quantity, DateTime entryDateTime, double entryPrice, string symbol) : base(portfolioId, portfolioName, strategyId, strategyName, strategyTradeType, message, eventType, eventSubType)
        {
            TradeId = tradeId;
            TradeIndex = tradeIndex;
            Status = status;
            Quantity = quantity;
            EntryDateTime = entryDateTime;
            EntryPrice = entryPrice;
            Symbol = symbol;
        }

        public TradeEventData(long portfolioId, string portfolioName, long strategyId, string strategyName, string strategyTradeType, string message, string eventType, string eventSubType, long tradeId, int tradeIndex, string status, int quantity, DateTime entryDateTime, double entryPrice, DateTime exitDateTime, double exitPrice, double tradeProfitLoss, double tradeProfitLossPercent, string symbol) : base(portfolioId, portfolioName, strategyId, strategyName, strategyTradeType, message, eventType, eventSubType)
        {
            TradeId = tradeId;
            TradeIndex = tradeIndex;
            Status = status;
            Quantity = quantity;
            EntryDateTime = entryDateTime;
            EntryPrice = entryPrice;
            ExitDateTime = exitDateTime;
            ExitPrice = exitPrice;
            TradeProfitLoss = tradeProfitLoss;
            TradeProfitLossPercent = tradeProfitLossPercent;
            Symbol = symbol;
        }
    }
}
