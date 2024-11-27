using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Migrations;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using System.Linq;
using System.Security.Cryptography;

namespace QLViecLam.Controllers.Admin.Manages.KeHoachThuThapTT
{
    public class KeHoachThuThapController : Controller
    {
        private readonly ApplicationDbContext _db;

        public KeHoachThuThapController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("KeHoachThuThap")]
        public IActionResult Index(string kydieutra, string xa)
        {
            var model = _db.KeHoachThuThapThongTin.AsQueryable();
            var DsKyDieuTra = model.Select(k => k.KeHoach).Distinct().OrderByDescending(k => k).ToList();
            var dmhanhchinh = _db.DmHanhChinh;
            var dmdonvi = _db.DmDonvi.AsQueryable();

            var capdo = Helpers.GetSsAdminKey(HttpContext.Session, "CapDo");
            if (capdo == "T")
            {
                if (xa != null)
                {
                    model = model.Where(x => x.MaDiaBan == xa);
                }
                ViewData["DsXa"] = dmhanhchinh.Where(x => x.CapDo == "X");
            }
            else if (capdo == "H")
            {
                var madonvi = Helpers.GetSsAdminKey(HttpContext.Session, "MaDonVi");
                var madiaban_huyen = dmdonvi.FirstOrDefault(xa => xa.MaDonVi == madonvi)!.MaDiaBan;
                if (xa != null)
                {
                    model = model.Where(x => x.MaDiaBan == xa);
                }
                else
                {
                    var list_madiaban_xa = dmhanhchinh.OrderBy(x => x.Parent == madiaban_huyen).Select(x => x.MaDb);
                    model = model.Where(x => list_madiaban_xa.Contains(x.MaDiaBan)).AsQueryable();
                }
                ViewData["DsXa"] = dmhanhchinh.Where(x => x.Parent == madiaban_huyen);
            }
            // cấp xã
            else
            {
                var madonvi = Helpers.GetSsAdminKey(HttpContext.Session, "MaDonVi");
                xa = dmdonvi.FirstOrDefault(x => x.MaDonVi == madonvi)!.MaDiaBan!;
                model = model.Where(x => x.MaDiaBan == xa);
                ViewData["DsXa"] = dmhanhchinh.Where(x => x.MaDb == xa);
            }

            if (kydieutra == null)
            {
                kydieutra = model.OrderByDescending(t => t.NgayLapKeHoach).FirstOrDefault()!.KeHoach!;
            }
            var DsKyDieuTra_check = model.Where(x => x.MaDiaBan == xa).Select(k => k.KeHoach).Distinct().ToList();
            model = model.Where(t => t.KeHoach == kydieutra);

            var newmodel = (from m in model
                            join hc in dmhanhchinh on m.MaDiaBan equals hc.MaDb
                            select new KeHoachThuThapThongTin
                            {
                                Id = m.Id,
                                KeHoach = m.KeHoach,
                                NgayLapKeHoach = m.NgayLapKeHoach,
                                MaDiaBan = m.MaDiaBan,
                                TenDiaBan = hc.Name,
                                Status = m.Status,
                                LyDoTraLai = m.LyDoTraLai,
                                NgayKyKeHoach = m.NgayKyKeHoach,
                            });


            var nam = (DateTime.Now.Year).ToString();

            var checkNam = DsKyDieuTra_check.Contains(nam);
            if (!checkNam)
            {
                ViewData["CheckThemMoi"] = true;
            }
            else
            {
                ViewData["CheckThemMoi"] = false;
            }
            if (xa == null)
            {
                ViewData["CheckThemMoi"] = false;
            }


            ViewData["xa"] = xa;
            ViewData["kydieutra"] = kydieutra;
            ViewData["DsKyDieuTra"] = DsKyDieuTra;
            ViewData["Title"] = "Kế hoạch thu thập thông tin lao động";
            ViewData["MenuLv1"] = "kehoach";
            ViewData["MenuLv2"] = "kehoach_thuthap";
            return View("Views/Admin/Manages/KeHoachThuThapTT/DanhSach/Index.cshtml", newmodel);
        }

        [HttpGet("KeHoachThuThap/Create")]
        public IActionResult Create(string xa, string huyen)
        {
            //madonvi và user lấy tự động từ phiên đăng nhập
            var ssadmin = Helpers.GetSsAdmin(HttpContext.Session);
            var user = ssadmin.Id;
            var madonvi = ssadmin.MaDonVi;
            var model = new KeHoachThuThapThongTin
            {
                //madiaban được chọn từ view
                MaDiaBan = xa,
                User = user,
                MaDonVi = madonvi,
                KeHoach = DateTime.Now.Year.ToString(),
                MaKeHoach = DateTime.Now.ToString("yymmddHis"),
                NgayLapKeHoach = DateTime.Now,
                Status = "ChoChuyen"
            };

            ViewData["dbxa"] = _db.DmHanhChinh.FirstOrDefault(x => x.MaDb == xa)!.Name;
            ViewData["Title"] = "Kế hoạch thu thập thông tin lao động";
            ViewData["MenuLv1"] = "kehoach";
            ViewData["MenuLv2"] = "kehoach_thuthap";
            return View("Views/Admin/Manages/KeHoachThuThapTT/DanhSach/Create.cshtml", model);
        }

