using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD.ThuThapTT.HeThongTruyVanTT;

namespace QLViecLam.Controllers.Admin.Manages.CungLaoDong
{
    public class BienDongCungController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BienDongCungController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("BienDongCung")]
        [HttpGet]
        public IActionResult Index(string kydieutra, string xa, string huyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "BienDongCung", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var dmhanhchinh = _db.DmHanhChinh;
            var list_huyen = dmhanhchinh.Where(x => x.CapDo == "H");
            var biendong = _db.BienDong.Where(x => x.LoaiBang == "NhanKhau");

            if (kydieutra != null)
            {
                biendong = biendong.Where(x => x.KyDieuTra == kydieutra);
            }

            var list_xa = dmhanhchinh.Where(x => x.CapDo == "X");
            if (huyen == null)
            {
                huyen = list_huyen.First().MaDb!;
            }

            list_xa = list_xa.Where(x => x.Parent == huyen);

            if (xa != null)
            {
                list_xa = list_xa.Where(x => x.MaDb == xa);
            }

            var sl_biendong = biendong.GroupBy(x => x.User)
             .Select(group => new VM_Count_Chucnang
             {
                 Mota_int = group.Key,
                 Count = group.Sum(x => x.SoLuong!),
             });

            var model = new List<DmHanhChinh>();
            if (sl_biendong.Count() > 0)
            {
                foreach (var item in list_xa)
                {
                    var bd = sl_biendong.Where(x => x.Mota == item.MaDb).First();
                    var h = list_huyen.Where(x => x.MaDb == item.Parent).First();
                    if (bd != null)
                    {
                        model.Add(new DmHanhChinh
                        {
                            Id = item.Id,
                            NameHuyen = h.Name!,
                            Name = item.Name,
                            MaDb = item.MaDb,
                            //Count = bd.Count,
                            Count = Convert.ToInt16(bd.Count),
                        });
                    }
                    else
                    {
                        model.Add(new DmHanhChinh
                        {
                            Id = item.Id,
                            NameHuyen = h.Name!,
                            Name = item.Name,
                            MaDb = item.MaDb,
                            Count = 0,
                        });
                    }
                }
            }
            else
            {
                foreach (var item in list_xa)
                {
                    var h = list_huyen.Where(x => x.MaDb == item.Parent).First();
                    model.Add(new DmHanhChinh
                    {
                        Id = item.Id,
                        NameHuyen = h.Name!,
                        Name = item.Name,
                        MaDb = item.MaDb,
                        Count = 0,
                    });

                }
            }
          
            ViewData["list_huyen"] = list_huyen;
            ViewData["list_xa"] = list_xa;
            ViewData["mahuyen"] = huyen;
            ViewData["maxa"] = xa;
            ViewData["kydieutra"] = kydieutra;

            ViewData["MenuLv1"] = "cunglaodong";
            ViewData["MenuLv2"] = "cunglaodong_biendongcung";
            return View("Views/Admin/Manages/CungLaoDong/BienDongCung/Index.cshtml", model);
        }


        [Route("BienDongCung/Detail")]
        [HttpGet]
        public IActionResult Detail(string madb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "BienDongCung", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
    
            var mdv = _db.DmDonvi.Where(x => x.MaDiaBan == madb).FirstOrDefault()!.MaDonVi;
            var user = _db.Users.Where(x => x.MaDonVi == mdv).FirstOrDefault()!.Id;

            var model = _db.BienDong.Where(x => x.User == user);

            ViewData["MenuLv1"] = "cunglaodong";
            ViewData["MenuLv2"] = "cunglaodong_biendongcung";
            return View("Views/Admin/Manages/CungLaoDong/BienDongCung/Detail.cshtml", model);
        }
    }
}
