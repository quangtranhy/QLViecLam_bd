using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using System;
namespace QLViecLam.Controllers.Admin.Systems.HeThongChung.ChucNang
{
    public class ChucNangController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ChucNangController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("ChucNang")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "ChucNang", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var ChucNang = _db.ChucNang;
            var model = ChucNang;
            ViewData["model_c1"] = ChucNang.Where(x => x.CapDo == 1);
            ViewData["model_c2"] = ChucNang.Where(x => x.CapDo == 2);
            ViewData["model_c3"] = ChucNang.Where(x => x.CapDo == 3);
            ViewData["model_c4"] = ChucNang.Where(x => x.CapDo == 4);
            ViewData["model_c5"] = ChucNang.Where(x => x.CapDo == 5);
            ViewData["ChucNang"] = ChucNang.Where(x => x.TrangThai == "1" && x.CapDo < 5);
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_chucnang";
            return View("Views/Admin/Systems/HeThongChung/ChucNang/Index.cshtml",model);
        }



        [Route("ChucNang/Store")]
        [HttpPost]
        public IActionResult Store(Models.Admin.Systems.HeThongChung.ChucNang request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "ChucNang", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var madv = DateTime.Now.ToString("yyyyMMddHHmmss");

            var model = new Models.Admin.Systems.HeThongChung.ChucNang
            {
                MaChucNang = request.MaChucNang,
                TenChucNang = request.TenChucNang,
                CapDo = request.CapDo,
                Parent = request.Parent,
                TrangThai = request.TrangThai,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.ChucNang.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "ChucNang");
        }

        [Route("ChucNang/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {

            var ChucNang = _db.ChucNang.Where(x => x.TrangThai == "1" && x.Id != id && x.CapDo < 5);
            var model = _db.ChucNang.FirstOrDefault(p => p.Id == id);
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã chức năng</b><span class='require' >*</span></label>";
                result += "<input type='text' id='machucnang' name='machucnang' value='" + model.MaChucNang + "' class='form-control' required />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-8'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên chức năng</b><span class='require' >*</span></label>";
                result += "<input type='text' id='tenchucnang' name='tenchucnang' value='" + model.TenChucNang + "' class='form-control' required />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Trực thuộc chức năng</b></label>";
                result += "<select type='text' id='parent_edit' name='parent' class='form-control'>";
                result += "<option value ='' data-edit-capdo='0'>---Không chọn(chức năng gốc)---</option>";
                foreach (var cn in ChucNang)
                {
                    result += "<option value='" + cn.MaChucNang + "' data-edit-capdo='" + cn.CapDo + "' ";
                    if (cn.MaChucNang == model.Parent)
                    {
                        result += "selected";
                    }
                    result += ">" + cn.TenChucNang + "(Cấp " + cn.CapDo + ")</option>";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Cấp độ chức năng</b></label>";
                result += "<input type='text' id='capdo_edit_dis' name='capdo_edit_dis' value='" + model.CapDo + "'class='form-control' disabled/>";
                result += "<input type='hidden' id='capdo_edit' name='capdo' value='" + model.CapDo + "' />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Trạng thái</b></label>";
                result += "<select type='text' id='trangthai' name='trangthai' class='form-control'>";
                result += "<option value='1'" + (model.TrangThai == "1" ? "selected" : "") + ">Kích hoạt</option>";
                result += "<option value='0'" + (model.TrangThai == "0" ? "selected" : "") + ">Vô hiệu</option>";
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<input hidden type='text' id='Id' name='Id' value='" + model.Id + "'/>";
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

        [Route("ChucNang/Update")]
        [HttpPost]
        public IActionResult Update(Models.Admin.Systems.HeThongChung.ChucNang request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "ChucNang", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.ChucNang.FirstOrDefault(t => t.Id == request.Id);

            if (model != null)
            {
                model.MaChucNang = request.MaChucNang;
                model.TenChucNang = request.TenChucNang;
                model.CapDo = request.CapDo;
                model.Parent = request.Parent;
                model.TrangThai = request.TrangThai;
                model.Updated_at = DateTime.Now;
                _db.ChucNang.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "ChucNang");
        }

        [Route("ChucNang/Delete")]
        [HttpPost]
        public IActionResult Delete(int Id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "ChucNang", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.ChucNang.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.ChucNang.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "ChucNang");
        }
















    }
}
