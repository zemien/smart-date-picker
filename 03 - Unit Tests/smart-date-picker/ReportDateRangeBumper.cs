using System;

namespace smart_date_picker
{
    /// <summary>
    /// Helper class to bump dates
    /// </summary>
    public static class ReportDateRangeBumper
    {
        /// <summary>
        /// Bumps the given start and end dates into a valid data report range
        /// </summary>
        /// <param name="start">Selected start date</param>
        /// <param name="end">Selected end date</param>
        /// <param name="dataAvailability">Dates the system has data for</param>
        /// <param name="periodType">Period Type</param>
        /// <param name="datePriority">Which date to prioritise maintaining</param>
        /// <returns></returns>
        public static DateRange BumpDates(DateTime start, DateTime end, DateRange dataAvailability, PeriodType periodType, DatePriority datePriority = DatePriority.StartDate)
        {
            var constrainedDataAvailability = GetConstrainedDataAvailability(dataAvailability, periodType);

            //Abort efforts if dataAvailability won't fit in our period at all
            if (constrainedDataAvailability == null)
                return dataAvailability;

            //Determines the starting point pre-bumping, based on which date has priority (i.e. which one the user picked to change)
            var finalStart = (datePriority == DatePriority.StartDate) ? start : (start > end) ? end : start;
            var finalEnd = (datePriority == DatePriority.EndDate) ? end : (end < start) ? start : end;

            finalStart = finalStart.ToStart(periodType);
            finalEnd = finalEnd.ToEnd(periodType);

            if (finalStart > constrainedDataAvailability.Value.EndDate)
            {
                finalStart = constrainedDataAvailability.Value.EndDate.ToStart(periodType);
                finalEnd = constrainedDataAvailability.Value.EndDate;
            }

            if (finalEnd < constrainedDataAvailability.Value.StartDate)
            {
                finalStart = constrainedDataAvailability.Value.StartDate;
                finalEnd = constrainedDataAvailability.Value.StartDate.ToEnd(periodType);
            }

            if (finalStart < constrainedDataAvailability.Value.StartDate)
            {
                finalStart = constrainedDataAvailability.Value.StartDate;
            }

            if (finalEnd > constrainedDataAvailability.Value.EndDate)
            {
                finalEnd = constrainedDataAvailability.Value.EndDate;
            }

            return new DateRange(finalStart, finalEnd);
        }

        public static DateRange? GetConstrainedDataAvailability(DateRange dataAvailability, PeriodType periodType)
        {
            var constrainedStartDate = dataAvailability.StartDate.ToStart(periodType);
            var constrainedEndDate = dataAvailability.EndDate.ToEnd(periodType);

            if (constrainedStartDate < dataAvailability.StartDate)
            {
                //To the next period
                constrainedStartDate = dataAvailability.StartDate.ToEnd(periodType).AddDays(1);
            }

            if (constrainedEndDate > dataAvailability.EndDate)
            {
                //To the previous period
                constrainedEndDate = dataAvailability.EndDate.ToStart(periodType).AddDays(-1);
            }

            if (constrainedStartDate > constrainedEndDate)
            {
                //There is no hope of bumping to a valid range
                return null;
            }

            return new DateRange(constrainedStartDate, constrainedEndDate);
        }

        /// <summary>
        /// Extension method for date time to get the start of a particular period type.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="periodType"></param>
        /// <returns></returns>
        public static DateTime ToStart(this DateTime date, PeriodType periodType)
        {
            switch (periodType)
            {
                case PeriodType.Weeks:
                    return date.AddDays(-1 * (int)date.DayOfWeek);

                case PeriodType.Months:
                    return new DateTime(date.Year, date.Month, 1);

                case PeriodType.Quarters:
                    for (int month = 4; month <= 13; month += 3)
                    {
                        if (date.Month < month)
                        {
                            return new DateTime(date.Year, month - 3, 1);
                        }
                    }
                    return DateTime.MinValue;

                default:
                    return date;
            }
        }

        /// <summary>
        /// Extension method for DateTime to get the end of a particular period type.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="periodType"></param>
        /// <returns></returns>
        public static DateTime ToEnd(this DateTime date, PeriodType periodType)
        {
            switch (periodType)
            {
                case PeriodType.Weeks:
                    var difference = DayOfWeek.Saturday - date.DayOfWeek;
                    return date.AddDays(difference);

                case PeriodType.Months:
                    return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

                case PeriodType.Quarters:
                    for (int month = 10; month >= 1; month -= 3)
                    {
                        if (date.Month >= month)
                        {
                            var newMonth = month + 2;
                            return new DateTime(date.Year, newMonth, DateTime.DaysInMonth(date.Year, newMonth));
                        }
                    }
                    return DateTime.MaxValue;

                default:
                    return date;
            }
        }
    }
}