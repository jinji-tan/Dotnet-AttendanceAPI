namespace AttendanceAPI.Models;

public class AttendanceRecord
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime TimeIn { get; set; }
    public DateTime? TimeOut { get; set; }
}