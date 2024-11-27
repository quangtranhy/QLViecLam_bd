using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QLViecLam.Controllers.Admin.Manages.KeHoachThuThapTT
{
    public class XetDuyetKeHoachController : Controller
    {
        private readonly ApplicationDbContext _db;

        public XetDuyetKeHoachController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("XetDuyetKeHoach")]
        public IActionResult Index(string kydieutra, string status)
        {
            var model = _db.KeHoachThuThapThongTin.AsQueryable();
            var dmhanhchinh = _db.DmHanhChinh;
            var DsKyDieuTra = model.Select(k => k.KeHoach).Distinct().OrderByDescending(k => k).ToList();
            kydieutra = string.IsNullOrEmpty(kydieutra) ? DsKyDieuTra.FirstOrDefault()! : kydieutra;

            model = model.Where(t => t.KeHoach == kydieutra);
            if (status != null)
            {
                model = model.Where(t => t.Status == status);
            }

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

            ViewData["DsKyDieuTra"] = DsKyDieuTra;
            ViewData["kydieutra"] = kydieutra;
            ViewData["Status"] = status;
            ViewData["Title"] = "Xét duyệt kế hoạch thu thập thông tin";
            ViewData["MenuLv1"] = "kehoach";
            ViewData["MenuLv2"] = "kehoach_xetduyet";
            return View("Views/Admin/Manages/KeHoachThuThapTT/XetDuyet/Index.cshtml", newmodel);
        }



        [HttpPost("XetDuyetKeHoach/TraLai")]
        public JsonResult TraLai(int Id, string LyDoTraLai)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var data = new { status = "error", message = "Phiên đăng nhập đã kết thúc! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }

            var model = _db.KeHoachThuThapThongTin.FirstOrDefault(t => t.Id == Id)!;
            if (model != null)
            {
                model.Status = "BiTraLai";
                model.LyDoTraLai = LyDoTraLai;
                _db.KeHoachThuThapThongTin.Update(model);
                _db.SaveChanges();
                var data = new { status = "success", message = "Trả lại thành công" };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Trả lại thất bại" };
                return Json(data);
            }

        }



        [HttpPost("XetDuyetKeHoach/Duyet")]
        public JsonResult Duyet(int Id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var data = new { status = "error", message = "Phiên đăng nhập đã kết thúc! Đăng nhập lại để tiếp tục công việc" };
            }

            var model = _db.KeHoachThuThapThongTin.FirstOrDefault(t => t.Id == Id)!;
            if (model != null)
            {
                model.Status = "DaDuyet";
                //model.SoKeHoach = SoKeHoach;
                model.NgayKyKeHoach = DateTime.Now;
                _db.KeHoachThuThapThongTin.Update(model);
                _db.SaveChanges();
                var data = new { status = "success", message = "Duyệt thành công" };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Duyệt thất bại" };
                return Json(data);
            }

        }
        

        [HttpPost("XetDuyetKeHoach/HuyDuyet")]
        public JsonResult HuyDuyet(int Id)
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
                var data = new { status = "success", message = "Hủy duyệt thành công" };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Hủy duyệt thất bại" };
                return Json(data);
            }

        }
    }
}

