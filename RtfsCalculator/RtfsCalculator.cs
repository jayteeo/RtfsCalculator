using RtfsCalculator.Abstractions.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace RtfsCalculator
{
    public class RtfsCalculator
    {
        private readonly IRtfsCalculatorService _rtfsCalculatorService;
        public static volatile bool Continue = true;
        public RtfsCalculator(IRtfsCalculatorService rtfsCalculatorService)
        {
            _rtfsCalculatorService = rtfsCalculatorService;
        }
        
        public async Task Run()
        {
            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                Continue = false;
            };

            Console.Write("Enter formatted string to calculate: ");

            while (Continue)
            {
                var input = Console.ReadLine();
                Console.WriteLine(await _rtfsCalculatorService.HandleAddFunctionOfFormattedString(input));
                Console.Write("Enter formatted string to calculate: ");
            }
        }
    }
}
