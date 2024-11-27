namespace QLViecLam.ViewModels.Admin.Systems
{
    public class VM_HinhThucDoanhNghiep
    {
        public int Id { get; set; }
        public string? MaHinhThucDoanhNghiep { get; set; }
        public string? TenHinhThucDoanhNghiep { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public string? LoaiHinhDoanhNghiep { get; set; }
    }
}
