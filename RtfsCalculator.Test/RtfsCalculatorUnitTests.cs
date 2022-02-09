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
        [TestCase("1,5000", "1")]
        [TestCase("", "0")]
        [TestCase("1,2,3,4,5,6,7,8,9,10,11,12", "78")]
        [TestCase(@"1\n2,3", "6")]
        [TestCase("2,1001,6", "8")]
        [TestCase(@"//[***]\n11***22***33", "66")]
        [TestCase(@"//[*][!!][r9r]\n11r9r22*hh*33!!44", "110")]
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
