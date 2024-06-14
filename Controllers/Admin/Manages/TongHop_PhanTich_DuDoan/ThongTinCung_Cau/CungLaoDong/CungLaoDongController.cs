using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Manages.DanhMuc;
using QLViecLam.Models.Admin.Manages.ThongTinThiTruongLD;
using QLViecLam.Models.Admin.Systems;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD;
using QLViecLam.ViewModels.Helpers.DanhMucChung;
using System.Collections.Generic;

namespace QLViecLam.Controllers.Admin.Manages.TongHop_PhanTich_DuDoan.ThongTinCung_Cau.CungLaoDong
{
    public class CungLaoDongController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CungLaoDongController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("CungLaoDong")]
        [HttpGet]
        public IActionResult Index(string ttvl, string huyen, string xa, string kydieutra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var madv = "";
                    var nam = DateTime.Now.Year.ToString();
                    var dmhanhchinh = _db.DmHanhChinh.AsQueryable();
                    var dmdonvi = _db.DmDonvi.AsQueryable();
                    var model = _db.NhanKhau.AsQueryable();
                    var k = (kydieutra == null) ? nam : kydieutra;

                    model = model.Where(x => x.KyDieuTra == k);

                    if (huyen != null)
                    {

                        var ds_xa = dmhanhchinh.Where(x => x.Parent == huyen);
                        var xd_xa = "";
                        foreach (var item in ds_xa)
                        {
                            if (item.MaQuocGia == xa)
                            {
                                xd_xa = "tontai";
                            }
                        }

                        if (xd_xa == "tontai")
                        {
                            xa = dmhanhchinh.Where(x => x.MaQuocGia == xa).FirstOrDefault()!.MaQuocGia!;
                            var id_xa = dmhanhchinh.Where(x => x.MaQuocGia == xa).FirstOrDefault()!.Id.ToString();
                            madv = dmdonvi.Where(x => x.MaDiaBan == id_xa).FirstOrDefault()!.MaDonVi;
                            model = model.Where(x => x.MaDonVi == madv);
                        }
                        else
                        {
                            xa = dmhanhchinh.Where(x => x.Parent == huyen).FirstOrDefault()!.MaQuocGia!;
                            var id_xa = dmhanhchinh.Where(x => x.Parent == huyen).FirstOrDefault()!.Id.ToString();
                            madv = dmdonvi.Where(x => x.MaDiaBan == id_xa).FirstOrDefault()!.MaDonVi;
                            model = model.Where(x => x.MaDonVi == madv);
                        }

                    }
                    else
                    {
                        huyen = dmhanhchinh.Where(x => x.CapDo == "H").FirstOrDefault()!.MaQuocGia!;
                        xa = dmhanhchinh.Where(x => x.Parent == huyen).FirstOrDefault()!.MaQuocGia!;
                        var id_xa = dmhanhchinh.Where(x => x.Parent == huyen).FirstOrDefault()!.Id.ToString();
                        madv = dmdonvi.Where(x => x.MaDiaBan == id_xa).FirstOrDefault()!.MaDonVi;
                        model = model.Where(x => x.MaDonVi == madv);
                    }

                    if (ttvl != null)
                    {
                        if (ttvl == "1")
                        {
                            model = model.Where(x => x.TinhTrangVL == 1);
                        }
                        if (ttvl == "2")
                        {
                            model = model.Where(x => x.TinhTrangVL == 2);
                        }
                        if (ttvl == "3")
                        {
                            model = model.Where(x => x.TinhTrangVL == 3);
                        }
                    }

                    ViewData["ttvl"] = ttvl;
                    ViewData["huyen"] = huyen;
                    ViewData["xa"] = xa;
                    ViewData["kydieutra"] = k;
                    ViewData["DsHuyen"] = dmhanhchinh.Where(t => t.CapDo == "H");
                    ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "kh");
                    if (string.IsNullOrEmpty(xa))
                    {
                        ViewData["DsXa"] = dmhanhchinh.Where(t => t.CapDo == "X");
                    }
                    else
                    {
                        ViewData["DsXa"] = dmhanhchinh.Where(t => t.CapDo == "X" && t.Parent == huyen);
                    }

                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_cunglaodong";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/CungLaoDong/Index.cshtml", model);
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

        [Route("CungLaoDong/Create")]
        [HttpGet]
        public IActionResult Create(string ttvl, string huyen, string xa, string kydieutra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var tinh = "44";
                    var id_xa = _db.DmHanhChinh.Where(t => t.MaQuocGia == xa).FirstOrDefault()!.Id.ToString();
                    var madv = _db.DmDonvi.Where(t => t.MaDiaBan == id_xa).FirstOrDefault()!.MaDonVi;

                    var dmhanhchinh = _db.DmHanhChinh;
                    var tenxa = dmhanhchinh.Where(x => x.MaQuocGia == xa).FirstOrDefault()!.Name;
                    var tenhuyen = dmhanhchinh.Where(x => x.MaQuocGia == huyen).FirstOrDefault()!.Name;
                    var tentinh = dmhanhchinh?.Where(x => x.MaQuocGia == tinh).FirstOrDefault()!.Name;
                    var thuongtru = tenxa + ", " + tenhuyen + ", tỉnh " + tentinh;

                    ViewData["thuongtru"] = thuongtru;
                    ViewData["madonvi"] = madv;
                    ViewData["ttvl"] = ttvl;
                    ViewData["huyen"] = huyen;
                    ViewData["xa"] = xa;
                    ViewData["kydieutra"] = kydieutra;

                    ViewData["DanToc"] = _db.DanToc.Where(x => x.TrangThai == "kh");
                    ViewData["NganhHoc"] = _db.NganhHoc.Where(x => x.TrangThai == "kh");
                    ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "kh");
                    ViewData["QuocGia"] = _db.QuocGia.Where(x => x.TrangThai == "kh");
                    ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "kh");
                    ViewData["TrinhDoHV"] = _db.TrinhDoHV.Where(x => x.TrangThai == "kh");

                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_cunglaodong";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/CungLaoDong/Create.cshtml");
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

        [Route("CungLaoDong/Store")]
        [HttpPost]
        public IActionResult Store(NhanKhau request, string ttvl, string kydieutra, string xa, string huyen, string tinh)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    
                    var model = new NhanKhau()
                    {
                        SoDinhDanhHoGD = request.SoDinhDanhHoGD,
                        QuanHe = request.QuanHe,
                        HoVaTen = request.HoVaTen,
                        Gioitinh = request.Gioitinh,
                        SoCCCD = request.SoCCCD,
                        NgayThangNamSinh = request.NgayThangNamSinh,
                        Sdt = request.Sdt,
                        KhuVuc = request.KhuVuc,
                        NoiOHienTai = request.NoiOHienTai,
                        ThuongTru = request.ThuongTru,
                        UuTien = request.UuTien,
                        DanToc = request.DanToc,
                        TrinhDoHV = request.TrinhDoHV,
                        TrinhDoCMKT = request.TrinhDoCMKT,
                        DoiTuongTimViecLam = request.DoiTuongTimViecLam,
                        ViecLamMongMuon = request.ViecLamMongMuon,
                        ThiTruongLamViec = request.ThiTruongLamViec,
                        NganhNgheMuonHoc = request.NganhNgheMuonHoc,
                        TrinhDoChuyenMonMuonHoc = request.TrinhDoChuyenMonMuonHoc,
                        KyDieuTra = request.KyDieuTra,
                        MaDonVi = request.MaDonVi,
                        TinhTrangVL = request.TinhTrangVL,
                        //CongViecCuThe = request.CongViecCuThe,
                        //NoiLamViec = request.NoiLamViec,
                        //DiaChiNoiLamViec = request.DiaChiNoiLamViec,
                        //ThoiGianThatNghiep = request.ThoiGianThatNghiep,
                        //LyDoKhongThamGia = request.LyDoKhongThamGia,
                    };

                    if (request.TinhTrangVL == 1)
                    {
                        model.CongViecCuThe = request.CongViecCuThe;
                        model.NoiLamViec = request.NoiLamViec;
                        model.DiaChiNoiLamViec = request.DiaChiNoiLamViec;
                    }
                    if (request.TinhTrangVL == 2)
                    {
                        model.ThoiGianThatNghiep = request.ThoiGianThatNghiep;
                    }
                    if (request.TinhTrangVL == 3)
                    {
                        model.LyDoKhongThamGia = request.LyDoKhongThamGia;
                    }

                    _db.NhanKhau.Add(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "CungLaoDong", new { ttvl, huyen, xa, kydieutra });
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

        [Route("CungLaoDong/Edit")]
        [HttpGet]
        public IActionResult Edit(string ttvl, string huyen, string xa, string kydieutra, int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.NhanKhau.FirstOrDefault(x => x.Id == Id);

                    ViewData["tinhtrangvl"] = ttvl;
                    ViewData["huyen"] = huyen;
                    ViewData["xa"] = xa;
                    ViewData["kydieutra"] = kydieutra;

                    ViewData["DanToc"] = _db.DanToc.Where(x => x.TrangThai == "kh");
                    ViewData["NganhHoc"] = _db.NganhHoc.Where(x => x.TrangThai == "kh");
                    ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "kh");
                    ViewData["QuocGia"] = _db.QuocGia.Where(x => x.TrangThai == "kh");
                    ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "kh");
                    ViewData["TrinhDoHV"] = _db.TrinhDoHV.Where(x => x.TrangThai == "kh");

                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_cunglaodong";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/CungLaoDong/Edit.cshtml", model);
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

        [Route("CungLaoDong/Update")]
        [HttpPost]
        public IActionResult Update(NhanKhau request, string ttvl, string huyen, string xa, string kydieutra, int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.NhanKhau.FirstOrDefault(x => x.Id == Id);
                    if (model != null)
                    {
                        model.SoDinhDanhHoGD = request.SoDinhDanhHoGD;
                        model.QuanHe = request.QuanHe;
                        model.HoVaTen = request.HoVaTen;
                        model.Gioitinh = request.Gioitinh;
                        model.SoCCCD = request.SoCCCD;
                        model.NgayThangNamSinh = request.NgayThangNamSinh;
                        model.Sdt = request.Sdt;
                        model.KhuVuc = request.KhuVuc;
                        model.NoiOHienTai = request.NoiOHienTai;
                        model.UuTien = request.UuTien;
                        model.DanToc = request.DanToc;
                        model.TrinhDoHV = request.TrinhDoHV;
                        model.TrinhDoCMKT = request.TrinhDoCMKT;
                        model.DoiTuongTimViecLam = request.DoiTuongTimViecLam;
                        model.ViecLamMongMuon = request.ViecLamMongMuon;
                        model.ThiTruongLamViec = request.ThiTruongLamViec;
                        model.NganhNgheMuonHoc = request.NganhNgheMuonHoc;
                        model.TrinhDoChuyenMonMuonHoc = request.TrinhDoChuyenMonMuonHoc;
                        model.TinhTrangVL = request.TinhTrangVL;

                        if (request.TinhTrangVL == 1)
                        {
                            model.CongViecCuThe = request.CongViecCuThe;
                            model.NoiLamViec = request.NoiLamViec;
                            model.DiaChiNoiLamViec = request.DiaChiNoiLamViec;

                            model.ThoiGianThatNghiep = null;
                            model.LyDoKhongThamGia = null;
                        }
                        if (request.TinhTrangVL == 2)
                        {
                            model.CongViecCuThe = null;
                            model.NoiLamViec = null;
                            model.DiaChiNoiLamViec = null;
                            model.ThoiGianThatNghiep = request.ThoiGianThatNghiep;
                            model.LyDoKhongThamGia = null;
                        }
                        if (request.TinhTrangVL == 3)
                        {
                            model.CongViecCuThe = null;
                            model.NoiLamViec = null;
                            model.DiaChiNoiLamViec = null;
                            model.ThoiGianThatNghiep = null;
                            model.LyDoKhongThamGia = request.LyDoKhongThamGia;
                        }

                        _db.NhanKhau.Update(model);
                        _db.SaveChanges();
                    };


                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_cunglaodong";
                    return RedirectToAction("Index", "CungLaoDong", new { ttvl, huyen, xa, kydieutra });
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
        [Route("CungLaoDong/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete, string ttvl, string huyen, string xa, string kydieutra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.NhanKhau.FirstOrDefault(t => t.Id == id_delete);
                    if (model != null)
                    {
                        _db.NhanKhau.Remove(model);
                        _db.SaveChanges();
                    }
                    return RedirectToAction("Index", "CungLaoDong", new { ttvl, huyen, xa, kydieutra });
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

        [Route("CungLaoDong/BcChiTiet")]
        [HttpGet]
        public IActionResult BcChiTiet(string huyen, string xa, string kydieutra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var dmhanhchinh = _db.DmHanhChinh;
                    var dmdonvi = _db.DmDonvi;

                    var id_xa = dmhanhchinh.FirstOrDefault(x => x.MaQuocGia == xa)!.Id.ToString();
                    var madv = dmdonvi.FirstOrDefault(x => x.MaDiaBan == id_xa)!.MaDonVi;
                    var model = _db.NhanKhau.Where(x => x.MaDonVi == madv && x.KyDieuTra == kydieutra).ToList();
                    var DoiTuongUT = DanhMucChung.DoiTuongUT();
                    var DanToc = _db.DanToc.ToList();
                    var TrinhDoHV = _db.TrinhDoHV.ToList();
                    var TrinhDoCMKT = _db.TrinhDoCMKT.ToList();
                    var NganhHoc = _db.NganhHoc.ToList();
                    var ThoiGianThatNghiep = DanhMucChung.ThoiGianThatNghiep();
                    var LyDoKhongThamGiaHDKT = DanhMucChung.LyDoKhongThamGiaHDKT();
                    var newmodel = (from m in model
                                    join u in DoiTuongUT on m.UuTien equals u.MaDoiTuongUT
                                    //into left
                                    //from model in left.DefaultIfEmpty()

                                    join d in DanToc on m.DanToc equals d.MaDanToc
                                    join hv in TrinhDoHV on m.TrinhDoHV equals hv.MaTrinhDoHV
                                    join cmkt in TrinhDoCMKT on m.TrinhDoCMKT equals cmkt.MaTrinhDoCMKT
                                    join nh in NganhHoc on m.NganhNgheMongMuon equals nh.MaNganhHoc into details
                                    from nh_de in details.DefaultIfEmpty()
                                    join thoigian in ThoiGianThatNghiep on m.ThoiGianThatNghiep equals thoigian.MaThoiGianThatNghiep into details_2
                                    from thoigian_de in details_2.DefaultIfEmpty()
                                    join khongthamgia in LyDoKhongThamGiaHDKT on m.LyDoKhongThamGia equals khongthamgia.MaLyDoKhongThamGiaHDKT into details_3
                                    from khongthamgia_de in details_3.DefaultIfEmpty()
                                    select new VM_NhanKhau
                                    {
                                        HoVaTen = m.HoVaTen,
                                        Gioitinh = m.Gioitinh,
                                        SoCCCD = m.SoCCCD,
                                        NgayThangNamSinh = m.NgayThangNamSinh,
                                        KhuVuc = m.KhuVuc,
                                        //NoiOHienTai = m.NoiOHienTai,
                                        TenUuTien = u.TenDoiTuongUT,
                                        TenDanToc = d.TenDanToc,
                                        TenTrinhDoHV = hv.TenTrinhDoHV,
                                        TenTrinhDoCMKT = cmkt.TenTrinhDoCMKT,
                                        DoiTuongTimViecLam = m.DoiTuongTimViecLam,
                                        ViecLamMongMuon = m.ViecLamMongMuon,
                                        ThiTruongLamViec = m.ThiTruongLamViec,
                                        TenNganhHoc = nh_de != null ? nh_de.TenNganhHoc : null,

                                        TrinhDoChuyenMonMuonHoc = m.TrinhDoChuyenMonMuonHoc,

                                        TinhTrangVL = m.TinhTrangVL,
                                        CongViecCuThe = m.CongViecCuThe,
                                        NoiLamViec = m.NoiLamViec,
                                        DiaChiNoiLamViec = m.DiaChiNoiLamViec,
                                        TenThoiGianThatNghiep = thoigian_de != null ? thoigian_de.TenThoiGianThatNghiep : null,
                                        TenLyDoKhongThamGia = khongthamgia_de != null ? khongthamgia_de.TenLyDoKhongThamGiaHDKT : null,

                                    });


                    ViewData["tenxa"] = dmhanhchinh.FirstOrDefault(x => x.MaQuocGia == xa)!.Name;
                    ViewData["tenhuyen"] = dmhanhchinh.FirstOrDefault(x => x.MaQuocGia == huyen)!.Name;
                    ViewData["tentinh"] = dmhanhchinh.FirstOrDefault(x => x.CapDo == "T")!.Name;
                    ViewData["kydieutra"] = kydieutra;

                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_cunglaodong";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/CungLaoDong/BcChitiet.cshtml", newmodel);
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
