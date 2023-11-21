using LanguageExt.UnitTesting;
using TaskScheduler.Enums;
using TaskScheduler.Extensions;
using TaskScheduler.Schedules.WeeklySchedules;

namespace TaskScheduler.Tests.WeeklyScheduleTests;

[TestFixture]
public class WeeklyRecurringScheduleTests
{
    [Test]
    public void SeriesMonday()
    {
        var currentDate = new DateTime(2023, 11, 5);
        var schedule = new WeeklyRecurringSchedule
        {
            Name = "Go to Spain",
            IsEnabled = true,
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2023, 12, 31),
            EveryAfterWeeks = 2,
            EveryAfter = 1,
            IntervalType = IntervalType.Hour,
            StartingTime = new TimeSpan(2, 0, 0),
            EndingTime = new TimeSpan(4, 0, 0),
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Monday
            }
        };

        var series = schedule.WeeklyRecurringScheduleSeries(currentDate, 6);
        Assert.That(series, Has.Count.EqualTo(6));
        Assert.Multiple(() =>
        {
            series[0].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 2, 0, 0))));
            series[1].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 3, 0, 0))));
            series[2].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 11, 6, 4, 0, 0))));
            series[3].ShouldBeRight(value => Assert.That(value, Is.EqualTo(new DateTime(2023, 12, 18, 2, 0, 0))));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
            series[5].ShouldBeLeft(value => Assert.That(value, Is.EqualTo("Current date is past end date!")));
        });
    }
}