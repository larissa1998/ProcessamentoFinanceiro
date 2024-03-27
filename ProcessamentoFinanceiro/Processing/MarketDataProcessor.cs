using FinancialProcessing.Data;

namespace FinancialProcessing.Processing
{
    public class MarketDataProcessor
    {
        public async Task<ProcessedMarketData> ProcessMarketDataAsync(MarketData marketData)
        {
            try
            {
                MarketData[] marketDataArray = new MarketData[] { marketData };
                return await ProcessMarketDataAsync(marketDataArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar dados do mercado: {ex.Message}");
                throw;
            }
        }

        public async Task<ProcessedMarketData> ProcessMarketDataAsync(MarketData[] marketDataArray)
        {
            try
            {
                Task<double> movingAverageTask = Task.Run(() => CalculateMovingAverage(marketDataArray));
                Task<double> macdTask = Task.Run(() => CalculateMACD(marketDataArray));

                await Task.WhenAll(movingAverageTask, macdTask);

                double movingAverage = await movingAverageTask;
                double macd = await macdTask;

                MarketData marketData = marketDataArray.FirstOrDefault();

                return new ProcessedMarketData
                {
                    Timestamp = marketData.Timestamp,
                    StockSymbol = marketData.StockSymbol,
                    MovingAverage = movingAverage,
                    MACD = macd
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar dados do mercado: {ex.Message}");
                throw;
            }
        }
        public double CalculateMovingAverage(MarketData[] marketDataArray)
        {
            if (marketDataArray.Length == 0)
            {
                throw new ArgumentException("O array de dados de mercado está vazio.");
            }

            double totalSum = 0;

            foreach (MarketData data in marketDataArray)
            {
                totalSum += data.ClosePrice;
            }

            return totalSum / marketDataArray.Length;
        }

        public double CalculateMACD(MarketData[] marketDataArray)
        {
            int shortTermPeriod = 12;
            int longTermPeriod = 26;
            int signalLinePeriod = 9;

            Task<double> shortTermEMATask = Task.Run(() => CalculateEMA(marketDataArray, shortTermPeriod));
            Task<double> longTermEMATask = Task.Run(() => CalculateEMA(marketDataArray, longTermPeriod));

            Task.WaitAll(shortTermEMATask, longTermEMATask);

            double shortTermEMA = shortTermEMATask.Result;
            double longTermEMA = longTermEMATask.Result;

            double macd = shortTermEMA - longTermEMA;

            return macd;
        }

        private double CalculateEMA(MarketData[] marketDataArray, int period)
        {
            if (marketDataArray.Length < period)
            {
                throw new ArgumentException("Não há dados suficientes para calcular a média móvel exponencial.");
            }

            double multiplier = 2.0 / (period + 1);
            double ema = marketDataArray[0].ClosePrice;

            for (int i = 1; i < period; i++)
            {
                ema = (marketDataArray[i].ClosePrice - ema) * multiplier + ema;
            }

            return ema;
        }
    }
}
