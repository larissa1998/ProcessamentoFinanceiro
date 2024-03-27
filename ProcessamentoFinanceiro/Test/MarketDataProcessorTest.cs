using FinancialProcessing.Data;
using FinancialProcessing.Processing;
using NUnit.Framework;

namespace FinancialProcessing.Test
{
    [TestFixture]
    public class MarketDataProcessorTest
    {
        public void CalculateMovingAverage_ValidData_ReturnsCorrectValue()
        {
            // Arrange
            var processor = new MarketDataProcessor();
            var testData = new MarketData[]
            {
                new MarketData { ClosePrice = 10.0 },
                new MarketData { ClosePrice = 12.0 },
                new MarketData { ClosePrice = 14.0 },
                new MarketData { ClosePrice = 16.0 },
                new MarketData { ClosePrice = 18.0 }
            };

            // Act
            var result = processor.CalculateMovingAverage(testData);

            // Assert
            double expectedMovingAverage = (10.0 + 12.0 + 14.0 + 16.0 + 18.0) / 5;
            Assert.Equals(expectedMovingAverage, result);
        }

        [Test]
        public void CalculateMACD_ValidData_ReturnsCorrectValue()
        {
            // Arrange
            var processor = new MarketDataProcessor();
            var testData = new MarketData[]
            {
                new MarketData { ClosePrice = 10.0 },
                new MarketData { ClosePrice = 12.0 },
                new MarketData { ClosePrice = 14.0 },
                new MarketData { ClosePrice = 16.0 },
                new MarketData { ClosePrice = 18.0 }
            };

            // Act
            var result = processor.CalculateMACD(testData);

            // Assert
            double expectedMACD = 2.0;
            Assert.Equals(expectedMACD, result);
        }
    }
}