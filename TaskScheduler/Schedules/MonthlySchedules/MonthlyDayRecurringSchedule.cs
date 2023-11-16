using System;
using LanguageExt;
using TaskScheduler.Enums;

namespace TaskScheduler.Schedules.MonthlySchedules;

public class MonthlyDayRecurringSchedule : MonthlySchedule
{
    public int MonthlyDay { get; set; }
    public int EveryAfter { get; set; }
    public IntervalType IntervalType { get; set; }
    public TimeSpan StartingTime { get; set; }
    public TimeSpan EndingTime { get; set; }
    
    private DateTime GetExactStartingDate()
    {
        if (StartDate.Day == MonthlyDay)
            return StartDate;
        if (MonthlyDay > StartDate.Day)
            return new DateTime(StartDate.Year, StartDate.Month, MonthlyDay);
        var exactDate = StartDate.AddMonths(1);
        return new DateTime(exactDate.Year, exactDate.Month, MonthlyDay);
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
        if (EveryAfterMonths <= 0)
            return "Number of months can't be non positive!";
        if (MonthlyDay is <= 0 or > 31)
            return "Invalid month date!";
        if (currentDate < StartDate)
            return StartDate;
        if (!ValidateCurrentDate(currentDate)) 
            return "Current date is past end date!";
        if (StartingTime >= EndingTime)
            return "Starting time can't be later than or equal to ending time";
        
        var startingDate = GetExactStartingDate();
        startingDate = new DateTime(startingDate.Year, startingDate.Month, startingDate.Day,
                StartingTime.Hours, StartingTime.Minutes, StartingTime.Seconds);

        while (true)
        {
            if (startingDate > currentDate)
                return startingDate;
            if (startingDate.Date == currentDate.Date)
            {
                if (currentDate.TimeOfDay < StartingTime)
                    return startingDate;
                if (currentDate.TimeOfDay >= StartingTime && currentDate.TimeOfDay < EndingTime)
                {
                    return IntervalType switch
                    {
                        IntervalType.Hours => startingDate.AddHours(EveryAfter),
                        IntervalType.Minutes => startingDate.AddMinutes(EveryAfter),
                        _ => startingDate.AddSeconds(EveryAfter),
                    };
                }
                return startingDate.AddMonths(EveryAfterMonths);
            }
            startingDate = startingDate.AddMonths(EveryAfterMonths);
        }
    }

    public override Either<string, ScheduleDetails> GetTaskDescription(DateTime currentDate)
    {
        throw new NotImplementedException();
    }

    protected override bool ValidateCurrentDate(DateTime currentDate)
    {
        if (EndDate.IsNone) 
            return true;
        var endDate = (DateTime)EndDate;
        if (currentDate > endDate)
            return false;
        return endDate.Date != currentDate.Date || currentDate.TimeOfDay <= endDate.TimeOfDay;
    }
}