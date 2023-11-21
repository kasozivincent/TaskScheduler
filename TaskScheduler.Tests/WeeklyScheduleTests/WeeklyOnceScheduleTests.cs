using LanguageExt.UnitTesting;
using TaskScheduler.Extensions;
using TaskScheduler.Schedules.WeeklySchedules;

namespace TaskScheduler.Tests.WeeklyScheduleTests;

[TestFixture]
public class WeeklyOnceScheduleTests
{
    [Test]
    public void CurrentDateBeforeStartDate_Returns_StartDate()
    {
        var currentDate = new DateTime(2022, 06, 04);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 1,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek> { DayOfWeek.Monday , DayOfWeek.Saturday}
        };

        var nextDate = schedule.GetNextExecutionDate(currentDate);
        var expectedDate = new DateTime(2023, 1, 1);
        nextDate.ShouldBeRight(value => Assert.That(value, Is.EqualTo(expectedDate)));
    }
    
    [Test]
    public void ScheduleDisabled_Returns_ErrorMessage()
    {
        var currentDate = new DateTime(2022, 06, 04);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = false,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 1,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek> { DayOfWeek.Monday , DayOfWeek.Saturday}
        };

        var nextDate = schedule.GetNextExecutionDate(currentDate);
        const string expectedResult = "Schedule was cancelled!";
        nextDate.ShouldBeLeft(value => Assert.That(value, Is.EqualTo(expectedResult)));
    }
    
    #region Single day
    [Test]
    public void WeeklySeries_Monday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Monday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 20, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 4, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 18, 2, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Monday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Monday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Monday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 20, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 4, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 18, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 1, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 15, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Monday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Tuesday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Tuesday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 21, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 5, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 19, 2, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Tuesday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Tuesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Tuesday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 21, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 5, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 19, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 2, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 16, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Tuesday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Wednesday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Wednesday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 15, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 29, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 13, 2, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Wednesday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Wednesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Wednesday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 15, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 29, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 13, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 27, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 10, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 24, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Wednesday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Thursday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Thursday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 16, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 30, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 14, 2, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Thursday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Thursday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Thursday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 16, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 30, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 14, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 28, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 11, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 25, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Thursday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Friday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Friday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 17, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 1, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 15, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 29, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Friday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Friday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Friday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 17, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 1, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 15, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 29, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 12, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 26, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Friday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Saturday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Saturday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 18, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 2, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 16, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 30, 2, 0, 0))));
            series[4].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Saturday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Saturday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Saturday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 18, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 2, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 16, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 30, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 13, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 27, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Saturday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Sunday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Sunday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 19, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 3, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 17, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 31, 2, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Sunday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Sunday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Sunday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 19, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 3, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 17, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 31, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 14, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Sunday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    #endregion
    
    #region Two days

    [Test]
    public void WeeklySeries_Monday_Tuesday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 7, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 20, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 21, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 4, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 5, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 18, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 19, 2, 0, 0))));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Monday and Tuesday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Monday_Tuesday_NoEndDate()
    {
         var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 7, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 20, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 21, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 4, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 5, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 18, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 19, 2, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 1, 2, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 2, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Monday and Tuesday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Wednesday_Friday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Wednesday,
                DayOfWeek.Friday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 15, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 17, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 29, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 1, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 13, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 15, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 27, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 29, 2, 0, 0))));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Wednesday and Friday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Wednesday_Friday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Wednesday,
                DayOfWeek.Friday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 15, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 17, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 29, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 1, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 13, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 15, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 27, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 29, 2, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 10, 2, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 12, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Wednesday and Friday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Thursday_Sunday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Thursday,
                DayOfWeek.Sunday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 16, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 19, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 30, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 3, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 14, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 17, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 28, 2, 0, 0))));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Sunday and Thursday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Thursday_Sunday_NoEndDate()
    {
         var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Thursday,
                DayOfWeek.Sunday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 16, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 19, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 30, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 3, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 14, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 17, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 28, 2, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 31, 2, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 11, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Sunday and Thursday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }

    [Test]
    public void WeeklySeries_Monday_Saturday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Saturday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 18, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 20, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 2, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 4, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 16, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 18, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 30, 2, 0, 0))));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Monday and Saturday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Monday_Saturday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 2,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Saturday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 18, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 20, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 2, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 4, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 16, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 18, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 30, 2, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 1, 2, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 13, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Monday and Saturday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    #endregion
    
    #region Three days
    [Test]
    public void WeeklySeries_Monday_Tuesday_Wednesday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 3,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 7, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 22, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 27, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 28, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 13, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 18, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 19, 2, 0, 0))));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Monday, Tuesday and Wednesday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Monday_Tuesday_Wednesday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 3,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 7, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 22, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 27, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 28, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 13, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 18, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 19, 2, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 3, 2, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 8, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Monday, Tuesday and Wednesday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Monday_Wednesday_Friday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 3,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Wednesday,
                DayOfWeek.Friday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 22, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 24, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 27, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 13, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 15, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 18, 2, 0, 0))));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Monday, Wednesday and Friday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Monday_Wednesday_Friday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 3,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Wednesday,
                DayOfWeek.Friday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 22, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 24, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 27, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 13, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 15, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 18, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 3, 2, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 5, 2, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 8, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Monday, Wednesday and Friday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Sunday_Friday_Saturday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 3,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Sunday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 24, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 25, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 26, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 15, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 16, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 17, 2, 0, 0))));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Sunday, Friday and Saturday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Sunday_Friday_Saturday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 3,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Sunday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 5, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 24, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 25, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 26, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 15, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 16, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 17, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 5, 2, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 6, 2, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 7, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Sunday, Friday and Saturday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Tuesday_Wednesday_Friday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 3,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Friday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 22, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 24, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 28, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 13, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 15, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 19, 2, 0, 0))));
            series[7].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[8].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[9].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Tuesday, Wednesday and Friday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    
    [Test]
    public void WeeklySeries_Tuesday_Wednesday_Friday_NoEndDate()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyOnceSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EveryAfterWeeks = 3,
            ExecutionTime = new TimeSpan(2, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Friday
            }
        };

        var series = schedule.WeeklyOnceScheduleSeries(currentDate, 10);
        Assert.That(series, Has.Count.EqualTo(10));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 7, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 22, 2, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 24, 2, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 28, 2, 0, 0))));
            series[4].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 13, 2, 0, 0))));
            series[5].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 15, 2, 0, 0))));
            series[6].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 19, 2, 0, 0))));
            series[7].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 3, 2, 0, 0))));
            series[8].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 5, 2, 0, 0))));
            series[9].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2024, 1, 9, 2, 0, 0))));
        });
        
        var description = (string)schedule.GetTaskDescription();
        var expected = $"Occurs every week on Tuesday, Wednesday and Friday at {new TimeSpan(2, 0, 0)}. Schedule will be used" +
                       $"starting on {new DateTime(2023, 11, 1)}";
        
        Assert.That(description, Is.EqualTo(expected));
    }
    #endregion
}