        [HttpPost]
        public IActionResult Store(KeHoachThuThapThongTin request)
        {

            _db.KeHoachThuThapThongTin.Add(request);
            _db.SaveChanges();
            var huyen = _db.DmHanhChinh.FirstOrDefault(x => x.MaDb == request.MaDiaBan)!.Parent;
            return RedirectToAction("Index", "KeHoachThuThap", new { kydieutra = request.KeHoach, xa = request.MaDiaBan, huyen = huyen });
        }

        [HttpGet("KeHoachThuThap/Edit")]
        public IActionResult Edit(int Id)
        {

            var model = _db.KeHoachThuThapThongTin.FirstOrDefault(t => t.Id == Id);

            if (model != null)
            {
                ViewData["dbxa"] = _db.DmHanhChinh.FirstOrDefault(x => x.MaDb == model!.MaDiaBan)!.Name;
                ViewData["Title"] = "Kế hoạch thu thập thông tin lao động";
                ViewData["MenuLv1"] = "kehoach";
                ViewData["MenuLv2"] = "kehoach_thuthap";
                return View("Views/Admin/Manages/KeHoachThuThapTT/DanhSach/Edit.cshtml", model);
            }
            else
            {
                ViewData["Messages"] = "Không thể chỉnh sửa thông tin!";
                return View("Views/Admin/Error/Page.cshtml");
            }

        }

        [HttpPost]
        public IActionResult Update(KeHoachThuThapThongTin request)
        {

            var model = _db.KeHoachThuThapThongTin.FirstOrDefault(t => t.Id == request.Id);
            if (model != null)
            {
                model.KeHoach = request.KeHoach;
                model.CanCu = request.CanCu;
                model.YeuCau = request.YeuCau;
                model.NgayKyKeHoach = request.NgayKyKeHoach;
                model.MucDich = request.MucDich;
                model.YeuCau = request.YeuCau;
                model.NguyenTacThuThap = request.NguyenTacThuThap;
                model.KhoiLuongThongTinThuThap = request.KhoiLuongThongTinThuThap;
                model.NoiDungThuThap = request.NoiDungThuThap;
                model.SanPhamThuThap = request.SanPhamThuThap;
                model.KeHoachThucHien = request.KeHoachThucHien;
                model.KinhPhiThucHien = request.KinhPhiThucHien;
                model.ToChucThucHien = request.ToChucThucHien;
                _db.KeHoachThuThapThongTin.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Index", "KeHoachThuThap", new { kydieutra = request.KeHoach, madiaban = request.MaDiaBan });
            }
            else
            {
                ViewData["Messages"] = "Không thể chỉnh sửa thông tin!";
                return View("Views/Admin/Error/Page.cshtml");
            }

        }


        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            var model = _db.KeHoachThuThapThongTin.FirstOrDefault(t => t.Id == id_delete);
            ViewData["MenuLv1"] = "kehoach";
            ViewData["MenuLv2"] = "kehoach_thuthap";
            if (model != null)
            {
                _db.KeHoachThuThapThongTin.Remove(model);
                _db.SaveChanges();
                return RedirectToAction("Index", "KeHoachThuThap", new { madiaban = model.MaDiaBan });
            }
            else
            {
                ViewData["Messages"] = "Không thể chỉnh xóa thông tin!";
                return View("Views/Admin/Error/Page.cshtml");
            }
        }


        [HttpGet("KeHoachThuThap/Show")]
        public IActionResult Show(int Id)
        {
            var model = _db.KeHoachThuThapThongTin.FirstOrDefault(t => t.Id == Id);
            ViewData["MenuLv1"] = "kehoach";
            ViewData["MenuLv2"] = "kehoach_thuthap";

            if (model != null)
            {
                ViewData["TenDonViBaoCao"] = "ỦY BAN NHÂN DÂN";
                ViewData["NgayDuyetKeHoach"] = Helpers.ConvertDateToText(model.NgayKyKeHoach);
                ViewData["Title"] = "Kế hoạch thu thập thông tin lao động";
                return View("Views/Admin/Manages/KeHoachThuThapTT/DanhSach/Show.cshtml", model);
            }
            else
            {
                ViewData["Messages"] = "Không tìm thấy thông tin!";
                return View("Views/Admin/Error/Page.cshtml");
            }


        }


        [HttpPost("KeHoachThuThap/Chuyen")]
        public JsonResult Chuyen(int Id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var data = new { status = "error", message = "Phiên đăng nhập đã kết thúc! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }

            var model = _db.KeHoachThuThapThongTin.FirstOrDefault(t => t.Id == Id)!;
            if (model != null)
            {
                model.Status = "ChoDuyet";
                _db.KeHoachThuThapThongTin.Update(model);
                _db.SaveChanges();

                var data = new { status = "success", message = "Chuyển thông tin thành công" };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Chuyển thông tin không thành công" };
                return Json(data);
            }

        }



    }
}

