using QLViecLam.ViewModels.Helpers.DanhMucChung;

namespace QLViecLam.Helper
{
    public class DanhMucChung
    {
        public static List<VM_QuanHe> QuanHe()
        {
            List<VM_QuanHe> list = new List<VM_QuanHe> { };
            list.Add(new VM_QuanHe { MaQuanHe = 00, TenQuanHe = "Chưa có thông tin" });
            list.Add(new VM_QuanHe { MaQuanHe = 01, TenQuanHe = "Ông" });
            list.Add(new VM_QuanHe { MaQuanHe = 02, TenQuanHe = "Bà" });
            list.Add(new VM_QuanHe { MaQuanHe = 03, TenQuanHe = "Cha" });
            list.Add(new VM_QuanHe { MaQuanHe = 04, TenQuanHe = "Mẹ" });
            list.Add(new VM_QuanHe { MaQuanHe = 05, TenQuanHe = "Vợ" });
            list.Add(new VM_QuanHe { MaQuanHe = 06, TenQuanHe = "Chồng" });
            list.Add(new VM_QuanHe { MaQuanHe = 07, TenQuanHe = "Con" });
            list.Add(new VM_QuanHe { MaQuanHe = 08, TenQuanHe = "Anh" });
            list.Add(new VM_QuanHe { MaQuanHe = 09, TenQuanHe = "Chị" });
            list.Add(new VM_QuanHe { MaQuanHe = 10, TenQuanHe = "Em" });
            list.Add(new VM_QuanHe { MaQuanHe = 11, TenQuanHe = "Cháu ruột" });
            list.Add(new VM_QuanHe { MaQuanHe = 99, TenQuanHe = "Khác" });
            return list;
        }

        public static List<VM_GioiTinh> GioiTinh()
        {
            List<VM_GioiTinh> list = new List<VM_GioiTinh> { };
            list.Add(new VM_GioiTinh { MaGioiTinh = 0, TenGioiTinh = "Chưa có thông tin" });
            list.Add(new VM_GioiTinh { MaGioiTinh = 1, TenGioiTinh = "Nam" });
            list.Add(new VM_GioiTinh { MaGioiTinh = 2, TenGioiTinh = "Nữ" });
            return list;
        }


        public static List<VM_DoiTuongUT> DoiTuongUT()
        {
            List<VM_DoiTuongUT> list = new List<VM_DoiTuongUT> { };
            list.Add(new VM_DoiTuongUT { MaDoiTuongUT = 1, TenDoiTuongUT = "Người khuyết tật" });
            list.Add(new VM_DoiTuongUT { MaDoiTuongUT = 2, TenDoiTuongUT = "Thuộc hộ nghèo" });
            list.Add(new VM_DoiTuongUT { MaDoiTuongUT = 3, TenDoiTuongUT = "Thuộc hộ cận nghèo" });
            list.Add(new VM_DoiTuongUT { MaDoiTuongUT = 4, TenDoiTuongUT = "Thuộc hộ bị thu hồi đất" });
            list.Add(new VM_DoiTuongUT { MaDoiTuongUT = 5, TenDoiTuongUT = "Thân nhân của người có công với cách mạng" });
            list.Add(new VM_DoiTuongUT { MaDoiTuongUT = 6, TenDoiTuongUT = "Dân tộc thiểu số" });
            return list;
        }
        public static List<VM_LyDoKhongThamGiaHDKT> LyDoKhongThamGiaHDKT()
        {
            List<VM_LyDoKhongThamGiaHDKT> list = new List<VM_LyDoKhongThamGiaHDKT> { };
            list.Add(new VM_LyDoKhongThamGiaHDKT { MaLyDoKhongThamGiaHDKT = 1, TenLyDoKhongThamGiaHDKT = "Đi học" });
            list.Add(new VM_LyDoKhongThamGiaHDKT { MaLyDoKhongThamGiaHDKT = 2, TenLyDoKhongThamGiaHDKT = "Nội trợ" });
            list.Add(new VM_LyDoKhongThamGiaHDKT { MaLyDoKhongThamGiaHDKT = 3, TenLyDoKhongThamGiaHDKT = "Hưu trí" });
            list.Add(new VM_LyDoKhongThamGiaHDKT { MaLyDoKhongThamGiaHDKT = 4, TenLyDoKhongThamGiaHDKT = "Khuyết tật" });
            list.Add(new VM_LyDoKhongThamGiaHDKT { MaLyDoKhongThamGiaHDKT = 5, TenLyDoKhongThamGiaHDKT = "Khác" });
            return list;
        }

        public static List<VM_ThoiGianThatNghiep> ThoiGianThatNghiep()
        {
            List<VM_ThoiGianThatNghiep> list = new List<VM_ThoiGianThatNghiep> { };
            list.Add(new VM_ThoiGianThatNghiep { MaThoiGianThatNghiep = 1, TenThoiGianThatNghiep = "Dưới 3 tháng" });
            list.Add(new VM_ThoiGianThatNghiep { MaThoiGianThatNghiep = 2, TenThoiGianThatNghiep = "Từ 3 đến 6 tháng" });
            list.Add(new VM_ThoiGianThatNghiep { MaThoiGianThatNghiep = 3, TenThoiGianThatNghiep = "Từ 6 tháng đến 1 năm" });
            list.Add(new VM_ThoiGianThatNghiep { MaThoiGianThatNghiep = 4, TenThoiGianThatNghiep = "Trên 1 năm" });
            return list;
        }
        //public static List<VM_TinhTrangHonNhan> TinhTrangHonNhan()
        //{
        //    List<VM_TinhTrangHonNhan> list = new List<VM_TinhTrangHonNhan> { };
        //    list.Add(new VM_TinhTrangHonNhan { MaTinhTrangHonNhan = 0, TenTinhTrangHonNhan = "Chưa có thông tin" });
        //    list.Add(new VM_TinhTrangHonNhan { MaTinhTrangHonNhan = 1, TenTinhTrangHonNhan = "Chưa kết hôn" });
        //    list.Add(new VM_TinhTrangHonNhan { MaTinhTrangHonNhan = 2, TenTinhTrangHonNhan = "Đang có vợ/ chồng" });
        //    list.Add(new VM_TinhTrangHonNhan { MaTinhTrangHonNhan = 3, TenTinhTrangHonNhan = "Đã ly hôn hoặc góa vợ/chồng" });
        //    return list;
        //}

        //public static List<VM_PhanLoaiHo> PhanLoaiHo()
        //{
        //    List<VM_PhanLoaiHo> list = new List<VM_PhanLoaiHo> { };
        //    list.Add(new VM_PhanLoaiHo { MaPhanLoaiHo = 0, TenPhanLoaiHo = "Hộ nghèo" });
        //    list.Add(new VM_PhanLoaiHo { MaPhanLoaiHo = 1, TenPhanLoaiHo = "Hộ cận nghèo" });
        //    list.Add(new VM_PhanLoaiHo { MaPhanLoaiHo = 2, TenPhanLoaiHo = "Hộ không giàu" });
        //    return list;
        //}




    }
}
