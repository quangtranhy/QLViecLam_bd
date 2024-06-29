using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.HeThongChung
{
    public class ChucNang
    {
        [Key]
        public int Id { get; set; }
        public string? MaChucNang { get; set; }
        public string? TenChucNang { get; set; }
        public string? Parent { get; set; }
        public int? CapDo { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public string? Stt { get; set; }
    }
}
