using System;
using LanguageExt;
using TaskScheduler.Enums;

namespace TaskScheduler.Schedules.MonthlySchedules;

public class MonthlyPeriodOnceSchedule: MonthlySchedule
{
    public Day Day { get; set; }
    public Position Position {get; set; }
    
    public override Either<string, DateTime> GetNextExecutionDate(DateTime currentDate)
    {
        throw new NotImplementedException();
    }

    public override Either<string, ScheduleDetails> GetTaskDescription(DateTime currentDate)
    {
        throw new NotImplementedException();
    }

    protected override bool ValidateCurrentDate(DateTime currentDate)
    {
        throw new NotImplementedException();
    }
}