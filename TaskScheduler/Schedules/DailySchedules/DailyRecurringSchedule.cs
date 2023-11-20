using System;
using LanguageExt;
using TaskScheduler.Enums;

namespace TaskScheduler.Schedules.DailySchedules;

public class DailyRecurringSchedule : RecurringSchedule
{
    public int EveryAfter { get; set; }
    public IntervalType IntervalType { get; set; }
    public TimeSpan StartingTime { get; set; }
    public TimeSpan EndingTime { get; set; }


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
        var date = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
            StartingTime.Hours, StartingTime.Minutes, StartingTime.Seconds);
        
        if (currentDate.TimeOfDay < StartingTime)
            return date;
        if (currentDate.TimeOfDay >= StartingTime && currentDate.TimeOfDay < EndingTime)
        {
            return IntervalType switch
            {
                IntervalType.Hour => GetNextWholeHour(currentDate),
                IntervalType.Minute => currentDate.AddMinutes(EveryAfter),
                _ => currentDate.AddSeconds(EveryAfter)
            };
        }
        return date.AddDays(1);
    }

    public override string GetTaskDescription()
    {
        return $"Occurs everyday every {EveryAfter} {IntervalType}(s) between {StartingTime}" +
               $"and {EndingTime}. Schedule will be used starting on {StartDate}";
    }
    
    private DateTime GetNextWholeHour(DateTime time)
    {
        var nextHour = time.AddHours(EveryAfter);
        var nextWholeHour = new DateTime(nextHour.Year, nextHour.Month, nextHour.Day, nextHour.Hour, 0, 0);
        return nextWholeHour;
    }

    protected override bool ValidateCurrentDate(DateTime currentDate)
    {
        return EndDate.Match(date => date > currentDate || (date.Date == currentDate.Date && EndingTime > currentDate.TimeOfDay)
            ,() => true);
    }
}