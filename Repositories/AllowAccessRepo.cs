using K1Y25_BE02_HuynhVinhHung_BT2.Data;
using K1Y25_BE02_HuynhVinhHung_BT2.Models;
using Microsoft.EntityFrameworkCore;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Repositories
{
    public class AllowAccessRepo
    {
        private readonly ApplicationDbContext _context;

        public AllowAccessRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<AllowAccess> GetAllowAccessList()
        {
            return _context.AllowAccesses.Include(a => a.Role).ToList();
        }

        public AllowAccess? GetAllowAccessById(long id)
        {
            return _context.AllowAccesses.Include(a => a.Role).FirstOrDefault(a => a.Id == id);
        }

        public AllowAccess CreateAllowAccess(AllowAccess allowAccess)
        {
            _context.AllowAccesses.Add(allowAccess);
            _context.SaveChanges();
            return allowAccess;
        }

        public AllowAccess UpdateAllowAccess(AllowAccess allowAccess)
        {
            _context.AllowAccesses.Update(allowAccess);
            _context.SaveChanges();
            return allowAccess;
        }

        public bool DeleteAllowAccess(long id)
        {
            var allowAccess = GetAllowAccessById(id);
            if (allowAccess != null)
            {
                _context.AllowAccesses.Remove(allowAccess);
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public List<string> GetAllowedColumns(int roleId)
        {
            return _context.AllowAccesses
                           .Where(a => a.RoleId == roleId)
                           .Select(a => a.TableName)
                           .ToList();
        }

    }
}
