using System;
using LanguageExt;
using TaskScheduler.Contracts;

namespace TaskScheduler.Schedules;

public abstract class RecurringSchedule : ScheduleConfiguration
{
    public DateTime StartDate { get; set; }
    public Option<DateTime> EndDate { get; set; }
    protected abstract bool ValidateCurrentDate(DateTime currentDate) ;
}