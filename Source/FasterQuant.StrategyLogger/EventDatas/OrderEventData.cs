using System;

namespace FasterQuant.StrategyLogger
{
    public class OrderEventData : StrategyEventData
    {
        public long OrderId { get; }
        public int OrderIndex { get; }
        public DateTime CreateDateTime { get; }
        public string Type { get; }
        public string Status { get; }
        public string OrderComment { get; }
        public int Quantity { get; }
        public DateTime FillDateTime { get; }
        public int FillQuantity { get; }
        public double FillPrice { get; }
        public string Symbol { get; }

        public OrderEventData(long portfolioId, string portfolioName, long strategyId, string strategyName, string strategyTradeType, string message, string eventType, string eventSubType, long orderId, int orderIndex, DateTime createDateTime, string type, string status, string orderComment, int quantity, string symbol) : base(portfolioId, portfolioName, strategyId, strategyName, strategyTradeType, message, eventType, eventSubType)
        {
            OrderId = orderId;
            OrderIndex = orderIndex;
            CreateDateTime = createDateTime;
            Type = type;
            Status = status;
            OrderComment = orderComment;
            Quantity = quantity;
            Symbol = symbol;
        }

        public OrderEventData(long portfolioId, string portfolioName, long strategyId, string strategyName, string strategyTradeType, string message, string eventType, string eventSubType, long orderId, int orderIndex, DateTime createDateTime, string type, string status, string orderComment, int quantity, DateTime fillDateTime, int fillQuantity, double fillPrice, string symbol) : base(portfolioId, portfolioName, strategyId, strategyName, strategyTradeType, message, eventType, eventSubType)
        {
            OrderId = orderId;
            OrderIndex = orderIndex;
            CreateDateTime = createDateTime;
            Type = type;
            Status = status;
            OrderComment = orderComment;
            Quantity = quantity;
            FillDateTime = fillDateTime;
            FillQuantity = fillQuantity;
            FillPrice = fillPrice;
            Symbol = symbol;
        }
    }
}
