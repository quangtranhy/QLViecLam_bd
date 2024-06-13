using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Migrations;
using QLViecLam.Models.Admin.Manages.ThongTinThiTruongLD;
using QLViecLam.Models.Admin.Systems;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD.ThuThapTT.HeThongTruyVanTT;
using QLViecLam.ViewModels.Admin.Manages.TongHop_PhanTich_DuDoan.ThongTinCung_Cau;

namespace QLViecLam.Controllers.Admin.Manages.TongHop_PhanTich_DuDoan.ThongTinCung_Cau.BienDongViecLam
{
    public class BienDongViecLamController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BienDongViecLamController(ApplicationDbContext db)
        {
            _db = db;
        }


        [Route("BienDongVieclam")]
        [HttpGet]
        public IActionResult Index(string phanloai, DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {

                    var biendong = _db.BienDong.Where(x => x.LoaiBang == "NguoiLaoDong").AsQueryable();
                    if (phanloai == "baotang")
                    {
                        biendong = biendong.Where(x => x.PhanLoai == "baotang");
                    }
                    if (phanloai == "baogiam")
                    {
                        biendong = biendong.Where(x => x.PhanLoai == "baogiam");
                    }
                    if (phanloai == "tamdung")
                    {
                        biendong = biendong.Where(x => x.PhanLoai == "tamdung");
                    }
                    if (phanloai == "ketthuctamdung")
                    {
                        biendong = biendong.Where(x => x.PhanLoai == "ketthuctamdung");
                    }
                    if (phanloai == "khongbiendong")
                    {
                        biendong = biendong.Where(x => x.PhanLoai == "khongbiendong");
                    }
                    //if (phanloai == "chuakhaibao")
                    //{
                    //}

                    if (Helpers.ConvertDateTimeToStr(tungay) != "")
                    {
                        biendong = biendong.Where(x => x.ThoiDiem >= tungay);
                    }
                    if (Helpers.ConvertDateTimeToStr(denngay) != "")
                    {
                        biendong = biendong.Where(x => x.ThoiDiem <= denngay);
                    }

                    var company = _db.Company;

                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["phanloai"] = phanloai;
                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_biendongvieclam";


                    if (phanloai == "chuakhaibao")
                    {
                        var model = new List<VM_BienDongViecLam>();
                        var ckb = biendong.GroupBy(x => x.User)
                        .Select(group => new VM_Count_Chucnang
                        {
                            Mota_int = group.Key,
                        });
                        var ckbUsers = ckb.Select(g => g.Mota_int).ToList();
                       var com = company.Where(x=> !ckbUsers.Contains(x.User));

                        foreach (var item in com) 
                        {
                            model.Add(new VM_BienDongViecLam
                            {
                                Id = item.Id,
                                TenDn = item.Name,
                            });
                        }

                        return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/BienDongViecLam/Index.cshtml", model);
                    }
                    if (phanloai == "khongbiendong")
                    {
                        var model = (from c in company
                                     join b in biendong on c.User equals b.User
                                     select new VM_BienDongViecLam
                                     {
                                         Id = c.Id,
                                         TenDn = c.Name,
                                     });

                        return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/BienDongViecLam/Index.cshtml", model);
                    }
                    else
                    {
                        var model = new List<VM_BienDongViecLam>();

                        if (phanloai == null)
                        {
                            foreach (var item in company)
                            {
                                var bdong = biendong.Where(x => x.User == item.User);
                                var baotang = bdong.Where((x) => x.PhanLoai == "baotang");
                                var sl_baotang = baotang.Sum(x => x.SoLuong);
                                var baogiam = bdong.Where((x) => x.PhanLoai == "baogiam");
                                var sl_baogiam = baogiam.Sum(x => x.SoLuong);

                                var tamdung = bdong.Where((x) => x.PhanLoai == "tamdung");
                                var sl_tamdung = tamdung.Sum(x => x.SoLuong);
                                var ketthuctamdung = bdong.Where((x) => x.PhanLoai == "ketthuctamdung");
                                var sl_ketthuctamdung = ketthuctamdung.Sum(x => x.SoLuong);
                                model.Add(new VM_BienDongViecLam
                                {
                                    Id = item.Id,
                                    TenDn = item.Name,
                                    BaoTang = sl_baotang,
                                    BaoGiam = sl_baogiam,
                                    TamDung = sl_tamdung,
                                    KetThucTamDung = sl_ketthuctamdung,
                                });
                            }

                        }
                        //if (phanloai == "baotang" || phanloai == "baogiam" || phanloai == "tamdung" || phanloai == "ketthuctamdung")
                        else
                        {
                            var company_list = (from c in company
                                                join b in biendong on c.User equals b.User
                                                select new Company
                                                {
                                                    Id = c.Id,
                                                    User = c.User,
                                                    Name = c.Name,
                                                });
                            foreach (var item in company_list)
                            {
                                var bdong = biendong.Where(x => x.User == item.User);
                                var baotang = bdong.Where((x) => x.PhanLoai == "baotang");
                                var sl_baotang = baotang.Sum(x => x.SoLuong);
                                var baogiam = bdong.Where((x) => x.PhanLoai == "baogiam");
                                var sl_baogiam = baogiam.Sum(x => x.SoLuong);
                                var tamdung = bdong.Where((x) => x.PhanLoai == "tamdung");
                                var sl_tamdung = tamdung.Sum(x => x.SoLuong);
                                var ketthuctamdung = bdong.Where((x) => x.PhanLoai == "ketthuctamdung");
                                var sl_ketthuctamdung = ketthuctamdung.Sum(x => x.SoLuong);
                                model.Add(new VM_BienDongViecLam
                                {
                                    Id = item.Id,
                                    TenDn = item.Name,
                                    BaoTang = sl_baotang,
                                    BaoGiam = sl_baogiam,
                                    TamDung = sl_tamdung,
                                    KetThucTamDung = sl_ketthuctamdung,
                                });
                            }
                        }
                        return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/BienDongViecLam/Index.cshtml", model);
                    }

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


        [Route("BienDongVieclam/Detail")]
        [HttpGet]
        public IActionResult Detail(string Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {

                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_biendongvieclam";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/BienDongViecLam/Detail.cshtml");
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
