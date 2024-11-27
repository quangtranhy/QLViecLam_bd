using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD.ThuThapTT.HeThongTruyVanTT;
using QLViecLam.ViewModels.Admin.Manages.TongHop_PhanTich_DuDoan.ThongTinCung_Cau;

namespace QLViecLam.Controllers.Admin.Manages.DoanhNghiep
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
                if (!Helpers.CheckChucNang(HttpContext.Session, "BienDongVieclam", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
           
            var biendong = _db.BienDong.Where(x => x.LoaiBang == "nguoilaodong").AsQueryable();

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



            if (Helpers.ConvertDateToStr(tungay) != "")
            {
                biendong = biendong.Where(x => x.ThoiDiem >= tungay);
            }
            if (Helpers.ConvertDateToStr(denngay) != "")
            {
                biendong = biendong.Where(x => x.ThoiDiem <= denngay);
            }

            var company = _db.Company;

            ViewData["tungay"] = tungay;
            ViewData["denngay"] = denngay;
            ViewData["phanloai"] = phanloai;
            ViewData["MenuLv1"] = "doanhnghiep";
            ViewData["MenuLv2"] = "doanhnghiep_biendongvieclam";


            if (phanloai == "chuakhaibao")
            {
                var model = new List<VM_BienDongViecLam>();
                var ckb = biendong.GroupBy(x => x.User)
                .Select(group => new VM_Count_Chucnang
                {
                    Mota_int = group.Key,
                });
                var ckbUsers = ckb.Select(g => g.Mota_int).ToList();
                var com = company.Where(x => !ckbUsers.Contains(x.User));

                foreach (var item in com)
                {
                    model.Add(new VM_BienDongViecLam
                    {
                        Id = item.Id,
                        TenDn = item.Name,
                        User = item.User,
                    });
                }
             
                return View("Views/Admin/Manages/DoanhNghiep/BienDongViecLam/Index.cshtml", model);
            }
            if (phanloai == "khongbiendong")
            {
                var model = (from c in company
                             join b in biendong on c.User equals b.User
                             select new VM_BienDongViecLam
                             {
                                 Id = c.Id,
                                 TenDn = c.Name,
                                 User = c.User,
                             });
               
                return View("Views/Admin/Manages/DoanhNghiep/BienDongViecLam/Index.cshtml", model);
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
                            User = item.User,
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
                            User = item.User,
                            TenDn = item.Name,
                            BaoTang = sl_baotang,
                            BaoGiam = sl_baogiam,
                            TamDung = sl_tamdung,
                            KetThucTamDung = sl_ketthuctamdung,
                        });
                    }
                }

                return View("Views/Admin/Manages/DoanhNghiep/BienDongViecLam/Index.cshtml", model);
            }

        }


        [Route("BienDongVieclam/Detail")]
        [HttpGet]
        public IActionResult Detail(int user,string congty)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "BienDongVieclam", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.BienDong.Where(x => x.LoaiBang == "nguoilaodong" && x.User == user);

            ViewData["congty"] = congty;
            ViewData["MenuLv1"] = "doanhnghiep";
            ViewData["MenuLv2"] = "doanhnghiep_biendongvieclam";
            return View("Views/Admin/Manages/DoanhNghiep/BienDongViecLam/Detail.cshtml",model);

        }
    }
}
