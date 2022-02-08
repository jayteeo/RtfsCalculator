using NUnit.Framework;
using RtfsCalculator.Abstractions.Interfaces.Services;
using RtfsCalculator.Exceptions;
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
        [TestCase("", "0")]
        [TestCase("1,2,3,4,5,6,7,8,9,10,11,12", "78")]
        [TestCase(@"1\n2,3", "6")]
        public async Task HandleAddFunctionOfFormattedText_Test(string formattedText,
            string expectedResult)
        {
            var result = await _rtfsCalculatorService.HandleAddFunctionOfFormattedString(formattedText);
            Assert.That(result == expectedResult);
        }

        [Test]
        public void HandleAddFunctionOfFormattedText_ThrowsInvalidInputExceptionWhenContainsNegativeValues()
        {
            Assert.ThrowsAsync<InvalidInputException>(new AsyncTestDelegate(AddFormattedTextContainingNegativeValues));
        }

        public async Task AddFormattedTextContainingNegativeValues()
        {
            await _rtfsCalculatorService.HandleAddFunctionOfFormattedString("1,2,-5,-2");
        }
    }
}
