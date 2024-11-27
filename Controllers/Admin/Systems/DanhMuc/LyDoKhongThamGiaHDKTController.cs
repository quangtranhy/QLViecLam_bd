using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.DanhMuc;
namespace QLViecLam.Controllers.Admin.Systems.DanhMuc
{
    public class LyDoKhongThamGiaHDKTController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LyDoKhongThamGiaHDKTController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("LyDoKhongThamGiaHDKT")]
        public IActionResult Index()
        {
            var model = _db.LyDoKhongThamGiaHDKT;
            var stt = model.AsEnumerable().Select(e => int.Parse(e.MaLyDoKhongThamGia!)).DefaultIfEmpty(0).Max();

            ViewData["stt"] = stt != 0 ? stt + 1 : 1;
            ViewData["Title"] = "Lý do không tham gia hoạt động kinh tế";
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_danhmuc";
            ViewData["MenuLv3"] = "hethong_danhmuc_lydokhongthamgiahdkt";
            return View("Views/Admin/Systems/Danhmuc/LyDoKhongThamGiaHDKT/Index.cshtml", model);
        }

        [HttpPost("LyDoKhongThamGiaHDKT/Store")]
        public IActionResult Store(string TenLyDoKhongThamGia_create, string MaLyDoKhongThamGia_create, string TrangThai_create)
        {
            var model = new LyDoKhongThamGiaHDKT
            {
                TenLyDoKhongThamGia = TenLyDoKhongThamGia_create,
                MaLyDoKhongThamGia = MaLyDoKhongThamGia_create,
                TrangThai = TrangThai_create,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.LyDoKhongThamGiaHDKT.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "LyDoKhongThamGiaHDKT");
        }

        [HttpPost("LyDoKhongThamGiaHDKT/Edit")]
        public JsonResult Edit(int id)
        {
            var model = _db.LyDoKhongThamGiaHDKT.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã danh mục</b></label>";
                result += "<input type='number' id='MaLyDoKhongThamGia_edit' name='MaLyDoKhongThamGia_edit' value='" + model.MaLyDoKhongThamGia + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên danh mục</b></label>";
                result += "<input type='text' id='TenLyDoKhongThamGia_edit' name='TenLyDoKhongThamGia_edit' value='" + model.TenLyDoKhongThamGia + "' class='form-control'/>";
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

        [HttpPost("LyDoKhongThamGiaHDKT/Update")]
        public IActionResult Update(string TenLyDoKhongThamGia_edit, string MaLyDoKhongThamGia_edit, int Id_edit, string TrangThai_edit)
        {
            var model = _db.LyDoKhongThamGiaHDKT.FirstOrDefault(t => t.Id == Id_edit);
            if (model != null)
            {
                model.MaLyDoKhongThamGia = MaLyDoKhongThamGia_edit;
                model.TenLyDoKhongThamGia = TenLyDoKhongThamGia_edit;
                model.TrangThai = TrangThai_edit;
                model.Updated_at = DateTime.Now;
                _db.LyDoKhongThamGiaHDKT.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "LyDoKhongThamGiaHDKT");
        }

        [HttpPost("LyDoKhongThamGiaHDKT/Delete")]
        public IActionResult Delete(int Id_delete)
        {
            var model = _db.LyDoKhongThamGiaHDKT.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.LyDoKhongThamGiaHDKT.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "LyDoKhongThamGiaHDKT");
        }
    }
}
