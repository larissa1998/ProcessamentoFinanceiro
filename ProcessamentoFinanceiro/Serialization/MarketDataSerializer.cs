using FinancialProcessing.Data;
using Newtonsoft.Json;

namespace FinancialProcessing.Serialization
{
    public class MarketDataSerializer
    {
        public string SerializeProcessedData(ProcessedMarketData processedData)
        {
            return JsonConvert.SerializeObject(processedData);
        }

        public MarketData DeserializeMarketData(string jsonData)
        {
            return JsonConvert.DeserializeObject<MarketData>(jsonData);
        }
    }
}
