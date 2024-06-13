using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLViecLam.Models.Admin.Manages.ThongTinThiTruongLD
{
    public class NhanKhau
    {
        [Key]
        public int Id { get; set; }
        public int? DanhSach_id { get; set; }

        public int? SoDinhDanhHoGD { get; set; }
        public int? QuanHe { get; set; }
        public string? HoVaTen { get; set; }
        public int? SoCCCD { get; set; }
        public int? Gioitinh { get; set; }
        public DateTime NgayThangNamSinh { get; set; }
        public string? Sdt { get; set; }

        public int? DanToc { get; set; }
        public string? ThuongTru { get; set; }
        public string? NoiOHienTai { get; set; }
        public int? TrinhDoHV { get; set; }
        public int? TrinhDoCMKT { get; set; }
        public int? ThamGiaBHXH { get; set; }
        public int? MaBHXH { get; set; }
        public int? UuTien { get; set; }

        public int? TinhTrangVL { get; set; }
        public string? CongViecCuThe { get; set; }
        public string? NoiLamViec { get; set; }
        public string? DiaChiNoiLamViec { get; set; }
        public int? ThoiGianThatNghiep { get; set; }
        public int? LyDoKhongThamGia { get; set; }


        public string? MaDonVi { get; set; }
        public string? KyDieuTra { get; set; }
        public int? KhuVuc { get; set; }
        public string? ThiTruongLamViec { get; set; }
      
        public int? DoiTuongTimViecLam { get; set; }
        public int? ViecLamMongMuon { get; set; }
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

        [NotMapped]
        public string? Tentdkt { get; set; }
        [NotMapped]
        public short? IdTuyenDung { get; set; }
        public bool TinhTrangXacThuc { get; set; }

        //public int? TonGiao { get; set; }
        //public int? QueQuan { get; set; }
        //public string? DiaChiCuThe { get; set; }
        //public int? TinhTrangHonNhan { get; set; }
        //public int? PhanLoaiHo { get; set; }
        public string? ChuyenNganh { get; set; }
        //public string? Hdld { get; set; }
        //public string? NguoiCoVieclam { get; set; }
        //public string? LoaiHinhNoiLamViec { get; set; }
        //public string? ThatNghiep { get; set; }
        //public string? KhongThamGiaHdkt { get; set; }
    }
}
