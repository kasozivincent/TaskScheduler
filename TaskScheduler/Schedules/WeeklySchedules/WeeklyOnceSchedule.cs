using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using TaskScheduler.Contracts;

namespace TaskScheduler.Schedules.WeeklySchedules;

public class WeeklyOnceSchedule : WeeklySchedule
{
    public TimeSpan ExecutionTime { get; set; }
    
    public override Either<string, DateTime> GetNextExecutionDate(DateTime currentDate)
    {
        if (IsEnabled == false)
            return "Schedule was cancelled!";
        if (EndDate.IsSome)
        {
            if(StartDate > EndDate)
                return "Start date can't be later than end date";
        }
        if (currentDate < StartDate)
            return StartDate;
        if (!ValidateCurrentDate(currentDate)) 
            return "Current date is past end date!";
        var startWeek = GetStartingWeek();
        var exactWeek = ComputeWeek(startWeek, currentDate).ToList();

        if (exactWeek[0] > currentDate)
        {
            foreach (var date in exactWeek.Where(date => Days.Contains(date.DayOfWeek)))
                return ValidateNextDate(date);
        }

        var currentDay = currentDate.DayOfWeek;

        if (Days.Contains(currentDay))
        {
            if (currentDate.TimeOfDay < ExecutionTime)
                return ValidateNextDate(currentDate);
        }
        while (true)
        {
            currentDate = currentDate.AddDays(1);
            if (currentDate > exactWeek[6])
            {
                var newWeek = AddWeek(exactWeek, EveryAfterWeeks);
                foreach (var date in newWeek.Where(date => Days.Contains(date.DayOfWeek)))
                    return ValidateNextDate(date);
            }
            if (Days.Contains(currentDate.DayOfWeek))
                return ValidateNextDate(currentDate);
        }
    }

    protected override Either<string, DateTime> ValidateNextDate(DateTime date)
    {
        if (!ValidateCurrentDate(date))
            return "Current date is past end date!";
        return new DateTime(date.Year, date.Month, date.Day,
            ExecutionTime.Hours, ExecutionTime.Minutes, ExecutionTime.Seconds);
    }
    
    public override string GetTaskDescription()
    {
        return $"Occurs every week on {SortAndFormatDayOfWeek()} at {ExecutionTime}. Schedule will be used" +
               $"starting on {StartDate}";
    }
}