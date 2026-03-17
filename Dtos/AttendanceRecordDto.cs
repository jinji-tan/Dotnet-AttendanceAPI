
namespace AttendanceAPI.DTOs;

public class AttendanceRecordDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime TimeIn { get; set; }
    public DateTime? TimeOut { get; set; }
    
    // Formatting for Military Time representations
    public string TimeInStr => TimeIn.ToString("yyyy-MM-dd HH:mm:ss");
    public string TimeOutStr => TimeOut?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A";

    public double HoursWorked => TimeOut.HasValue ? (TimeOut.Value - TimeIn).TotalHours : 0;
}