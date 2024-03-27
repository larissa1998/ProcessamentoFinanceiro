namespace FinancialProcessing.Data
{
    public class ProcessedMarketData
    {
        public DateTime Timestamp { get; set; }
        public string? StockSymbol { get; set; }
        public double MovingAverage { get; set; }
        public double MACD { get; set; }
    }
}
