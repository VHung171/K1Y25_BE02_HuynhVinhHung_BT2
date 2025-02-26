using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Response;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Request;
using K1Y25_BE02_HuynhVinhHung_BT2.Models;
using K1Y25_BE02_HuynhVinhHung_BT2.Repositories;
using K1Y25_BE02_HuynhVinhHung_BT2.IServices;
using AutoMapper;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Services
{
    public class AllowAccessService : IAllowAccessService
    {
        private readonly AllowAccessRepo _repository;
        private readonly RoleRepo _roleRepository;
        private readonly IMapper _mapper;

        public AllowAccessService(AllowAccessRepo repository, RoleRepo roleRepository, IMapper mapper)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public ApiResponse<ICollection<AllowAccessResponse>> GetAllowAccessList()
        {
            var allowAccessList = _repository.GetAllowAccessList();
            var response = _mapper.Map<ICollection<AllowAccessResponse>>(allowAccessList);

            return allowAccessList.Any()
                ? ApiResponse<ICollection<AllowAccessResponse>>.Success(response)
                : ApiResponse<ICollection<AllowAccessResponse>>.NotFound("Không có quyền truy cập nào.");
        }

        public ApiResponse<AllowAccessResponse> GetAllowAccessById(long id)
        {
            var allowAccess = _repository.GetAllowAccessById(id);
            return allowAccess != null
                ? ApiResponse<AllowAccessResponse>.Success(_mapper.Map<AllowAccessResponse>(allowAccess))
                : ApiResponse<AllowAccessResponse>.NotFound($"Không tìm thấy quyền truy cập #{id}");
        }

        public ApiResponse<AllowAccessResponse> CreateAllowAccess(AllowAccessRequest request)
        {
            var roleExists = _roleRepository.GetRoles().Any(r => r.RoleId == request.RoleId);
            if (!roleExists)
            {
                return ApiResponse<AllowAccessResponse>.Conflict("Vai trò không tồn tại.");
            }

            var created = _repository.CreateAllowAccess(new AllowAccess
            {
                RoleId = request.RoleId,
                TableName = request.TableName,
                AccessProperties = request.AccessProperties
            });

            return ApiResponse<AllowAccessResponse>.Success(_mapper.Map<AllowAccessResponse>(created));
        }
            
        public ApiResponse<AllowAccessResponse> UpdateAllowAccess(long id, AllowAccessRequest request)
        {
            var existingAccess = _repository.GetAllowAccessById(id);
            if (existingAccess == null)
            {
                return ApiResponse<AllowAccessResponse>.NotFound("Không tìm thấy quyền truy cập.");
            }

            existingAccess.TableName = request.TableName;
            existingAccess.AccessProperties = request.AccessProperties;

            _repository.UpdateAllowAccess(existingAccess);

            return ApiResponse<AllowAccessResponse>.Success(_mapper.Map<AllowAccessResponse>(existingAccess));
        }

        public ApiResponse<bool> DeleteAllowAccess(long id)
        {
            var success = _repository.DeleteAllowAccess(id);
            return success
                ? ApiResponse<bool>.Success(true, $"Xóa thành công quyền truy cập #{id}")
                : ApiResponse<bool>.NotFound($"Không tìm thấy quyền truy cập #{id}");
        }
    }
}
