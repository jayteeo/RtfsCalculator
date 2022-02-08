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
        public async Task HandleAddFunctionOfFormattedText_Test(string formattedText,
            string expectedResult)
        {
            var result = await _rtfsCalculatorService.HandleAddFunctionOfFormattedString(formattedText);
            Assert.That(result == expectedResult);
        }
        
        [Test]
        public void HandleAddFunctionOfFormattedText_ThrowsWhenInputExceedsTwoValues()
        {
            Assert.ThrowsAsync<InvalidInputException>(new AsyncTestDelegate(AddFormattedStringWithThreeValues));
        }

        public async Task AddFormattedStringWithThreeValues()
        {
            await _rtfsCalculatorService.HandleAddFunctionOfFormattedString("1,2,3");
        }
    }
}
