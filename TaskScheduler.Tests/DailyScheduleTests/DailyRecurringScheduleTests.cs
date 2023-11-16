using LanguageExt.UnitTesting;
using TaskScheduler.Enums;
using TaskScheduler.Extensions;
using TaskScheduler.Schedules.DailySchedules;

namespace TaskScheduler.Tests;

[TestFixture]
public class DailyRecurringScheduleTests
{
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Hours()
    {
        var currentDate = new DateTime(2020, 5, 7, 1, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 8),
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0))));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 7, 1, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 3, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 4, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0))));
        });
    }

    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Minutes()
    {
        var currentDate = new DateTime(2020, 5, 8, 1, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 8),
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 8, 1, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 30, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 3, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 3, 30, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 4, 0, 0))));
        });
    }

    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Seconds()
    {
        var currentDate = new DateTime(2020, 5, 8, 1, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2021, 2, 9),
            EveryAfter = 30,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 30))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 1, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 1, 30))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 2, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 2, 30))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 3, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 3, 30))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 4, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 4, 30))));
        });
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_BeforeStartingTime_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 8, 1, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfter = 30,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 30))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 1, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 1, 30))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 2, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 2, 30))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 3, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 3, 30))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 4, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 4, 30))));
        });
    }

    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Hours()
    {
        var currentDate = new DateTime(2020, 5, 7, 2, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 8),
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 7, 2, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 3, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 4, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 3, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 4, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 3, 0, 0))));
        });
    }

    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Minutes()
    {
        var currentDate = new DateTime(2020, 5, 8, 2, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 8),
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 30, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 30, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 8, 2, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 30, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 30, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 30, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 3, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 3, 30, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 4, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Seconds()
    {
        var currentDate = new DateTime(2020, 5, 8, 2, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2021, 2, 9),
            EveryAfter = 30,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 30))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 1, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 1, 30))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 2, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 2, 30))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 3, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 3, 30))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 4, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 4, 30))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 5, 0))));
        });
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_StartingTime_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 8, 2, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfter = 30,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 30))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 1, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 1, 30))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 2, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 2, 30))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 3, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 3, 30))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 4, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 4, 30))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 5, 0))));
        });
    }

    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Hours()
    {
        var currentDate = new DateTime(2020, 5, 7, 4, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 8),
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 7, 4, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 4, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 3, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 4, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 11, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Minutes()
    {
        var currentDate = new DateTime(2020, 5, 7, 4, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 8),
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[6].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 8, 4, 0, 0);
        var dailyRecurringSchedule = new DailyRecurringSchedule
        {
            Name = "Check email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
        };

        var series = dailyRecurringSchedule.DailyRecurringScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 2, 30, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 3, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 3, 30, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Seconds()
    {
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_Equal_To_EndingTime_Seconds_NoEndDate()
    {
    }

    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Hours()
    {
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Hours_NoEndDate()
    {
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Minutes()
    {
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Minutes_NoEndDate()
    {
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Seconds()
    {
    }
    
    [Test]
    public void SeriesDailyRecurring_CurrentDateTime_AfterEndingTime_Seconds_NoEndDate()
    {
    }
}