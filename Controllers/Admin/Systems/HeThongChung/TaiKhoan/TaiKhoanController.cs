using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using System.Security.Cryptography;

namespace QLViecLam.Controllers.Admin.Systems.HeThongChung.TaiKhoan
{
    public class TaiKhoanController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TaiKhoanController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("TaiKhoan")]
        [HttpGet]
        public IActionResult Index(string phanloai, string madb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "TaiKhoan", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            ViewData["phanloai"] = phanloai;
            ViewData["madb"] = madb;
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_taikhoan";

            if (phanloai == "doanhnghiep")
            {
                var user = _db.Users.Where(x => x.PhanLoaiTk == "2");


                return View("Views/Admin/Systems/HeThongChung/TaiKhoan/DoanhNghiep/Index.cshtml",user);
            }
            else
            // phanloai == hanhchinh
            {
                var DmHanhChinh = _db.DmHanhChinh;
                var DmDonvi = _db.DmDonvi;

                var HcXa = DmHanhChinh.Where(x => x.CapDo == "X");
                var HcHuyen = DmHanhChinh.Where(x => x.CapDo == "T" || x.CapDo == "H");
                var dshuyen = DmHanhChinh.Where(x => x.CapDo == "T" || x.CapDo == "H");

                if (madb != null)
                {
                    var check = HcHuyen.Where(x => x.MaDb == madb).FirstOrDefault()!.CapDo;
                    if (check == "T")
                    {
                        HcHuyen = HcHuyen.Where(x => x.MaDb == madb);
                    }
                    if (check == "H")
                    {
                        HcHuyen = HcHuyen.Where(x => x.MaDb == madb || x.Parent == madb);
                    }

                }

                ViewData["DmDonvi"] = DmDonvi;
                ViewData["HcHuyen"] = HcHuyen;
                ViewData["HcXa"] = HcXa;
                ViewData["dshuyen"] = dshuyen;

                return View("Views/Admin/Systems/HeThongChung/TaiKhoan/HanhChinh/Index.cshtml");
            }

        }

        [Route("TaiKhoan/Detail")]
        [HttpGet]
        public IActionResult Detail(string madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "TaiKhoan", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var user = _db.Users.Where(x => x.PhanLoaiTk == "1").Where(x => x.MaDonVi == madv);
            var donvi = _db.DmDonvi.Where(x => x.MaDonVi == madv).FirstOrDefault()!;
            ViewData["tendv"] = donvi.TenDv;
            ViewData["madv"] = donvi.MaDonVi;
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_taikhoan";
            return View("Views/Admin/Systems/HeThongChung/TaiKhoan/HanhChinh/Detail.cshtml", user);
        }

        [Route("TaiKhoan/Create")]
        [HttpGet]
        public IActionResult Create(string madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "TaiKhoan", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var DsNhomTaiKhoan = _db.DsNhomTaiKhoan;
            var donvibaocao = _db.Users.Where(x => x.PhanLoaiTk == "1").Where(x => x.TongHop == true);
            //return Ok(donvibaocao);
            ViewData["DsNhomTaiKhoan"] = DsNhomTaiKhoan;
            ViewData["donvi"] = _db.DmDonvi.Where(x => x.MaDonVi == madv).FirstOrDefault();
            ViewData["donvibaocao"] = donvibaocao;
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_taikhoan";
            return View("Views/Admin/Systems/HeThongChung/TaiKhoan/HanhChinh/Create.cshtml");
        }

        [Route("TaiKhoan/Store")]
        [HttpPost]
        public IActionResult Store(Users request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "TaiKhoan", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            string md5_password = "";
            using (MD5 md5Hash = MD5.Create())
            {
                string change = Helpers.GetMd5Hash(md5Hash, request.Password!);
                md5_password = change;
            }


            var model = new Users
            {
                Name = request.Name,
                MaDonVi = request.MaDonVi,
                MaDvBc = request.MaDvBc,
                Username = request.Username,
                Password = md5_password,
                PhanLoaiTk = "1",
                CapDo = request.CapDo,
                Status = request.Status,
                MaNhomChucNang = request.MaNhomChucNang,
                Theme = "Dark",
                Menu = "Fixed",
                Content = "Max",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.Users.Add(model);
            _db.SaveChanges();

            //var chucnang = _db.ChucNang.Where(x=>x.TrangThai == "1");



            var madv = request.MaDonVi;
            return RedirectToAction("Detail", "TaiKhoan", new { madv });
        }


        [Route("TaiKhoan/Edit")]
        [HttpGet]
        public IActionResult Edit(string madv, int id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "TaiKhoan", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var user = _db.Users;
            var model = user.Where(x => x.Id == id).FirstOrDefault();
            var DsNhomTaiKhoan = _db.DsNhomTaiKhoan;

            var donvibaocao = user.Where(x => x.TongHop == true);
            ViewData["model"] = model;
            ViewData["DsNhomTaiKhoan"] = DsNhomTaiKhoan;
            ViewData["donvi"] = _db.DmDonvi.Where(x => x.MaDonVi == madv).FirstOrDefault();
            ViewData["donvibaocao"] = donvibaocao;
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_taikhoan";
            return View("Views/Admin/Systems/HeThongChung/TaiKhoan/HanhChinh/Edit.cshtml",model);
        }

        [Route("TaiKhoan/Update")]
        [HttpPost]
        public IActionResult Update(Users request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "TaiKhoan", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.Users.FirstOrDefault(x => x.Id == request.Id);
            if (model != null)
            {
                string md5_password = "";
                using (MD5 md5Hash = MD5.Create())
                {
                    string change = Helpers.GetMd5Hash(md5Hash, request.Password!);
                    md5_password = change;
                }
                model.Name = request.Name;
                model.MaDvBc = request.MaDvBc;
                model.Username = request.Username;
                model.Password = md5_password;
                model.CapDo = request.CapDo;
                model.Status = request.Status;
                model.MaNhomChucNang = request.MaNhomChucNang;
                model.Updated_at = DateTime.Now;

                _db.Users.Update(model);
                _db.SaveChanges();
            }
            var madv = request.MaDonVi;
            return RedirectToAction("Detail", "TaiKhoan", new { madv });
        }

        [Route("TaiKhoan/Delete")]
        [HttpPost]
        public IActionResult Delete(int Id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "TaiKhoan", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
            var model = _db.Users.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.Users.Remove(model);
                _db.SaveChanges();
            }
            var madv = model!.MaDonVi;
            return RedirectToAction("Detail", "TaiKhoan", new { madv });
        }

    }
}

