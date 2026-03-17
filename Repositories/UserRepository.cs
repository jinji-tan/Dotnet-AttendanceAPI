using AttendanceAPI.Data;
using AttendanceAPI.Models;

namespace AttendanceAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContextDapper dapper;
    public UserRepository(DataContextDapper dapper)
    {
        this.dapper = dapper;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var sql = "SELECT * FROM Users WHERE Username = @Username";

        return await dapper.LoadDataSingle<User>(sql, new { Username = username });
    }

    public async Task<int> CreateUserAsync(User user)
    {
        var sql = @"
            INSERT INTO Users (Username, PasswordHash, FirstName, LastName, Role)
            VALUES (@Username, @PasswordHash, @FirstName, @LastName, @Role);
            
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
        return await dapper.ExecuteSqlScalar(sql, user);
    }
}