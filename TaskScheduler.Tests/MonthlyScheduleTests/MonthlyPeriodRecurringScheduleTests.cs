using LanguageExt.UnitTesting;
using TaskScheduler.Enums;
using TaskScheduler.Extensions;
using TaskScheduler.Schedules.MonthlySchedules;

namespace TaskScheduler.Tests.MonthlyScheduleTests;

[TestFixture]
public class MonthlyPeriodRecurringScheduleTests
{
    [Test]
    public void MonthlyPeriodRecurring_TaskDescription()
    {
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.First,
        };

        var description = monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the First Day of every 1 month(s) every" +
                       $"1 Hour(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));

    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstDay_Hour()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the First Day of every 1 month(s) every" +
                       $"1 Hour(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstDay_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.First,
        };


        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 4, 0, 0))));
        });
        
        var description = (string)monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the First Day of every 1 month(s) every" +
                       $"1 Hour(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstDay_Minute()
    {
        var currentDate = new DateTime(2023, 6, 1, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the First Day of every 1 month(s) every" +
                       $"30 Minute(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstDay_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 1, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 0))));
        });
        
        var description = (string)monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the First Day of every 1 month(s) every" +
                       $"30 Minute(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstDay_Second()
    {
        var currentDate = new DateTime(2023, 6, 1, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the First Day of every 1 month(s) every" +
                       $"30 Second(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstDay_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 1, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 2))));
        });
        
        var description = (string)monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the First Day of every 1 month(s) every" +
                       $"30 Second(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondDay_Hour()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        var description = (string)monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the Second Day of every 1 month(s) every" +
                       $"1 Hour(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondDay_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 4, 0, 0))));
        });
        
        var description = (string)monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the Second Day of every 1 month(s) every" +
                       $"1 Hour(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondDay_Minute()
    {
        var currentDate = new DateTime(2023, 6, 1, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the Second Day of every 1 month(s) every" +
                       $"30 Minute(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondDay_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 2, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondDay_Second()
    {
        var currentDate = new DateTime(2023, 6, 2, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondDay_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 2, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdDay_Hour()
    {
        var currentDate = new DateTime(2023, 5, 17, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Third,
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
        
        var description = (string)monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the Third Day of every 1 month(s) every" +
                       $"1 Hour(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdDay_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 17, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 4, 0, 0))));
        });
        var description = (string)monthlyPeriodRecurringSchedule.GetTaskDescription();
        var expected = $"Occurs the Third Day of every 1 month(s) every" +
                       $"1 Hour(s) between {new TimeSpan(2, 0, 0)} and {new TimeSpan(4, 0, 0)}" +
                       $"starting on {new DateTime(2023, 1, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdDay_Minute()
    {
        var currentDate = new DateTime(2023, 6, 3, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Third,
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
    public void SeriesMonthlyPeriodRecurring_ThirdDay_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 20, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Third,
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
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdDay_Second()
    {
        var currentDate = new DateTime(2023, 6, 3, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Third,
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
    public void SeriesMonthlyPeriodRecurring_ThirdDay_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 3, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthDay_Hour()
    {
        var currentDate = new DateTime(2023, 5, 23, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Fourth,
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
    public void SeriesMonthlyPeriodRecurring_FourthDay_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 23, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthDay_Minute()
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
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Fourth,
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
    public void SeriesMonthlyPeriodRecurring_FourthDay_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 4, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Fourth,
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
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthDay_Second()
    {
        var currentDate = new DateTime(2023, 6, 4, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 6, 28),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Fourth,
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
    public void SeriesMonthlyPeriodRecurring_FourthDay_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 4, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastDay_Hour()
    {
        var currentDate = new DateTime(2023, 5, 31, 6, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastDay_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 15, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastDay_Minute()
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
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastDay_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 26, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Last,
        };
        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastDay_Second()
    {
        var currentDate = new DateTime(2023, 6, 30, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastDay_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 30, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Day,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Hour()
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
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Minute()
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
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Second()
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
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_FirstMonday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_SecondMonday_Hour()
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
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_SecondMonday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_SecondMonday_Minute()
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
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurringSecondMonday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_SecondMonday_Second()
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
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_SecondMonday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 12, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Hour()
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
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 17, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Minute()
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
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 20, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Second()
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
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_ThirdMonday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 19, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Hour()
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
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 23, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Minute()
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
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 26, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Second()
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
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_FourthMonday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 26, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_LastMonday_Hour()
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
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_LastMonday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 30, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_LastMonday_Minute()
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
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_LastMonday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 26, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_LastMonday_Second()
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
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_LastMonday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 26, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Hour()
    {
        var currentDate = new DateTime(2023, 5, 7, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 7, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Minute()
    {
        var currentDate = new DateTime(2023, 6, 3, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 3, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Second()
    {
        var currentDate = new DateTime(2023, 6, 3, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_FirstWeekEnd_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 3, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_SecondWeekEnd_Hour()
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
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_SecondWeekEnd_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 8, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_SecondWeekEnd_Minute()
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
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurringSecondWeekEnd_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 4, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_SecondWeekEnd_Second()
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
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_SecondWeekEnd_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 4, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Hour()
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
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 14, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Minute()
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
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 10, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Second()
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
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_ThirdWeekEnd_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 10, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Hour()
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
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 15, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Minute()
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
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 11, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Second()
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
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_FourthWeekEnd_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 11, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Hour()
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
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 29, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
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
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Minute()
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
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 25, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
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
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Second()
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
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_LastWeekEnd_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 25, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
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
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Hour()
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
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 6, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 6, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Second()
    {
        var currentDate = new DateTime(2023, 6, 6, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstTuesday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 6, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondTuesday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 10, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondTuesday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 10, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 11, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 11, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 11, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondTuesday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 13, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondTuesday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 13, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 11, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondTuesday_Second()
    {
        var currentDate = new DateTime(2023, 6, 13, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondTuesday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 13, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 11, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 11, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 11, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 16, 6, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 16, 4, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 18, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 18, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 18, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 20, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 20, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 18, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Second()
    {
        var currentDate = new DateTime(2023, 6, 20, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdTuesday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 20, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 18, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 18, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 18, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 23, 5, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 23, 5, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 27, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 27, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Second()
    {
        var currentDate = new DateTime(2023, 6, 27, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthTuesday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 27, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 31, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 31, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 27, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 27, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Second()
    {
        var currentDate = new DateTime(2023, 6, 27, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastTuesday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 27, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Tuesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 2))));
        });
    }
    
         [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Hour()
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
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 5, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 5, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 5, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 7, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 7, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 5, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Second()
    {
        var currentDate = new DateTime(2023, 6, 7, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstWednesday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 7, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 5, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 5, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 5, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWednesday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 11, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWednesday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 11, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 12, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 12, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 12, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWednesday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 14, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondWednesday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 14, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 12, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWednesday_Second()
    {
        var currentDate = new DateTime(2023, 6, 14, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondWednesday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 14, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 12, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 12, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 12, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 18, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 18, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 19, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 19, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 19, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 21, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 21, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 19, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Second()
    {
        var currentDate = new DateTime(2023, 6, 21, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdWednesday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 21, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 19, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 19, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 19, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 25, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 25, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 28, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 28, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Second()
    {
        var currentDate = new DateTime(2023, 6, 28, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthWednesday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 28, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 31, 6, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 28, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 28, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Second()
    {
        var currentDate = new DateTime(2023, 6, 28, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastWednesday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 28, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Wednesday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Hour()
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
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 6, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 6, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 6, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 1, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 1, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 6, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Second()
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
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstThursday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 1, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 6, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 6, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 6, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondThursday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 12, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondThursday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 12, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 13, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 13, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 13, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondThursday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 8, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondThursday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 8, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 13, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondThursday_Second()
    {
        var currentDate = new DateTime(2023, 6, 8, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondThursday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 8, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 13, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 13, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 13, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 19, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 19, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 20, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 20, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 20, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 15, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 15, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 20, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Second()
    {
        var currentDate = new DateTime(2023, 6, 15, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdThursday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 15, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 20, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 20, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 20, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 26, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 26, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 22, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 22, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Second()
    {
        var currentDate = new DateTime(2023, 6, 22, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthThursday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 22, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 26, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 26, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 29, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 29, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Second()
    {
        var currentDate = new DateTime(2023, 6, 29, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastThursday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 29, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Thursday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 2))));
        });
    }
    
      [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 6, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 6, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 7, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 7, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 7, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 2, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 2, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 7, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Second()
    {
        var currentDate = new DateTime(2023, 6, 2, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FirstFriday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 2, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.First,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 7, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 7, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 7, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondFriday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 13, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondFriday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 13, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 14, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 14, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 14, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondFriday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 9, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurringSecondFriday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 9, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 14, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_SecondFriday_Second()
    {
        var currentDate = new DateTime(2023, 6, 9, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_SecondFriday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 9, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Second,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 14, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 14, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 14, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 20, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 20, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 21, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 21, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 21, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 16, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 16, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 21, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Second()
    {
        var currentDate = new DateTime(2023, 6, 16, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdFriday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 16, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 21, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 21, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 21, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 26, 4, 0, 1);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 27, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 23, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 23, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Second()
    {
        var currentDate = new DateTime(2023, 6, 23, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthFriday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 23, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 27, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 27, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 30, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 30, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Second()
    {
        var currentDate = new DateTime(2023, 6, 30, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastFriday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 30, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Friday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 7, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
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
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 7, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
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
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 3, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
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
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 3, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
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
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Second()
    {
        var currentDate = new DateTime(2023, 6, 3, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
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
    public void SeriesMonthlyPeriodRecurring_FirstSaturday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 3, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
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
    public void SeriesMonthlyPeriodRecurring_Secondaturday_Hour()
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
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurring_Secondaturday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 13, 6, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurring_Secondaturday_Minute()
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
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurringSecondaturday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 10, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurring_Secondaturday_Second()
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
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurring_Secondaturday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 10, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 21, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 21, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 15, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 15, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 15, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 17, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 17, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 15, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Second()
    {
        var currentDate = new DateTime(2023, 6, 17, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSaturday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 17, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 15, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 15, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 15, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 28, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 28, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 22, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 22, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 22, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 24, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 24, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 22, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Second()
    {
        var currentDate = new DateTime(2023, 6, 24, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSaturday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 24, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 22, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 22, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 22, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 28, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 28, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 29, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 29, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 29, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 24, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 24, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 29, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Second()
    {
        var currentDate = new DateTime(2023, 6, 24, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSaturday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 24, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Saturday,
            Position = Position.Last,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 29, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 29, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 29, 2, 0, 2))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Hour()
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
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
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
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 8, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
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
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Minute()
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
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
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
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 4, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
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
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Second()
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
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
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
    public void SeriesMonthlyPeriodRecurring_FirstSunday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 4, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.First,
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
    public void SeriesMonthlyPeriodRecurring_Secondunday_Hour()
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
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurring_Secondunday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 15, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurring_Secondunday_Minute()
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
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurringSecondunday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 11, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurring_Secondunday_Second()
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
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurring_Secondunday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 11, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Second,
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
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Hour()
    {
        var currentDate = new DateTime(2023, 5, 22, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 22, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 16, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 16, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 16, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Minute()
    {
        var currentDate = new DateTime(2023, 6, 18, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 4, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 18, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 2, 30, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 3, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 3, 30, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 4, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 16, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Second()
    {
        var currentDate = new DateTime(2023, 6, 18, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 7, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 4, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_ThirdSunday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 18, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Third,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 16, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 16, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 16, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Hour()
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
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
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
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 29, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 23, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 23, 3, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 23, 4, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Minute()
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
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
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
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 25, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
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
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 23, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Second()
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
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
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
    public void SeriesMonthlyPeriodRecurring_FourthSunday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 25, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
            Position = Position.Fourth,
        };

        var series = monthlyPeriodRecurringSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 59, 58))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 3, 59, 59))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 23, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 23, 2, 0, 1))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 23, 2, 0, 2))));
        });
    }
    
    [Test]
    public void SeriesMonthlyPeriodRecurring_LastSunday_Hour()
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
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
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
    public void SeriesMonthlyPeriodRecurring_LastSunday_Hour_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 29, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
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
    public void SeriesMonthlyPeriodRecurring_LastSunday_Minute()
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
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
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
    public void SeriesMonthlyPeriodRecurring_LastSunday_Minute_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 25, 1, 0, 0);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 30,
            IntervalType = IntervalType.Minute,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
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
    public void SeriesMonthlyPeriodRecurring_LastSunday_Second()
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
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
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
    public void SeriesMonthlyPeriodRecurring_LastSunday_Second_NoEndDate()
    {
        var currentDate = new DateTime(2023, 6, 25, 3, 59, 57);
        var monthlyPeriodRecurringSchedule = new MonthlyPeriodRecurringSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            EveryAfter = 1,
            IntervalType = IntervalType.Second,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Day = Day.Sunday,
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
}