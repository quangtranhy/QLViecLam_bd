using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class DoiTuongUuTien
    {
        [Key]
        public int Id { get; set; }
        public string? MaDoiTuongUuTien { get; set; }
        public string? TenDoiTuongUuTien { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
