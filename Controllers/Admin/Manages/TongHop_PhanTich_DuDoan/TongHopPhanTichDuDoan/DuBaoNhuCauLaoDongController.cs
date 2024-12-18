﻿using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD.ThuThapTT.HeThongTruyVanTT;
using QLViecLam.ViewModels.Admin.Manages.TongHop_PhanTich_DuDoan.TongHopPhanTichDuDoan;

namespace QLViecLam.Controllers.Admin.Manages.TongHop_PhanTich_DuDoan.TongHopPhanTichDuDoan
{
    public class DuBaoNhuCauLaoDongController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DuBaoNhuCauLaoDongController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("DuBaoNhuCauLaoDong")]
        [HttpGet]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
            //if (!Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hoso", "Index"))
            //{
            //    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
            //    return View("Views/Admin/Error/Page.cshtml");
            //}

            var nhankhau = _db.NhanKhau;
            var NganhHoc = _db.NganhHoc;
            var model = new List<VM_Count_Chucnang>();


            foreach (var item in NganhHoc)
            {
                var count = nhankhau.Where(X => X.ViecLamMongMuon!.ToString() == item.MaNganhHoc!).Count();
                if (count > 0)
                {
                    model.Add(new VM_Count_Chucnang
                    {
                        Mota = item.TenNganhHoc,
                        Count = count,
                    });
                }

            }


            return View("Views/Admin/Manages/TongHop_PhanTich_DuDoan/DuBaoThiTruongLaoDong.cshtml", model);

        }


    }
}
