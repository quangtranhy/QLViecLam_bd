﻿using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Manages;

namespace QLViecLam.Controllers.Admin.Manages.TongHop_PhanTich_DuDoan.ThongTinCung_Cau.TTRuiRo_DieuKien_PhuongTien
{
    public class PhuongTienLamViecController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PhuongTienLamViecController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("PhuongTienLamViec")]
        [HttpGet]
        public IActionResult Index()
        {
            var model = _db.PhuongTienLamViec;
            var lastRecord = _db.PhuongTienLamViec.OrderByDescending(e => e.Id).FirstOrDefault();

            ViewData["MenuLv1"] = "menu_capnhatcungcau";
            ViewData["MenuLv2"] = "menu_capnhatcungcau_ruiro_phuongtien_dieukien";
            ViewData["MenuLv3"] = "menu_capnhatcungcau_phuongtien";
            return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/TTRuiRoPhuongTienDieuKien/PhuongTienLamViec/Index.cshtml", model);
        }
        [Route("PhuongTienLamViec/Store")]
        [HttpPost]
        public IActionResult Store(string tenphuongtien_create, string capdo_create, string ghichu_create)
        {
            var model = new PhuongTienLamViec
            {
                TenPhuongTien = tenphuongtien_create,
                CapDo = capdo_create,
                GhiChu = ghichu_create,
                MaPhuongTien = DateTime.Now.ToString("yyMMddssmmHH"),
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.PhuongTienLamViec.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "PhuongTienLamViec");
        }
        [Route("PhuongTienLamViec/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {
            var model = _db.PhuongTienLamViec.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên chế độ chính sách</b></label>";
                result += "<input type='text' id='tenphuongtien_edit' name='tenphuongtien_edit' value='" + model.TenPhuongTien + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Cấp độ</b></label>";
                result += "<input type='text' id='capdo_edit' name='capdo_edit' value='" + model.CapDo + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Ghi chú</b></label>";
                result += "<textarea type='text' id='ghichu_edit' name='ghichu_edit' cols='12' rows='3' class='form-control'>" + model.GhiChu + "</textarea>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }
        [Route("PhuongTienLamViec/Update")]
        [HttpPost]
        public IActionResult Update(string tenphuongtien_edit, string capdo_edit, string ghichu_edit, int id_edit)
        {
            var model = _db.PhuongTienLamViec.FirstOrDefault(t => t.Id == id_edit);
            if (model != null)
            {
                model.TenPhuongTien = tenphuongtien_edit;
                model.CapDo = capdo_edit;
                model.GhiChu = ghichu_edit;
                model.Updated_at = DateTime.Now;
                _db.PhuongTienLamViec.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "PhuongTienLamViec");
        }
        [Route("PhuongTienLamViec/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            var model = _db.PhuongTienLamViec.FirstOrDefault(t => t.Id == id_delete);
            if (model != null)
            {
                _db.PhuongTienLamViec.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "PhuongTienLamViec");
        }
    }
}
