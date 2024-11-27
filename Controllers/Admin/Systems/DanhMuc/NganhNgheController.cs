using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;
namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class NganhNgheController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NganhNgheController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("NganhNghe")]
        public IActionResult Index()
        {
            var model = _db.NganhNghe;
          
            ViewData["Title"] = "Ngành nghề";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_nganhnghe";
            return View("Views/Admin/Systems/Danhmuc/NganhNghe/Index.cshtml", model);
        }

        [HttpPost("NganhNghe/Store")]
        public IActionResult Store(string TenNganhNghe_create, string MaNganhNghe_create, string TrangThai_create)
        {
            var model = new NganhNghe
            {
                TenNganhNghe = TenNganhNghe_create,
                MaNganhNghe = MaNganhNghe_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.NganhNghe.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "NganhNghe");
        }

        [HttpPost("NganhNghe/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.NganhNghe.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaNganhNghe_edit' name='MaNganhNghe_edit' value='" + model.MaNganhNghe + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenNganhNghe_edit' name='TenNganhNghe_edit' value='" + model.TenNganhNghe + "' class='form-control'/>";
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

        [HttpPost("NganhNghe/Update")]
        public IActionResult Update(string TenNganhNghe_edit, string MaNganhNghe_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.NganhNghe.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaNganhNghe = MaNganhNghe_edit;
                model.TenNganhNghe = TenNganhNghe_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.NganhNghe.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "NganhNghe");
        }

        [HttpPost("NganhNghe/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.NganhNghe.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.NganhNghe.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "NganhNghe");
        }
    }
}
