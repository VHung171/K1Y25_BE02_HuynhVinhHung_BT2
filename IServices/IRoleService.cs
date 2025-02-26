using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Request;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Response;

namespace K1Y25_BE02_HuynhVinhHung_BT2.IServices
{
    public interface IRoleService
    {
        ApiResponse<ICollection<RoleResponse>> GetRoles();
        ApiResponse<RoleResponse> GetRoleById(int id);
        ApiResponse<RoleResponse> CreateRole(RoleRequest roleRequest);
        ApiResponse<RoleResponse> UpdateRole(int id, RoleRequest roleRequest);
        ApiResponse<bool> DeleteRole(int id);
    }
}
