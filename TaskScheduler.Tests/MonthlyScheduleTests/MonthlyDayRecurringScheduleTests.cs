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
            IntervalType = IntervalType.Hours,
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
            IntervalType = IntervalType.Hours,
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
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 0,
            IntervalType = IntervalType.Hours,
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
            IntervalType = IntervalType.Hours,
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
            IntervalType = IntervalType.Hours,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Hours()
    {
        var currentDate = new DateTime(2020, 5, 4, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Hours,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 4, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Hours,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Minutes()
    {
        var currentDate = new DateTime(2020, 5, 4, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Minutes,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 4, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Minutes,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Seconds()
    {
        var currentDate = new DateTime(2020, 5, 4, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Seconds,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_BeforeStartingTime_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 4, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Seconds,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Hours()
    {
        var currentDate = new DateTime(2020, 5, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Hours,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 4, 1, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Hours,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Minutes()
    {
        var currentDate = new DateTime(2020, 5, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Minutes,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Minutes,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Seconds()
    {
        var currentDate = new DateTime(2020, 5, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Seconds,
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
    public void SeriesMonthlyDayRecurring_CurrentDay_Equal_To_MonthDay_Equal_To_StartingTime_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 4, 2, 0, 0);
        var monthlyDayRecurringSchedule = new MonthlyDayRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 1,
            MonthlyDay = 4,
            EveryAfter = 3,
            IntervalType = IntervalType.Seconds,
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
}