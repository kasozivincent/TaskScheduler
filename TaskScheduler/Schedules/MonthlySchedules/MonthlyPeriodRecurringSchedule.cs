using System;
using System.Collections.Generic;
using LanguageExt;
using TaskScheduler.Enums;

namespace TaskScheduler.Schedules.MonthlySchedules;

public class MonthlyPeriodRecurringSchedule : MonthlySchedule
{
    public int EveryAfter { get; set; }
    public IntervalType IntervalType { get; set; }
    public TimeSpan StartingTime { get; set; }
    public TimeSpan EndingTime { get; set; }
    public Day Day { get; set; }
    public Position Position {get; set; }
    
    public override Either<string, DateTime> GetNextExecutionDate(DateTime currentDate)
    {
        if (IsEnabled == false)
            return "Schedule was cancelled!";
        if (EndDate.IsSome)
        {
            if(StartDate > EndDate)
                return "Start date can't be later than end date";
        }
        if (EveryAfterMonths <= 0)
            return "Number of months can't be non positive!";
        if (currentDate < StartDate)
            return StartDate;
        if (!ValidateCurrentDate(currentDate)) 
            return "Current date is past end date!";
        if (StartingTime >= EndingTime)
            return "Starting time can't be later than or equal to ending time";
        var startingDate = GetExactStartDate(StartDate.Month, StartDate.Year);
        startingDate = new DateTime(startingDate.Year, startingDate.Month, startingDate.Day,
            StartingTime.Hours, StartingTime.Minutes, StartingTime.Seconds);

        while (currentDate > startingDate)
        {
            if (currentDate.Year == startingDate.Year && currentDate.Month == startingDate.Month)
                break;
            startingDate = startingDate.AddMonths(EveryAfterMonths);
        }
        
        if (startingDate.Month == currentDate.Month && startingDate.Year == currentDate.Year)
        {
            var targetDate = GetDateOfDay(Position, Day, currentDate.Month, currentDate.Year);
            if (targetDate.Day == currentDate.Day)
            {
                if (currentDate.TimeOfDay < StartingTime)
                    return targetDate;
                if (currentDate.TimeOfDay >= StartingTime && currentDate.TimeOfDay < EndingTime)
                {
                    return IntervalType switch
                    {
                        IntervalType.Hours => currentDate.AddHours(EveryAfter),
                        IntervalType.Minutes => currentDate.AddMinutes(EveryAfter),
                        _ => currentDate.AddSeconds(EveryAfter)
                    };
                }
            }

            if (currentDate.Day < targetDate.Day)
            {
                return targetDate;
            }
            var nextMonth = startingDate.AddMonths(EveryAfterMonths);
           
            if (!ValidateCurrentDate(GetDateOfDay(Position, Day, nextMonth.Month, nextMonth.Year))) 
                return "Current date is past end date!";
            return GetDateOfDay(Position, Day, nextMonth.Month, nextMonth.Year);
        }
        return GetDateOfDay(Position, Day, startingDate.Month, startingDate.Year);
    }

    public override Either<string, ScheduleDetails> GetTaskDescription(DateTime currentDate)
    {
        throw new NotImplementedException();
    }

    protected override bool ValidateCurrentDate(DateTime currentDate)
    {
        if (EndDate.IsNone) 
            return true;
        var endDate = (DateTime)EndDate;
        if (currentDate > endDate)
            return false;
        return endDate.Date != currentDate.Date || currentDate.TimeOfDay <= endDate.TimeOfDay;
    }
    
      private DateTime GetExactStartDate(int month, int year)
    {
        var date = GetDateOfDay(Position, Day, month, year);
        return date >= StartDate 
            ?  StartDate
            : StartDate.AddMonths(EveryAfterMonths);
    }
    private  DateTime GetDateOfDay(Position position, Day day, int month, int year)
    {
        return day switch
        {
            Day.WeekendDay => GetWeekendDate(position, month, year),
            Day.Day => DayDate(position, month, year),
            Day.WeekDay => default,
            _ => GetDayDate(position, Convert(day), month, year)
        };
    }
    private  DateTime DayDate(Position position, int month, int year)
    {
        return position switch
        {
            Position.First => new DateTime(year, month, 1,StartingTime.Hours, StartingTime.Minutes, StartingTime.Seconds),
            Position.Second => new DateTime(year, month, 2,StartingTime.Hours, StartingTime.Minutes, StartingTime.Seconds),
            Position.Third => new DateTime(year, month, 3,StartingTime.Hours, StartingTime.Minutes, StartingTime.Seconds),
            Position.Fourth => new DateTime(year, month, 4,StartingTime.Hours, StartingTime.Minutes, StartingTime.Seconds),
            _ => new DateTime(year, month, DateTime.DaysInMonth(year, month),StartingTime.Hours, StartingTime.Minutes, StartingTime.Seconds)
        };
    }
    private  DateTime GetWeekendDate(Position position, int month, int year)
    {
        var dates = WeekEndDates();
        return position switch
        {
            Position.First => dates[0],
            Position.Second => dates[1],
            Position.Third => dates[2],
            Position.Fourth => dates[3],
            Position.Last => dates[^1],
            _ => throw new ArgumentException("Invalid position value", nameof(position))
        };
        
        List<DateTime> WeekEndDates()
        {
            var weekEndDates = new List<DateTime>();
            var currentDate = new DateTime(year, month, 1, StartingTime.Hours, StartingTime.Minutes,
                StartingTime.Seconds);
            while (currentDate.Month == month)
            {
                if(currentDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                    weekEndDates.Add(currentDate);
                currentDate = currentDate.AddDays(1);
            }
            return weekEndDates;
        }
    }
    private  DateTime GetDayDate(Position position, DayOfWeek day, int month, int year)
    {
        var dates = DayDates();
        return position switch
        {
            Position.First => dates[0],
            Position.Second => dates[1],
            Position.Third => dates[2],
            Position.Fourth => dates[3],
            Position.Last => dates[^1],
            _ => throw new ArgumentException("Invalid position value", nameof(position))
        };
        
        List<DateTime> DayDates()
        {
            var dayDates = new List<DateTime>();
            var currentDate = new DateTime(year, month, 1,StartingTime.Hours, StartingTime.Minutes,
                StartingTime.Seconds);
            while (currentDate.Month == month)
            {
                if(currentDate.DayOfWeek == day)
                    dayDates.Add(currentDate);
                currentDate = currentDate.AddDays(1);
            }
            return dayDates;
        }
    }
    private static DayOfWeek Convert(Day day)
    {
        var dayOfWeek = day switch
        {
            Day.WeekendDay => DayOfWeek.Monday,
            Day.WeekDay => DayOfWeek.Monday,
            Day.Monday => DayOfWeek.Monday,
            Day.Tuesday => DayOfWeek.Tuesday,
            Day.Wednesday => DayOfWeek.Wednesday,
            Day.Thursday => DayOfWeek.Thursday,
            Day.Friday => DayOfWeek.Friday,
            Day.Saturday => DayOfWeek.Saturday,
            Day.Sunday => DayOfWeek.Sunday,
            Day.Day => DayOfWeek.Monday,
            _ => throw new ArgumentException()
        };
        return dayOfWeek;
    }
}