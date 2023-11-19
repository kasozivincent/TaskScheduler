using LanguageExt.UnitTesting;
using TaskScheduler.Enums;
using TaskScheduler.Extensions;
using TaskScheduler.Schedules.MonthlySchedules;

namespace TaskScheduler.Tests.MonthlyScheduleTests;

[TestFixture]
public class MonthlyPeriodRecurringScheduleTests
{
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondMonday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 8, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 8, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondMonday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 8, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 8, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondMonday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondMonday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 10, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondMonday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 12, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondMonday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 12, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 10, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 10, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 10, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 17, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 17, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 17, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 17, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 17, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 20, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 17, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 19, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 19, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 17, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 17, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 17, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 23, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 23, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 24, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 24, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 24, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 26, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 26, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 24, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 26, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 26, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 24, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 24, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 24, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastMonday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 30, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastMonday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 30, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastMonday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 26, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastMonday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 26, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Last,
        };
        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastMonday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 26, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastMonday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 26, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Monday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 1, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 3, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWeekEnd_Hours()
    {
        var currentDate = new DateTime(2023, 5, 8, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWeekEnd_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 8, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWeekEnd_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 4, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondWeekEnd_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 4, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWeekEnd_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 4, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWeekEnd_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 4, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Hours()
    {
        var currentDate = new DateTime(2023, 5, 14, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 14, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 8, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 8, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 8, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 10, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 10, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 8, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 10, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 10, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 8, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 8, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 8, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Hours()
    {
        var currentDate = new DateTime(2023, 5, 15, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 15, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 9, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 9, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 9, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 11, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 11, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 9, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 11, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 11, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 9, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 9, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 9, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Hours()
    {
        var currentDate = new DateTime(2023, 5, 29, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 29, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 30, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 30, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 30, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 25, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 25, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 30, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 25, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 25, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.WeekendDay,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 30, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 30, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 30, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondTuesday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondTuesday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondTuesday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondTuesday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondTuesday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondTuesday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
         [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWednesday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWednesday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWednesday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondWednesday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWednesday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWednesday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondThursday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondThursday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondThursday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondThursday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondThursday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondThursday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
      [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondFriday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondFriday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondFriday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondFriday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondFriday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondFriday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondSaturday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondSaturday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondSaturday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondSaturday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondSaturday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondSaturday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondSunday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondSunday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondSunday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondSunday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondSunday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondSunday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSunday_Hours()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSunday_Hours_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hours,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSunday_Minutes()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSunday_Minutes_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minutes,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastSunday_Seconds()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSunday_Seconds_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Seconds,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
}