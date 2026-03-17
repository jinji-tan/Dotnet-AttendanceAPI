using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AttendanceAPI.Repositories;
using AttendanceAPI.DTOs;

namespace AttendanceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AttendanceController(IAttendanceRepository attendanceRepository) : ControllerBase
{
    private readonly IAttendanceRepository _attendanceRepository = attendanceRepository;

    private int GetCurrentUserId()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return idClaim != null ? int.Parse(idClaim) : 0;
    }

    [HttpPost("time-in")]
    public async Task<IActionResult> TimeIn()
    {
        var userId = GetCurrentUserId();
        if (userId == 0) return Unauthorized();
        var activeRecord = await _attendanceRepository.GetActiveTimeInAsync(userId);
        if (activeRecord != null) return BadRequest(new { Message = "You are already timed in." });
        var timeIn = DateTime.Now;
        await _attendanceRepository.TimeInAsync(userId, timeIn);
        return Ok(new { Message = "Successfully timed in.", TimeIn = timeIn.ToString("yyyy-MM-dd HH:mm:ss") });
    }

    [HttpPut("time-out")]
    public async Task<IActionResult> TimeOut()
    {
        var userId = GetCurrentUserId();
        if (userId == 0) return Unauthorized();
        var activeRecord = await _attendanceRepository.GetActiveTimeInAsync(userId);
        if (activeRecord == null) return BadRequest(new { Message = "You have not timed in yet." });
        var timeOut = DateTime.Now;
        await _attendanceRepository.TimeOutAsync(activeRecord.Id, timeOut);
        return Ok(new { Message = "Successfully timed out.", TimeOut = timeOut.ToString("yyyy-MM-dd HH:mm:ss") });
    }

    [HttpGet("my-records")]
    public async Task<IActionResult> GetMyRecords()
    {
        var userId = GetCurrentUserId();
        if (userId == 0) return Unauthorized();
        var records = await _attendanceRepository.GetRecordsByUserIdAsync(userId);
        return Ok(records);
    }

    [HttpGet("my-hours")]
    public async Task<IActionResult> GetMyHoursReport()
    {
        var userId = GetCurrentUserId();
        if (userId == 0) return Unauthorized();
        var today = DateTime.Today;
        int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
        var startOfWeek = today.AddDays(-1 * diff).Date;
        var startOfMonth = new DateTime(today.Year, today.Month, 1);
        var report = await _attendanceRepository.GetUserHoursReportAsync(userId, today, startOfWeek, startOfMonth);
        return Ok(report);
    }

    [HttpGet("all-records")]
    public async Task<IActionResult> GetAllRecords()
    {
        var records = await _attendanceRepository.GetAllRecordsAsync();
        return Ok(records);
    }
}
