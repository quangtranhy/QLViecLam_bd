using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;


namespace QLViecLam.Controllers.Admin.Danhmuc.TrinhDoHV
{
    public class TrinhDoHVController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TrinhDoHVController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("TrinhDoHV")]
        [HttpGet]
        public IActionResult Index()
        {
            var model = _db.TrinhDoHV;
            var lastRecord = _db.TrinhDoHV.OrderByDescending(e => e.MaTrinhDoHV).FirstOrDefault();
            ViewData["stt"] = lastRecord != null ? lastRecord.MaTrinhDoHV : 1;

            ViewData["MenuLv1"] = "menu_quanlydanhmucdulieu";
            ViewData["MenuLv2"] = "menu_quanlydanhmuc_trinhdohocvan";
            return View("Views/Admin/Manages/Danhmuc/TrinhDoHV/Index.cshtml", model);
        }
        [Route("TrinhDoHV/Store")]
        [HttpPost]
        public IActionResult Store(string TenTrinhDoHV_create, int MaTrinhDoHV_create , string TrangThai_create)
        {
            var model = new Models.Admin.Manages.DanhMuc.TrinhDoHV
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
        [Route("TrinhDoHV/Edit")]
        [HttpPost]
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
                result += "<option value='kh'>Kích hoạt</option>";
                result += "<option value='kkh'>Không kích hoạt</option>";
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
        [Route("TrinhDoHV/Update")]
        [HttpPost]
        public IActionResult Update(string TenTrinhDoHV_edit, int MaTrinhDoHV_edit, int Id_edit  ,string TrangThai_edit)
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
        [Route("TrinhDoHV/Delete")]
        [HttpPost]
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
