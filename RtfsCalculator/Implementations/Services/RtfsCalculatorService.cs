using RtfsCalculator.Abstractions.Interfaces.Services;
using RtfsCalculator.Exceptions;
using System.Threading.Tasks;

namespace RtfsCalculator.Implementations.Services
{
    public class RtfsCalculatorService : IRtfsCalculatorService
    {
        const char Delimiter = ',';

        public async Task<string> HandleAddFunctionOfFormattedString(string formattedString)
        {            
            var inputValues = formattedString.Split(Delimiter);

            if (inputValues.Length > 2) { throw new InvalidInputException("Input cannot exceed 2 values."); }

            var result = 0;
            for (int i = 0; i < inputValues.Length; i++)
            {
                var currentValue = 0;
                int.TryParse(inputValues[i], out currentValue);
                result = result + currentValue;
            }

            return await Task.FromResult(result.ToString());
        }
    }
}
