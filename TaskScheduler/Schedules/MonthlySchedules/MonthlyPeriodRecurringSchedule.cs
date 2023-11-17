using System;
using LanguageExt;

namespace TaskScheduler.Schedules.MonthlySchedules;

public class MonthlyPeriodRecurringSchedule : MonthlySchedule
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