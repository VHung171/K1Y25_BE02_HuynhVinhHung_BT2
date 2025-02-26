using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace K1Y25_BE02_HuynhVinhHung_BT2.IServices
{
    public interface IInternService
    {
        Task<IEnumerable<Dictionary<string, object?>>> GetInternsByAccessAsync(string roleName, string tableName);
    }
}
