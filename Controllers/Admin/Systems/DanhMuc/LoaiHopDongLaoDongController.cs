﻿using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;
namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class LoaiHopDongLaoDongController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoaiHopDongLaoDongController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("LoaiHopDongLaoDong")]
        public IActionResult Index()
        {
            var model = _db.LoaiHopDongLaoDong;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaLoaiHopDongLaoDong!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Hợp đồng lao động";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_loaihopdonglaodong";
            return View("Views/Admin/Systems/Danhmuc/LoaiHopDongLaoDong/Index.cshtml", model);
        }

        [HttpPost("LoaiHopDongLaoDong/Store")]
        public IActionResult Store(string TenLoaiHopDongLaoDong_create, string MaLoaiHopDongLaoDong_create, string TrangThai_create)
        {
            var model = new LoaiHopDongLaoDong
            {
                TenLoaiHopDongLaoDong = TenLoaiHopDongLaoDong_create,
                MaLoaiHopDongLaoDong = MaLoaiHopDongLaoDong_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.LoaiHopDongLaoDong.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "LoaiHopDongLaoDong");
        }

        [HttpPost("LoaiHopDongLaoDong/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.LoaiHopDongLaoDong.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaLoaiHopDongLaoDong_edit' name='MaLoaiHopDongLaoDong_edit' value='" + model.MaLoaiHopDongLaoDong + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenLoaiHopDongLaoDong_edit' name='TenLoaiHopDongLaoDong_edit' value='" + model.TenLoaiHopDongLaoDong + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Trạng thái</b></label>";
                result += "<select id='TrangThai_edit' name='TrangThai_edit' class='form-control'>";
                result += "<option value='1' " + (model.TrangThai == "1" ? "selected" : "") + " >Kích hoạt</option>";
                result += "<option value='0' " + (model.TrangThai == "0" ? "selected" : "") + " >Vô hiệu</option>";
                result += "</select>";
                result += "</div>";
                result += "</div>";
                result += "<input hidden type='text' id='Id_edit' name='Id_edit' value='" + model.Id + "'/>";
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

        [HttpPost("LoaiHopDongLaoDong/Update")]
        public IActionResult Update(string TenLoaiHopDongLaoDong_edit, string MaLoaiHopDongLaoDong_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.LoaiHopDongLaoDong.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaLoaiHopDongLaoDong = MaLoaiHopDongLaoDong_edit;
                model.TenLoaiHopDongLaoDong = TenLoaiHopDongLaoDong_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.LoaiHopDongLaoDong.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "LoaiHopDongLaoDong");
        }

        [HttpPost("LoaiHopDongLaoDong/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.LoaiHopDongLaoDong.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.LoaiHopDongLaoDong.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "LoaiHopDongLaoDong");
        }
    }
}