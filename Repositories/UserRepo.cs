using K1Y25_BE02_HuynhVinhHung_BT2.Data;
using K1Y25_BE02_HuynhVinhHung_BT2.Models;
using Microsoft.EntityFrameworkCore;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Repositories
{
    public class UserRepo
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.Include(u => u.Role).ToList();
        }

        public User? GetUserById(long id)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == id);
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public bool DeleteUser(long id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
         
        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email != null && u.Email.ToLower() == email.ToLower());
        }

    }
}
