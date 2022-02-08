using RtfsCalculator.Abstractions.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace RtfsCalculator
{
    public class RtfsCalculator
    {
        private readonly IRtfsCalculatorService _rtfsCalculatorService;
        public RtfsCalculator(IRtfsCalculatorService rtfsCalculatorService)
        {
            _rtfsCalculatorService = rtfsCalculatorService;
        }
        
        public async Task Run()
        {
            while (true)
            {
                Console.Write("Enter formatted string to calculate: ");
                var input = Console.ReadLine();
                Console.WriteLine(await _rtfsCalculatorService.HandleAddFunctionOfFormattedString(input));
            }
        }
    }
}
