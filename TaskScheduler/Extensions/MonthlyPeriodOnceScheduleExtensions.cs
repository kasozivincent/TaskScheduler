using System;
using System.Collections.Generic;
using LanguageExt;
using TaskScheduler.Schedules.MonthlySchedules;

namespace TaskScheduler.Extensions;

public static class MonthlyPeriodOnceScheduleExtensions
{
    public static List<Either<string, DateTime>> MonthlyPeriodOnceScheduleSeries(this MonthlyPeriodOnceSchedule monthlyPeriodOnceSchedule, DateTime currentDate, int count)
    {
        var results = new List<Either<string, DateTime>>();
        for (var i = 0; i < count; i++)
        {
            var date = monthlyPeriodOnceSchedule.GetNextExecutionDate(currentDate);
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