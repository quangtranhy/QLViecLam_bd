﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using QLViecLam.Data;
using System.Security.Cryptography;
using QLViecLam.Helper;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLViecLam.Models.Admin.Systems.DanhMuc;
using Azure.Core;

namespace QLViecLam.Controllers.Admin.Systems.DanhMuc.NgheCongViec
{
    public class DmNgheCongViecController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DmNgheCongViecController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("NgheCongViec")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.DmNgheCongViec;
                    var lastRecord = _db.DmNgheCongViec.OrderByDescending(e => e.Id).FirstOrDefault();
                    ViewData["stt"] = lastRecord != null ? lastRecord.Stt : 1;
                    return View("Views/Admin/Systems/Danhmuc/DmNgheCongViec/Index.cshtml", model);
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
        [Route("NgheCongViec/Store")]
        [HttpPost]
        public IActionResult Store(string tendm_create, int stt_create)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = new DmNgheCongViec
                    {
                        TenDm = tendm_create,
                        Stt = stt_create.ToString(),
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.DmNgheCongViec.Add(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "NgheCongViec");
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
        [Route("NgheCongViec/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {
            var model = _db.DmNgheCongViec.FirstOrDefault(p => p.Id == id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Số thứ tự</b></label>";
                result += "<input type='text' id='stt_edit' name='stt_edit' value='" + model.Stt + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên nghề công việc</b></label>";
                result += "<input type='text' id='tendm_edit' name='tendm_edit' value='" + model.TenDm + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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
        [Route("NgheCongViec/Update")]
        [HttpPost]
        public IActionResult Update(string tendm_edit, int stt_edit, int id_edit)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.DmNgheCongViec.FirstOrDefault(t => t.Id == id_edit);
                    if (model != null)
                    {
                        model.TenDm = tendm_edit;
                        model.Stt = stt_edit.ToString();
                        model.Updated_at = DateTime.Now;
                        _db.DmNgheCongViec.Update(model);
                        _db.SaveChanges();
                    }

                    return RedirectToAction("Index", "NgheCongViec");
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
        [Route("NgheCongViec/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.DmNgheCongViec.FirstOrDefault(t => t.Id == id_delete);
                    if (model != null)
                    {
                        _db.DmNgheCongViec.Remove(model);
                        _db.SaveChanges();
                    }
                    return RedirectToAction("Index", "NgheCongViec");
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