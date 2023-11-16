using System;
using System.Collections.Generic;
using LanguageExt;
using static LanguageExt.Prelude;
using TaskScheduler.Schedules;

namespace TaskScheduler.Extensions;

public static class DailyOnceScheduleExtensions
{
    public static List<Either<string, DateTime>> DailyOnceScheduleSeries(this DailyOnceSchedule dailyOnce, DateTime currentDate, int count)
    {
        var results = new List<Either<string, DateTime>>();
        for (var i = 0; i < count; i++)
        {
            var date = dailyOnce.GetNextExecutionDate(currentDate);
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