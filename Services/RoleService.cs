using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Response;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Request;
using K1Y25_BE02_HuynhVinhHung_BT2.Models;
using K1Y25_BE02_HuynhVinhHung_BT2.Repositories;
using K1Y25_BE02_HuynhVinhHung_BT2.IServices;
using AutoMapper;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepo _repository;
        private readonly IMapper _mapper;

        public RoleService(RoleRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ApiResponse<ICollection<RoleResponse>> GetRoles()
        {
            var roles = _repository.GetRoles();
            var response = _mapper.Map<ICollection<RoleResponse>>(roles);

            return roles.Any()
                ? ApiResponse<ICollection<RoleResponse>>.Success(response)
                : ApiResponse<ICollection<RoleResponse>>.NotFound("Không có vai trò nào.");
        }

        public ApiResponse<RoleResponse> GetRoleById(int id)
        {
            var role = _repository.GetRoleById(id);
            return role != null
                ? ApiResponse<RoleResponse>.Success(_mapper.Map<RoleResponse>(role))
                : ApiResponse<RoleResponse>.NotFound($"Không tìm thấy vai trò #{id}");
        }

        public ApiResponse<RoleResponse> CreateRole(RoleRequest roleRequest)
        {
            var existing = _repository.GetRoles().FirstOrDefault(r => r.RoleName.ToLower() == roleRequest.RoleName.ToLower());
            if (existing != null)
            {
                return ApiResponse<RoleResponse>.Conflict("Tên vai trò đã tồn tại.");
            }

            var created = _repository.CreateRole(new Role
            {
                RoleName = roleRequest.RoleName
            });

            return ApiResponse<RoleResponse>.Success(_mapper.Map<RoleResponse>(created));
        }

        public ApiResponse<RoleResponse> UpdateRole(int id, RoleRequest roleRequest)
        {
            var existingRole = _repository.GetRoleById(id);
            if (existingRole == null)
            {
                return ApiResponse<RoleResponse>.NotFound("Không tìm thấy vai trò.");
            }

            existingRole.RoleName = roleRequest.RoleName;
            _repository.UpdateRole(existingRole);

            return ApiResponse<RoleResponse>.Success(_mapper.Map<RoleResponse>(existingRole));
        }

        public ApiResponse<bool> DeleteRole(int id)
        {
            var success = _repository.DeleteRole(id);
            return success
                ? ApiResponse<bool>.Success(true, $"Xóa thành công vai trò #{id}")
                : ApiResponse<bool>.NotFound($"Không tìm thấy vai trò #{id}");
        }
    }
}
