using Microsoft.AspNetCore.Mvc;
using QLViecLam.Data;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using QLViecLam.ViewModels.Admin.Systems;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD.ThuThapTT.HeThongTruyVanTT;
using QLViecLam.Models.Admin.Manages.ThongTinThiTruongLD;
using QLViecLam.Helper;

namespace QLViecLam.Controllers.Admin.Systems.HeThongChung.DonVi
{
    public class DonViController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DonViController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("DonVi")]
        [HttpGet]
        public IActionResult Index(string huyen, string xa)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "DonVi", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
            var DmDonvi = _db.DmDonvi.AsQueryable();
            var DmHanhChinh = _db.DmHanhChinh;
            var hanhchinh = DmHanhChinh.AsQueryable();
            //if (huyen != null && xa != null)
            //{
            //    hanhchinh = hanhchinh.Where(x => x.MaDb == xa);
            //}
            //if (huyen != null && xa == null)
            //{
            //    hanhchinh = hanhchinh.Where(x => x.Parent == huyen);
            //}
            //if (huyen == null && xa != null)
            //{
            //    model = model.Where(x => x.MaDiaBan == xa);
            //    hanhchinh = hanhchinh.Where(x => x.MaDb == xa);
            //    huyen = hanhchinh.FirstOrDefault()!.Parent!;
            //}
            var newDonVi = (from hc in hanhchinh
                            join dv in DmDonvi on hc.MaDb equals dv.MaDiaBan
                            //into details from m_de in details.DefaultIfEmpty()
                            select new VM_HanhChinh_DonVi
                            {
                                TenDv = dv.TenDv,
                                MaDb = hc.MaDb,
                            });
            var count_model = newDonVi.GroupBy(x => x.MaDb)
                        .Select(group => new VM_ChucNang_Count
                        {
                            Mota_string = group.Key,
                            Count_string = string.Join(", ", group.Select(x => x.TenDv)),
                        });

            var model = new List<VM_HanhChinh_DonVi>();
            foreach (var item in hanhchinh)
            {
                var check = count_model.Where(x => x.Mota_string == item.MaDb).FirstOrDefault();
                if (check != null)
                {
                    model.Add(new VM_HanhChinh_DonVi
                    {
                        Id = item.Id,
                        TenDb = item.Name,
                        MaDb = item.MaDb,
                        TenDv = check.Count_string,
                        CapDo = item.CapDo,
                        Parent = item.Parent,
                    });
                }
                else
                {
                    model.Add(new VM_HanhChinh_DonVi
                    {
                        Id = item.Id,
                        TenDb = item.Name,
                        MaDb = item.MaDb,
                        TenDv = "",
                        CapDo = item.CapDo,
                        Parent = item.Parent,
                    });
                }
            }

            var dsxa = model.Where(x => x.CapDo == "X");
            var dshuyen = model.Where(x => x.CapDo == "T" || x.CapDo == "H");

            ViewData["dsxa"] = dsxa;
            ViewData["dshuyen"] = dshuyen;
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_donvi";

            return View("Views/Admin/Systems/HeThongChung/DonVi/Index.cshtml");
        }

        [Route("DonVi/Detail")]
        [HttpGet]
        public IActionResult Detail(string madb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "DonVi", "DanhSach"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DmDonvi.Where(x => x.MaDiaBan == madb);
       
            ViewData["TenDb"] = _db.DmHanhChinh.Where(x => x.MaDb == madb).FirstOrDefault()!.Name;
            ViewData["MaDb"] = madb;
            ViewData["MenuLv1"] = "hethong";
            ViewData["MenuLv2"] = "hethong_donvi";
            return View("Views/Admin/Systems/HeThongChung/DonVi/Detail.cshtml",model);
        }

        [Route("DonVi/Store")]
        [HttpPost]
        public IActionResult Store(DmDonvi request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "DonVi", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var madv = DateTime.Now.ToString("yyyyMMddHHmmss");
 
            var model = new DmDonvi
            {
                MaDonVi = madv,
                MaDiaBan = request.MaDiaBan, 
                TenDv = request.TenDv,
                TenDvHienThi = request.TenDv,
                DiaChi = request.DiaChi,
                DiaDanh = request.DiaDanh,
                ChucVuKy = request.ChucVuKy,
                NguoiKy = request.NguoiKy,
                TtLienHe = request.TtLienHe,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            var madb = request.MaDiaBan;
            _db.DmDonvi.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Detail", "DonVi", new { madb});
        }

        [Route("DonVi/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {

            var DmDonvi = _db.DmDonvi;
            var model = DmDonvi.FirstOrDefault(p => p.Id == id);
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên đơn vị</b><span class='require' >*</span></label>";
                result += "<input type='text' id='tendv' name='tendv' value='" + model.TenDv + "' class='form-control' required />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Địa chỉ trụ sở</b></label>";
                result += "<input type='text' id='diachi' name='diachi' value='" + model.DiaChi + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Địa danh</b></label>";
                result += "<input type='text' id='diadanh' name='diadanh' value='" + model.DiaDanh + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Chức vụ người ký</b></label>";
                result += "<input type='text' id='chucvuky' name='chucvuky' value='" + model.ChucVuKy + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Họ tên nguời ký</b></label>";
                result += "<input type='text' id='nguoiky' name='nguoiky' value='" + model.NguoiKy + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Thông tin liên hệ</b></label>";
                result += "<input type='text' id='ttlienhe' name='ttlienhe' value='" + model.TtLienHe + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<input hidden type='text' id='Id' name='Id' value='" + model.Id + "'/>";
                result += "<input hidden type='text' id='madiaban' name='madiaban' value='" + model.MaDiaBan + "'/>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [Route("DonVi/Update")]
        [HttpPost]
        public IActionResult Update(DmDonvi request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "DonVi", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DmDonvi.FirstOrDefault(t => t.Id == request.Id);
            var madb = request.MaDiaBan;
            if (model != null)
            {
                model.TenDv = request.TenDv;
                model.DiaChi = request.DiaChi;
                model.DiaDanh = request.DiaDanh;
                model.ChucVuKy = request.ChucVuKy;
                model.NguoiKy = request.NguoiKy;
                model.TtLienHe = request.TtLienHe;
                model.Updated_at = DateTime.Now;
                _db.DmDonvi.Update(model);
                _db.SaveChanges();
            }

            return RedirectToAction("Detail", "DonVi", new { madb });
        }

        [Route("DonVi/Delete")]
        [HttpPost]
        public IActionResult Delete(int Id_delete,string madb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!Helpers.CheckChucNang(HttpContext.Session, "DonVi", "ThayDoi"))
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

            var model = _db.DmDonvi.FirstOrDefault(t => t.Id == Id_delete);
            if (model != null)
            {
                _db.DmDonvi.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Detail", "DonVi", new {madb});
        }
    }
}
