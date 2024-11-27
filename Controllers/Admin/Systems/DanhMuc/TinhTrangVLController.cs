using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;
namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class TinhTrangVLController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TinhTrangVLController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("TinhTrangVL")]
        public IActionResult Index()
        {
            var model = _db.TinhTrangVL;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaTinhTrangVL!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Tình trạng việc làm";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_tinhtrangvl";
            return View("Views/Admin/Systems/Danhmuc/TinhTrangVL/Index.cshtml", model);
        }

        [HttpPost("TinhTrangVL/Store")]
        public IActionResult Store(string TenTinhTrangVL_create, string MaTinhTrangVL_create, string TrangThai_create)
        {
            var model = new TinhTrangVL
            {
                TenTinhTrangVL = TenTinhTrangVL_create,
                MaTinhTrangVL = MaTinhTrangVL_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.TinhTrangVL.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "TinhTrangVL");
        }

        [HttpPost("TinhTrangVL/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.TinhTrangVL.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaTinhTrangVL_edit' name='MaTinhTrangVL_edit' value='" + model.MaTinhTrangVL + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenTinhTrangVL_edit' name='TenTinhTrangVL_edit' value='" + model.TenTinhTrangVL + "' class='form-control'/>";
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

        [HttpPost("TinhTrangVL/Update")]
        public IActionResult Update(string TenTinhTrangVL_edit, string MaTinhTrangVL_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.TinhTrangVL.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaTinhTrangVL = MaTinhTrangVL_edit;
                model.TenTinhTrangVL = TenTinhTrangVL_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.TinhTrangVL.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "TinhTrangVL");
        }

        [HttpPost("TinhTrangVL/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.TinhTrangVL.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.TinhTrangVL.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "TinhTrangVL");
        }
    }
}
