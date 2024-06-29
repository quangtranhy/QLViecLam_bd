using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class TrinhDoCMKT
    {
        [Key]
        public int Id { get; set; }
        public int? MaTrinhDoCMKT { get; set; }
        public string? TenTrinhDoCMKT { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
