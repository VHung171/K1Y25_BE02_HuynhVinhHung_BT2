using K1Y25_BE02_HuynhVinhHung_BT2.Data;
using K1Y25_BE02_HuynhVinhHung_BT2.Models;
using Microsoft.EntityFrameworkCore;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Repositories
{
    public class RoleRepo
    {
        private readonly ApplicationDbContext _context;

        public RoleRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public Role? GetRoleById(int id)
        {
            return _context.Roles.Find(id);
        }


        public Role CreateRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public Role UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return role;
        }

        public bool DeleteRole(int id)
        {
            var role = GetRoleById(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                return _context.SaveChanges() > 0;
            }
            return false;
        }

    }
}
