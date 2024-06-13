using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems;
using QLViecLam.ViewModels.Admin.Systems;

namespace QLViecLam.Controllers.Admin.Manages.TongHop_PhanTich_DuDoan.HeThongBaoCaoThongKe.TraCuuBaoCaoTongHop.TrichXuatTTCungCau
{
    public class TrichXuatTTCungCauController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TrichXuatTTCungCauController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("TrichXuatTTCungCau")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var dmhanhchinh = _db.DmHanhChinh.Where(x => x.CapDo == "X");
                    var hanhchinh = (from hc in dmhanhchinh
                                     join dv in _db.DmDonvi
                                     on hc.Id equals Convert.ToInt16(dv.MaDiaBan)
                                     select new VM_HanhChinh_DonVi
                                     {
                                         Name = hc.Name,
                                         MaDv = dv.MaDonVi,
                                     });
                    
                    ViewData["Title"] = "Hệ Thống báo cáo thống kê";
                    ViewData["hanhchinh"] = hanhchinh;
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/HeThongBaoCaoThongKe/TraCuuBaoCaoTongHop/TrichXuatTTCungCau/Index.cshtml");
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


        [Route("TrichXuatTTCungCau/PrintCung")]
        [HttpGet]
        public IActionResult PrintCung(string kydieutra , string madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var model = _db.NhanKhau.Where(x=> x.KyDieuTra == kydieutra && x.MaDonVi == madv );

                    ViewData["Title"] = "thong-tin-cung-lao-dong";

                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/HeThongBaoCaoThongKe/TraCuuBaoCaoTongHop/TrichXuatTTCungCau/PrintCung.cshtml",model);
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

        [Route("TrichXuatTTCungCau/PrintCau")]
        [HttpGet]
        public IActionResult PrintCau( DateTime tungay ,DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var tuyendung = _db.TuyenDung.AsQueryable();
 
                    if (tungay.ToString("yyyyMMdd") != "00010101")
                    {
                        tuyendung = tuyendung.Where(x => x.ThoiHan >= tungay);
                    }
                    if (denngay.ToString("yyyyMMdd") != "00010101")
                    {
                        tuyendung = tuyendung.Where(x => x.ThoiHan <= denngay);
                    }
                    var vitri = _db.ViTriTuyenDung;

					var model = (from vt in vitri
								 join td in tuyendung
								 on vt.IdTuyenDung equals td.Id
                                 select new VM_TuyenDung_ViTriTuyenDung
                                 {
                                     User = td.User,
                                     ThoiHan = td.ThoiHan,
                                     SoLuong = vt.Soluong,
                                     Name = vt.Name,
                                 });
					
					var NewModel =( from comp in _db.Company
                                    join md in model 
                                    on comp.User equals md.User 
                                    select new VM_TuyenDung_ViTriTuyenDung
                                    {
                                        CongTy = comp.Name,
                                        ThoiHan = md.ThoiHan,
                                        SoLuong = md.SoLuong,
                                        Name = md.Name,
                                    });

                    ViewData["Title"] = "thong-tin-cau-lao-dong";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/HeThongBaoCaoThongKe/TraCuuBaoCaoTongHop/TrichXuatTTCungCau/PrintCau.cshtml", NewModel);
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
