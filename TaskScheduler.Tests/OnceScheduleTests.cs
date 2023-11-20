using TaskScheduler.Schedules;
using LanguageExt.UnitTesting;

namespace TaskScheduler.Tests;

[TestFixture]
public class OnceScheduleTests
{
   [Test]
    public void GetNextExecutionDate_CurrentDateBeforeExecutionDate_ReturnsExecutionDate()
    {
        var currentDate = new DateTime(2022, 1, 1, 2, 0, 0);
        var schedule = new OnceSchedule
        {
            Name = "Schedule 2",
            ExecutionDate = new DateTime(2022, 1, 10,2, 0, 0)
        };

        var result = schedule.GetNextExecutionDate(currentDate);
        result.ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2022, 1, 10,2, 0, 0))));
    }

    [Test]
    public void GetNextExecutionDate_CurrentDateEqualToExecutionDate_BeforeExecutionTime_ReturnsExecutionDate()
    {
        var currentDate = new DateTime(2022, 1, 10, 1, 0, 0);
        var schedule = new OnceSchedule
        {
            Name = "Schedule 2",
            ExecutionDate = new DateTime(2022, 1, 10, 2, 0, 0)
        };

        var result = schedule.GetNextExecutionDate(currentDate);
        result.ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2022, 1, 10, 2, 0, 0))));
    }
    
    [Test]
    public void GetNextExecutionDate_CurrentDateEqualToExecutionDate_ExactExecutionTime_ReturnsExecutionDate()
    {
        var currentDate = new DateTime(2022, 1, 10, 2, 0, 0);
        var schedule = new OnceSchedule
        {
            Name = "Schedule 2",
            ExecutionDate = new DateTime(2022, 1, 10, 2, 0, 0)
        };

        var result = schedule.GetNextExecutionDate(currentDate);
        
        result.ShouldBeLeft(error => Assert.That(error, Is.EqualTo("Current Date is past execution date")));
    }
    
    [Test]
    public void GetNextExecutionDate_CurrentDateEqualToExecutionDate_AfterExecutionTime_ReturnsExecutionDate()
    {
        var currentDate = new DateTime(2022, 1, 10, 4, 0, 0);
        var schedule = new OnceSchedule
        {
            Name = "Schedule 2",
            ExecutionDate = new DateTime(2022, 1, 10, 2, 0, 0)
        };

        var result = schedule.GetNextExecutionDate(currentDate);
        
        result.ShouldBeLeft(error => Assert.That(error, Is.EqualTo("Current Date is past execution date")));
    }

    [Test]
    public void GetNextExecutionDate_CurrentDateAfterExecutionDate_ReturnsPastExecutionDateError()
    {
        var currentDate = new DateTime(2022, 1, 15, 1, 0, 0);
        var schedule = new OnceSchedule
        {
            Name = "Schedule 3",
            ExecutionDate = new DateTime(2022, 1, 10, 2, 0, 0)
        };

        var result = schedule.GetNextExecutionDate(currentDate);

        result.ShouldBeLeft(error => Assert.That(error, Is.EqualTo("Current Date is past execution date")));
    }

    [Test]
    public void GetTaskDescription_ReturnsScheduleDetails()
    {
        var currentDate = new DateTime(2022, 1, 1);
        var schedule = new OnceSchedule
        {
            Name = "Schedule 4",
            ExecutionDate = new DateTime(2022, 1, 10)
        };

        var result = (string)schedule.GetTaskDescription();
        Assert.That(result, Is.EqualTo($"Task will occur on {schedule.ExecutionDate}"));

    }
}