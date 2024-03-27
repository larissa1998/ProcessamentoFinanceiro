namespace FinancialProcessing.Data
{
    public class MarketData
    {
        public DateTime Timestamp { get; set; }
        public string? Interval { get; set; }
        public string? StockSymbol { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public double Volume { get; set; }
    }
}
