using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;
namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class DoiTuongUuTienController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DoiTuongUuTienController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("DoiTuongUuTien")]
        public IActionResult Index()
        {
            var model = _db.DoiTuongUuTien;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaDoiTuongUuTien!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Đối tượng ưu tiên";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_doituonguutien";
            return View("Views/Admin/Systems/Danhmuc/DoiTuongUuTien/Index.cshtml", model);
        }

        [HttpPost("DoiTuongUuTien/Store")]
        public IActionResult Store(string TenDoiTuongUuTien_create, string MaDoiTuongUuTien_create, string TrangThai_create)
        {
            var model = new DoiTuongUuTien
            {
                TenDoiTuongUuTien = TenDoiTuongUuTien_create,
                MaDoiTuongUuTien = MaDoiTuongUuTien_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.DoiTuongUuTien.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "DoiTuongUuTien");
        }

        [HttpPost("DoiTuongUuTien/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.DoiTuongUuTien.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaDoiTuongUuTien_edit' name='MaDoiTuongUuTien_edit' value='" + model.MaDoiTuongUuTien + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenDoiTuongUuTien_edit' name='TenDoiTuongUuTien_edit' value='" + model.TenDoiTuongUuTien + "' class='form-control'/>";
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

        [HttpPost("DoiTuongUuTien/Update")]
        public IActionResult Update(string TenDoiTuongUuTien_edit, string MaDoiTuongUuTien_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.DoiTuongUuTien.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaDoiTuongUuTien = MaDoiTuongUuTien_edit;
                model.TenDoiTuongUuTien = TenDoiTuongUuTien_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.DoiTuongUuTien.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "DoiTuongUuTien");
        }

        [HttpPost("DoiTuongUuTien/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.DoiTuongUuTien.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.DoiTuongUuTien.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "DoiTuongUuTien");
        }
    }
}
