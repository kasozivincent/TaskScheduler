using System;
using System.Collections.Generic;
using LanguageExt;

namespace TaskScheduler.Schedules;

public class WeeklyOnceSchedule : RecurringSchedule
{
    public int EveryAfterWeeks { get; set; }
    
    public List<bool> Days { get; set; }
    public override Either<string, DateTime> GetNextExecutionDate(DateTime currentDate)
    {
        throw new NotImplementedException();
    }

    public override string GetTaskDescription()
    {
        throw new NotImplementedException();
    }

    protected override bool ValidateCurrentDate(DateTime currentDate)
    {
        throw new NotImplementedException();
    }
}