using System;
using System.Collections.Generic;
using LanguageExt;
using TaskScheduler.Schedules;

namespace TaskScheduler.Extensions;

public static class RecurringDailyScheduleExtensions
{
    public static List<Either<string, DateTime>> DailyRecurringScheduleSeries(this DailyRecurringSchedule dailyRecurring, DateTime currentDate, int count)
    {
        var results = new List<Either<string, DateTime>>();
        for (var i = 0; i < count; i++)
        {
            var date = dailyRecurring.GetNextExecutionDate(currentDate);
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