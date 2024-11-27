using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class HinhThucDoanhNghiep
    {
        [Key]
        public int Id { get; set; }
        public string? MaHinhThucDoanhNghiep { get; set; }
        public string? TenHinhThucDoanhNghiep { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
