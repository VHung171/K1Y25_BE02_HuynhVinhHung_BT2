using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Request;
using K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Response;
using K1Y25_BE02_HuynhVinhHung_BT2.Repositories;

namespace K1Y25_BE02_HuynhVinhHung_BT2.IServices
{
    public interface IUserService
    {
        ApiResponse<ICollection<UserResponse>> GetUsers(int page, int pageSize, string search, string sortColumn, string sortOrder);
        ApiResponse<UserResponse> GetUserById(long id);
        ApiResponse<UserResponse> CreateUser(UserRequest userRequest);
        ApiResponse<UserResponse> UpdateUser(long id, UserRequest userRequest);
        ApiResponse<bool> DeleteUser(long id);
    }
}
