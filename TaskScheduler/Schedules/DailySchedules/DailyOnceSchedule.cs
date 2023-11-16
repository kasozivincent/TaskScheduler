﻿using System;
using LanguageExt;

namespace TaskScheduler.Schedules.DailySchedules;

public class DailyOnceSchedule : RecurringSchedule
{
    public TimeSpan ExecutionTime { get; set; }

    public override Either<string, DateTime> GetNextExecutionDate(DateTime currentDate)
    {
        if (IsEnabled == false)
            return "Schedule was cancelled!";
        if (StartDate > EndDate)
            return "Start date can't be later than end date";
        if (currentDate < StartDate)
            return StartDate;
        if (!ValidateCurrentDate(currentDate)) 
            return "Current date is past end date!";
        var date = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
            ExecutionTime.Hours, ExecutionTime.Minutes, ExecutionTime.Seconds);

        return currentDate.TimeOfDay >= ExecutionTime 
            ? date.AddDays(1) 
            : date;
    }

    public override Either<string, ScheduleDetails> GetTaskDescription(DateTime currentDate)
    {
        if (IsEnabled == false)
            return "Schedule was cancelled!";
        if (EndDate.IsSome)
        {
            if(StartDate > EndDate)
                return "Start date can't be later than end date";
        }
        if (currentDate < StartDate)
            return new ScheduleDetails(StartDate, $"Occurs every day. Schedule will be used starting on {StartDate}");
        if (!ValidateCurrentDate(currentDate)) 
            return "Current date is past end date!";
        var date = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
            ExecutionTime.Hours, ExecutionTime.Minutes, ExecutionTime.Seconds);

        return currentDate.TimeOfDay >= ExecutionTime 
            ? new ScheduleDetails(date.AddDays(1), $"Occurs every day. Schedule will be used starting on {StartDate}") 
            : new ScheduleDetails(date, $"Occurs every day. Schedule will be used starting on {StartDate}");
    }
    
    protected override bool ValidateCurrentDate(DateTime currentDate) 
        => EndDate.Match(date => date > currentDate || (date.Date == currentDate.Date && date.TimeOfDay > currentDate.TimeOfDay)
    ,() => true);
}
