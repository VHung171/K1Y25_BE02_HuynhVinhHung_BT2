using AutoMapper;
using K1Y25_BE02_HuynhVinhHung_BT2.Data;
using K1Y25_BE02_HuynhVinhHung_BT2.IServices;
using K1Y25_BE02_HuynhVinhHung_BT2.Models;
using K1Y25_BE02_HuynhVinhHung_BT2.Repositories;
using Microsoft.EntityFrameworkCore;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Services
{
    public class InternService : IInternService
    {
        private readonly AllowAccessRepo _repository;
        private readonly RoleRepo _roleRepository;
        private readonly InternRepo _internrepo;
        private readonly ApplicationDbContext _context;

        public InternService(ApplicationDbContext context, AllowAccessRepo repository, RoleRepo roleRepository, InternRepo internrepo)
        {
            _context = context;
             
        }
        public async Task<IEnumerable<Dictionary<string, object?>>> GetInternsByAccessAsync(string roleName, string tableName)
        {
            var allowAccess = await _context.AllowAccesses
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Role.RoleName == roleName && a.TableName == tableName);

            if (allowAccess == null || string.IsNullOrWhiteSpace(allowAccess.AccessProperties))
                return Enumerable.Empty<Dictionary<string, object?>>();  

            var allowedColumns = allowAccess.AccessProperties
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim())
                .ToList();

            var interns = await _context.Interns.ToListAsync();

            var result = interns.Select(internObj =>
            {
                var dict = new Dictionary<string, object?>();
                foreach (var col in allowedColumns)
                {
                    var propInfo = typeof(Intern).GetProperty(col);
                    if (propInfo != null)
                        dict[col] = propInfo.GetValue(internObj);
                }
                return dict;
            });

            return result.ToList(); 
        }

    }
}
