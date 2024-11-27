namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class KhuCongNghiep
    {
        public int Id { get; set; }
        public string? MaKhuCongNghiep { get; set; }
        public string? TenKhuCongNghiep { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
