using WebApplication5.Data;
using WebApplication5.Model;
using WebApplication5.Service.Abstractions;

namespace WebApplication5.Service.Implementations
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AuthenticationContext _context;

        public AuthenticationRepository(AuthenticationContext context)
        {
            _context = context;
        }

        public User DeleteUserId(int userId)
        {
            var user = _context.users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _context.users.Remove(user);
                _context.SaveChanges();
            }
            return user;
        }

        public User GetUserId(int userId)
        {
            return _context.users.FirstOrDefault(u => u.Id == userId);
        }

        public List<User> GetUsers()
        {
            var users = _context.users.ToList();
            
            return users;
        }

        public User LogIn(string userName, string password)
        {
            var user = _context.users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            return user;
        }

        public User Registration(User user)
        {
            
            var existingUserName = _context.users.FirstOrDefault(u => u.UserName == user.UserName);
            if (existingUserName != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }

            
            var existingEmail = _context.users.FirstOrDefault(u => u.Email == user.Email);
            if (existingEmail != null)
            {
                throw new InvalidOperationException("Email already exists.");
            }

            
            _context.users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User UpdateUserId(int userId, User updatedUser)
        {
            var user = _context.users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.UserName = updatedUser.UserName;
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;

                _context.users.Update(user);
                _context.SaveChanges();
            }
            return user;
        }
    }
}

