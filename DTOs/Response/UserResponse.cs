using K1Y25_BE02_HuynhVinhHung_BT2.Utils;

namespace K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Response
{
    public class UserResponse
    {
        public int? UserId { get; set; }
        public string? FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; } 
        public string? Email { get; set; } = string.Empty;
        public string? RoleName { get; set; } = string.Empty;
        
    }
}
