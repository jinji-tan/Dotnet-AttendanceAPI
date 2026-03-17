using AttendanceAPI.Data;
using AttendanceAPI.DTOs;
using AttendanceAPI.Models;

namespace AttendanceAPI.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly DataContextDapper dapper;
    public AttendanceRepository(DataContextDapper dapper)
    {
        this.dapper = dapper;
    }

    public async Task<AttendanceRecord?> GetActiveTimeInAsync(int userId)
    {
        var sql = "SELECT TOP 1 * FROM AttendanceRecords WHERE UserId = @UserId AND TimeOut IS NULL ORDER BY TimeIn DESC";

        return await dapper.LoadDataSingle<AttendanceRecord>(sql, new { UserId = userId });
    }

    public async Task<int> TimeInAsync(int userId, DateTime timeIn)
    {
        var sql = @"
            INSERT INTO AttendanceRecords (UserId, TimeIn)
            VALUES (@UserId, @TimeIn);
            
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
        return await dapper.ExecuteSqlScalar(sql, new { UserId = userId, TimeIn = timeIn });
    }

    public async Task<int> TimeOutAsync(int recordId, DateTime timeOut)
    {
        var query = "UPDATE AttendanceRecords SET TimeOut = @TimeOut WHERE Id = @RecordId";

        return await dapper.ExecuteSqlWithRowCount(query, new { RecordId = recordId, TimeOut = timeOut });
    }

    public async Task<IEnumerable<AttendanceRecordDto>> GetRecordsByUserIdAsync(int userId)
    {
        var sql = @"
            SELECT 
                r.Id, r.UserId, u.Username, u.FirstName, u.LastName, r.TimeIn, r.TimeOut
            FROM AttendanceRecords r
            INNER JOIN Users u ON r.UserId = u.Id
            WHERE r.UserId = @UserId
            ORDER BY r.TimeIn DESC
        ";

        return await dapper.LoadDataWitParameters<AttendanceRecordDto>(sql, new { UserId = userId });
    }

    public async Task<IEnumerable<AttendanceRecordDto>> GetAllRecordsAsync()
    {
        var sql = @"
            SELECT 
                r.Id, r.UserId, u.Username, u.FirstName, u.LastName, r.TimeIn, r.TimeOut
            FROM AttendanceRecords r
            INNER JOIN Users u ON r.UserId = u.Id
            ORDER BY r.TimeIn DESC
        ";
        return await dapper.LoadData<AttendanceRecordDto>(sql);
    }

    public async Task<AttendanceReportDto> GetUserHoursReportAsync(int userId, DateTime todayStr, DateTime startOfWeek, DateTime startOfMonth)
    {
        var sql = @"
            SELECT
                u.Id as UserId,
                u.Username,
                ISNULL((SELECT SUM(DATEDIFF(MINUTE, r.TimeIn, r.TimeOut)) / 60.0 FROM AttendanceRecords r WHERE r.UserId = @UserId AND r.TimeIn >= @TodayStr AND r.TimeOut IS NOT NULL), 0) AS DailyHours,
                ISNULL((SELECT SUM(DATEDIFF(MINUTE, r.TimeIn, r.TimeOut)) / 60.0 FROM AttendanceRecords r WHERE r.UserId = @UserId AND r.TimeIn >= @StartOfWeek AND r.TimeOut IS NOT NULL), 0) AS WeeklyHours,
                ISNULL((SELECT SUM(DATEDIFF(MINUTE, r.TimeIn, r.TimeOut)) / 60.0 FROM AttendanceRecords r WHERE r.UserId = @UserId AND r.TimeIn >= @StartOfMonth AND r.TimeOut IS NOT NULL), 0) AS MonthlyHours
            FROM Users u
            WHERE u.Id = @UserId
        ";
        return await dapper.LoadDataSingle<AttendanceReportDto>(sql, new { UserId = userId, TodayStr = todayStr, StartOfWeek = startOfWeek, StartOfMonth = startOfMonth }) ?? new AttendanceReportDto();
    }
}
