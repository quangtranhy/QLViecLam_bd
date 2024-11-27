using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class QuanHe
    {
        [Key]
        public int Id { get; set; }
        public string? MaQuanHe { get; set; }
        public string? TenQuanHe { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
