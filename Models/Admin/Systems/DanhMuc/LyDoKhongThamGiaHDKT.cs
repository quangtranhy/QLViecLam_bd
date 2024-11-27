using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class LyDoKhongThamGiaHDKT
    {
        [Key]
        public int Id { get; set; }
        public string? MaLyDoKhongThamGia { get; set; }
        public string? TenLyDoKhongThamGia { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
