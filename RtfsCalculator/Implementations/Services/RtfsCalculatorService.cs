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
            formattedString = formattedString.Replace(@"\n", ",");
            var inputValues = formattedString.Split(Delimiter);

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
