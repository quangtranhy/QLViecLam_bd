using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class NganhNghe
    {
        [Key]
        public int Id { get; set; }
        public string? MaNganhNghe { get; set; }
        public string? TenNganhNghe { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
