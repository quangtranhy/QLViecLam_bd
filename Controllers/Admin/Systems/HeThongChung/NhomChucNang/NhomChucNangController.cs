using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using QLViecLam.ViewModels.Admin.Systems;
using System;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QLViecLam.Controllers.Admin.Systems.HeThongChung.NhomChucNang
{
    public class NhomChucNangController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhomChucNangController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("NhomChucNang")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DsNhomTaiKhoan;
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_nhomchucnang";
            return View("Views/Admin/Systems/HeThongChung/NhomChucNang/Index.cshtml", model);
        }

        [Route("NhomChucNang/Detail")]
        [HttpGet]
        public IActionResult Detail(string manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.Users.Where(x => x.MaNhomChucNang == manhom);
            ViewData["TenNhom"] = _db.DsNhomTaiKhoan.FirstOrDefault(x => x.MaNhomChucNang == manhom)!.TenNhomChucNang;

            return View("Views/Admin/Systems/HeThongChung/NhomChucNang/Detail.cshtml", model);
        }


        [Route("NhomChucNang/Store")]
        [HttpPost]
        public IActionResult Store(DsNhomTaiKhoan request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var manhom = DateTime.Now.ToString("yyyyMMddHHmmss");

            var model = new DsNhomTaiKhoan
            {
                MaNhomChucNang = manhom,
                TenNhomChucNang = request.TenNhomChucNang,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.DsNhomTaiKhoan.Add(model);
            _db.SaveChanges();

            var chucnang = _db.ChucNang.Where(x => x.TrangThai == "1");
            foreach (var cn in chucnang)
            {
                var newChucNang = new DsNhomTaiKhoan_PhanQuyen
                {
                    MaNhomChucNang = manhom,
                    MaChucNang = cn.MaChucNang,
                    PhanQuyen = true,
                    DanhSach = false,
                    ThayDoi = false,
                    HoanThanh = false,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                };
                _db.DsNhomTaiKhoan_PhanQuyen.Add(newChucNang);
            }
            _db.SaveChanges();


            return RedirectToAction("Index", "NhomChucNang");
        }

        [Route("NhomChucNang/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {

            var DsNhomTaiKhoan = _db.DsNhomTaiKhoan;
            var model = DsNhomTaiKhoan.FirstOrDefault(p => p.Id == id);
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên nhóm chức năng</b><span class='require' >*</span></label>";
                result += "<input type='text' id='tennhomchucnang' name='tennhomchucnang' value='" + model.TenNhomChucNang + "' class='form-control' required />";
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

        [Route("NhomChucNang/Update")]
        [HttpPost]
        public IActionResult Update(DsNhomTaiKhoan request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DsNhomTaiKhoan.FirstOrDefault(t => t.Id == request.Id);

            if (model != null)
            {
                model.TenNhomChucNang = request.TenNhomChucNang;
                model.Updated_at = DateTime.Now;
                _db.DsNhomTaiKhoan.Update(model);
                _db.SaveChanges();

            }

            return RedirectToAction("Index", "NhomChucNang");
        }

        [Route("NhomChucNang/Delete")]
        [HttpPost]
        public IActionResult Delete(int Id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DsNhomTaiKhoan.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.DsNhomTaiKhoan.Remove(model);
                _db.SaveChanges();
                var dsNhom = _db.DsNhomTaiKhoan_PhanQuyen.Where(x => x.MaNhomChucNang == model.MaNhomChucNang);
                if (dsNhom.Any())
                {
                    foreach (var item in dsNhom)
                    {
                        _db.DsNhomTaiKhoan_PhanQuyen.Remove(item);
                    }
                    _db.SaveChanges();
                }
            }

            return RedirectToAction("Index", "NhomChucNang");
        }


        [Route("NhomChucNang/Setting")]
        [HttpGet]
        public IActionResult Setting(string manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }


            var model = _db.DsNhomTaiKhoan_PhanQuyen.Where(x => x.MaNhomChucNang == manhom);

            var ChucNang = _db.ChucNang;
            var newModel = (from m in model
                            join cn in ChucNang on m.MaChucNang equals cn.MaChucNang
                            select new VM_NhomTkpq_ChucNang
                            {
                                Id = m.Id,
                                TenChucNang = cn.TenChucNang,
                                MaChucNang = m.MaChucNang,
                                DanhSach = m.DanhSach,
                                ThayDoi = m.ThayDoi,
                                HoanThanh = m.HoanThanh,
                            });

            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_nhomchucnang";
            return View("Views/Admin/Systems/HeThongChung/NhomChucNang/Setting.cshtml", newModel);
        }

        [Route("NhomChucNang/EditPhanQuyen")]
        [HttpPost]
        public JsonResult EditPhanQuyen(int id)
        {

            var model = _db.DsNhomTaiKhoan_PhanQuyen.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                var TenChucNang = _db.ChucNang.FirstOrDefault(x => x.MaChucNang == model.MaChucNang)!.TenChucNang;
                string result = "<div class='modal-body' id='edit_thongtin'>";


                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên nhóm chức năng</b></label>";
                result += "<input type = text' value='" + TenChucNang + "' class='form-control' disabled />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label class='col-xl-12 col-form-label'>Phân quyền chức năng:</label>";
                result += "<div class='col-xl-12 col-form-label'>";
                result += "<div class='checkbox-inline'>";
                result += "<label class='checkbox checkbox-outline checkbox-success'>";
                result += "<input type='checkbox' name='danhsach' " + (model.DanhSach == true ? "checked" : "") + "  >";
                result += "<span></span>Danh sách";
                result += "</label>";
                result += "<label class='checkbox checkbox-outline checkbox-success' style='margin-left:5%'>";
                result += "<input type='checkbox' name='thaydoi'" + (model.ThayDoi == true ? "checked" : "") + " >";
                result += "<span></span>Thay đổi";
                result += "</label>";
                result += "<label class='checkbox checkbox-outline checkbox-success' style='margin-left:5% '>";
                result += "<input type = 'checkbox' name='hoanthanh'" + (model.HoanThanh == true ? "checked" : "") + " >";
                result += "<span></span>Hoàn thành";
                result += "</label>";
                result += "</div>";
                result += "<span class='form-text text-muted'>";
                result += "\"Danh sách\" mặc định được chọn khi chọn \"Thay đổi\" hoặc \"Hoàn thành\",<br />";
                result += "Nếu bỏ chọn \"Danh sách\" thì \"Thay đổi\" và \"Hoàn thành\" sẽ tự động bỏ chọn";
                result += "</span>";
                result += "</div>";
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

        [Route("NhomChucNang/UpdatePhanQuyen")]
        [HttpPost]
        public IActionResult UpdatePhanQuyen(int id ,string danhsach, string thaydoi,string hoanthanh)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NhomChucNang", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DsNhomTaiKhoan_PhanQuyen.FirstOrDefault(t => t.Id == id);

            if (model != null)
            {
                model.DanhSach = danhsach == "on" ? true : false;
                model.ThayDoi = thaydoi == "on" ? true : false;
                model.HoanThanh = hoanthanh == "on" ? true : false;
                model.Updated_at = DateTime.Now;
                _db.DsNhomTaiKhoan_PhanQuyen.Update(model);
                _db.SaveChanges();
            }
            var manhom = model!.MaNhomChucNang;
            return RedirectToAction("Setting", "NhomChucNang",new{ manhom });
        }
    }
}
