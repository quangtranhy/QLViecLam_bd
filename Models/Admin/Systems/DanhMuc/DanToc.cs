using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class DanToc
    {
        [Key]
        public int Id { get; set; }
        public string? MaDanToc { get; set; }
        public string? TenDanToc { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
