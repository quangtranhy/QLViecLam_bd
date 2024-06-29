using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class TinhTrangVL
    {
        [Key]
        public int Id { get; set; }
        public int? MaTinhTrangVL { get; set; }
        public string? TenTinhTrangVL { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
