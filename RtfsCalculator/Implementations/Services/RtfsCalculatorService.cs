using RtfsCalculator.Abstractions.Interfaces.Services;
using RtfsCalculator.Exceptions;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RtfsCalculator.Implementations.Services
{
    public class RtfsCalculatorService : IRtfsCalculatorService
    {
        const char CommaDelimiter = ',';
        const string NewLineDelimiter = @"\n";

        public async Task<string> HandleAddFunctionOfFormattedString(string formattedString)
        {
            if (!RtfsCalculator.Continue) { return string.Empty; }
            var customDelimiters = await GetCustomStringDelimiters(formattedString);
            if (customDelimiters != null)
            {
                foreach(var customDelimiter in customDelimiters)
                {
                    formattedString = formattedString.Replace(customDelimiter, ",");
                }
            }
            formattedString = formattedString.Replace(NewLineDelimiter, ",");
            var inputValues = formattedString.Split(CommaDelimiter).ToList();
            
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

        private async Task<List<string>> GetCustomStringDelimiters(string formattedString)
        {
            var lastIndexOfClosingBracket = formattedString.LastIndexOf("]");
            if (lastIndexOfClosingBracket > -1 && formattedString.StartsWith("//"))
            {
                var formattedStringDelimitersSection = formattedString.Substring(0, lastIndexOfClosingBracket + 1);
                var matches = new Regex(@"(?<=\[)(.*?)(?=\])").Matches(formattedStringDelimitersSection);
                return await Task.FromResult(matches.Select(match => match.Value).ToList());
            }
            return null;
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
