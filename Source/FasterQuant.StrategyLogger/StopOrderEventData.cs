using System;

namespace FasterQuant.StrategyLogger
{
    public class StopOrderEventData : OrderEventData
    {
        public double StopPrice { get; }

        public StopOrderEventData(long portfolioId, string portfolioName, long strategyId, string strategyName, string strategyTradeType, string message, string eventType, string eventSubType, long orderId, int orderIndex, DateTime createDateTime, string type, string status, string orderComment, int quantity, double stopPrice, string symbol) : base(portfolioId, portfolioName, strategyId, strategyName, strategyTradeType, message, eventType, eventSubType, orderId, orderIndex, createDateTime, type, status, orderComment, quantity, symbol)
        {
            StopPrice = stopPrice;
        }

        public StopOrderEventData(long portfolioId, string portfolioName, long strategyId, string strategyName, string strategyTradeType, string message, string eventType, string eventSubType, long orderId, int orderIndex, DateTime createDateTime, string type, string status, string orderComment, int quantity, double stopPrice, DateTime fillDateTime, int fillQuantity, double fillPrice, string symbol) : base(portfolioId, portfolioName, strategyId, strategyName, strategyTradeType, message, eventType, eventSubType, orderId, orderIndex, createDateTime, type, status, orderComment, quantity, fillDateTime, fillQuantity, fillPrice, symbol)
        {
            StopPrice = stopPrice;
        }
    }
}
