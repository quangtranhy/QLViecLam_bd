using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;

namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class DanTocController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DanTocController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("DanToc")]
        public IActionResult Index()
        {
            var model = _db.DanToc;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaDanToc!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Dân tộc";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_dantoc";
            return View("Views/Admin/Systems/Danhmuc/DanToc/Index.cshtml", model);
        }

        [HttpPost("DanToc/Store")]
        public IActionResult Store(string TenDanToc_create, string MaDanToc_create, string TrangThai_create)
        {
            var model = new DanToc
            {
                TenDanToc = TenDanToc_create,
                MaDanToc = MaDanToc_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.DanToc.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "DanToc");
        }

        [HttpPost("DanToc/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.DanToc.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaDanToc_edit' name='MaDanToc_edit' value='" + model.MaDanToc + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenDanToc_edit' name='TenDanToc_edit' value='" + model.TenDanToc + "' class='form-control'/>";
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

        [HttpPost("DanToc/Update")]
        public IActionResult Update(string TenDanToc_edit, string MaDanToc_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.DanToc.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaDanToc = MaDanToc_edit;
                model.TenDanToc = TenDanToc_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.DanToc.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "DanToc");
        }

        [HttpPost("DanToc/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.DanToc.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.DanToc.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "DanToc");
        }
    }
}
