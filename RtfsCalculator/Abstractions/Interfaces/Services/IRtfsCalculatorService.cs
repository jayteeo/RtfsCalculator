using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RtfsCalculator.Abstractions.Interfaces.Services
{
    public interface IRtfsCalculatorService
    {
        Task<string> HandleAddFunctionOfFormattedString(string formattedString);
    }
}
