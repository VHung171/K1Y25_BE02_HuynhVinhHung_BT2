namespace K1Y25_BE02_HuynhVinhHung_BT2.DTOs.Response
{
    public class AllowAccessResponse
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public string AccessProperties { get; set; } = string.Empty;
    }
}
