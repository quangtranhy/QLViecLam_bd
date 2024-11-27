using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Manages;

namespace QLViecLam.Controllers.Admin.Manages.TongHop_PhanTich_DuDoan.QuanLyDanhMucDuLieu.DmNguoiTimViec
{
    public class DmNguoiTimViecController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DmNguoiTimViecController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("DmNguoiTimViec")]
        [HttpGet]
        public IActionResult Index(string huyen, string xa, string kydieutra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var Madv = "";
                    if (string.IsNullOrEmpty(xa))
                    {
                        Madv = _db.DmDonvi.FirstOrDefault()!.MaDonVi!;
                    }
                    else
                    {
                        Madv = _db.DmDonvi.Where(x => x.MaDiaBan == xa).FirstOrDefault()!.MaDonVi!;
                    }

                    kydieutra = (string.IsNullOrEmpty(kydieutra)) ? "2022" : kydieutra;
                    var model = _db.NhanKhau.Where(x => x.MaDonVi == Madv && x.KyDieuTra == kydieutra).AsQueryable();
                    /*if (loai == null)
                    {
                        model = model.Where(x => x.nguoicovieclam == "1");
                    }*/
                    ViewData["Madv"] = Madv;
                    ViewData["mahuyen"] = huyen;
                    ViewData["maxa"] = xa;
                    ViewData["kydieutra"] = kydieutra;
                    //ViewData["Tinh"] = _db.DmHanhChinh.Where(t => string.IsNullOrEmpty(t.parent) || t.parent == "0");
                    ViewData["Huyen"] = _db.DmHanhChinh.Where(t => t.CapDo == "H");
                    if (string.IsNullOrEmpty(xa))
                    {
                        ViewData["Xa"] = _db.DmHanhChinh.Where(t => t.CapDo == "X");
                    }
                    else
                    {
                        ViewData["Xa"] = _db.DmHanhChinh.Where(t => t.CapDo == "X" && t.Parent == huyen);
                    }


                    ViewData["MenuLv1"] = "menu_quanlydanhmucdulieu";
                    ViewData["MenuLv2"] = "menu_quanlydanhmuc_DmNguoiTimViec";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/QuanLyDanhMucDuLieu/DmNguoiTimViec/Index.cshtml", model);
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
            
        }
        [Route("DmNguoiTimViec/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string KyDieuTra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = new NhanKhau
                    {
                        MaDonVi = Madv,
                        KyDieuTra = KyDieuTra
                    };

                    ViewData["Madv"] = Madv;
                    ViewData["KyDieuTra"] = KyDieuTra;

                    ViewData["MenuLv1"] = "menu_quanlydanhmucdulieu";
                    ViewData["MenuLv2"] = "menu_quanlydanhmuc_DmNguoiTimViec";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/QuanLyDanhMucDuLieu/DmNguoiTimViec/Create.cshtml", model);
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
            
        }
        [Route("DmNguoiTimViec/Store")]
        [HttpPost]
        public IActionResult Store(NhanKhau request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = new NhanKhau
                    {
                        Gioitinh = request.Gioitinh,
                        HoVaTen = request.HoVaTen,
                        NgayThangNamSinh = request.NgayThangNamSinh,
                        SoCCCD = request.SoCCCD,
                        MaBHXH = request.MaBHXH,
                        ThuongTru = request.ThuongTru,
                        NoiOHienTai = request.NoiOHienTai,
                        UuTien = request.UuTien,
                        DanToc = request.DanToc,
                        TrinhDoCMKT = request.TrinhDoCMKT,
                        TrinhDoHV = request.TrinhDoHV,
                        ChuyenNganh = request.ChuyenNganh,
                        TinhTrangVL = request.TinhTrangVL,
                        CongViecCuThe = request.CongViecCuThe,
                        ThamGiaBHXH = request.ThamGiaBHXH,
                        //Hdld = request.Hdld,
                        NoiLamViec = request.NoiLamViec,
                        DiaChiNoiLamViec = request.DiaChiNoiLamViec,
                        ThoiGianThatNghiep = request.ThoiGianThatNghiep,
                        LyDoKhongThamGia = request.LyDoKhongThamGia,
                        SoDinhDanhHoGD = request.SoDinhDanhHoGD,
                        QuanHe = request.QuanHe,
                        MaLoi = request.MaLoi,
                        MaLoaiLoi = request.MaLoaiLoi,
                        MaDonVi = request.MaDonVi,
                        KyDieuTra = request.KyDieuTra,
                        SoLuongTrung = request.SoLuongTrung,
                        LoaiBienDong = request.LoaiBienDong,
                        RuongBienDong = request.RuongBienDong,
                        DoiTuongTimViecLam = request.DoiTuongTimViecLam,
                        ViecLamMongMuon = request.ViecLamMongMuon,
                        NganhNgheMongMuon = request.NganhNgheMongMuon,
                        NganhNgheMuonHoc = request.NganhNgheMuonHoc,
                        TrinhDoChuyenMonMuonHoc = request.TrinhDoChuyenMonMuonHoc,
                        Sdt = request.Sdt,
                        KhuVuc = request.KhuVuc,
                        ThiTruongLamViec = request.ThiTruongLamViec,
                        LyDo = request.LyDo,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.NhanKhau.Add(model);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_quanlydanhmucdulieu";
                    ViewData["MenuLv2"] = "menu_quanlydanhmuc_DmNguoiTimViec";
                    return RedirectToAction("Index", "DmNguoiTimViec");
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
            
        }
        [Route("DmNguoiTimViec/Edit")]
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.NhanKhau.FirstOrDefault(t => t.Id == Id);

                    ViewData["MenuLv1"] = "menu_quanlydanhmucdulieu";
                    ViewData["MenuLv2"] = "menu_quanlydanhmuc_DmNguoiTimViec";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/QuanLyDanhMucDuLieu/DmNguoiTimViec/Edit.cshtml", model);
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
            

        }
        [Route("DmNguoiTimViec/Update")]
        [HttpPost]
        public IActionResult Update(NhanKhau request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.NhanKhau.FirstOrDefault(t => t.Id == request.Id);
                    model!.Gioitinh = request.Gioitinh;
                    model.HoVaTen = request.HoVaTen;
                    model.NgayThangNamSinh = request.NgayThangNamSinh;
                    model.SoCCCD = request.SoCCCD;
                    model.MaBHXH = request.MaBHXH;
                    model.ThuongTru = request.ThuongTru;
                    model.NoiOHienTai = request.NoiOHienTai;
                    model.UuTien = request.UuTien;
                    model.DanToc = request.DanToc;
                    model.TrinhDoCMKT = request.TrinhDoCMKT;
                    model.TrinhDoHV = request.TrinhDoHV;
                    model.ChuyenNganh = request.ChuyenNganh;
                    model.TinhTrangVL = request.TinhTrangVL;
                    model.CongViecCuThe = request.CongViecCuThe;
                    model.ThamGiaBHXH = request.ThamGiaBHXH;
                    //model.Hdld = request.Hdld;
                    model.NoiLamViec = request.NoiLamViec;
                    model.DiaChiNoiLamViec = request.DiaChiNoiLamViec;
                    model.ThoiGianThatNghiep = request.ThoiGianThatNghiep;
                    model.LyDoKhongThamGia = request.LyDoKhongThamGia;
                    model.SoDinhDanhHoGD = request.SoDinhDanhHoGD;
                    model.QuanHe = request.QuanHe;
                    model.MaLoi = request.MaLoi;
                    model.MaLoaiLoi = request.MaLoaiLoi;
                    model.MaDonVi = request.MaDonVi;
                    model.KyDieuTra = request.KyDieuTra;
                    model.SoLuongTrung = request.SoLuongTrung;
                    model.LoaiBienDong = request.LoaiBienDong;
                    model.RuongBienDong = request.RuongBienDong;
                    model.DoiTuongTimViecLam = request.DoiTuongTimViecLam;
                    model.ViecLamMongMuon = request.ViecLamMongMuon;
                    model.NganhNgheMongMuon = request.NganhNgheMongMuon;
                    model.NganhNgheMuonHoc = request.NganhNgheMuonHoc;
                    model.TrinhDoChuyenMonMuonHoc = request.TrinhDoChuyenMonMuonHoc;
                    model.Sdt = request.Sdt;
                    model.KhuVuc = request.KhuVuc;
                    model.ThiTruongLamViec = request.ThiTruongLamViec;
                    model.LyDo = request.LyDo;
                    model.Created_at = DateTime.Now;
                    model.Updated_at = DateTime.Now;

                    _db.NhanKhau.Update(model);
                    _db.SaveChanges();

                    ViewData["MenuLv1"] = "menu_quanlydanhmucdulieu";
                    ViewData["MenuLv2"] = "menu_quanlydanhmuc_DmNguoiTimViec";
                    return RedirectToAction("Index", "DmNguoiTimViec");
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
            
        }
        [Route("ThongTinCung/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.NhanKhau.FirstOrDefault(t => t.Id == id_delete);
                    _db.NhanKhau.Remove(model!);
                    _db.SaveChanges();

                    ViewData["MenuLv1"] = "menu_quanlydanhmucdulieu";
                    ViewData["MenuLv2"] = "menu_quanlydanhmuc_DmNguoiTimViec";
                    return RedirectToAction("Index", "DmNguoiTimViec");
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
            
        }
    }
}
