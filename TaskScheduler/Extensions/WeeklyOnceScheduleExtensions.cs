using System;
using System.Collections.Generic;
using LanguageExt;
using TaskScheduler.Schedules.WeeklySchedules;

namespace TaskScheduler.Extensions;

public static class WeeklyOnceScheduleExtensions
{
    public static List<Either<string, DateTime>> WeeklyOnceScheduleSeries(this WeeklyOnceSchedule weeklyOnceSchedule, DateTime currentDate, int count)
    {
        var results = new List<Either<string, DateTime>>();
        for (var i = 0; i < count; i++)
        {
            var date = weeklyOnceSchedule.GetNextExecutionDate(currentDate);
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