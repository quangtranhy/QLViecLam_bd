using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD.ThuThapTT.HeThongTruyVanTT;

namespace QLViecLam.Controllers.Admin.Manages.TongHop_PhanTich_DuDoan.ThongTinCung_Cau.BienDongCungLD
{
    public class BienDongCungLDController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BienDongCungLDController(ApplicationDbContext db)
        {
            _db = db;
        }


        [Route("BienDongCungLD")]
        [HttpGet]
        public IActionResult Index(string kydieutra, string xa, string huyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
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
                                Count = 0,
                            });

                        }
                    }

                    ViewData["list_huyen"] = list_huyen;
                    ViewData["list_xa"] = list_xa;
                    ViewData["mahuyen"] = huyen;
                    ViewData["maxa"] = xa;
                    ViewData["kydieutra"] = kydieutra;
                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_biendongcunglaodong";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/BienDongCungLD/Index.cshtml", model);
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


        [Route("BienDongCungLD/Detail")]
        [HttpGet]
        public IActionResult Detail(string Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var mdv = _db.DmDonvi.Where(x => x.MaDiaBan == Id).First().MaDonVi;
                    var user = _db.Users.Where(x => x.MaDonVi == mdv).First().Id;

                    var model = _db.BienDong.Where(x => Convert.ToInt16(x.User) == user);
                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_biendongcunglaodong";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/BienDongCungLD/Detail.cshtml", model);
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
