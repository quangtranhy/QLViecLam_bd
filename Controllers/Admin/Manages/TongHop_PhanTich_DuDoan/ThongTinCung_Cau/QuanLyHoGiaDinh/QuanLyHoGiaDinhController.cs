using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using QLViecLam.Data;
using System.Security.Cryptography;
using QLViecLam.Helper;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;
using QLViecLam.Models.Admin.Systems;
using System.Diagnostics.Metrics;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD.ThuThapTT.HeThongTruyVanTT;
using QLViecLam.ViewModels.Admin.Manages.TongHop_PhanTich_DuDoan.ThongTinCung_Cau;
using QLViecLam.ViewModels.Helpers.DanhMucChung;
using QLViecLam.Models.Admin.Manages.ThongTinThiTruongLD;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD;

namespace QLViecLam.Controllers.Admin.Manages.TongHop_PhanTich_DuDoan.ThongTinCung_Cau.QuanLyHoGiaDinh
{
    public class QuanLyHoGiaDinhController : Controller
    {
        private readonly ApplicationDbContext _db;

        public QuanLyHoGiaDinhController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("QuanLyHoGiaDinh")]
        [HttpGet]
        public IActionResult Index( string huyen, string xa, string kydieutra)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
                    var madv = "";
                    var nam = DateTime.Now.Year.ToString();
                    var dmhanhchinh = _db.DmHanhChinh.AsQueryable();
                    var dmdonvi = _db.DmDonvi.AsQueryable();
                    var model = _db.NhanKhau.AsQueryable();
                    var k = (kydieutra == null) ? nam : kydieutra;

                    model = model.Where(x => x.KyDieuTra == k);

                    if (huyen != null)
                    {

                        var ds_xa = dmhanhchinh.Where(x => x.Parent == huyen);
                        var xd_xa = "";
                        foreach (var item in ds_xa)
                        {
                            if (item.MaDb == xa)
                            {
                                xd_xa = "tontai";
                            }
                        }

                        if (xd_xa == "tontai")
                        {
                            xa = dmhanhchinh.Where(x => x.MaDb == xa).FirstOrDefault()!.MaDb!;
                            madv = dmdonvi.Where(x => x.MaDiaBan == xa).FirstOrDefault()!.MaDonVi;
                            model = model.Where(x => x.MaDonVi == madv);
                        }
                        else
                        {
                            xa = dmhanhchinh.Where(x => x.Parent == huyen).FirstOrDefault()!.MaDb!;
                            var id_xa = dmhanhchinh.Where(x => x.Parent == huyen).FirstOrDefault()!.Id.ToString();
                            madv = dmdonvi.Where(x => x.MaDiaBan == id_xa).FirstOrDefault()!.MaDonVi;
                            model = model.Where(x => x.MaDonVi == madv);
                        }

                    }
                    else
                    {
                        huyen = dmhanhchinh.Where(x => x.CapDo == "H").FirstOrDefault()!.MaDb!;
                        xa = dmhanhchinh.Where(x => x.Parent == huyen).FirstOrDefault()!.MaDb!;
                        var id_xa = dmhanhchinh.Where(x => x.Parent == huyen).FirstOrDefault()!.Id.ToString();
                        madv = dmdonvi.Where(x => x.MaDiaBan == id_xa).FirstOrDefault()!.MaDonVi;
                        model = model.Where(x => x.MaDonVi == madv);
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
                        var thanhvien = "" ;
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
                    ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "kh");
                    if (string.IsNullOrEmpty(xa))
                    {
                        ViewData["DsXa"] = dmhanhchinh.Where(t => t.CapDo == "X");
                    }
                    else
                    {
                        ViewData["DsXa"] = dmhanhchinh.Where(t => t.CapDo == "X" && t.Parent == huyen);
                    }

                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_quanlyhogiadinh";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/QuanLyHoGiaDinh/Index.cshtml", newModel);
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


        [Route("QuanLyHoGiaDinh/Detail")]
        [HttpGet]
        public IActionResult Detail(int sohogd)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {

                    var NhanKhau = _db.NhanKhau.Where(x => x.SoDinhDanhHoGD == sohogd).ToList();
                    var QuanHe = DanhMucChung.QuanHe();
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
                    

                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_quanlyhogiadinh";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/QuanLyHoGiaDinh/Detail.cshtml", model);
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


        [Route("QuanLyHoGiaDinh/Show")]
        [HttpGet]
        public IActionResult Show(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                bool check_per = true;
                if (check_per)
                {
     
                    var model = _db.NhanKhau.Where(x => x.Id == Id).FirstOrDefault();

                    ViewData["DanToc"] = _db.DanToc.Where(x => x.TrangThai == "kh");
                    ViewData["NganhHoc"] = _db.NganhHoc.Where(x => x.TrangThai == "kh");
                    ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "kh");
                    ViewData["QuocGia"] = _db.QuocGia.Where(x => x.TrangThai == "kh");
                    ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "kh");
                    ViewData["TrinhDoHV"] = _db.TrinhDoHV.Where(x => x.TrangThai == "kh");

                    ViewData["MenuLv1"] = "menu_capnhatcungcau";
                    ViewData["MenuLv2"] = "menu_capnhatcungcau_quanlyhogiadinh";
                    return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/ThongTinCung_Cau/QuanLyHoGiaDinh/Show.cshtml", model);
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
