using System.ComponentModel.DataAnnotations.Schema;

namespace QLViecLam.ViewModels.Admin.Manages
{
    public class VM_NguoiLaoDong
    {
        public int Id { get; set; }
        public string? Gioitinh { get; set; }
        public DateTime NgayThangNamSinh { get; set; }
        public string? LoaiHdld { get; set; }
        public DateTime BdHopDong { get; set; }
        public DateTime BdBhxh { get; set; }
        public string? ViTri { get; set; }
        public string? LoaiHinhDoanhNghiep { get; set; }
}
}
