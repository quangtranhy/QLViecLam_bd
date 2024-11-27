using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using System.Collections.Generic;
using Aspose.Words;
using Aspose.Words.Saving;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.Models.Admin.Systems.HeThongChung;

namespace QLViecLam.Controllers.Admin.Manages.BanTin
{
    public class BanTinController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BanTinController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("BanTin")]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "BanTin", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
            var model = _db.BanTin;
            return View("Views/Admin/Manages/BanTin/Show.cshtml", model
                
                );
        }

        [HttpGet("BanTin/Create")]
        public IActionResult Create()
        {

            return View("Views/Admin/Manages/BanTin/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Store(string TieuDe, string editorContent)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".docx";
            byte[] byteArray = Encoding.UTF8.GetBytes(editorContent.Contains("<html>") ? editorContent : "<html>" + editorContent + "</html>");
            MemoryStream stream = new MemoryStream(byteArray);
            Document doc = new Document(stream);

            var outputStream = new MemoryStream();
            doc.Save(outputStream, SaveFormat.Docx);
            outputStream.Position = 0;

            // Lưu tệp vào thư mục
            var path = Path.Combine("wwwroot/Uploads", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                outputStream.CopyTo(fileStream);
            }

            var BanTin = new QLViecLam.Models.Admin.Manages.BanTin
            {
                TieuDe = TieuDe,
                NoiDung = path,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.BanTin.Add(BanTin);
            _db.SaveChanges();

            TempData["Message"] = "Thêm mới thành công!";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "BanTin");
        }

  

        [HttpPost]
        public IActionResult Update(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var path = Path.Combine("wwwroot/Uploads", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Lưu thông tin tệp vào cơ sở dữ liệu
                var BanTin = new QLViecLam.Models.Admin.Manages.BanTin
                {
                    TieuDe = file.FileName,
                    NoiDung = path
                };
                _db.BanTin.Add(BanTin);
                _db.SaveChanges();

                Document doc = new Document(path);
                var outStream = new MemoryStream();
                HtmlSaveOptions options = new HtmlSaveOptions
                {
                    ExportImagesAsBase64 = true,
                    ExportFontsAsBase64 = true
                };
                doc.Save(outStream, options);

                outStream.Position = 0;
                using (StreamReader reader = new StreamReader(outStream))
                {
                    ViewBag.HtmlContent = reader.ReadToEnd();
                }

                ViewBag.FileName = file.FileName;
            }

            TempData["Message"] = "Thêm mới thành công!";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "BanTin");
        }

        public IActionResult DownloadFile(string fileName)
        {
            var path = Path.Combine("wwwroot/uploads", fileName);
            var fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        [HttpGet]
        public IActionResult Show(int id)
        {
            var BanTin = _db.BanTin.Find(id);
            if (BanTin == null)
            {
                return NotFound();
            }

            var path = BanTin.NoiDung;
            Document doc = new Document(path);
            var outStream = new MemoryStream();
            HtmlSaveOptions options = new HtmlSaveOptions
            {
                ExportImagesAsBase64 = true,
                ExportFontsAsBase64 = true
            };
            doc.Save(outStream, options);

            outStream.Position = 0;
            using (StreamReader reader = new StreamReader(outStream))
            {
                ViewBag.HtmlContent = reader.ReadToEnd();
            }

            ViewBag.FileName = BanTin.TieuDe;
            return View("Views/Admin/Manages/BanTin/Show2.cshtml");
        }

 
    }



}



