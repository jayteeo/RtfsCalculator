using RtfsCalculator.Abstractions.Interfaces.Services;
using RtfsCalculator.Exceptions;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace RtfsCalculator.Implementations.Services
{
    public class RtfsCalculatorService : IRtfsCalculatorService
    {
        const char Delimiter = ',';

        public async Task<string> HandleAddFunctionOfFormattedString(string formattedString)
        {
            formattedString = formattedString.Replace(@"\n", ",");
            var inputValues = formattedString.Split(Delimiter).ToList();
            
            await ValidateInput(inputValues);
            
            var result = 0;
            
            for (int i = 0; i < inputValues.Count; i++)
            {
                var currentValue = 0;
                int.TryParse(inputValues[i], out currentValue);
                if (currentValue > 1000) { currentValue = 0; }
                result = result + currentValue;
            }

            return await Task.FromResult(result.ToString());
        }

        private async Task ValidateInput(List<string> inputValues)
        {
            var negativeInputValues = inputValues.Where(val =>
            {
                var inputValue = 0;
                int.TryParse(val, out inputValue);
                return inputValue < 0;
            });

            if (negativeInputValues.Any())
            {
                var negativeInputValuesToString = string.Join(", ", negativeInputValues);
                throw new InvalidInputException($"Negative values are not permitted: {negativeInputValuesToString}.");
            }
            
            await Task.CompletedTask;
        }
    }
}
