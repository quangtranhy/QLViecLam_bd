using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD;
using QLViecLam.ViewModels.Admin.Manages.TongHop_PhanTich_DuDoan.ThongTinCung_Cau;

namespace QLViecLam.Controllers.Admin.Manages.CungLaoDong
{
    public class HoGiaDinhController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HoGiaDinhController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("HoGiaDinh")]
        [HttpGet]
        public IActionResult Index(string huyen, string xa, string kydieutra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "HoGiaDinh", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var nam = DateTime.Now.Year.ToString();
            var dmhanhchinh = _db.DmHanhChinh.AsQueryable();
            var model = _db.NhanKhau.AsQueryable();
            var k = (kydieutra == null) ? nam : kydieutra;

            model = model.Where(x => x.KyDieuTra == k);

            if (huyen != null)
            {
                var danhsach_xa = dmhanhchinh.Where(x => x.Parent == huyen);
                var xacdinh_xa = "";
                foreach (var item in danhsach_xa)
                {
                    if (item.MaDb == xa)
                    {
                        xacdinh_xa = "tructhuochuyen";
                    }
                }
                if (xacdinh_xa == "tructhuochuyen")
                {
                    model = model.Where(x => x.MaDiaBan == xa);
                }
                else
                {
                    var ds_xa = dmhanhchinh.Where(x => x.Parent == huyen).Select(x => x.MaDb).ToArray();
                    model = model.Where(x => ds_xa.Contains(x.MaDiaBan));
                }
            }
            else
            {
                huyen = dmhanhchinh.Where(x => x.CapDo == "H").FirstOrDefault()!.MaDb!;
                var ds_xa = dmhanhchinh.Where(x => x.Parent == huyen).Select(x => x.MaDb).ToArray();
                model = model.Where(x => ds_xa.Contains(x.MaDiaBan));
            }

            var HoGD = model.GroupBy(x => x.SoDinhDanhHoGD)
                .Select(group => new VM_Count_HoGiaDinh
                {
                    SoDinhDanhHoGD = group.Key,
                    //Count = group.Count(),
                    //ThanhVien = "",
                });

            var newModel = new List<VM_Count_HoGiaDinh>();

            foreach (var item in HoGD)
            {
                var thanhvien = "";
                var gd_model = model.Where(x => x.SoDinhDanhHoGD == item.SoDinhDanhHoGD);

                foreach (var tv in gd_model)
                {
                    thanhvien += tv.HoVaTen + ", ";
                }
                newModel.Add(new VM_Count_HoGiaDinh
                {
                    SoDinhDanhHoGD = item.SoDinhDanhHoGD,
                    ThanhVien = thanhvien,
                    Count = gd_model.Count(),
                });

            }

            ViewData["huyen"] = huyen;
            ViewData["xa"] = xa;
            ViewData["kydieutra"] = k;
            ViewData["DsHuyen"] = dmhanhchinh.Where(t => t.CapDo == "H");
            ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "1");
            if (string.IsNullOrEmpty(xa))
            {
                ViewData["DsXa"] = dmhanhchinh.Where(t => t.CapDo == "X");
            }
            else
            {
                ViewData["DsXa"] = dmhanhchinh.Where(t => t.CapDo == "X" && t.Parent == huyen);
            }

            ViewData["MenuLv1"] = "cunglaodong";
            ViewData["MenuLv2"] = "cunglaodong_hogiadinh";
            return View("Views/Admin/Manages/CungLaoDong/HoGiaDinh/Index.cshtml", newModel);
        }


        [Route("HoGiaDinh/Detail")]
        [HttpGet]
        public IActionResult Detail(string sohogd)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "HoGiaDinh", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var NhanKhau = _db.NhanKhau.Where(x => x.SoDinhDanhHoGD == sohogd).ToList();
            var QuanHe = _db.QuanHe.Where(x=>x.TrangThai == "1");
            var model = (from nk in NhanKhau
                         join qh in QuanHe on nk.QuanHe equals qh.MaQuanHe into details
                         from qh_de in details.DefaultIfEmpty()
                         select new VM_NhanKhau
                         {
                             Id = nk.Id,
                             SoDinhDanhHoGD = nk.SoDinhDanhHoGD,
                             SoCCCD = nk.SoCCCD,
                             TenQuanHe = qh_de != null ? qh_de.TenQuanHe : null,
                             HoVaTen = nk.HoVaTen,
                             NgayThangNamSinh = nk.NgayThangNamSinh,
                             Gioitinh = nk.Gioitinh,
                             ThuongTru = nk.ThuongTru,
                         });

            ViewData["MenuLv1"] = "cunglaodong";
            ViewData["MenuLv2"] = "cunglaodong_hogiadinh";
            return View("Views/Admin/Manages/CungLaoDong/HoGiaDinh/Detail.cshtml", model);

        }


        [Route("HoGiaDinh/Show")]
        [HttpGet]
        public IActionResult Show(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "HoGiaDinh", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.NhanKhau.Where(x => x.Id == Id).FirstOrDefault();

            ViewData["DanToc"] = _db.DanToc.Where(x => x.TrangThai == "1");
            ViewData["NganhHoc"] = _db.NganhHoc.Where(x => x.TrangThai == "1");
            ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "1");
            ViewData["QuocGia"] = _db.QuocGia.Where(x => x.TrangThai == "1");
            ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "1");
            ViewData["TrinhDoHV"] = _db.TrinhDoHV.Where(x => x.TrangThai == "1");

            ViewData["MenuLv1"] = "cunglaodong";
            ViewData["MenuLv2"] = "cunglaodong_hogiadinh";
            return View("Views/Admin/Manages/CungLaoDong/HoGiaDinh/Show.cshtml", model);
        }
    }
}
