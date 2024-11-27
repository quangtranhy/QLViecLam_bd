using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Systems.DanhMuc;
using QLViecLam.Models.Admin.Systems;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD;
using QLViecLam.ViewModels.Helpers.DanhMucChung;
using System.Collections.Generic;
using QLViecLam.Models.Admin.Manages;
using Newtonsoft.Json;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using QLViecLam.Models;

using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace QLViecLam.Controllers.Admin.Manages.CungLaoDong
{
    public class NhanKhauController : Controller
    {

        private readonly ApplicationDbContext _db;

        public NhanKhauController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("NhanKhau")]
        public IActionResult Index(string ttvl, string huyen, string xa, string kydieutra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhanKhau", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            //var madv = "";
            var nam = DateTime.Now.Year.ToString();
            var dmhanhchinh = _db.DmHanhChinh.AsQueryable();
            var hanhchinh_huyen = dmhanhchinh.Where(t => t.CapDo == "H");
            var hanhchinh_xa = dmhanhchinh.Where(t => t.CapDo == "X");
            //var dmdonvi = _db.DmDonvi.AsQueryable();
            var model = _db.NhanKhau.AsQueryable();

            var k = (kydieutra == null) ? nam : kydieutra;
            model = model.Where(x => x.KyDieuTra == k);
    
            if (huyen != null)
            {
                var danhsach_xa = dmhanhchinh.Where(x => x.Parent == huyen);
                var xacdinh_xa = "";
                foreach (var item in danhsach_xa)
                {
                    if (item.MaDb == xa)
                    {
                        xacdinh_xa = "tructhuochuyen";
                    }
                }
                if (xacdinh_xa == "tructhuochuyen")
                {
                    model = model.Where(x => x.MaDiaBan == xa);
                }
                else
                {
                    var ds_xa = dmhanhchinh.Where(x => x.Parent == huyen).Select(x => x.MaDb).ToArray();
                    model = model.Where(x => ds_xa.Contains(x.MaDiaBan));
                }
            }
            else
            {
                huyen = dmhanhchinh.Where(x => x.CapDo == "H").FirstOrDefault()!.MaDb!;
                var ds_xa = dmhanhchinh.Where(x => x.Parent == huyen).Select(x => x.MaDb).ToArray();
                model = model.Where(x => ds_xa.Contains(x.MaDiaBan));
            }

            if (ttvl != null)
            {
                if (ttvl == "1")
                {
                    model = model.Where(x => x.TinhTrangVL == "1");
                }
                if (ttvl == "2")
                {
                    model = model.Where(x => x.TinhTrangVL == "2");
                }
                if (ttvl == "3")
                {
                    model = model.Where(x => x.TinhTrangVL == "3");
                }
            }

            ViewData["ttvl"] = ttvl;
            ViewData["huyen"] = huyen;
            ViewData["xa"] = xa;
            ViewData["kydieutra"] = k;
            ViewData["DsHuyen"] = dmhanhchinh.Where(t => t.CapDo == "H");
            ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "1");
            if (string.IsNullOrEmpty(xa))
            {
                ViewData["DsXa"] = dmhanhchinh.Where(t => t.CapDo == "X");
            }
            else
            {
                ViewData["DsXa"] = dmhanhchinh.Where(t => t.CapDo == "X" && t.Parent == huyen);
            }
            ViewData["title"] = "Cung lao động";
            ViewData["MenuLv1"] = "cunglaodong";
            ViewData["MenuLv2"] = "cunglaodong_nhankhau";
            return View("Views/Admin/Manages/CungLaoDong/NhanKhau/Index.cshtml", model);
        }

        [HttpGet("NhanKhau/Create")]
        public IActionResult Create(string ttvl, string huyen, string xa, string kydieutra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhanKhau", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var session = HttpContext.Session.GetString("SsAdmin");
            var sessionObject = JsonConvert.DeserializeObject<Users>(session!);
            var madv = sessionObject!.MaDonVi;

            var dmhanhchinh = _db.DmHanhChinh;
            var tenxa = dmhanhchinh.Where(x => x.MaDb == xa).FirstOrDefault()!.Name;
            var tenhuyen = dmhanhchinh.Where(x => x.MaDb == huyen).FirstOrDefault()!.Name;
            var tentinh = dmhanhchinh?.Where(x => x.CapDo == "T").FirstOrDefault()!.Name;
            var thuongtru = tenxa + ", " + tenhuyen + ", " + tentinh;


            ViewData["thuongtru"] = thuongtru;
            ViewData["madonvi"] = madv;
            ViewData["ttvl"] = ttvl;
            ViewData["huyen"] = huyen;
            ViewData["xa"] = xa;
            ViewData["kydieutra"] = kydieutra;
            ViewData["DoiTuongUuTien"] = _db.DoiTuongUuTien.Where(x => x.TrangThai == "1");
            ViewData["DanToc"] = _db.DanToc.Where(x => x.TrangThai == "1");
            ViewData["NganhNghe"] = _db.NganhNghe.Where(x => x.TrangThai == "1");
            ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "1");
            ViewData["QuocGia"] = _db.QuocGia.Where(x => x.TrangThai == "1");
            ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "1");
            ViewData["TrinhDoHV"] = _db.TrinhDoHV.Where(x => x.TrangThai == "1");
            ViewData["NganhHoc"] = _db.NganhHoc.Where(x => x.TrangThai == "1");
            ViewData["ThoiGianThatNghiep"] = _db.ThoiGianThatNghiep.Where(x => x.TrangThai == "1");
            ViewData["LyDoKhongThamGiaHDKT"] = _db.LyDoKhongThamGiaHDKT.Where(x => x.TrangThai == "1");
            ViewData["QuanHe"] = _db.QuanHe.Where(x => x.TrangThai == "1");
            ViewData["title"] = "Cung lao động";
            ViewData["MenuLv1"] = "cunglaodong";
            ViewData["MenuLv2"] = "cunglaodong_nhankhau";
            return View("Views/Admin/Manages/CungLaoDong/NhanKhau/Create.cshtml");
        }

        [HttpPost("NhanKhau/Store")]
        public IActionResult Store(NhanKhau request, string ttvl, string kydieutra, string xa, string huyen, string tinh)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhanKhau", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var session = HttpContext.Session.GetString("SsAdmin");
            var sessionObject = JsonConvert.DeserializeObject<Users>(session!);
            var user = sessionObject!.Id;

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
                MaDiaBan = xa,
                User = user,
                TinhTrangVL = request.TinhTrangVL,
                //CongViecCuThe = request.CongViecCuThe,
                //NoiLamViec = request.NoiLamViec,
                //DiaChiNoiLamViec = request.DiaChiNoiLamViec,
                //ThoiGianThatNghiep = request.ThoiGianThatNghiep,
                //LyDoKhongThamGia = request.LyDoKhongThamGia,
            };

            if (request.TinhTrangVL == "1")
            {
                model.CongViecCuThe = request.CongViecCuThe;
                model.NoiLamViec = request.NoiLamViec;
                model.DiaChiNoiLamViec = request.DiaChiNoiLamViec;
            }
            if (request.TinhTrangVL == "2")
            {
                model.ThoiGianThatNghiep = request.ThoiGianThatNghiep;
            }
            if (request.TinhTrangVL == "3")
            {
                model.LyDoKhongThamGia = request.LyDoKhongThamGia;
            }

            _db.NhanKhau.Add(model);
            _db.SaveChanges();
            TempData["Message"] = "Thêm mới thành công!";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "NhanKhau", new { ttvl, huyen, xa, kydieutra });
        }

        [HttpGet("NhanKhau/Edit")]
        public IActionResult Edit(string ttvl, string huyen, string xa, string kydieutra, int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhanKhau", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.NhanKhau.FirstOrDefault(x => x.Id == Id);
            if (model == null)
            {
                TempData["Message"] = "không tìm thấy thông tin hồ sơ!";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index", "NhanKhau", new { ttvl, huyen, xa, kydieutra });
            }
            else
            {
                ViewData["tinhtrang"] = model!.TinhTrangVL;
                ViewData["tinhtrangvl"] = ttvl;
                ViewData["huyen"] = huyen;
                ViewData["xa"] = xa;
                ViewData["kydieutra"] = kydieutra;
                ViewData["DanToc"] = _db.DanToc.Where(x => x.TrangThai == "1");
                ViewData["DoiTuongUuTien"] = _db.DoiTuongUuTien.Where(x => x.TrangThai == "1");
                ViewData["NganhNghe"] = _db.NganhNghe.Where(x => x.TrangThai == "1");
                ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "1");
                ViewData["QuocGia"] = _db.QuocGia.Where(x => x.TrangThai == "1");
                ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "1");
                ViewData["TrinhDoHV"] = _db.TrinhDoHV.Where(x => x.TrangThai == "1");
                ViewData["NganhHoc"] = _db.NganhHoc.Where(x => x.TrangThai == "1");
                ViewData["ThoiGianThatNghiep"] = _db.ThoiGianThatNghiep.Where(x => x.TrangThai == "1");
                ViewData["LyDoKhongThamGiaHDKT"] = _db.LyDoKhongThamGiaHDKT.Where(x => x.TrangThai == "1");
                ViewData["QuanHe"] = _db.QuanHe.Where(x => x.TrangThai == "1");
                ViewData["title"] = "Cung lao động";
                ViewData["MenuLv1"] = "cunglaodong";
                ViewData["MenuLv2"] = "cunglaodong_nhankhau";
                return View("Views/Admin/Manages/CungLaoDong/NhanKhau/Edit.cshtml", model);
            }
        }

        [HttpPost("NhanKhau/Update")]
        public IActionResult Update(NhanKhau request, string ttvl, string huyen, string xa, string kydieutra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhanKhau", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.NhanKhau.FirstOrDefault(x => x.Id == request.Id);

            if (model != null)
            {
                model.Id = request.Id;
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

                if (request.TinhTrangVL == "1")
                {
                    model.CongViecCuThe = request.CongViecCuThe;
                    model.NoiLamViec = request.NoiLamViec;
                    model.DiaChiNoiLamViec = request.DiaChiNoiLamViec;

                    model.ThoiGianThatNghiep = null;
                    model.LyDoKhongThamGia = null;
                }
                if (request.TinhTrangVL == "2")
                {
                    model.CongViecCuThe = null;
                    model.NoiLamViec = null;
                    model.DiaChiNoiLamViec = null;
                    model.ThoiGianThatNghiep = request.ThoiGianThatNghiep;
                    model.LyDoKhongThamGia = null;
                }
                if (request.TinhTrangVL == "3")
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

            TempData["Message"] = "Cập nhật thông tin thành công!";
            TempData["MessageType"] = "success";
            ViewData["MenuLv1"] = "cunglaodong";
            ViewData["MenuLv2"] = "cunglaodong_nhankhau";
            return RedirectToAction("Index", "NhanKhau", new { ttvl, huyen, xa, kydieutra });
        }

        [HttpPost("NhanKhau/Delete")]
        public IActionResult Delete(int id_delete, string ttvl, string huyen, string xa, string kydieutra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhanKhau", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            //var session = HttpContext.Session.GetString("SsAdmin");
            //var sessionObject = JsonConvert.DeserializeObject<Users>(session!);
            //var CapDo = sessionObject!.CapDo;

            //if (CapDo == "H" || CapDo == "X")
            //{

            //}
            var model = _db.NhanKhau.FirstOrDefault(t => t.Id == id_delete);
            if (model != null)
            {
                _db.NhanKhau.Remove(model);
                _db.SaveChanges();
                TempData["Message"] = "Xóa thông tin thành công!";
                TempData["MessageType"] = "success";
                return RedirectToAction("Index", "NhanKhau", new { ttvl, huyen, xa, kydieutra });
            }
            else
            {
                TempData["Message"] = "không tìm thấy thông tin hồ sơ!";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index", "NhanKhau", new { ttvl, huyen, xa, kydieutra });
            }
        }

        [HttpGet("NhanKhau/BcChiTiet")]
        public IActionResult BcChiTiet(string huyen, string xa, string kydieutra)
        {
            var dmhanhchinh = _db.DmHanhChinh;
            var dmdonvi = _db.DmDonvi;
            var madv = dmdonvi.FirstOrDefault(x => x.MaDiaBan == xa)!.MaDonVi;
            var model = _db.NhanKhau.Where(x => x.MaDonVi == madv && x.KyDieuTra == kydieutra).ToList();
            var DoiTuongUuTien = _db.DoiTuongUuTien.Where(x => x.TrangThai == "1").ToList();
            var DanToc = _db.DanToc.Where(x => x.TrangThai == "1").ToList();
            var TrinhDoHV = _db.TrinhDoHV.Where(x => x.TrangThai == "1").ToList();
            var TrinhDoCMKT = _db.TrinhDoCMKT.Where(x => x.TrangThai == "1").ToList();
            var NganhHoc = _db.NganhHoc.Where(x => x.TrangThai == "1").ToList();
            var ThoiGianThatNghiep = _db.ThoiGianThatNghiep;
            var LyDoKhongThamGiaHDKT = _db.LyDoKhongThamGiaHDKT;
            var newmodel = (from m in model
                            join u in DoiTuongUuTien on m.UuTien equals u.MaDoiTuongUuTien
                            //into left
                            //from model in left.DefaultIfEmpty()

                            join d in DanToc on m.DanToc equals d.MaDanToc
                            join hv in TrinhDoHV on m.TrinhDoHV equals hv.MaTrinhDoHV
                            join cmkt in TrinhDoCMKT on m.TrinhDoCMKT equals cmkt.MaTrinhDoCMKT
                            join nh in NganhHoc on m.NganhNgheMongMuon equals nh.MaNganhHoc into details
                            from nh_de in details.DefaultIfEmpty()
                            join thoigian in ThoiGianThatNghiep on m.ThoiGianThatNghiep equals thoigian.MaThoiGianThatNghiep into details_2
                            from thoigian_de in details_2.DefaultIfEmpty()
                            join khongthamgia in LyDoKhongThamGiaHDKT on m.LyDoKhongThamGia equals khongthamgia.MaLyDoKhongThamGia into details_3
                            from khongthamgia_de in details_3.DefaultIfEmpty()
                            select new VM_NhanKhau
                            {
                                HoVaTen = m.HoVaTen,
                                Gioitinh = m.Gioitinh,
                                SoCCCD = m.SoCCCD,
                                NgayThangNamSinh = m.NgayThangNamSinh,
                                KhuVuc = m.KhuVuc,
                                //NoiOHienTai = m.NoiOHienTai,
                                TenUuTien = u.TenDoiTuongUuTien,
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
                                TenLyDoKhongThamGia = khongthamgia_de != null ? khongthamgia_de.TenLyDoKhongThamGia : null,

                            });


            ViewData["tenxa"] = dmhanhchinh.FirstOrDefault(x => x.MaDb == xa)!.Name;
            ViewData["tenhuyen"] = dmhanhchinh.FirstOrDefault(x => x.MaDb == huyen)!.Name;
            ViewData["tentinh"] = dmhanhchinh.FirstOrDefault(x => x.CapDo == "T")!.Name;
            ViewData["kydieutra"] = kydieutra;

            ViewData["MenuLv1"] = "cunglaodong";
            ViewData["MenuLv2"] = "cunglaodong_nhankhau";
            return View("Views/Admin/Manages/CungLaoDong/NhanKhau/BcChitiet.cshtml", newmodel);
        }

        public async Task<IActionResult> Import(IFormFile formFile)
        {
            var session = HttpContext.Session.GetString("SsAdmin");
            var sessionObject = JsonConvert.DeserializeObject<Users>(session!);
            var madv = sessionObject!.MaDonVi;
            var user = sessionObject!.Id;

            if (formFile == null || formFile.Length <= 0)
                return View("Error", new ErrorViewModel { RequestId = "File is empty" });

            var data = new List<NhanKhau>();
            var Kydieutra = DateTime.Now.Year.ToString();
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return View("Error", new ErrorViewModel { RequestId = "Not Support file extension" });

            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var SoCCCD = worksheet.Cells[row, 6].Value?.ToString()?.Trim() ?? string.Empty;
                        var SoDinhDanhHoGD = worksheet.Cells[row, 2].Value?.ToString()?.Trim() ?? string.Empty;
                        var HoVaTen = worksheet.Cells[row, 5].Value?.ToString()?.Trim() ?? string.Empty;
                        if (SoCCCD != "" && SoDinhDanhHoGD != "" && HoVaTen != "")
                        {
                            data.Add(new NhanKhau
                            {

                                SoDinhDanhHoGD = worksheet.Cells[row, 2].Value?.ToString()?.Trim() ?? string.Empty,
                                QuanHe = worksheet.Cells[row, 4].Value?.ToString()?.Trim() ?? string.Empty,
                                HoVaTen = worksheet.Cells[row, 5].Value?.ToString()?.Trim() ?? string.Empty,
                                SoCCCD = worksheet.Cells[row, 6].Value?.ToString()?.Trim() ?? string.Empty,
                                Gioitinh = worksheet.Cells[row, 8].Value?.ToString()?.Trim() ?? string.Empty,
                                NgayThangNamSinh = DateTime.TryParse(worksheet.Cells[row, 9].Value?.ToString()?.Trim(), out DateTime ngayThangNamSinh) ? ngayThangNamSinh : DateTime.MinValue,
                                Sdt = worksheet.Cells[row, 10].Value?.ToString()?.Trim() ?? string.Empty,
                                KhuVuc = worksheet.Cells[row, 12].Value?.ToString()?.Trim() ?? string.Empty,
                                NoiOHienTai = worksheet.Cells[row, 13].Value?.ToString()?.Trim() ?? string.Empty,
                                ThuongTru = (worksheet.Cells[row, 16].Value?.ToString()?.Trim() ?? string.Empty) + ", "+
                                 (worksheet.Cells[row, 14].Value?.ToString()?.Trim() ?? string.Empty) + ", Bình Định",
                                UuTien = worksheet.Cells[row, 19].Value?.ToString()?.Trim() ?? string.Empty,
                                DanToc = worksheet.Cells[row, 21].Value?.ToString()?.Trim() ?? string.Empty,
                                TrinhDoHV = worksheet.Cells[row, 23].Value?.ToString()?.Trim() ?? string.Empty,
                                TrinhDoCMKT = worksheet.Cells[row, 25].Value?.ToString()?.Trim() ?? string.Empty,
                                ViecLamMongMuon = worksheet.Cells[row, 27].Value?.ToString()?.Trim() ?? string.Empty,
                                ThiTruongLamViec = worksheet.Cells[row, 29].Value?.ToString()?.Trim() ?? string.Empty,
                                NganhNgheMuonHoc = worksheet.Cells[row, 31].Value?.ToString()?.Trim() ?? string.Empty,
                                TrinhDoChuyenMonMuonHoc = worksheet.Cells[row, 33].Value?.ToString()?.Trim() ?? string.Empty,
                                DoiTuongTimViecLam = worksheet.Cells[row, 35].Value?.ToString()?.Trim() ?? string.Empty,
                                TinhTrangVL = worksheet.Cells[row, 37].Value?.ToString()?.Trim() ?? string.Empty,
                                CongViecCuThe = worksheet.Cells[row, 38].Value?.ToString()?.Trim() ?? string.Empty,
                                NoiLamViec = worksheet.Cells[row, 39].Value?.ToString()?.Trim() ?? string.Empty,
                                DiaChiNoiLamViec = worksheet.Cells[row, 40].Value?.ToString()?.Trim() ?? string.Empty,
                                ThoiGianThatNghiep = worksheet.Cells[row, 42].Value?.ToString()?.Trim() ?? string.Empty,
                                LyDoKhongThamGia = worksheet.Cells[row, 44].Value?.ToString()?.Trim() ?? string.Empty,
                                KyDieuTra = Kydieutra,
                                MaDiaBan = worksheet.Cells[row, 17].Value?.ToString()?.Trim() ?? string.Empty,
                                MaDonVi = madv,
                                User = user,
                            });
                        }
                    }
                }
            }
  
            try
            {
                _db.NhanKhau.AddRange(data);
                _db.SaveChanges();
                TempData["Message"] = "Nhập dữ liệu thành công!";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Nhập dữ liệu thất bại: " + ex.Message;
                TempData["MessageType"] = "error";
            }
            //if (data.Count() > 0)
            //{

            //    TempData["Message"] = "nhập dữ liệu thành công!";
            //    TempData["MessageType"] = "success";
            //}
            //else {
            //    TempData["Message"] = "nhập dữ liệu thất bại!";
            //    TempData["MessageType"] = "error";
            //}
            return RedirectToAction("Index", "NhanKhau");
        }




    }

}

