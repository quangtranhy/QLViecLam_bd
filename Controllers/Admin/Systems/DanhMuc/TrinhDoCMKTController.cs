using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;

namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class TrinhDoCMKTController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TrinhDoCMKTController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("TrinhDoCMKT")]
        public IActionResult Index()
        {
            var model = _db.TrinhDoCMKT;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaTrinhDoCMKT!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Trình độ chuyên môn kỹ thuật";
            ViewData["Title"] = "Ngành";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_trinhdocmkt";
            return View("Views/Admin/Systems/Danhmuc/TrinhDoCMKT/Index.cshtml", model);
        }

        [HttpPost("TrinhDoCMKT/Store")]
        public IActionResult Store(string TenTrinhDoCMKT_create, string MaTrinhDoCMKT_create, string TrangThai_create)
        {
            var model = new TrinhDoCMKT
            {
                TenTrinhDoCMKT = TenTrinhDoCMKT_create,
                MaTrinhDoCMKT = MaTrinhDoCMKT_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.TrinhDoCMKT.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "TrinhDoCMKT");
        }

        [HttpPost("TrinhDoCMKT/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.TrinhDoCMKT.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaTrinhDoCMKT_edit' name='MaTrinhDoCMKT_edit' value='" + model.MaTrinhDoCMKT + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenTrinhDoCMKT_edit' name='TenTrinhDoCMKT_edit' value='" + model.TenTrinhDoCMKT + "' class='form-control'/>";
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

        [HttpPost("TrinhDoCMKT/Update")]
        public IActionResult Update(string TenTrinhDoCMKT_edit, string MaTrinhDoCMKT_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.TrinhDoCMKT.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaTrinhDoCMKT = MaTrinhDoCMKT_edit;
                model.TenTrinhDoCMKT = TenTrinhDoCMKT_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.TrinhDoCMKT.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "TrinhDoCMKT");
        }

        [HttpPost("TrinhDoCMKT/Delete")]
        public IActionResult Delete(int id_delete)
        {
            var model = _db.TrinhDoCMKT.FirstOrDefault(t => t.Id == id_delete);
            if (model != null)
            {
                _db.TrinhDoCMKT.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "TrinhDoCMKT");
        }
    }
}
