using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Manages.ThongTinThiTruongLD;
using QLViecLam.Helper;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace QLViecLam.Controllers.Admin.Manages.ThongTinThiTruongLD.ThuThapTT.CSDLThuThapTT.NhuCauTuyenDungLD
{
    public class NhuCauTuyenDungLDController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhuCauTuyenDungLDController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("NhuCauTuyenDungLD")]
        [HttpGet]
        public IActionResult Index(DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    
                    var TuyenDung = _db.TuyenDung.AsQueryable();
                    if (Helpers.ConvertDateToStr(tungay) != "" ) 
                    {
                        TuyenDung = TuyenDung.Where(x => x.Created_at >= tungay);
                    }
                    if (Helpers.ConvertDateToStr(denngay) != "")
                    {
                        TuyenDung = TuyenDung.Where(x => x.ThoiHan <= denngay);
                    }
                    var Company = _db.Company;
                    var model = (from td in TuyenDung
                                 join comp in Company on td.User equals comp.User into details
                                 from comp_de in details.DefaultIfEmpty()
                                 select new TuyenDung
                                 {
                                     Id = td.Id,
                                     NoiDung = td.NoiDung,
                                     TenCongTy = comp_de != null ? comp_de.Name : "",

                                 });
              
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;

                    ViewData["Title"] = "Kế hoạch thu thập thông tin lao động";
                    ViewData["MenuLv1"] = "menu_thuthapthongtinthitruong";
                    ViewData["MenuLv2"] = "menu_thuthapthongtinthitruong_csdl";
                    ViewData["MenuLv3"] = "menu_thuthapthongtinthitruong_csdl_nhucautuyendung";
                    return View("Views/Admin/Manages/ThongTinThiTruongLD/ThuThapTT/CSDLThuThapTT/NhuCauTuyenDungLD/Index.cshtml", model);
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
        [Route("NhuCauTuyenDungLD/Detail")]
        [HttpGet]
        public IActionResult Detail(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {

                    var TuyenDung = _db.TuyenDung.Where(x=>x.Id == Id).FirstOrDefault();
                    var ViTriTuyenDung = _db.ViTriTuyenDung.Where(x=>x.IdTuyenDung == Id);

                    ViewData["TuyenDung"] = TuyenDung;
                    ViewData["ViTriTuyenDung"] = ViTriTuyenDung;
                    ViewData["Title"] = "Kế hoạch thu thập thông tin lao động";
                    ViewData["MenuLv1"] = "menu_thuthapthongtinthitruong";
                    ViewData["MenuLv2"] = "menu_thuthapthongtinthitruong_csdl";
                    ViewData["MenuLv3"] = "menu_thuthapthongtinthitruong_csdl_nhucautuyendung";
                    return View("Views/Admin/Manages/ThongTinThiTruongLD/ThuThapTT/CSDLThuThapTT/NhuCauTuyenDungLD/Detail.cshtml", TuyenDung);
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

        [Route("NhuCauTuyenDungLD/BcMau01")]
        [HttpGet]
        public IActionResult BcMau01()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    ViewData["DmLoaiHinhHdkt"] = _db.DmLoaiHinhHdkt;
                    return View("Views/Admin/Manages/ThongTinThiTruongLD/ThuThapTT/CSDLThuThapTT/NhuCauTuyenDungLD/BcMau01.cshtml");
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


        [Route("NhuCauTuyenDungLD/BcMau02")]
        [HttpGet]
        public IActionResult BcMau02()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                  

                    return View("Views/Admin/Manages/ThongTinThiTruongLD/ThuThapTT/CSDLThuThapTT/NhuCauTuyenDungLD/BcMau02.cshtml");
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


        [Route("NhuCauTuyenDungLD/BcMau03")]
        [HttpGet]
        public IActionResult BcMau03()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {


                    return View("Views/Admin/Manages/ThongTinThiTruongLD/ThuThapTT/CSDLThuThapTT/NhuCauTuyenDungLD/BcMau03.cshtml");
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


        [Route("NhuCauTuyenDungLD/BcMau03a")]
        [HttpGet]
        public IActionResult BcMau03a()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {


                    return View("Views/Admin/Manages/ThongTinThiTruongLD/ThuThapTT/CSDLThuThapTT/NhuCauTuyenDungLD/BcMau03a.cshtml");
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
