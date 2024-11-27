using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLViecLam.Models.Admin.Manages
{
    public class NhanKhau
    {
        [Key]
        public int Id { get; set; }
        public int? DanhSach_id { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string? SoDinhDanhHoGD { get; set; }
        public string? QuanHe { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string? HoVaTen { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string? SoCCCD { get; set; }
        public string? Gioitinh { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public DateTime NgayThangNamSinh { get; set; }
        public string? Sdt { get; set; }

        public string? DanToc { get; set; }
        public string? ThuongTru { get; set; }
        public string? NoiOHienTai { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string? TrinhDoHV { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string? TrinhDoCMKT { get; set; }
        public string? ThamGiaBHXH { get; set; }
        public string? MaBHXH { get; set; }
        public string? UuTien { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string? TinhTrangVL { get; set; }
        public string? CongViecCuThe { get; set; }
        public string? NoiLamViec { get; set; }
        public string? DiaChiNoiLamViec { get; set; }
        public string? ThoiGianThatNghiep { get; set; }
        public string? LyDoKhongThamGia { get; set; }


        public string? MaDonVi { get; set; }
        public string? KyDieuTra { get; set; }
        public string? KhuVuc { get; set; }
        public string? ThiTruongLamViec { get; set; }

        public string? DoiTuongTimViecLam { get; set; }
        public string? ViecLamMongMuon { get; set; }
        public string? NganhNgheMongMuon { get; set; }
        public string? NganhNgheMuonHoc { get; set; }
        public string? TrinhDoChuyenMonMuonHoc { get; set; }
        public int? SoLuongTrung { get; set; }
        public int? LoaiBienDong { get; set; }
        public string? RuongBienDong { get; set; }

        public string? LyDo { get; set; }
        public string? MaLoi { get; set; }
        public string? MaLoaiLoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public bool TinhTrangXacThuc { get; set; }
        public string? ChuyenNganh { get; set; }
        public string? MaDiaBan { get; set; } // join với MaDb của bảng DmHanhChinh, địa bàn cấp xã
        public int? User { get; set; }


        [NotMapped]
        public string? Tentdkt { get; set; }
        [NotMapped]
        public short? IdTuyenDung { get; set; }
    }
}
