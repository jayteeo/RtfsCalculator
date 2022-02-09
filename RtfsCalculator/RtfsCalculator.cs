using RtfsCalculator.Abstractions.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace RtfsCalculator
{
    public class RtfsCalculator
    {
        private readonly IRtfsCalculatorService _rtfsCalculatorService;
        private bool _continue = true;
        public RtfsCalculator(IRtfsCalculatorService rtfsCalculatorService)
        {
            _rtfsCalculatorService = rtfsCalculatorService;
        }
        
        public async Task Run()
        {
            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                _continue = false;
            };
                
            while (_continue)
            {
                Console.Write("Enter formatted string to calculate: ");
                var input = Console.ReadLine();
                Console.WriteLine(await _rtfsCalculatorService.HandleAddFunctionOfFormattedString(input));
            }
        }
    }
}
