using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;
namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class NganhHocController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NganhHocController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("NganhHoc")]
        public IActionResult Index()
        {
            var model = _db.NganhHoc;
      
            ViewData["Title"] = "Ngành học";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_nganhhoc";
            return View("Views/Admin/Systems/Danhmuc/NganhHoc/Index.cshtml", model);
        }

        [HttpPost("NganhHoc/Store")]
        public IActionResult Store(string TenNganhHoc_create, string MaNganhHoc_create, string TrangThai_create)
        {
            var model = new NganhHoc
            {
                TenNganhHoc = TenNganhHoc_create,
                MaNganhHoc = MaNganhHoc_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.NganhHoc.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "NganhHoc");
        }

        [HttpPost("NganhHoc/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.NganhHoc.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='text' id='MaNganhHoc_edit' name='MaNganhHoc_edit' value='" + model.MaNganhHoc + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenNganhHoc_edit' name='TenNganhHoc_edit' value='" + model.TenNganhHoc + "' class='form-control'/>";
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

        [HttpPost("NganhHoc/Update")]
        public IActionResult Update(string TenNganhHoc_edit, string MaNganhHoc_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.NganhHoc.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaNganhHoc = MaNganhHoc_edit;
                model.TenNganhHoc = TenNganhHoc_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.NganhHoc.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "NganhHoc");
        }

        [HttpPost("NganhHoc/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.NganhHoc.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.NganhHoc.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "NganhHoc");
        }
    }
}
