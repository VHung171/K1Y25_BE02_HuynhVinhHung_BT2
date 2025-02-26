namespace K1Y25_BE02_HuynhVinhHung_BT2.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<AllowAccess> AllowAccesses { get; set; } = new List<AllowAccess>();
    }

}
