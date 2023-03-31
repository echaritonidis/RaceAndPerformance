using System;
using System.Collections.Generic;
using System.Text;

namespace RaceAndPerformance.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException(Dictionary<string, string[]> errors)
        {
            Errors = errors;
        }

        public Dictionary<string, string[]> Errors { get; }

        public override string Message
        {
            get
            {
                var errorMessageBuilder = new StringBuilder();

                errorMessageBuilder.AppendLine("Validation failed:");

                foreach (var error in Errors)
                {
                    var propertyName = error.Key;
                    var errorMessages = error.Value;
                    errorMessageBuilder.AppendLine($"- {propertyName}: {string.Join(", ", errorMessages)}");
                }

                return errorMessageBuilder.ToString();
            }
        }
    }
}
