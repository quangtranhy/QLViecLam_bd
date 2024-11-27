using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class LoaiHopDongLaoDong
    {
        [Key]
        public int Id { get; set; }
        public string? MaLoaiHopDongLaoDong { get; set; }
        public string? TenLoaiHopDongLaoDong { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
