using System;
using LanguageExt;
using LanguageExt.Pipes;

namespace TaskScheduler.Schedules;

public abstract class MonthlySchedule : RecurringSchedule
{
    public int EveryAfterMonths { get; set; }
}

public class MonthlyDayOnceSchedule : MonthlySchedule
{
    public int MonthlyDay { get; set; }
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
        if (EveryAfterMonths <= 0)
            return "Number of months can't be non positive!";
        if (MonthlyDay is <= 0 or > 31)
            return "Invalid month date!";
        if (currentDate < StartDate)
            return StartDate;
        if (!ValidateCurrentDate(currentDate)) 
            return "Current date is past end date!";
        
        var startingDate = GetExactStartingDate();
        startingDate = new DateTime(startingDate.Year, startingDate.Month, startingDate.Day,
            ExecutionTime.Hours, ExecutionTime.Minutes, ExecutionTime.Seconds);

        while (true)
        {
            if (startingDate > currentDate)
                return startingDate;
            if (startingDate.Date == currentDate.Date)
            {
                if (currentDate.TimeOfDay < ExecutionTime)
                    return startingDate;
                if (currentDate.TimeOfDay >= ExecutionTime)
                    return startingDate.AddMonths(EveryAfterMonths);
            }
            startingDate = startingDate.AddMonths(EveryAfterMonths);
        }
    }

    public override Either<string, ScheduleDetails> GetTaskDescription(DateTime currentDate)
    {
        throw new NotImplementedException();
    }
    
    private DateTime GetExactStartingDate()
    {
        if (StartDate.Day == MonthlyDay)
            return StartDate;
        if (MonthlyDay > StartDate.Day)
            return new DateTime(StartDate.Year, StartDate.Month, MonthlyDay);
        var exactDate = StartDate.AddMonths(1);
        return new DateTime(exactDate.Year, exactDate.Month, MonthlyDay);
    }

    protected override bool ValidateCurrentDate(DateTime currentDate)
    {
        return EndDate.Match(date => date > currentDate || (date.Date == currentDate.Date && date.TimeOfDay > currentDate.TimeOfDay)
            ,() => true);
    }
}