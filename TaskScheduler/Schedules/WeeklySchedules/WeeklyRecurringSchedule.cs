using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using TaskScheduler.Contracts;
using TaskScheduler.Enums;

namespace TaskScheduler.Schedules.WeeklySchedules;

public class WeeklyRecurringSchedule : WeeklySchedule
{
    public int EveryAfter { get; set; }
    
    public IntervalType IntervalType { get; set; }
    public TimeSpan StartingTime { get; set; }
    public TimeSpan EndingTime { get; set; }
    
    protected override Either<string, DateTime> ValidateNextDate(DateTime date)
    {
        if (!ValidateCurrentDate(date))
            return "Current date is past end date!";
        return new DateTime(date.Year, date.Month, date.Day,
            StartingTime.Hours, StartingTime.Minutes, StartingTime.Seconds);
    }
    
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
        if (StartingTime >= EndingTime)
            return "Starting time can't be later than or equal to ending time";
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
            if (currentDate.TimeOfDay < StartingTime)
                return ValidateNextDate(currentDate);
            if (currentDate.TimeOfDay >= StartingTime && currentDate.TimeOfDay < EndingTime)
            {
                return IntervalType switch
                {
                    IntervalType.Hour => currentDate.AddHours(EveryAfter),
                    IntervalType.Minute => currentDate.AddMinutes(EveryAfter),
                    _ => currentDate.AddSeconds(EveryAfter)
                };
            }
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

    public override string GetTaskDescription()
    {
        throw new NotImplementedException();
    }

   
}