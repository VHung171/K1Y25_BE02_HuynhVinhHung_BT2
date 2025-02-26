namespace K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Request
{
    public class AllowAccessRequest
    {
        public int RoleId { get; set; }
        public string TableName { get; set; } = string.Empty;
        public string AccessProperties { get; set; } = string.Empty;
    }
}
