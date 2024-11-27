using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;
namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class HinhThucDoanhNghiepController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HinhThucDoanhNghiepController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("HinhThucDoanhNghiep")]
        public IActionResult Index()
        {
            var model = _db.HinhThucDoanhNghiep;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaHinhThucDoanhNghiep!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Hình thức doanh nghiệp";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_hinhthucdoanhnghiep";
            return View("Views/Admin/Systems/Danhmuc/HinhThucDoanhNghiep/Index.cshtml", model);
        }

        [HttpPost("HinhThucDoanhNghiep/Store")]
        public IActionResult Store(string TenHinhThucDoanhNghiep_create, string MaHinhThucDoanhNghiep_create, string TrangThai_create)
        {
            var model = new HinhThucDoanhNghiep
            {
                TenHinhThucDoanhNghiep = TenHinhThucDoanhNghiep_create,
                MaHinhThucDoanhNghiep = MaHinhThucDoanhNghiep_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.HinhThucDoanhNghiep.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "HinhThucDoanhNghiep");
        }

        [HttpPost("HinhThucDoanhNghiep/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.HinhThucDoanhNghiep.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaHinhThucDoanhNghiep_edit' name='MaHinhThucDoanhNghiep_edit' value='" + model.MaHinhThucDoanhNghiep + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenHinhThucDoanhNghiep_edit' name='TenHinhThucDoanhNghiep_edit' value='" + model.TenHinhThucDoanhNghiep + "' class='form-control'/>";
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

        [HttpPost("HinhThucDoanhNghiep/Update")]
        public IActionResult Update(string TenHinhThucDoanhNghiep_edit, string MaHinhThucDoanhNghiep_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.HinhThucDoanhNghiep.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaHinhThucDoanhNghiep = MaHinhThucDoanhNghiep_edit;
                model.TenHinhThucDoanhNghiep = TenHinhThucDoanhNghiep_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.HinhThucDoanhNghiep.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "HinhThucDoanhNghiep");
        }

        [HttpPost("HinhThucDoanhNghiep/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.HinhThucDoanhNghiep.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.HinhThucDoanhNghiep.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "HinhThucDoanhNghiep");
        }
    }
}
