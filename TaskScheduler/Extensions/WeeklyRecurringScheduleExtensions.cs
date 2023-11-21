using System;
using System.Collections.Generic;
using LanguageExt;
using TaskScheduler.Schedules.WeeklySchedules;

namespace TaskScheduler.Extensions;

public static class WeeklyRecurringScheduleExtensions
{
    public static List<Either<string, DateTime>> WeeklyRecurringScheduleSeries(this WeeklyRecurringSchedule weeklyRecurringSchedule, DateTime currentDate, int count)
    {
        var results = new List<Either<string, DateTime>>();
        for (var i = 0; i < count; i++)
        {
            var date = weeklyRecurringSchedule.GetNextExecutionDate(currentDate);
            if (date.IsRight)
            {
                results.Add((DateTime)date);
                currentDate = (DateTime)date;
            }
            else
                results.Add((string)date);
        }
        return results;
    }
}