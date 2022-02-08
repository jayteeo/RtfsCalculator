using System;

namespace RtfsCalculator.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message)
            : base(message) { }
    }
}
