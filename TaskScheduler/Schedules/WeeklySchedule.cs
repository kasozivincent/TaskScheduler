using System;
using LanguageExt;

namespace TaskScheduler.Schedules;

public class WeeklySchedule : RecurringSchedule
{
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