using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;

namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class ThoiGianThatNghiepController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ThoiGianThatNghiepController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("ThoiGianThatNghiep")]
        public IActionResult Index()
        {
            var model = _db.ThoiGianThatNghiep;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaThoiGianThatNghiep!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Thời gian thất nghiệp";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_thoigianthatnghiep";
            return View("Views/Admin/Systems/Danhmuc/ThoiGianThatNghiep/Index.cshtml", model);
        }

        [HttpPost("ThoiGianThatNghiep/Store")]
        public IActionResult Store(string TenThoiGianThatNghiep_create, string MaThoiGianThatNghiep_create, string TrangThai_create)
        {
            var model = new ThoiGianThatNghiep
            {
                TenThoiGianThatNghiep = TenThoiGianThatNghiep_create,
                MaThoiGianThatNghiep = MaThoiGianThatNghiep_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.ThoiGianThatNghiep.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "ThoiGianThatNghiep");
        }

        [HttpPost("ThoiGianThatNghiep/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.ThoiGianThatNghiep.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaThoiGianThatNghiep_edit' name='MaThoiGianThatNghiep_edit' value='" + model.MaThoiGianThatNghiep + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenThoiGianThatNghiep_edit' name='TenThoiGianThatNghiep_edit' value='" + model.TenThoiGianThatNghiep + "' class='form-control'/>";
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

        [HttpPost("ThoiGianThatNghiep/Update")]
        public IActionResult Update(string TenThoiGianThatNghiep_edit, string MaThoiGianThatNghiep_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.ThoiGianThatNghiep.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaThoiGianThatNghiep = MaThoiGianThatNghiep_edit;
                model.TenThoiGianThatNghiep = TenThoiGianThatNghiep_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.ThoiGianThatNghiep.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "ThoiGianThatNghiep");
        }

        [HttpPost("ThoiGianThatNghiep/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.ThoiGianThatNghiep.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.ThoiGianThatNghiep.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "ThoiGianThatNghiep");
        }
    }
}
