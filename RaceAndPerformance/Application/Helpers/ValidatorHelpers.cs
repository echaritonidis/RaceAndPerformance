using System;
using System.Globalization;

namespace RaceAndPerformance.Application.Helpers
{
    public static class ValidatorHelpers
    {
        public static bool IsValidDate(string date) => DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        public static bool IsValidTime(string time) => DateTime.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
