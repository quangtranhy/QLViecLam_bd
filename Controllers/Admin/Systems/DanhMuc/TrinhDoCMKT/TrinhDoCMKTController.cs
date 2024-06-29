using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;


namespace QLViecLam.Controllers.Admin.Systems.DanhMuc.TrinhDoCMKT
{
    public class TrinhDoCMKTController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TrinhDoCMKTController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("TrinhDoCMKT")]
        [HttpGet]
        public IActionResult Index()
        {
            var model = _db.TrinhDoCMKT;
            var lastRecord = _db.TrinhDoCMKT.OrderByDescending(e => e.MaTrinhDoCMKT).FirstOrDefault();
            ViewData["stt"] = lastRecord != null ? lastRecord.MaTrinhDoCMKT : 1;

            ViewData["MenuLv1"] = "danhmuc";
            ViewData["MenuLv2"] = "danhmuc_trinhdocmkt";
            return View("Views/Admin/Systems/Danhmuc/TrinhDoCMKT/Index.cshtml", model);
        }
        [Route("TrinhDoCMKT/Store")]
        [HttpPost]
        public IActionResult Store(string TenTrinhDoCMKT_create, int MaTrinhDoCMKT_create, string TrangThai_create)
        {
            var model = new Models.Admin.Systems.DanhMuc.TrinhDoCMKT
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
        [Route("TrinhDoCMKT/Edit")]
        [HttpPost]
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
                result += "<option value='kh' ";
                if (model.TrangThai == "kh")
                {
                    result += "selected";
                }
                result += ">Kích hoạt</option>";
                result += "<option value='kkh'";
                if (model.TrangThai == "kkh")
                {
                    result += "selected";
                }
                result += "> Không kích hoạt</option>";
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
        [Route("TrinhDoCMKT/Update")]
        [HttpPost]
        public IActionResult Update(string TenTrinhDoCMKT_edit, int MaTrinhDoCMKT_edit, int Id_edit, string TrangThai_edit)
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
        [Route("TrinhDoCMKT/Delete")]
        [HttpPost]
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
