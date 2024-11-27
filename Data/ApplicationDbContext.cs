using Microsoft.EntityFrameworkCore;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.Models.Admin.Systems.DanhMuc;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QLViecLam.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //Systems
        public DbSet<Users> Users { get; set; }
        public DbSet<DmDonvi> DmDonvi { get; set; }
        public DbSet<DmHanhChinh> DmHanhChinh { get; set; }
        public DbSet<DsNhomTaiKhoan> DsNhomTaiKhoan { get; set; }
        public DbSet<DsNhomTaiKhoan_PhanQuyen> DsNhomTaiKhoan_PhanQuyen { get; set; }
        public DbSet<ChucNang> ChucNang{ get; set; }
        public DbSet<DsTaiKhoan_PhanQuyen> DsTaiKhoan_PhanQuyen { get; set; }
        public DbSet<DvDieuTra> DvDieuTra { get; set; }


        //Manages
        public DbSet<KyDieuTra> KyDieuTra { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<DsThatNghiep> DsThatNghiep { get; set; }
        public DbSet<KyBaoCao> KyBaoCao { get; set; }
        public DbSet<NguoiLaoDong> NguoiLaoDong { get; set; }
        public DbSet<NhanKhau> NhanKhau { get; set; }
        public DbSet<TongHopCungLaoDong> TongHopCungLaoDong { get; set; }
        public DbSet<TuyenDung> TuyenDung { get; set; }
        public DbSet<ViTriTuyenDung> ViTriTuyenDung { get; set; }
        public DbSet<KeHoachThuThapThongTin> KeHoachThuThapThongTin { get; set; }
        public DbSet<BienDong> BienDong { get; set; }

        public DbSet<CheDoChinhSach> CheDoChinhSach { get; set; }
        public DbSet<DieuKienLamViec> DieuKienLamViec { get; set; }
        public DbSet<PhuongTienLamViec> PhuongTienLamViec { get; set; }
        public DbSet<RuiRoLamViec> RuiRoLamViec { get; set; }
        public DbSet<TaiNanLaoDong> TaiNanLaoDong { get; set; }
        public DbSet<BanTin> BanTin { get; set; }



        //DanhMuc
        public DbSet<TrinhDoCMKT> TrinhDoCMKT { get; set; }
        public DbSet<TrinhDoHV> TrinhDoHV { get; set; }
        public DbSet<QuocGia> QuocGia { get; set; }
        public DbSet<TinhTrangVL> TinhTrangVL { get; set; }
        public DbSet<NganhHoc> NganhHoc { get; set; }
        public DbSet<DanToc> DanToc { get; set; }
        public DbSet<KhuCongNghiep> KhuCongNghiep { get; set; }
        public DbSet<HinhThucDoanhNghiep> HinhThucDoanhNghiep { get; set; }
        public DbSet<ThoiGianThatNghiep> ThoiGianThatNghiep { get; set; }
        public DbSet<LyDoKhongThamGiaHDKT> LyDoKhongThamGiaHDKT { get; set; }
        public DbSet<QuanHe> QuanHe { get; set; }
        public DbSet<NganhNghe> NganhNghe { get; set; }
        public DbSet<LoaiHopDongLaoDong> LoaiHopDongLaoDong { get; set; }
        public DbSet<DoiTuongUuTien> DoiTuongUuTien { get; set; }
        public DbSet<ViTriViecLam> ViTriViecLam { get; set; }


        public DbSet<DmDoiTuongChinhSach> DmDoiTuongChinhSach { get; set; }








    }
}
