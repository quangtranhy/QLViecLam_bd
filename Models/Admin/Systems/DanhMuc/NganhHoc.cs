using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class NganhHoc
    {
        [Key]
        public int Id { get; set; }
        public string? MaNganhHoc { get; set; }
        public string? TenNganhHoc { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
