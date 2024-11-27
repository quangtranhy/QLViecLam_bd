using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.Models.Admin.Systems.HeThongChung;

namespace QLViecLam.Controllers.Admin.Emploer
{
    public class KhaiBaoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public KhaiBaoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("KhaiBao/BaoTang")]
        public IActionResult BaoTang()
        {
            ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT;
            ViewData["TrinhDoHV"] = _db.TrinhDoHV;
            ViewData["NganhNghe"] = _db.NganhNghe;
            ViewData["LoaiHopDongLaoDong"] = _db.LoaiHopDongLaoDong;
            ViewData["QuocGia"] = _db.QuocGia;

            ViewData["Title"] = "khai báo";
            ViewData["MenuLv1"] = "khaibao";
            ViewData["MenuLv2"] = "khaibao_baotang";
            return View("Views/Admin/Employer/KhaiBao/BaoTang.cshtml");
        }

        [HttpPost("KhaiBao/UpdateBaoTang")]
        public IActionResult UpdateBaoTang(NguoiLaoDong request)
        {
            var user = GetUser();
            var com = _db.Company.FirstOrDefault(x => x.User == user);
            var MaDn = com!.MaSoDn;

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

            var input = request;
            input.MaDn = MaDn;
            input.State = 1;
            _db.NguoiLaoDong.Add(input);
            _db.SaveChanges();
            return RedirectToAction("BaoTang", "KhaiBao");
        }

        [HttpGet("KhaiBao/BaoGiam")]
        public IActionResult BaoGiam()
        {
            var user = GetUser();
            var com = _db.Company.FirstOrDefault(x => x.User == user);
            var MaDn = com!.MaSoDn;
            var model = _db.NguoiLaoDong.Where(x => x.MaDn == MaDn).Where(x => x.State == 1);
            ViewData["Title"] = "khai báo";
            ViewData["MenuLv1"] = "khaibao";
            ViewData["MenuLv2"] = "khaibao_baogiam";
            return View("Views/Admin/Employer/KhaiBao/BaoGiam.cshtml", model);
        }

        [HttpPost("KhaiBao/UpdateBaoGiam")]
        public IActionResult UpdateBaoGiam(int Id)
        {

            var nguoilaodong = _db.NguoiLaoDong.FirstOrDefault(x => x.Id == Id);
            nguoilaodong!.State = 0;
            _db.NguoiLaoDong.Update(nguoilaodong);
            var user = _db.Company.Where(x => x.MaSoDn == nguoilaodong.MaDn).FirstOrDefault()!.User;
            var model = new BienDong()
            {
                LoaiBang = "nguoilaodong",
                PhanLoai = "baogiam",
                User = user,
                SoLuong = 1,
                MoTa = "giảm:" + nguoilaodong!.HoVaTen,
                ThoiDiem = DateTime.Now,
            };
            _db.BienDong.Add(model);

            _db.SaveChanges();
            return RedirectToAction("BaoGiam", "KhaiBao");
        }

        [HttpGet("KhaiBao/TamDung")]
        public IActionResult TamDung()
        {
            var user = GetUser();
            var com = _db.Company.FirstOrDefault(x => x.User == user);
            var MaDn = com!.MaSoDn;
            var model = _db.NguoiLaoDong.Where(x => x.MaDn == MaDn).Where(x => x.State == 1);
            ViewData["Title"] = "khai báo";
            ViewData["MenuLv1"] = "khaibao";
            ViewData["MenuLv2"] = "khaibao_tamdung";
            return View("Views/Admin/Employer/KhaiBao/TamDung.cshtml", model);
        }

        [HttpPost("KhaiBao/UpdateTamDung")]
        public IActionResult UpdateTamDung(int Id)
        {

            var nguoilaodong = _db.NguoiLaoDong.FirstOrDefault(x => x.Id == Id);
            nguoilaodong!.State = 2;
            _db.NguoiLaoDong.Update(nguoilaodong);
            var user = _db.Company.Where(x => x.MaSoDn == nguoilaodong.MaDn).FirstOrDefault()!.User;
            var model = new BienDong()
            {
                LoaiBang = "nguoilaodong",
                PhanLoai = "tamdung",
                User = user,
                SoLuong = 1,
                MoTa = "tạm dừng:" + nguoilaodong!.HoVaTen,
                ThoiDiem = DateTime.Now,
            };
            _db.BienDong.Add(model);

            _db.SaveChanges();
            return RedirectToAction("TamDung", "KhaiBao");
        }

        [HttpGet("KhaiBao/KetThucTamDung")]
        public IActionResult KetThucTamDung()
        {
            var user = GetUser();
            var com = _db.Company.FirstOrDefault(x => x.User == user);
            var MaDn = com!.MaSoDn;
            var model = _db.NguoiLaoDong.Where(x => x.MaDn == MaDn).Where(x => x.State == 2);
            ViewData["Title"] = "khai báo";
            ViewData["MenuLv1"] = "khaibao";
            ViewData["MenuLv2"] = "khaibao_ketthuctamdung";
            return View("Views/Admin/Employer/KhaiBao/KetThucTamDung.cshtml", model);
        }

        [HttpPost("KhaiBao/UpdateKetThucTamDung")]
        public IActionResult UpdateKetThucTamDung(int Id)
        {

            var nguoilaodong = _db.NguoiLaoDong.FirstOrDefault(x => x.Id == Id);
            nguoilaodong!.State = 1;
            _db.NguoiLaoDong.Update(nguoilaodong);
            var user = _db.Company.Where(x => x.MaSoDn == nguoilaodong.MaDn).FirstOrDefault()!.User;
            var model = new BienDong()
            {
                LoaiBang = "nguoilaodong",
                PhanLoai = "tamdung",
                User = user,
                SoLuong = 1,
                MoTa = "tạm dừng:" + nguoilaodong!.HoVaTen,
                ThoiDiem = DateTime.Now,
            };
            _db.BienDong.Add(model);

            _db.SaveChanges();
            return RedirectToAction("KetThucTamDung", "KhaiBao");
        }

        [HttpGet("KhaiBao/KhongBienDong")]
        public IActionResult KhongBienDong()
        {
            var user = GetUser();

            var today = DateTime.Now;
            var start = new DateTime(today.Year, today.Month, 1);
            var model = _db.BienDong.Where(x => x.User == user && x.PhanLoai == "khongbiendong" && x.ThoiDiem >= start && x.ThoiDiem <= today).FirstOrDefault();

            ViewData["Title"] = "khai báo";
            ViewData["MenuLv1"] = "khaibao";
            ViewData["MenuLv2"] = "khaibao_khongbiendong";
            return View("Views/Admin/Employer/KhaiBao/KhongBienDong.cshtml", model);
        }



        [HttpGet("KhaiBao/UpdateKhongBienDong")]
        public IActionResult UpdateKhongBienDong()
        {
            var user = GetUser();
            var model = new BienDong()
            {
                LoaiBang = "nguoilaodong",
                PhanLoai = "khongbiendong",
                User = user,
                MoTa = "không có biến động:",
                ThoiDiem = DateTime.Now,
            };
            _db.BienDong.Add(model);

            _db.SaveChanges();
            return RedirectToAction("KhongBienDong", "KhaiBao");
        }

        [HttpGet("KhaiBao/LichSu")]
        public IActionResult LichSu(DateTime tungay, DateTime denngay)
        {
            var user = GetUser();
            var date = DateTime.Now;
            var model = _db.BienDong.Where(x => x.User == user);
            if (Helper.Helpers.ConvertDateToStr(tungay) != "")
            {
                model = model.Where(x => x.ThoiDiem >= tungay);
            }
            else {
                tungay = new DateTime(date.Year, date.Month, 1);
            }

            if (Helper.Helpers.ConvertDateToStr(denngay) != "")
            {
                model = model.Where(x => x.ThoiDiem <= denngay);
            }
            else
            {
                denngay = date;
            }

            ViewData["tungay"] = tungay.ToString("yyyy-MM-dd");
            ViewData["denngay"] = denngay.ToString("yyyy-MM-dd");
            ViewData["Title"] = "khai báo";
            ViewData["MenuLv1"] = "khaibao";
            ViewData["MenuLv2"] = "khaibao_lichsu";
            return View("Views/Admin/Employer/KhaiBao/LichSu.cshtml", model);
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
