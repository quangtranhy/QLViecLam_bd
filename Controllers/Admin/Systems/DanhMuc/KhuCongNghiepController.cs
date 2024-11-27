using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;
namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class KhuCongNghiepController : Controller
    {
        private readonly ApplicationDbContext _db;

        public KhuCongNghiepController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("KhuCongNghiep")]
        public IActionResult Index()
        {
            var model = _db.KhuCongNghiep;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaKhuCongNghiep!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Khu công nghiệp";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_khucongnghiep";
            return View("Views/Admin/Systems/Danhmuc/KhuCongNghiep/Index.cshtml", model);
        }

        [HttpPost("KhuCongNghiep/Store")]
        public IActionResult Store(string TenKhuCongNghiep_create, string MaKhuCongNghiep_create, string TrangThai_create)
        {
            var model = new KhuCongNghiep
            {
                TenKhuCongNghiep = TenKhuCongNghiep_create,
                MaKhuCongNghiep = MaKhuCongNghiep_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.KhuCongNghiep.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "KhuCongNghiep");
        }

        [HttpPost("KhuCongNghiep/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.KhuCongNghiep.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaKhuCongNghiep_edit' name='MaKhuCongNghiep_edit' value='" + model.MaKhuCongNghiep + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenKhuCongNghiep_edit' name='TenKhuCongNghiep_edit' value='" + model.TenKhuCongNghiep + "' class='form-control'/>";
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

        [HttpPost("KhuCongNghiep/Update")]
        public IActionResult Update(string TenKhuCongNghiep_edit, string MaKhuCongNghiep_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.KhuCongNghiep.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaKhuCongNghiep = MaKhuCongNghiep_edit;
                model.TenKhuCongNghiep = TenKhuCongNghiep_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.KhuCongNghiep.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "KhuCongNghiep");
        }

        [HttpPost("KhuCongNghiep/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.KhuCongNghiep.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.KhuCongNghiep.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "KhuCongNghiep");
        }
    }
}
