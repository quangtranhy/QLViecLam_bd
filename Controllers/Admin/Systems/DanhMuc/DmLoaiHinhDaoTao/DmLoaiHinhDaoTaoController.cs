﻿using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;

namespace QLViecLam.Controllers.Admin.Systems.DanhMuc.DmLoaiHinhDaoTao
{
    public class DmLoaiHinhDaoTaoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DmLoaiHinhDaoTaoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("DmLoaiHinhDaoTao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.DmLoaiHinhDaoTao.ToList();
                    ViewData["MenuLv1"] = "menu_quanlydanhmucdulieu";
                    ViewData["MenuLv2"] = "menu_quanlydanhmuc_loaihinhdt";
                    return View("Views/Admin/Systems/Danhmuc/DmLoaiHinhDaoTao/Index.cshtml", model);
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

        }

        [Route("DmLoaiHinhDaoTao/Store")]
        [HttpPost]
        public IActionResult Store(Models.Admin.Systems.DanhMuc.DmLoaiHinhDaoTao request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = new Models.Admin.Systems.DanhMuc.DmLoaiHinhDaoTao
                    {
                        MaLoaiHinhDaoTao = request.MaLoaiHinhDaoTao,
                        TenLoaiHinhDaoTao = request.TenLoaiHinhDaoTao,
                        MoTa = request.MoTa,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.DmLoaiHinhDaoTao.Add(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DmLoaiHinhDaoTao");
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

        }

        [Route("DmLoaiHinhDaoTao/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.DmLoaiHinhDaoTao.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã loại hình đào tạo</b></label>";
                result += "<input type='text' id='MaLoaiHinhDaoTao_Edit' name='MaLoaiHinhDaoTao' value='" + model.MaLoaiHinhDaoTao + "' class='form-control'/>";
                result += "</div>";

                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên loại hình đào tạo</b></label>";
                result += "<input type='text' id='TenLoaiHinhDaoTao_Edit' name='TenLoaiHinhDaoTao' value='" + model.TenLoaiHinhDaoTao + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mô tả</b></label>";
                result += "<textarea type='text' id='MoTa' name='MoTa' cols='12' rows='3' class='form-control'>" + model.MoTa + "</textarea>";
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

        [Route("DmLoaiHinhDaoTao/Update")]
        [HttpPost]
        public IActionResult Update(Models.Admin.Systems.DanhMuc.DmLoaiHinhDaoTao request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.DmLoaiHinhDaoTao.FirstOrDefault(t => t.Id == request.Id);
                    if (model != null)
                    {
                        model.MaLoaiHinhDaoTao = request.MaLoaiHinhDaoTao;
                        model.TenLoaiHinhDaoTao = request.TenLoaiHinhDaoTao;
                        model.MoTa = request.MoTa;
                        model.Updated_at = DateTime.Now;
                        _db.DmLoaiHinhDaoTao.Update(model);
                        _db.SaveChanges();
                    }

                    return RedirectToAction("Index", "DmLoaiHinhDaoTao");
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

        }

        [Route("DmLoaiHinhDaoTao/Delete")]
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.DmLoaiHinhDaoTao.FirstOrDefault(t => t.Id == Id);
                    if (model != null)
                    {
                        _db.DmLoaiHinhDaoTao.Remove(model);
                        _db.SaveChanges();
                    }
                    return RedirectToAction("Index", "DmLoaiHinhDaoTao");
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

        }
    }
}
