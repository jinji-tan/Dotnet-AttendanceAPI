using AttendanceAPI.Models;

namespace AttendanceAPI.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<int> CreateUserAsync(User user);
}