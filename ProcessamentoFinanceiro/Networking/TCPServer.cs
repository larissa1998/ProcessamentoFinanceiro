using FinancialProcessing.Data;
using FinancialProcessing.Processing;
using FinancialProcessing.Serialization;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace FinancialProcessing.Networking
{
    public class TCPServer
    {
        private const int Port = 8888;
        private readonly TcpListener _listener;
        private readonly MarketDataProcessor _dataProcessor;
        private readonly MarketDataSerializer _dataSerializer;

        public TCPServer()
        {
            _listener = new TcpListener(System.Net.IPAddress.Any, Port);
            _dataProcessor = new MarketDataProcessor();
            _dataSerializer = new MarketDataSerializer();
        }

        public async Task StartAsync()
        {
            _listener.Start();
            Console.WriteLine("Servidor TCP iniciado...");

            while (true)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();
                Console.WriteLine("Cliente conectado.");

                await Task.Run(() => HandleClientAsync(client));
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using (var stream = client.GetStream())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        sb.AppendLine(line);
                    }
                    string jsonData = sb.ToString();

                    Console.WriteLine($"Dados recebidos do cliente: {jsonData}");

                    MarketData marketData = JsonConvert.DeserializeObject<MarketData>(jsonData);
                    ProcessedMarketData processedData = await _dataProcessor.ProcessMarketDataAsync(marketData);

                    string processedJsonData = _dataSerializer.SerializeProcessedData(processedData);
                    await writer.WriteLineAsync(processedJsonData);
                    await writer.FlushAsync();
                    Console.WriteLine("Dados processados enviados ao cliente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao lidar com o cliente: {ex.Message}");
                }
                finally
                {
                    client.Close();
                }
            }
        }
    }
}
