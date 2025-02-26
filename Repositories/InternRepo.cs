using K1Y25_BE02_HuynhVinhHung_BT2.Data;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Repositories
{
    public class InternRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly AllowAccessRepo _allowAccessRepo;

        public InternRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public object GetInternsByRole(int roleId)
        {
            var allowedColumns = _allowAccessRepo.GetAllowedColumns(roleId);

            var query = _context.Interns.AsQueryable();

            var interns = query.Select(i => new
            {
                InternMail = allowedColumns.Contains("InternMail") ? i.InternMail : null,
                InternName = allowedColumns.Contains("InternName") ? i.InternName : null,
                Major = allowedColumns.Contains("Major") ? i.Major : null
            }).ToList();

            return interns;
        }
    }
}
