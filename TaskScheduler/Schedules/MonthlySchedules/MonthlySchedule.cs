namespace TaskScheduler.Schedules.MonthlySchedules;

public abstract class MonthlySchedule : RecurringSchedule
{
    public int EveryAfterMonths { get; set; }
    
    
}

