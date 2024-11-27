using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD;

namespace QLViecLam.Controllers.Admin.Emploer
{
    public class DoanhNghiepController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DoanhNghiepController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("DoanhNghiep")]
        [HttpGet]
        public IActionResult Index()
        {

            var session = HttpContext.Session.GetString("SsAdmin");
            var sessionObject = JsonConvert.DeserializeObject<Users>(session!);
            var user = sessionObject!.Id;
            var model = _db.Company.FirstOrDefault(x => x.User == user);
            var MaDn = model!.MaSoDn;
            var DmHanhChinh = _db.DmHanhChinh;

            ViewData["NguoiLaoDong"] = _db.NguoiLaoDong.Where(x => x.MaDn == MaDn);
            ViewData["KhuCongNghiep"] = _db.KhuCongNghiep;
            ViewData["HinhThucDoanhNghiep"] = _db.HinhThucDoanhNghiep;
            ViewData["NganhHoc"] = _db.NganhHoc;
            ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "kh");
            ViewData["tinh"] = DmHanhChinh.Where(x => x.CapDo == "T");
            ViewData["huyen"] = DmHanhChinh.Where(x => x.CapDo == "H");
            ViewData["xa"] = DmHanhChinh.Where(x => x.CapDo == "X");
            ViewData["MenuLv1"] = "doanhnghiep";
            return View("Views/Admin/Employer/DoanhNghiep/Index.cshtml", model);
        }


        [HttpPost("DoanhNghiep/Update")]
        public IActionResult Update(Company request)
        {
           


            var model = _db.Company.FirstOrDefault(x => x.Id == request.Id);
            if (model != null)
            {

                model.Name = request.Name;
                //model.MaSoDn = request.MaSoDn;
                model.TinhTrangXacThuc = request.TinhTrangXacThuc;
                model.Phone = request.Phone;
                model.Fax = request.Fax;
                model.Website = request.Website;
                model.Email = request.Email;
                model.Tinh = request.Tinh;
                model.Huyen = request.Huyen;
                model.Xa = request.Xa;
                model.Address = request.Address;
                model.KhuCn = request.KhuCn;
                model.LoaiHinh = request.LoaiHinh; 
                model.NganhNghe = request.NganhNghe;

                _db.Company.Update(model);
                _db.SaveChanges();
            };
            return RedirectToAction("Index", "DoanhNghiep");
        }

        [Route("DoanhNghiep/NguoiLaoDong")]
        [HttpGet]
        public IActionResult NguoiLaoDong()
        {

            var session = HttpContext.Session.GetString("SsAdmin");
            var sessionObject = JsonConvert.DeserializeObject<Users>(session!);
            var user = sessionObject!.Id;
            var Company = _db.Company.FirstOrDefault(x => x.User == user);
            var Madn = Company!.MaSoDn;
            var model = _db.NguoiLaoDong.Where(x => x.MaDn == Madn);
            ViewData["MenuLv1"] = "nguoilaodong";
            return View("Views/Admin/Employer/NguoiLaoDong/Index.cshtml", model);
        }

        [Route("DoanhNghiep/BaoCao")]
        [HttpGet]
        public IActionResult BcChiTiet()
        {

            ViewData["MenuLv1"] = "baocao";
            return View("Views/Admin/Employer/BaoCao/Index.cshtml");
        }

        [Route("DoanhNghiep/BaoCao/BcMau01pli")]
        [HttpGet]
        public IActionResult BcMau01pli()
        {
            var session = HttpContext.Session.GetString("SsAdmin");
            var sessionObject = JsonConvert.DeserializeObject<Users>(session!);
            var user = sessionObject!.Id;
            var Company = _db.Company.FirstOrDefault(x => x.User == user);
            var MaDn = Company!.MaSoDn;
            var model = _db.NguoiLaoDong.Where(x => x.MaDn == MaDn);


            ViewData["Company"] = Company;
            ViewData["MenuLv1"] = "baocao";
            return View("Views/Admin/Employer/BaoCao/BcMau01pli.cshtml",model);
        }


    }
}