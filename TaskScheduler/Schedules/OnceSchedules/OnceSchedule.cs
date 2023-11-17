using System;
using LanguageExt;
using TaskScheduler.Contracts;

namespace TaskScheduler.Schedules;

public class OnceSchedule : ScheduleConfiguration
{
    public DateTime ExecutionDate { get; set; }

    public override Either<string, DateTime> GetNextExecutionDate(DateTime currentDate)
       => (ValidateCurrentDate(currentDate))
            ? ExecutionDate
              : "Current Date is past execution date";

    public override Either<string, ScheduleDetails> GetTaskDescription(DateTime currentDate)
        => ValidateCurrentDate(currentDate) 
        ? new ScheduleDetails(ExecutionDate, $"Schedule will execute on {ExecutionDate}")
        : "Current Date is past execution date";

    private bool ValidateCurrentDate(DateTime currentDate)
    {
        if (currentDate < ExecutionDate)
            return true;
        return currentDate.Date == ExecutionDate.Date && currentDate.TimeOfDay < ExecutionDate.TimeOfDay;
    }
}