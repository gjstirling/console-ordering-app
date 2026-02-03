using ConsoleOrderingApp.Services;

namespace ConsoleOrderingApp.Tests.NewFolder
{
    public class AuthServiceTests
    {
        [Fact]
        public void Login_ExistingUser_ReturnsUser()
        {
            var authService = new AuthService();

            var user = authService.Login("alice");

            Assert.NotNull(user);
            Assert.Equal("alice", user.Username);
        }

        [Fact]
        public void Login_NonExistingUser_ReturnsNull()
        {
            var authService = new AuthService();

            var user = authService.Login("graeme"); // graeme doesn't exist in seed

            Assert.Null(user);
        }
    }
}
