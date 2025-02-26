using System.Data;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Models
{
    public class User
    {
        public int? UserId { get; set; }
        public string? FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public int? RoleId { get; set; }

        public Role? Role { get; set; } = null!;
    }

}
