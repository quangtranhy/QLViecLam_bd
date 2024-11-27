using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using QLViecLam.Models.Admin.Systems.DanhMuc;
using Azure.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using QLViecLam.Helper;
namespace QLViecLam.Controllers.Admin.Systems.HeThongChung.DiaBan
{
    public class DiaBanController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DiaBanController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("DiaBan")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "DiaBan", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DmHanhChinh;

            ViewData["DmHanhChinh"] = model;
            ViewData["dshuyen"] = model.Where(x => x.CapDo == "T" || x.CapDo == "H");
            ViewData["dsxa"] = model.Where(x => x.CapDo == "X");
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_diaban";
            return View("Views/Admin/Systems/HeThongChung/DiaBan/Index.cshtml", model);
        }

        [Route("DiaBan/Store")]
        [HttpPost]
        public IActionResult Store(DmHanhChinh request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "DiaBan", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var capdo = "";
            if (request.Level == "Tỉnh")
            {
                capdo = "T";
            }
            if (request.Level == "Huyện" || request.Level == "Thành phố" || request.Level == "Thị xã")
            {
                capdo = "H";
            }
            if (request.Level == "Xã" || request.Level == "Phường" || request.Level == "Thị trấn")
            {
                capdo = "X";
            }
            var model = new DmHanhChinh
            {
                Name = request.Name,
                MaDb = request.MaDb,
                Parent = request.Parent,
                Level = request.Level,
                CapDo = capdo,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };

            _db.DmHanhChinh.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "DiaBan");
        }
        [Route("DiaBan/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {

            var phanLoaiDb = DanhMucChung.phanLoaiDb();
            var DmHanhChinh = _db.DmHanhChinh;
            var model = DmHanhChinh.FirstOrDefault(p => p.Id == id);
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã địa bàn</b></label>";
                result += "<input type='number' id='Madb_edit' name='Madb_edit' value='" + model.MaDb + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-9'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên địa bàn</b></label>";
                result += "<input type='text' id='Name_edit' name='Name_edit' value='" + model.Name + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Phân loại</b></label>";
                result += "<select id='Level_edit' name='Level_edit' class='form-control'>";
                foreach (var item in phanLoaiDb)
                {
                    result += "<option " + (item == model.Level ? "selected" : "") + ">" + item + "</option>";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Trực thuộc khu vực</b></label>";
                result += "<select id='Parent_edit' name='Parent_edit' class='form-control'>";
                result += "<option value=''>Không chọn</option>";
                foreach (var hc in DmHanhChinh)
                {
                    result += "<option value='" + hc.MaDb + "' " + (hc.MaDb == model.Parent ? "selected" : "") + ">" + hc.Name + "</option>";
                }
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

        [Route("DiaBan/Update")]
        [HttpPost]
        public IActionResult Update(string Name_edit, string MaDb_edit, string Parent_edit, string Level_edit, int Id_edit)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "DiaBan", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }


            var model = _db.DmHanhChinh.FirstOrDefault(t => t.Id == Id_edit);
            var capdo = "";
            if (Level_edit == "Tỉnh")
            {
                capdo = "T";
            }
            if (Level_edit == "Huyện" || Level_edit == "Thành phố" || Level_edit == "Thị xã")
            {
                capdo = "H";
            }
            if (Level_edit == "Xã" || Level_edit == "Phường" || Level_edit == "Thị trấn")
            {
                capdo = "X";
            }

            if (model != null)
            {
                model.Name = Name_edit;
                model.MaDb = MaDb_edit;
                model.Parent = Parent_edit;
                model.Level = Level_edit;
                model.CapDo = capdo;
                model.Updated_at = DateTime.Now;
                _db.DmHanhChinh.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "ThayDoi");
        }

        [Route("DiaBan/Delete")]
        [HttpPost]
        public IActionResult Delete(int Id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "DiaBan", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DmHanhChinh.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.DmHanhChinh.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "ThayDoi");
        }
    }
}
