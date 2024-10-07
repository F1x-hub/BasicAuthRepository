using WebApplication5.Model;

namespace WebApplication5.Service.Abstractions
{
    public interface IAuthenticationRepository
    {
        List<User> GetUsers();
        User GetUserId(int userId);
        User UpdateUserId(int userId, User updatedUser);
        User DeleteUserId(int userId);
        User Registration(User user);
        User LogIn(string userName, string password);
    }
}
