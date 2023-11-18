using LanguageExt.UnitTesting;
using TaskScheduler.Enums;
using TaskScheduler.Extensions;
using TaskScheduler.Schedules.MonthlySchedules;

namespace TaskScheduler.Tests.MonthlyScheduleTests;

[TestFixture]
public class MonthlyTheOnceScheduleTests
{
    [Test]
    public void SeriesMonthlyTheOnce_InvalidBounds()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2020, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Monday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
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
    public void SeriesMonthlyTheOnce_InvalidMonthsCount()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 0,
            Day = Day.Monday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
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
    public void SeriesMonthlyTheOnce_Disabled()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = false,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 0,
            Day = Day.Monday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
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
    public void SeriesMonthlyTheOnce_InvalidCurrentDate()
    {
        var currentDate = new DateTime(2024, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 10, 1),
            EveryAfterMonths = 1,
            Day = Day.Monday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
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
    public void SeriesMonthlyTheOnce_FirstMonday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 9, 1),
            EveryAfterMonths = 1,
            Day = Day.Monday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 7, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 4, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstMonday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Monday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 3, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 7, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 2, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_SecondMonday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Monday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 10, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 14, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_SecondMonday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Monday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 8, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 12, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 10, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 14, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 11, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 9, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdMonday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Monday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 15, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 17, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 21, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdMonday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Monday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 15, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 19, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 17, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 21, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 18, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 16, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_FourthMonday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Monday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 22, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 24, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 28, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FourthMonday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Monday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 22, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 24, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 25, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 27, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 22, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 25, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_LastMonday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Monday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 29, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 26, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 28, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastMonday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Monday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 29, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 31, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 25, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 27, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 29, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 25, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstTuesday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 9, 1),
            EveryAfterMonths = 1,
            Day = Day.Tuesday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 1, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 5, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstTuesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Tuesday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 4, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 1, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 5, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 3, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 7, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_SecondTuesday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Tuesday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 9, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 11, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 8, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_SecondTuesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Tuesday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 9, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 13, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 11, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 8, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 12, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 10, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdTuesday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Tuesday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 16, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 18, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 15, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdTuesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Tuesday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 16, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 20, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 18, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 15, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 19, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 17, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_FourthTuesday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Tuesday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 23, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 22, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FourthTuesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Tuesday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 23, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 26, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 28, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 23, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 26, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastTuesday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Tuesday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 30, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 27, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 29, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastTuesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Tuesday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 30, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 25, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 26, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 28, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 30, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 26, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstWeekEnd()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 9, 1),
            EveryAfterMonths = 1,
            Day = Day.WeekendDay,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 5, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 1, 2, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstWeekEnd_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.WeekendDay,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 5, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 2, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 1, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_SecondWeekEnd()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.WeekendDay,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 6, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_SecondWeekEnd_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.WeekendDay,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 6, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 3, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 7, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdWeekEnd()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.WeekendDay,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 13, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 8, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 12, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdWeekEnd_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.WeekendDay,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 13, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 8, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 12, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 9, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 8, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_FourthWeekEnd()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.WeekendDay,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 14, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 9, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 13, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FourthWeekEnd_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.WeekendDay,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 14, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 9, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 10, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 12, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 14, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 10, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_LastWeekEnd()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.WeekendDay,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 30, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 27, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastWeekEnd_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.WeekendDay,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 30, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 24, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 26, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 28, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 31, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstWednesday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 9, 1),
            EveryAfterMonths = 1,
            Day = Day.Wednesday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 5, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 2, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 6, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstWednesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Wednesday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 5, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 2, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 6, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 4, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 1, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_SecondWednesday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Wednesday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 10, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 12, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 9, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_SecondWednesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Wednesday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 10, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 14, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 12, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 9, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 13, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 11, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdWednesday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Wednesday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 17, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 19, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 16, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdWednesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Wednesday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 17, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 21, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 19, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 16, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 20, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 18, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_FourthWednesday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Wednesday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 24, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 23, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FourthWednesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Wednesday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 24, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 27, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 22, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 24, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 27, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastWednesday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Wednesday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 31, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 28, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 30, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastWednesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Wednesday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 31, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 26, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 27, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 29, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 31, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 27, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_FirstThursday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 9, 1),
            EveryAfterMonths = 1,
            Day = Day.Thursday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 6, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 3, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 7, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstThursday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Thursday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 1, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 6, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 3, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 7, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 5, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 2, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_SecondThursday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Thursday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 11, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 13, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 10, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_SecondThursday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Thursday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 11, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 8, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 13, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 10, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 14, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 12, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdThursday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Thursday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 18, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 20, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 17, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdThursday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Thursday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 18, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 15, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 20, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 17, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 21, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 19, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_FourthThursday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Thursday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 25, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 22, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 24, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FourthThursday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Thursday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 25, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 28, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 23, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 25, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 28, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastThursday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Thursday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 25, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 29, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 31, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastThursday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Thursday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 25, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 27, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 28, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 30, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 25, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 28, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_FirstFriday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 9, 1),
            EveryAfterMonths = 1,
            Day = Day.Friday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 7, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 1, 2, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstFriday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Friday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 2, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 7, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 4, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 1, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 6, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_SecondFriday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Friday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 12, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 14, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 11, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_SecondFriday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Friday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 12, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 9, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 14, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 11, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 8, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 13, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdFriday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Friday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 19, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 21, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 18, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdFriday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Friday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 19, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 16, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 21, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 18, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 15, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 20, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_FourthFriday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Friday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 23, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 25, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FourthFriday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Friday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 29, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 24, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 26, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 22, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastFriday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Friday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 30, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 25, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastFriday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Friday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 26, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 28, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 29, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 24, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 26, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 29, 2, 0, 0))));
        });
    }
    
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstSaturday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 9, 1),
            EveryAfterMonths = 1,
            Day = Day.Saturday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 5, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstSaturday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Saturday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 3, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 1, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 5, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 2, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 7, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_SecondSaturday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Saturday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 13, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 8, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 12, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_SecondSaturday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Saturday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 13, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 10, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 8, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 12, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 9, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 14, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdSaturday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Saturday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 20, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 15, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 19, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdSaturday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Saturday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 20, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 17, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 15, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 19, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 16, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 21, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_FourthSaturday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Saturday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 22, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 26, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FourthSaturday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Saturday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 22, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 23, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 25, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 27, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 23, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastSaturday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Saturday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 24, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 29, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 26, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastSaturday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Saturday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 27, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 29, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 30, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 25, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 27, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 30, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstSunday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 9, 1),
            EveryAfterMonths = 1,
            Day = Day.Sunday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 6, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FirstSunday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Sunday,
            Position = Position.First,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 4, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 2, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 6, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 3, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 1, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_SecondSunday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Sunday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 14, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 9, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 13, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_SecondSunday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Sunday,
            Position = Position.Second,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 14, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 11, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 9, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 13, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 10, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 8, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdSunday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Sunday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 21, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 16, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 20, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_ThirdSunday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 1,
            Day = Day.Sunday,
            Position = Position.Third,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 21, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 18, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 16, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 20, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 17, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 10, 15, 2, 0, 0))));
        });
    }
    
     [Test]
    public void SeriesMonthlyTheOnce_FourthSunday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Sunday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 23, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 27, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_FourthSunday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Sunday,
            Position = Position.Fourth,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 23, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 24, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 26, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 28, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 24, 2, 0, 0))));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastSunday()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 8, 1),
            EveryAfterMonths = 1,
            Day = Day.Sunday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 6, 25, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 30, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 8, 27, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesMonthlyTheOnce_LastSunday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 5, 5, 1, 0, 0);
        var monthlyPeriodOnceSchedule = new MonthlyPeriodOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EveryAfterMonths = 2,
            Day = Day.Sunday,
            Position = Position.Last,
            ExecutionTime = new TimeSpan(2, 0, 0),
        };

        var series = monthlyPeriodOnceSchedule.MonthlyTheOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 5, 28, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 7, 30, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 9, 24, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 26, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 28, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 3, 31, 2, 0, 0))));
        });
    }
}