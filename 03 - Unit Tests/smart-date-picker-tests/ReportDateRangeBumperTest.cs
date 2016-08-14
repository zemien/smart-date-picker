using NUnit.Framework;
using smart_date_picker;
using System;

namespace smart_date_picker_tests
{
    [TestFixture]
    public class ReportDateRangeBumperTest
    {
        [Test]
        public void WeekBumping_StartDate_ToPriorSunday_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-05-29"), DateTime.Parse("2016-06-18"), dataAvailability, PeriodType.Weeks);

            var expected = new DateRange(DateTime.Parse("2016-05-29"), DateTime.Parse("2016-06-18"));

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void WeekBumping_StartDate_ToPriorWednesday_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-06-01"), DateTime.Parse("2016-06-18"), dataAvailability, PeriodType.Weeks);

            var expected = new DateRange(DateTime.Parse("2016-05-29"), DateTime.Parse("2016-06-18"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WeekBumping_StartDate_ToFollowingMonday_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-06-27"), DateTime.Parse("2016-06-18"), dataAvailability, PeriodType.Weeks);

            var expected = new DateRange(DateTime.Parse("2016-06-26"), DateTime.Parse("2016-07-02"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WeekBumping_EndDate_ToAfterDataAvailability_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-06-12"), DateTime.Parse("2017-02-15"), dataAvailability, PeriodType.Weeks);

            var expected = new DateRange(DateTime.Parse("2016-06-12"), DateTime.Parse("2016-12-31"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WeekBumping_StartDate_ToAfterDataAvailability_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2017-02-15"), DateTime.Parse("2016-06-18"), dataAvailability, PeriodType.Weeks);

            var expected = new DateRange(DateTime.Parse("2016-12-25"), DateTime.Parse("2016-12-31"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MonthBumping_EndDate_ToFewDaysPrior_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-06-01"), DateTime.Parse("2016-06-27"), dataAvailability, PeriodType.Months);

            var expected = new DateRange(DateTime.Parse("2016-06-01"), DateTime.Parse("2016-06-30"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MonthBumping_EndDate_ToEndNextMonth_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-06-01"), DateTime.Parse("2016-07-31"), dataAvailability, PeriodType.Months);

            var expected = new DateRange(DateTime.Parse("2016-06-01"), DateTime.Parse("2016-07-31"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MonthBumping_EndDate_ToFewMonthsPrior_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-06-01"), DateTime.Parse("2016-02-28"), dataAvailability, PeriodType.Months);

            var expected = new DateRange(DateTime.Parse("2016-02-01"), DateTime.Parse("2016-02-29"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MonthBumping_StartDate_ToAfterDataAvailability_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2017-02-15"), DateTime.Parse("2016-06-30"), dataAvailability, PeriodType.Months);

            var expected = new DateRange(DateTime.Parse("2016-12-01"), DateTime.Parse("2016-12-31"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MonthBumping_EndDate_ToBeforeDataAvailability_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-06-01"), DateTime.Parse("2015-12-20"), dataAvailability, PeriodType.Months);

            var expected = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-01-31"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuarterBumping_Quarter1_NoChange_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var expected = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-03-31"));

            var actual = ReportDateRangeBumper.BumpDates(expected.StartDate, expected.EndDate, dataAvailability, PeriodType.Quarters);
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuarterBumping_Quarter2_NoChange_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var expected = new DateRange(DateTime.Parse("2016-04-01"), DateTime.Parse("2016-06-30"));

            var actual = ReportDateRangeBumper.BumpDates(expected.StartDate, expected.EndDate, dataAvailability, PeriodType.Quarters);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuarterBumping_Quarter3_NoChange_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var expected = new DateRange(DateTime.Parse("2016-07-01"), DateTime.Parse("2016-09-30"));

            var actual = ReportDateRangeBumper.BumpDates(expected.StartDate, expected.EndDate, dataAvailability, PeriodType.Quarters);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuarterBumping_Quarter4_NoChange_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var expected = new DateRange(DateTime.Parse("2016-10-01"), DateTime.Parse("2016-12-31"));

            var actual = ReportDateRangeBumper.BumpDates(expected.StartDate, expected.EndDate, dataAvailability, PeriodType.Quarters);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuarterBumping_StartDate_ToFutureWeekButSameQuarter_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-03-15"), DateTime.Parse("2016-06-30"), dataAvailability, PeriodType.Quarters);

            var expected = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-06-30"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuarterBumping_EndDate_ToPastWeekButFewerQuarters_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-03-15"), dataAvailability, PeriodType.Quarters);

            var expected = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-03-31"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuarterBumping_StartDate_ToFutureWeekDifferentQuarter_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-10-01"), DateTime.Parse("2016-06-30"), dataAvailability, PeriodType.Quarters);

            var expected = new DateRange(DateTime.Parse("2016-10-01"), DateTime.Parse("2016-12-31"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuarterBumping_EndDate_ToBeforeDataAvailability_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-01-01"), DateTime.Parse("2015-05-15"), dataAvailability, PeriodType.Quarters);

            var expected = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-03-31"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuarterBumping_StartDate_ToAfterDataAvailability_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-01-01"), DateTime.Parse("2016-12-31"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2017-08-23"), DateTime.Parse("2016-06-30"), dataAvailability, PeriodType.Quarters);

            var expected = new DateRange(DateTime.Parse("2016-10-01"), DateTime.Parse("2016-12-31"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuarterBumping_FromWeekMode_ConstrainedDataAvailability_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-06-05"), DateTime.Parse("2016-08-30"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-06-12"), DateTime.Parse("2016-06-18"), dataAvailability, PeriodType.Quarters);

            var expected = dataAvailability;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MonthBumping_FromWeekMode_ConstrainedDataAvailability_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-06-12"), DateTime.Parse("2016-06-18"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-06-12"), DateTime.Parse("2016-06-18"), dataAvailability, PeriodType.Months);

            var expected = dataAvailability;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MonthBumping_FromWeekMode_OutsideDataAvailability_Test()
        {
            var dataAvailability = new DateRange(DateTime.Parse("2016-06-05"), DateTime.Parse("2016-08-30"));
            var actual = ReportDateRangeBumper.BumpDates(DateTime.Parse("2016-06-12"), DateTime.Parse("2016-06-18"), dataAvailability, PeriodType.Months);

            var expected = new DateRange(DateTime.Parse("2016-07-01"), DateTime.Parse("2016-07-31"));

            Assert.AreEqual(expected, actual);
        }
    }
}
