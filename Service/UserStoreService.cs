using BulkyWeb.Models;

namespace BulkyWeb.Service
{
    public class UserStoreService
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "john", Password = "pass123", FullName = "John Doe" },
            new User { Id = 2, Username = "jane", Password = "pass456", FullName = "Jane Smith" }
        };

        public User? ValidateUser(string username, string password)
        {
            return _users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password
            );
        }

        public User? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }
}
