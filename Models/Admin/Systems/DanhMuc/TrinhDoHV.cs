using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class TrinhDoHV
    {
        [Key]
        public int Id { get; set; }
        public string? MaTrinhDoHV { get; set; }
        public string? TenTrinhDoHV { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
