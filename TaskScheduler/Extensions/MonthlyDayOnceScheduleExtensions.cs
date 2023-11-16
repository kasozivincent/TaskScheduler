using System;
using System.Collections.Generic;
using LanguageExt;
using TaskScheduler.Schedules;

namespace TaskScheduler.Extensions;

public static class MonthlyDayOnceScheduleExtensions
{
    public static List<Either<string, DateTime>> MonthlyDayOnceScheduleSeries(this MonthlyDayOnceSchedule monthlyDayOnceSchedule, DateTime currentDate, int count)
    {
        var results = new List<Either<string, DateTime>>();
        for (var i = 0; i < count; i++)
        {
            var date = monthlyDayOnceSchedule.GetNextExecutionDate(currentDate);
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