using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Manages
{
    public class KyDieuTra
    {
        [Key]
        public int Id { get; set; }
        public string? MaKyDieuTra { get; set; }
        public string? NoiDung { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
