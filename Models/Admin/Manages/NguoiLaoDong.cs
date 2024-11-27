using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using QLViecLam.Models.Admin.Manages;

namespace QLViecLam.Models.Admin.Manages
{
    public class NguoiLaoDong
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string? HoVaTen { get; set; }
        public string? Gioitinh { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public DateTime NgayThangNamSinh { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string? SoCCCD { get; set; }
        public string? DanToc { get; set; }
        public string? TrinhDoHV { get; set; }
        public string? TrinhDoCMKT { get; set; }
        public string? Nation { get; set; }
        public string? Tinh { get; set; }
        public string? Huyen { get; set; }
        public string? Xa { get; set; }
        public string? DiaChiCuThe { get; set; }
        public string? SoBaoHiem { get; set; }
        public string? NgheNghiep { get; set; }
        public string? LinhVucDaoTao { get; set; }
        public string? LoaiHdld { get; set; }
        public DateTime BdHopDong { get; set; }
        public DateTime KtHopDong { get; set; }
        public string? Luong { get; set; }
        public string? PcChucVu { get; set; }
        public string? PcThamNien { get; set; }
        public string? PcThamNienNghe { get; set; }
        public string? PcLuong { get; set; }
        public string? PcBoSung { get; set; }
        public DateTime BdDocHai { get; set; }
        public DateTime KtDocHai { get; set; }
        public string? ViTri { get; set; }
        public string? ChucVu { get; set; }
        public DateTime BdBhxh { get; set; }
        public DateTime KtBhxh { get; set; }
        public string? LuongBhxh { get; set; }
        public string? GhiChu { get; set; }
        //public string? nhankhau_id { get; set; }
        //public string? madv { get; set; }
        //public string? kydieutra { get; set; }
        //public string? maloi { get; set; }
        public string? MaDn { get; set; }
        public short State { get; set; } //0:không hoạt động 1: hoạt động, 2:tạm dừng,
        public short? FromTtdvvl { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        [NotMapped]
        public string? TenDn { get; set; }
        //sẽ lấy theo mã quốc gia ánh xạ vào đọc ra tên,nếu null=>trong nước
        public string? Abroad { get; set; }
        //public CheDoChinhSach? CheDoChinhSach { get; set; }
    }
}
