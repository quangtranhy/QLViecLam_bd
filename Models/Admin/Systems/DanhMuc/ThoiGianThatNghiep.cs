using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class ThoiGianThatNghiep
    {
        [Key]
        public int Id { get; set; }
        public string? MaThoiGianThatNghiep { get; set; }
        public string? TenThoiGianThatNghiep { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
