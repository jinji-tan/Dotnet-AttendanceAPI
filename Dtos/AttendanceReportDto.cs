using System;

namespace AttendanceAPI.DTOs;

public class AttendanceReportDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public double DailyHours { get; set; }
    public double WeeklyHours { get; set; }
    public double MonthlyHours { get; set; }
}
