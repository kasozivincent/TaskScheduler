using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using TaskScheduler.Schedules;

namespace TaskScheduler.Contracts;

public abstract class WeeklySchedule: RecurringSchedule
{
    public int EveryAfterWeeks { get; set; }
    public List<DayOfWeek> Days { get; set; }
    protected static List<DateTime> AddWeek(IEnumerable<DateTime> weekDates, int weeks)
    {
       return weekDates.Select(date => date.AddDays(weeks * 7)).ToList(); 
    }
    protected IEnumerable<DateTime> ComputeWeek(List<DateTime> startWeek, DateTime currentDate)
    {
        var week = startWeek;
        while (true)
        {
            if (week.Contains(currentDate.Date))
                return week;
            if (week[0] > currentDate)
                return week;
            week = AddWeek(week, EveryAfterWeeks);
        }
    }

    private bool SearchDate(List<DateTime> week, DateTime currentDate)
    {
        foreach (var date in week)
        {
            if (date.Date == currentDate.Date)
                return true;
        }
        return false;
    }
   
    protected abstract Either<string, DateTime> ValidateNextDate(DateTime date);
    protected override bool ValidateCurrentDate(DateTime currentDate)
    {
        if (EndDate.IsNone)
            return true;
        var endDate = (DateTime)EndDate;
        if (currentDate > endDate)
            return false;
        if (currentDate.Date == endDate.Date && currentDate.TimeOfDay > endDate.TimeOfDay)
            return false;
        return true;
    }
    protected List<DateTime> GetStartingWeek()
    {
        var firstDay = StartDate;
        var week = new List<DateTime>();
        for (var i = 0; i <= 6; i++)
        {
            week.Add(firstDay.AddDays(i));
        }
        return week;
    }
    
    protected  string SortAndFormatDayOfWeek()
    {
        if (Days.Count == 1)
            return Days[0].ToString();
        var sortedDays = Days.OrderBy(d => d).ToList();
        var formattedDays = string.Join(", ", sortedDays.Take(sortedDays.Count - 1).Select(d => d.ToString()));
        var lastDay = sortedDays.Last().ToString();
        return $"{formattedDays} and {lastDay}";
    }
}