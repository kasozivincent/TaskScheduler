using LanguageExt.UnitTesting;
using TaskScheduler.Enums;
using TaskScheduler.Extensions;
using TaskScheduler.Schedules.MonthlySchedules;

namespace TaskScheduler.Tests.MonthlyScheduleTests;

[TestFixture]
public class MonthlyDayRecurringScheduleTests
{
    [Test]
    public void SeriesMonthlyDayRecurring_InvalidBounds()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 0,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 6);
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
    public void SeriesMonthlyDayRecurring_InvalidTimeInterval()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(6, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Starting time can't be later than or equal to ending time")));
            series[1].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Starting time can't be later than or equal to ending time")));
            series[2].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Starting time can't be later than or equal to ending time")));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Starting time can't be later than or equal to ending time")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Starting time can't be later than or equal to ending time")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Starting time can't be later than or equal to ending time")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_InvalidMonthlyDay()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = -4,
            EveryAfter = 4,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 6);
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
    public void SeriesMonthlyDayRecurring_InvalidMonthsCount()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 0,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 6);
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
    public void SeriesMonthlyDayRecurring_Disabled()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = false,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 0,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 6);
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
    public void SeriesMonthlyDayRepetitive_InvalidCurrentDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 0,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 6);
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Hour()
    {
        var currentDate = new DateTime(2020, 7, 4, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2020, 7, 4, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Minute()
    {
        var currentDate = new DateTime(2020, 9, 4, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2020, 9, 4, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 30, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 30, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Second()
    {
        var currentDate = new DateTime(2020, 9, 4, 3, 58, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 58, 30))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 59, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 59, 30))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Second_NoEndDate()
    {
        var currentDate = new DateTime(2020, 9, 4, 3, 58, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 58, 30))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 59, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 59, 30))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 30))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 1, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 1, 30))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 2, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 2, 30))));
        });
    }
    
     [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Hour()
    {
        var currentDate = new DateTime(2020, 7, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 3, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 4, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2020, 7, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 3, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 4, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 2, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 3, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Minute()
    {
        var currentDate = new DateTime(2020, 9, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 30, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 30, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2020, 9, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 30, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 30, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 30, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 30, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Second()
    {
        var currentDate = new DateTime(2020, 9, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 30))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 1, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 1, 30))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 2, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 2, 30))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 3, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 3, 30))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 4, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 4, 30))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 5, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Second_NoEndDate()
    {
        var currentDate = new DateTime(2020, 9, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 30))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 1, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 1, 30))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 2, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 2, 30))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 3, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 3, 30))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 4, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 4, 30))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 5, 0))));
        });
    }


    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_Equal_To_EndingTime_Hour()
    {
        var currentDate = new DateTime(2020, 7, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 3, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 4, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_Equal_To_EndingTime_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2020, 7, 4, 4, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 3, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 4, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 3, 4, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_Equal_To_EndingTime_Minute()
    {
        var currentDate = new DateTime(2020, 9, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 30, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 30, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_Equal_To_EndingTime_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2020, 9, 4, 4, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 2, 30, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 3, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 3, 30, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_Equal_To_EndingTime_Second()
    {
        var currentDate = new DateTime(2020, 9, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 30))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 1, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 1, 30))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 2, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 2, 30))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 3, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 3, 30))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 4, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 4, 30))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 5, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_Equal_To_EndingTime_Second_NoEndDate()
    {
        var currentDate = new DateTime(2020, 9, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 30))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 1, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 1, 30))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 2, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 2, 30))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 3, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 3, 30))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 4, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 4, 30))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 5, 0))));
        });
    }

    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_In_Interval_Hour()
    {
        var currentDate = new DateTime(2020, 7, 4, 3, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 12, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 4, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_In_Interval_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2020, 7, 4, 3, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 7, 4, 4, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 2, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 3, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_In_Interval_Minute()
    {
        var currentDate = new DateTime(2020, 9, 4, 2, 30, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 12, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 30, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 30, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_In_Interval_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2020, 9, 4, 2, 30, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 30, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 30, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 2, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 2, 30, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_In_Interval_Second()
    {
        var currentDate = new DateTime(2020, 9, 4, 3, 58, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 58, 30))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 59, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 59, 30))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthlyDay_In_Interval_Second_NoEndDate()
    {
        var currentDate = new DateTime(2020, 9, 4, 3, 58, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 30,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 58, 30))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 59, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 59, 30))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 30))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 1, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 1, 30))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 2, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 2, 30))));
        });
    }

    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Not_Equal_To_MonthDay_Hour()
    {
        var currentDate = new DateTime(2020, 7, 5, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 12, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Not_Equal_To_MonthDay_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2020, 7, 5, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 2,
            MonthlyDay = 4,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0)
        };

        var series = monthlyDayRecurringSchedule.MonthlyDayRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 9, 4, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 11, 4, 4, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 3, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 1, 4, 4, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2021, 3, 4, 2, 0, 0))));
        });
    }

    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Not_Equal_To_MonthDay_Minute()
    {
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Not_Equal_To_MonthDay_Minute_NoEndDate()
    {
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Not_Equal_To_MonthDay_Second()
    {
    }
    
    [Test]
    public void SeriesMonthlyDayRecurring_CurrentDay_Not_Equal_To_MonthDay_Second_NoEndDate()
    {
    }
}