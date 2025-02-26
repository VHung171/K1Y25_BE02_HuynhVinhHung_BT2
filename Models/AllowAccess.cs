namespace K1Y25_BE02_HuynhVinhHung_BT2.Models
{
    public class AllowAccess
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string TableName { get; set; } = string.Empty;
        public string AccessProperties { get; set; } = string.Empty; 

        public Role Role { get; set; } = null!;
    }

}
