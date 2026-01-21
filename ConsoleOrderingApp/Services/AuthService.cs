using ConsoleOrderingApp.Data;
using ConsoleOrderingApp.Models;

namespace ConsoleOrderingApp.Services;

public class AuthService
{
    public User? Login(string username)
    {
        // Look up username in seeded users
        return SeedData.Users
            .FirstOrDefault(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
    }
}
