using System;
using LanguageExt;

namespace TaskScheduler.Contracts;

public abstract class ScheduleConfiguration
{
    public  string Name { get; set; }
    public bool IsEnabled { get; set; }
    public abstract Either<string, DateTime> GetNextExecutionDate(DateTime currentDate);
    public abstract string GetTaskDescription();
}