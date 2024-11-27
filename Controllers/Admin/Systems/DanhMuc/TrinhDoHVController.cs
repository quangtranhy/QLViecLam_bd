using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;

namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class TrinhDoHVController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TrinhDoHVController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("TrinhDoHV")]
        public IActionResult Index()
        {
            var model = _db.TrinhDoHV;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaTrinhDoHV!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Trình độ học vấn";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_trinhdohv";
            return View("Views/Admin/Systems/Danhmuc/TrinhDoHV/Index.cshtml", model);
        }

        [HttpPost("TrinhDoHV/Store")]
        public IActionResult Store(string TenTrinhDoHV_create, string MaTrinhDoHV_create, string TrangThai_create)
        {
            var model = new TrinhDoHV
            {
                TenTrinhDoHV = TenTrinhDoHV_create,
                MaTrinhDoHV = MaTrinhDoHV_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.TrinhDoHV.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "TrinhDoHV");
        }

        [HttpPost("TrinhDoHV/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.TrinhDoHV.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaTrinhDoHV_edit' name='MaTrinhDoHV_edit' value='" + model.MaTrinhDoHV + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenTrinhDoHV_edit' name='TenTrinhDoHV_edit' value='" + model.TenTrinhDoHV + "' class='form-control'/>";
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

        [HttpPost("TrinhDoHV/Update")]
        public IActionResult Update(string TenTrinhDoHV_edit, string MaTrinhDoHV_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.TrinhDoHV.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaTrinhDoHV = MaTrinhDoHV_edit;
                model.TenTrinhDoHV = TenTrinhDoHV_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.TrinhDoHV.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "TrinhDoHV");
        }

        [HttpPost("TrinhDoHV/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.TrinhDoHV.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.TrinhDoHV.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "TrinhDoHV");
        }
    }
}
