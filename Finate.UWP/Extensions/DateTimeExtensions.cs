using System;

namespace Finate.UWP.Extensions
{
    /// <summary>
    /// The extensions for <see cref="DateTime"/> class.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets the index of the day of week.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="startingMonday">Value indicating whether week is starting with monday or sunday.</param>
        /// <returns>Returns zero-based index of the day of the week. </returns>
        public static int GetDayOfWeekIndex(this DateTime date, bool startingMonday)
        {
            if (startingMonday)
            {
                if (date.Date.DayOfWeek == DayOfWeek.Sunday)
                    return 6;
                return (int) date.Date.DayOfWeek - 1;
            }
            return (int)date.Date.DayOfWeek;
        }
    }
}
