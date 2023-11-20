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

    public override string GetTaskDescription()
    {
        return $"Task will occur on {ExecutionDate}";
    }

    private bool ValidateCurrentDate(DateTime currentDate)
    {
        if (currentDate < ExecutionDate)
            return true;
        return currentDate.Date == ExecutionDate.Date && currentDate.TimeOfDay < ExecutionDate.TimeOfDay;
    }
}