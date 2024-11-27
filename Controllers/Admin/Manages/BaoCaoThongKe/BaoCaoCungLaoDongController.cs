using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Manages;
using QLViecLam.Models.Admin.Systems.DanhMuc;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using QLViecLam.ViewModels.Admin.Manages.ThongTinThiTruongLD;
using QLViecLam.ViewModels.Admin.Manages;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace QLViecLam.Controllers.Admin.Manages.BaoCaoThongKe
{
    public class BaoCaoCungLaoDongController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BaoCaoCungLaoDongController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("BaoCaoCungLaoDong")]
        public IActionResult Index()
        {
            var model = _db.KeHoachThuThapThongTin;
            var DsKyDieuTra = model.Select(k => k.KeHoach).Distinct().OrderByDescending(k => k).ToList();
            var user = Helpers.GetSsAdminKey(HttpContext.Session, "Id");
            var DmHanhChinh = _db.DmHanhChinh;
            var DsHuyen = DmHanhChinh.Where(x => x.CapDo == "H");
            var DsXa = DmHanhChinh.Where(x => x.CapDo == "X");
            ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "1");
            ViewData["TrinhDoHV"] = _db.TrinhDoHV.Where(x => x.TrangThai == "1");
            ViewData["NganhHoc"] = _db.NganhHoc.Where(x => x.TrangThai == "1");
            ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x => x.TrangThai == "1");
            ViewData["DoiTuongUuTien"] = _db.DoiTuongUuTien.Where(x => x.TrangThai == "1");
            ViewData["DanToc"] = _db.DanToc.Where(x => x.TrangThai == "1");
            ViewData["DsXa"] = DsXa;
            ViewData["DsHuyen"] = DsHuyen;
            ViewData["DsKyDieuTra"] = DsKyDieuTra;
            ViewData["user"] = user;
            ViewData["Title"] = "Báo cáo thống kê";
            ViewData["MenuLv1"] = "baocao";
            ViewData["MenuLv2"] = "baocao_cung";
            return View("Views/Admin/Manages/BaoCaoThongKe/BaoCaoCungLaoDong/Index.cshtml");
        }

        [HttpGet("BaoCaoCungLaoDong/Mau01b")]
        public IActionResult Mau01b(string kydieutra, string huyen, string xa)
        {
            var model = _db.NhanKhau.AsQueryable();
            var DmHanhChinh = _db.DmHanhChinh;
            if (kydieutra != null)
            {
                model = model.Where(x => x.KyDieuTra == kydieutra);
            }
            if (huyen != null)
            {
                if (xa != null)
                {
                    model = model.Where(x => x.MaDiaBan == xa);
                    ViewData["DsDiaBan"] = DmHanhChinh.Where(x => x.MaDb == xa);
                    ViewData["Xa"] = DmHanhChinh.Where(x => x.MaDb == xa).FirstOrDefault()!.Name;
                }
                else
                {
                    var DsXa = DmHanhChinh.Where(x => x.Parent == huyen).Select(x => x.MaDb).ToList();
                    model = model.Where(x => DsXa.Contains(x.MaDiaBan));
                    ViewData["DsDiaBan"] = DmHanhChinh.Where(x => x.Parent == huyen);
                }
                ViewData["Huyen"] = DmHanhChinh.Where(x => x.MaDb == huyen).FirstOrDefault()!.Name;
            }
            else
            {

                ViewData["DsDiaBan"] = DmHanhChinh.Where(x => x.CapDo == "H");
            }
            ViewData["Tinh"] = DmHanhChinh.Where(x => x.CapDo == "T").FirstOrDefault()!.Name;
            ViewData["DsHuyen"] = DmHanhChinh.Where(x => x.CapDo == "H");
            ViewData["DsXa"] = DmHanhChinh.Where(x => x.CapDo == "X");
            ViewData["DoiTuongUuTien"] = _db.DoiTuongUuTien.Where(x=>x.TrangThai == "1"); 
            ViewData["TrinhDoHV"] = _db.TrinhDoHV.Where(x => x.TrangThai == "1");
            ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "1");
            ViewData["kydieutra"] = kydieutra;
            ViewData["Title"] = "Báo cáo tổng hợp cung lao động";
            return View("Views/Admin/Manages/BaoCaoThongKe/BaoCaoCungLaoDong/Mau01b.cshtml", model);
        }

        [HttpGet("BaoCaoCungLaoDong/Mau03")]
        public IActionResult Mau03(string kydieutra, string huyen, string xa)
        {

            var model = _db.NhanKhau.AsQueryable();
            var kydieutra_truoc = (Convert.ToInt16(kydieutra) - 1).ToString();
            var KyTruoc = model.Where(x => x.KyDieuTra == kydieutra_truoc);
            var KyBaoCao = model.Where(x => x.KyDieuTra == kydieutra);
            var DmHanhChinh = _db.DmHanhChinh;
            //if (kydieutra != null)
            //{
            //    model = model.Where(x => x.KyDieuTra == kydieutra);
            //}
            if (huyen != null)
            {
                if (xa != null)
                {
                    KyTruoc = KyTruoc.Where(x => x.MaDiaBan == xa);
                    KyBaoCao = KyBaoCao.Where(x => x.MaDiaBan == xa);
                    ViewData["DsDiaBan"] = DmHanhChinh.Where(x => x.MaDb == xa);
                    ViewData["Xa"] = DmHanhChinh.Where(x => x.MaDb == xa).FirstOrDefault()!.Name;
                }
                else
                {
                    var DsXa = DmHanhChinh.Where(x => x.Parent == huyen).Select(x => x.MaDb).ToList();
                    KyTruoc = KyTruoc.Where(x => DsXa.Contains(x.MaDiaBan));
                    KyBaoCao = KyBaoCao.Where(x => DsXa.Contains(x.MaDiaBan));
                    ViewData["DsDiaBan"] = DmHanhChinh.Where(x => x.Parent == huyen);
                }
                ViewData["Huyen"] = DmHanhChinh.Where(x => x.MaDb == huyen).FirstOrDefault()!.Name;
            }
            else
            {

                ViewData["DsDiaBan"] = DmHanhChinh.Where(x => x.CapDo == "H");
            }

            ViewData["KyTruoc"] = KyTruoc;
            ViewData["KyBaoCao"] = KyBaoCao;
            ViewData["Tinh"] = DmHanhChinh.Where(x => x.CapDo == "T").FirstOrDefault()!.Name;
            ViewData["DsHuyen"] = DmHanhChinh.Where(x => x.CapDo == "H");
            ViewData["DsXa"] = DmHanhChinh.Where(x => x.CapDo == "X");
            ViewData["DoiTuongUuTien"] = _db.DoiTuongUuTien.Where(x=>x.TrangThai == "1");
            ViewData["NganhHoc"] = _db.NganhHoc.Where(x => x.TrangThai == "1");
            ViewData["TrinhDoCMKT"] = _db.TrinhDoCMKT.Where(x => x.TrangThai == "1");
            ViewData["kydieutra"] = kydieutra;
            ViewData["Title"] = "Báo cáo thông tin cung lao động";
            return View("Views/Admin/Manages/BaoCaoThongKe/BaoCaoCungLaoDong/Mau03.cshtml");
        }


        [HttpGet("BaoCaoCungLaoDong/BcTuyChon")]
        public IActionResult BcTuyChon(NhanKhau request, string kydieutra, string huyen, string xa,
            string gioitinh_tuybien, string dantoc_tuybien, string uutien_tuybien, string tinhtrangvl_tuybien, string trinhdohv_tuybien,
            string trinhdocmkt_tuybien, string doituongtkvl_tuybien, string vlmongmuon_tuybien, string nghemuonhoc_tuybien)
        {
            var model = _db.NhanKhau.AsQueryable();
            var DmHanhChinh = _db.DmHanhChinh;
            if (xa != null)
            {
                model = model.Where(x => x.MaDiaBan == xa);
                ViewData["Xa"] = DmHanhChinh.Where(x => x.MaDb == xa).FirstOrDefault()!.Name;
            }
            else
            {
                var DsXa = DmHanhChinh.Where(x => x.Parent == huyen).Select(x => x.MaDb).ToList();
                model = model.Where(x => DsXa.Contains(x.MaDiaBan));
            }

            if (gioitinh_tuybien == "1")
            {
                model = model.Where(x => x.Gioitinh == request.Gioitinh);
            }
            if (dantoc_tuybien == "1")
            {
                model = model.Where(x => x.DanToc == request.DanToc);
            }
            if (uutien_tuybien == "1")
            {
                model = model.Where(x => x.UuTien == request.UuTien);
            }
            if (tinhtrangvl_tuybien == "1")
            {
                model = model.Where(x => x.TinhTrangVL == request.TinhTrangVL);
            }
            if (trinhdohv_tuybien == "1")
            {
                model = model.Where(x => x.TrinhDoHV == request.TrinhDoHV);
            }
            if (trinhdocmkt_tuybien == "1")
            {
                model = model.Where(x => x.TrinhDoCMKT == request.TrinhDoCMKT);
            }
            if (doituongtkvl_tuybien == "1")
            {
                model = model.Where(x => x.DoiTuongTimViecLam == request.DoiTuongTimViecLam);
            }
            if (vlmongmuon_tuybien == "1")
            {
                model = model.Where(x => x.ViecLamMongMuon == request.ViecLamMongMuon);
            }
            if (nghemuonhoc_tuybien == "1")
            {
                model = model.Where(x => x.NganhNgheMongMuon == request.NganhNgheMongMuon);
            }
            var model_list = model.ToList();
            var DoiTuongUuTien = _db.DoiTuongUuTien.Where(x=>x.TrangThai == "1");
            var DanToc = _db.DanToc.Where(x => x.TrangThai == "1").ToList();
            var TrinhDoHV = _db.TrinhDoHV.Where(x => x.TrangThai == "1").ToList();
            var TrinhDoCMKT = _db.TrinhDoCMKT.Where(x => x.TrangThai == "1").ToList();
            var NganhHoc = _db.NganhHoc.Where(x => x.TrangThai == "1").ToList();

            var newmodel = (from m in model_list
                            join u in DoiTuongUuTien on m.UuTien equals u.MaDoiTuongUuTien into details_0
                            from de_0 in details_0.DefaultIfEmpty()
                            join d in DanToc on m.DanToc equals d.MaDanToc into details_1
                            from de_1 in details_1.DefaultIfEmpty()
                            join hv in TrinhDoHV on m.TrinhDoHV equals hv.MaTrinhDoHV into details_2
                            from de_2 in details_2.DefaultIfEmpty()
                            join cmkt in TrinhDoCMKT on m.TrinhDoCMKT equals cmkt.MaTrinhDoCMKT into details_3
                            from de_3 in details_3.DefaultIfEmpty()
                            join nh in NganhHoc on m.NganhNgheMuonHoc equals nh.MaNganhHoc into details_4
                            from de_4 in details_4.DefaultIfEmpty()
                            join nh in NganhHoc on m.NganhNgheMongMuon equals nh.MaNganhHoc into details_5
                            from de_5 in details_4.DefaultIfEmpty()
                            select new VM_NhanKhau
                            {
                                HoVaTen = m.HoVaTen,
                                Gioitinh = m.Gioitinh,
                                SoCCCD = m.SoCCCD,
                                NgayThangNamSinh = m.NgayThangNamSinh,
                                KhuVuc = m.KhuVuc,
                                Sdt = m.Sdt,
                                //NoiOHienTai = m.NoiOHienTai,
                                TenUuTien = de_0 != null ? de_0.TenDoiTuongUuTien : null,
                                TenDanToc = de_1 != null ? de_1.TenDanToc : null,
                                TenTrinhDoHV = de_2 != null ? de_2.TenTrinhDoHV : null,
                                TenTrinhDoCMKT = de_3 != null ? de_3.TenTrinhDoCMKT : null,
                                DoiTuongTimViecLam = m.DoiTuongTimViecLam,
                                ViecLamMongMuon = m.ViecLamMongMuon,
                                ThiTruongLamViec = m.ThiTruongLamViec,
                                TenNganhHoc = de_4 != null ? de_4.TenNganhHoc : null,
                                TenNganhNgheMongMuon = de_5 != null ? de_5.TenNganhHoc : null,
                                TrinhDoChuyenMonMuonHoc = m.TrinhDoChuyenMonMuonHoc,

                                TinhTrangVL = m.TinhTrangVL,
                                CongViecCuThe = m.CongViecCuThe,
                                NoiLamViec = m.NoiLamViec,
                                DiaChiNoiLamViec = m.DiaChiNoiLamViec,
                                TenChuyenNganh = m.ChuyenNganh,

                                MaDiaBan = m.MaDiaBan,
                            });



            ViewData["Huyen"] = DmHanhChinh.Where(x => x.MaDb == huyen).FirstOrDefault()!.Name;
            ViewData["Tinh"] = DmHanhChinh.Where(x => x.CapDo == "T").FirstOrDefault()!.Name;
            ViewData["DsXa"] = DmHanhChinh.Where(x => x.CapDo == "X");

            ViewData["kydieutra"] = kydieutra;
            ViewData["Title"] = "Báo cáo thông tin cung lao động";
            return View("Views/Admin/Manages/BaoCaoThongKe/BaoCaoCungLaoDong/BcTuyChon.cshtml", newmodel);
        }


        [HttpGet("BaoCaoCungLaoDong/BcThongKeHanhChinh")]
        public IActionResult BcThongKeHanhChinh(string KyDieuTra)
        {
            var model = _db.NhanKhau.Where(x => x.KyDieuTra == KyDieuTra).Select(x => new VM_BcThongKeHanhChinh_NhanKhau { Id = x.Id, MaDiaBan = x.MaDiaBan, TinhTrangVL =x.TinhTrangVL }).ToList();
            var DmHanhChinh = _db.DmHanhChinh;
            ViewData["DsHuyen"] = DmHanhChinh.Where(x=>x.CapDo == "H");
            ViewData["DsXa"] = DmHanhChinh.Where(x => x.CapDo == "X");
            ViewData["TinhTrangVL"] = _db.TinhTrangVL.Where(x=>x.TrangThai == "kh");
            ViewData["Tinh"] = DmHanhChinh.Where(x => x.CapDo == "T").FirstOrDefault()!.Name;
            ViewData["Title"] = "Báo cáo thông tin cung lao động";
            return View("Views/Admin/Manages/BaoCaoThongKe/BaoCaoCungLaoDong/BcThongKeHanhChinh.cshtml", model);
        }


        [HttpGet("BaoCaoCungLaoDong/GetDsXa")]
        public JsonResult GetDsXa(string huyen)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var data = new { status = "error", message = "Phiên đăng nhập đã kết thúc! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }

            var model = _db.DmHanhChinh.Where(x => x.Parent == huyen);
            if (model != null)
            {
                string result = "<option value=''> Tất cả </option>";
                foreach (var xa in model)
                {
                    result += "<option value='" + xa.MaDb + "'>" + xa.Name + "</option>";
                }
                var data = new { status = "success", message = "Tìm thấy các xã thuộc địa bàn huyện", result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy các xã thuộc địa bàn huyện" };
                return Json(data);
            }
        }

    }
}
