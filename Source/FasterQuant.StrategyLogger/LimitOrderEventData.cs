using System;

namespace FasterQuant.StrategyLogger
{
    public class LimitOrderEventData : OrderEventData
    { 
        public double LimitPrice { get; }

        public LimitOrderEventData(long portfolioId, string portfolioName, long strategyId, string strategyName, string strategyTradeType, string message, string eventType, string eventSubType, long orderId, int orderIndex, DateTime createDateTime, string type, string status, string orderComment, int quantity, double limitPrice, string symbol) : base(portfolioId, portfolioName, strategyId, strategyName, strategyTradeType, message, eventType, eventSubType, orderId, orderIndex, createDateTime, type, status, orderComment, quantity, symbol)
        {
            LimitPrice = limitPrice;
        }

        public LimitOrderEventData(long portfolioId, string portfolioName, long strategyId, string strategyName, string strategyTradeType, string message, string eventType, string eventSubType, long orderId, int orderIndex, DateTime createDateTime, string type, string status, string orderComment, int quantity, double limitPrice, DateTime fillDateTime, int fillQuantity, double fillPrice, string symbol) : base(portfolioId, portfolioName, strategyId, strategyName, strategyTradeType, message, eventType, eventSubType, orderId, orderIndex, createDateTime, type, status, orderComment, quantity, fillDateTime, fillQuantity, fillPrice, symbol)
        {
            LimitPrice = limitPrice;
        }
    }
}
