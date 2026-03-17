
using AttendanceAPI.DTOs;
using AttendanceAPI.Models;

namespace AttendanceAPI.Repositories;

public interface IAttendanceRepository
{
    Task<AttendanceRecord?> GetActiveTimeInAsync(int userId);
    Task<int> TimeInAsync(int userId, DateTime timeIn);
    Task<int> TimeOutAsync(int recordId, DateTime timeOut);
    Task<IEnumerable<AttendanceRecordDto>> GetRecordsByUserIdAsync(int userId);
    Task<IEnumerable<AttendanceRecordDto>> GetAllRecordsAsync();
    Task<AttendanceReportDto> GetUserHoursReportAsync(int userId, DateTime todayStr, DateTime startOfWeek, DateTime startOfMonth);
}
