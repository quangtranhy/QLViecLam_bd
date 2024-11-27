using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.Models.Admin.Systems.HeThongChung;

namespace QLViecLam.Controllers.Admin.Manages.DoanhNghiep
{
    public class NguoiLaoDongController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NguoiLaoDongController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("NguoiLaoDong")]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NguoiLaoDong", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var nguoilaodong = _db.NguoiLaoDong.Where(x => x.Nation == "VN");
            var company = _db.Company;
            var model = (from m in nguoilaodong
                         join c in company on m.MaDn equals c.MaSoDn into details
                         from c_de in details.DefaultIfEmpty()
                         select new NguoiLaoDong
                         {
                             Id = m.Id,
                             HoVaTen = m.HoVaTen,
                             TenDn = c_de.Name,
                             NgayThangNamSinh = m.NgayThangNamSinh,
                             Tinh = m.Tinh,
                             SoCCCD = m.SoCCCD,
                         });
         
            ViewData["MenuLv1"] = "doanhnghiep";
            ViewData["MenuLv2"] = "doanhnghiep_nguoilaodong";
            return View("Views/Admin/Manages/DoanhNghiep/NguoiLaoDong/Index.cshtml", model);
        }


        [HttpGet("NguoiLaoDong/Create")]
        public IActionResult Create()
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NguoiLaoDong", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "1");
            ViewData["TrinhDoHV"] = _db.TrinhDoHV.Where(x => x.TrangThai == "1");
            ViewData["NganhNghe"] = _db.NganhNghe.Where(x => x.TrangThai == "1");
            ViewData["LoaiHopDongLaoDong"] = _db.LoaiHopDongLaoDong;
            ViewData["QuocGia"] = _db.QuocGia.Where(x => x.TrangThai == "1");
            ViewData["MenuLv1"] = "doanhnghiep";
            ViewData["MenuLv2"] = "doanhnghiep_nguoilaodong";
            return View("Views/Admin/Manages/DoanhNghiep/NguoiLaoDong/Create.cshtml");
        }


        [HttpPost("NguoiLaoDong/Store")]
        public IActionResult Store(NguoiLaoDong request)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NguoiLaoDong", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var input = request;
            _db.NguoiLaoDong.Add(input);
            var user = GetUser();
            var model = new BienDong()
            {
                LoaiBang = "nguoilaodong",
                PhanLoai = "baotang",
                User = user,
                SoLuong = 1,
                MoTa = "Thêm:" + request.HoVaTen,
                ThoiDiem = DateTime.Now,
            };
            _db.BienDong.Add(model);
            _db.SaveChanges();

            TempData["Message"] = "Thêm mới hồ sơ thành công!";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "NguoiLaoDong");
        }

        [HttpGet("NguoiLaoDong/Edit")]
        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NguoiLaoDong", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
            var model = _db.NguoiLaoDong.FirstOrDefault(x => x.Id == Id);
            if (model == null)
            {
                TempData["Message"] = "không tìm thấy thông tin hồ sơ!";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index", "NguoiLaoDong");
            }
            else
            {
                ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "1");
                ViewData["TrinhDoHV"] = _db.TrinhDoHV.Where(x => x.TrangThai == "1");
                ViewData["NganhNghe"] = _db.NganhNghe.Where(x => x.TrangThai == "1");
                ViewData["LoaiHopDongLaoDong"] = _db.LoaiHopDongLaoDong.Where(x => x.TrangThai == "1");
                ViewData["QuocGia"] = _db.QuocGia.Where(x => x.TrangThai == "1");
                ViewData["MenuLv1"] = "doanhnghiep";
                ViewData["MenuLv2"] = "doanhnghiep_nguoilaodong";
                return View("Views/Admin/Manages/DoanhNghiep/NguoiLaoDong/Edit.cshtml", model);
            }
        }

        [HttpPost("NguoiLaoDong/Update")]
        public IActionResult Update(NguoiLaoDong request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NguoiLaoDong", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var user = GetUser();
            var model = new BienDong()
            {
                LoaiBang = "nguoilaodong",
                PhanLoai = "capnhat",
                User = user,
                SoLuong = 1,
                MoTa = "cập nhật:" + request.HoVaTen,
                ThoiDiem = DateTime.Now,
            };
            _db.BienDong.Add(model);
            _db.NguoiLaoDong.Update(request);
            _db.SaveChanges();

            TempData["Message"] = "Cập nhật thông tin  thành công!";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "NguoiLaoDong");
        }

        [HttpPost("NguoiLaoDong/Delete")]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "NguoiLaoDong", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.NguoiLaoDong.FirstOrDefault(x => x.Id == id_delete);
            if (model != null)
            {
                _db.NguoiLaoDong.Remove(model);
                var user = GetUser();
                var biendong = new BienDong()
                {
                    LoaiBang = "nguoilaodong",
                    PhanLoai = "capnhat",
                    User = user,
                    SoLuong = 1,
                    MoTa = "Giảm:" + model.HoVaTen,
                    ThoiDiem = DateTime.Now,
                };
                _db.BienDong.Add(biendong);
                _db.SaveChanges();
                TempData["Message"] = "đã xóa thông tin hồ sơ!";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "không tìm thấy thông tin hồ sơ!";
                TempData["MessageType"] = "error";
            }
            return RedirectToAction("Index", "NguoiLaoDong");
        }

        public int GetUser()
        {
            var session = HttpContext.Session.GetString("SsAdmin");
            var sessionObject = JsonConvert.DeserializeObject<Users>(session!);
            var user = sessionObject!.Id;
            return user;
        }


    }
}