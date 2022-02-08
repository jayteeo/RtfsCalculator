using NUnit.Framework;
using RtfsCalculator.Abstractions.Interfaces.Services;
using RtfsCalculator.Implementations.Services;
using System.Threading.Tasks;

namespace RtfsCalculator.Test
{
    [TestFixture]
    public class RtfsCalculatorUnitTests
    {
        private IRtfsCalculatorService _rtfsCalculatorService;
        [SetUp]
        public void Setup()
        {
            _rtfsCalculatorService = new RtfsCalculatorService();
        }

        [TestCase("20", "20")]
        [TestCase("1,5000", "5001")]
        [TestCase("4,-3", "1")]
        [TestCase("", "0")]
        [TestCase("1,2,3,4,5,6,7,8,9,10,11,12", "78")]
        public async Task HandleAddFunctionOfFormattedText_Test(string formattedText,
            string expectedResult)
        {
            var result = await _rtfsCalculatorService.HandleAddFunctionOfFormattedString(formattedText);
            Assert.That(result == expectedResult);
        }
    }
}