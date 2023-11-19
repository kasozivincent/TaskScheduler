using LanguageExt.UnitTesting;
using TaskScheduler.Extensions;
using TaskScheduler.Schedules.DailySchedules;

namespace TaskScheduler.Tests.DailyScheduleTests;

[TestFixture]
public class DailyOnceScheduleTests
{
    [Test]
    public void SeriesDailyOnce_CurrentDateTime_BeforeExecutionTime()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var dailyOnceSchedule = new DailyOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 9),
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = dailyOnceSchedule.DailyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesDailyOnce_CurrentDateTime_BeforeExecutionTime_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var dailyOnceSchedule = new DailyOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = dailyOnceSchedule.DailyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0))));
        });
    }

    [Test]
    public void SeriesDailyOnce_CurrentDateTime_Equal_To_ExecutionTime()
    {
        var currentDate = new DateTime(2020, 5, 5, 2, 0, 0);
        var dailyOnceSchedule = new DailyOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 7),
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = dailyOnceSchedule.DailyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0))));
            series[2].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
    
    [Test]
    public void SeriesDailyOnce_CurrentDateTime_Equal_To_ExecutionTime_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 5, 2, 0, 0);
        var dailyOnceSchedule = new DailyOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = dailyOnceSchedule.DailyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 11, 2, 0, 0))));
        });
    }

    [Test]
    public void SeriesDailyOnce_CurrentDateTime_AfterExecutionTime()
    {
        var currentDate = new DateTime(2020, 5, 5, 3, 0, 0);
        var dailyOnceSchedule = new DailyOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 8),
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = dailyOnceSchedule.DailyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[3].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }

    [Test]
    public void SeriesDailyOnce_CurrentDateTime_AfterExecutionTime_NoEndDate()
    {
        var currentDate = new DateTime(2020, 5, 5, 3, 0, 0);
        var dailyOnceSchedule = new DailyOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2020, 1, 1),
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = dailyOnceSchedule.DailyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 7, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 8, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 9, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 10, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2020, 5, 11, 2, 0, 0))));
        });
    }

    [Test]
    public void SeriesDailyOnce_CurrentDateTime_AfterExecutionTime_NoEndDate_InvalidBounds()
    {
        var currentDate = new DateTime(2020, 5, 5, 3, 0, 0);
        var dailyOnceSchedule = new DailyOnceSchedule
        {
            Name = "Send email",
            IsEnabled = true,
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2020, 5, 8),
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = dailyOnceSchedule.DailyOnceScheduleSeries(currentDate, 6);
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
    public void SeriesDailyOnce_CurrentDateTime_BeforeExecutionTime_Disabled()
    {
        var currentDate = new DateTime(2020, 5, 5, 1, 0, 0);
        var dailyOnceSchedule = new DailyOnceSchedule
        {
            Name = "Send email",
            IsEnabled = false,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 8),
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = dailyOnceSchedule.DailyOnceScheduleSeries(currentDate, 6);
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
    public void SeriesDailyOnce_CurrentDateTime_Equal_To_ExecutionTime_Disabled()
    {
        var currentDate = new DateTime(2020, 5, 5, 2, 0, 0);
        var dailyOnceSchedule = new DailyOnceSchedule
        {
            Name = "Send email",
            IsEnabled = false,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 8),
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = dailyOnceSchedule.DailyOnceScheduleSeries(currentDate, 6);
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
    public void SeriesDailyOnce_CurrentDateTime_After_ExecutionTime_Disabled()
    {
        var currentDate = new DateTime(2020, 5, 5, 3, 0, 0);
        var dailyOnceSchedule = new DailyOnceSchedule
        {
            Name = "Send email",
            IsEnabled = false,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 5, 8),
            ExecutionTime = new TimeSpan(2, 0, 0)
        };

        var series = dailyOnceSchedule.DailyOnceScheduleSeries(currentDate, 6);
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
}