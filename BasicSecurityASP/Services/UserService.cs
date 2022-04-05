using BasicSecurityASP.Models;

namespace BasicSecurityASP.Services
{
    public class UserService:IUserService
    {
        private List<User> users = new List<User>()
        {
            new User() { Email = "manu@hotmail.com", Password = "123456" },
            new User() { Email = "mma@hotmail.com", Password = "adf456" }
        };

        public bool IsUser(string email, string password) =>
            users.Where(a => a.Email == email && a.Password == password).Count() > 0;
    }
}
