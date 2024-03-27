using FinancialProcessing.Networking;

namespace FinancialProcessing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var server = new TCPServer();
                await server.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao iniciar o servidor: {ex.Message}");
            }
        }
    }
}