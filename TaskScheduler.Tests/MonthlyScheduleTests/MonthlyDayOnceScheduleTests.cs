using LanguageExt.UnitTesting;
using TaskScheduler.Extensions;
using TaskScheduler.Schedules;

namespace TaskScheduler.Tests.MonthlyScheduleTests;

[TestFixture]
public class MonthlyDayOnceScheduleTests
{
    [Test]
    public void SeriesMonthlyDayOnce_InvalidBounds()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyDayOnceSchedule = new MonthlyDayOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = monthlyDayOnceSchedule.MonthlyDayOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Start date can't be later than end date")));
            series[1].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Start date can't be later than end date")));
            series[2].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Start date can't be later than end date")));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Start date can't be later than end date")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Start date can't be later than end date")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Start date can't be later than end date")));
        });
    }

    [Test]
    public void SeriesMonthlyDayOnce_InvalidMonthlyDay()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyDayOnceSchedule = new MonthlyDayOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 0,
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = monthlyDayOnceSchedule.MonthlyDayOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Invalid month date!")));
            series[1].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Invalid month date!")));
            series[2].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Invalid month date!")));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Invalid month date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Invalid month date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Invalid month date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayOnce_InvalidMonthsCount()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyDayOnceSchedule = new MonthlyDayOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = -1,
            MonthlyDay = 4,
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = monthlyDayOnceSchedule.MonthlyDayOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Number of months can't be non positive!")));
            series[1].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Number of months can't be non positive!")));
            series[2].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Number of months can't be non positive!")));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Number of months can't be non positive!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Number of months can't be non positive!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Number of months can't be non positive!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayOnce_Disabled()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyDayOnceSchedule = new MonthlyDayOnceSchedule
        {
            Name = "Send email",
            IsEnabled = false,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = -1,
            MonthlyDay = 4,
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = monthlyDayOnceSchedule.MonthlyDayOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Schedule was cancelled!")));
            series[1].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Schedule was cancelled!")));
            series[2].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Schedule was cancelled!")));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Schedule was cancelled!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Schedule was cancelled!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Schedule was cancelled!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayOnce_InvalidCurrentDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyDayOnceSchedule = new MonthlyDayOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = monthlyDayOnceSchedule.MonthlyDayOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[1].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[2].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyDayOnce_CurrentDay_Equal_To_MonthDay_BeforeExecutionTime()
    {
        var currentDate = new DateTime(2020, 5, 4, 1, 0, 0);
        var monthlyDayOnceSchedule = new MonthlyDayOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = monthlyDayOnceSchedule.MonthlyDayOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
}