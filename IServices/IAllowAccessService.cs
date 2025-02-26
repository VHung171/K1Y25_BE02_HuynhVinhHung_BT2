using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Request;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Response;

namespace K1Y25_BE02_HuynhVinhHung_BT2.IServices
{
    public interface IAllowAccessService
    {
        ApiResponse<ICollection<AllowAccessResponse>> GetAllowAccessList();
        ApiResponse<AllowAccessResponse> GetAllowAccessById(long id);
        ApiResponse<AllowAccessResponse> CreateAllowAccess(AllowAccessRequest request);
        ApiResponse<AllowAccessResponse> UpdateAllowAccess(long id, AllowAccessRequest request);
        public ApiResponse<bool> DeleteAllowAccess(long id);
    }
}
