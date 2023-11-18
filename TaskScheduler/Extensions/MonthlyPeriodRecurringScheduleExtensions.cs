using System;
using System.Collections.Generic;
using LanguageExt;
using TaskScheduler.Schedules.MonthlySchedules;

namespace TaskScheduler.Extensions;

public static class MonthlyPeriodRecurringScheduleExtensions
{
    public static List<Either<string, DateTime>> MonthlyTheOnceScheduleSeries(this MonthlyPeriodRecurringSchedule monthlyPeriodRecurringSchedule, DateTime currentDate, int count)
    {
        var results = new List<Either<string, DateTime>>();
        for (var i = 0; i < count; i++)
        {
            var date = monthlyPeriodRecurringSchedule.GetNextExecutionDate(currentDate);
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