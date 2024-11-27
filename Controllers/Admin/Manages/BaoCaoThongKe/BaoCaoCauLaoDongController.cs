using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.Models.Admin.Systems.DanhMuc;
using QLViecLam.ViewModels.Admin.Manages;

namespace QLViecLam.Controllers.Admin.Manages.BaoCaoThongKe
{
    public class BaoCaoCauLaoDongController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BaoCaoCauLaoDongController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("BaoCaoCauLaoDong")]
        public IActionResult Index()
        {
            int year = DateTime.Now.Year;
            DateTime tungay = new DateTime(year, 1, 1);
            ViewData["tungay"] = tungay;
            ViewData["denngay"] = DateTime.Now;
            var DsKyDieuTra = _db.KeHoachThuThapThongTin.Select(k => k.KeHoach).Distinct().OrderByDescending(k => k).ToList();
            ViewData["DsKyDieuTra"] = DsKyDieuTra;
            ViewData["Title"] = "Báo cáo thống kê";
            ViewData["MenuLv1"] = "baocao";
            ViewData["MenuLv2"] = "baocao_cau";
            return View("Views/Admin/Manages/BaoCaoThongKe/BaoCaoCauLaoDong/Index.cshtml");
        }

        [HttpGet("BaoCaoCauLaoDong/Mau02pli")]
        public IActionResult Mau02pli(DateTime tungay, DateTime denngay)
        {
            var company = _db.Company;
            var model = _db.NguoiLaoDong.Where(x => x.BdHopDong >= tungay && x.KtHopDong <= denngay);
            var newmodel = (from m in model
                            join c in company on m.MaDn equals c.MaSoDn
                            select new VM_NguoiLaoDong
                            {
                                Id = m.Id,
                                Gioitinh = m.Gioitinh,
                                NgayThangNamSinh = m.NgayThangNamSinh,
                                LoaiHdld = m.LoaiHdld,
                                BdBhxh = m.BdBhxh,
                                BdHopDong = m.BdHopDong,
                                LoaiHinhDoanhNghiep = c.LoaiHinh,
                            });
            var DmHanhChinh = _db.DmHanhChinh;
            ViewData["DsHuyen"] = DmHanhChinh.Where(x => x.CapDo == "H");
            ViewData["DsXa"] = DmHanhChinh.Where(x => x.CapDo == "X");
            ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "1");
            ViewData["LoaiHopDongLaoDong"] = _db.LoaiHopDongLaoDong.Where(x => x.TrangThai == "1");
            ViewData["ViTriViecLam"] = _db.ViTriViecLam.Where(x => x.TrangThai == "1");
            ViewData["HinhThucDoanhNghiep"] = _db.HinhThucDoanhNghiep.Where(x => x.TrangThai == "1");
            ViewData["Title"] = "Mẫu số 02pli Tình hình sử dụng lao động";
            return View("Views/Admin/Manages/BaoCaoThongKe/BaoCaoCauLaoDong/Mau02pli.cshtml", newmodel);
        }

        [HttpGet("BaoCaoCauLaoDong/Mau04")]
        public IActionResult Mau04(int kydieutra)
        {
            var company = _db.Company;

            var kytruoc_int = kydieutra - 1;
            var kybaocao_int = kydieutra;
            var kytruoc = (kydieutra - 1).ToString();
            var kybaocao = kydieutra.ToString();

            var NhanKhau = _db.NhanKhau;
            ViewData["KyTruoc_NK"] = NhanKhau.Where(x => x.KyDieuTra == kytruoc);
            ViewData["KyBaoCao_NK"] = NhanKhau.Where(x => x.KyDieuTra == kybaocao);

            var NguoiLaoDong = _db.NguoiLaoDong;
            ViewData["KyTruoc_NLD"] = NguoiLaoDong.Where(x => x.BdHopDong.Year >= kytruoc_int && x.KtHopDong.Year <= kytruoc_int);
            ViewData["KyBaoCao_NLD"] = NguoiLaoDong.Where(x => x.BdHopDong.Year >= kybaocao_int && x.KtHopDong.Year <= kybaocao_int);


            ViewData["ThoiGianThatNghiep"] = _db.ThoiGianThatNghiep.Where(x => x.TrangThai == "1");
            ViewData["ViTriViecLam"] = _db.ViTriViecLam.Where(x => x.TrangThai == "1");
            ViewData["Company"] = _db.Company;
            ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "1");
            ViewData["Title"] = "Mẫu số 02pli Tình hình sử dụng lao động";
            return View("Views/Admin/Manages/BaoCaoThongKe/BaoCaoCauLaoDong/Mau04.cshtml");
        }
    }
}
