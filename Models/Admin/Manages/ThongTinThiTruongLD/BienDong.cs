﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLViecLam.Models.Admin.Manages.ThongTinThiTruongLD
{
    public class BienDong
    {
        [Key]
        public int Id { get; set; }
        public string? KyDieuTra { get; set; }
        public int? User { get; set; }
        public string? PhanLoai { get; set; }
        public string? LoaiBang { get; set; }  //nhankhau - là biến động Cung, nguoilaodong - là biến động việc làm
        public int? SoLuong { get; set; }
        public string? MoTa { get; set; }
        public DateTime? ThoiDiem { get; set; }

        [NotMapped]
        public string? TenDn { get; set; }
    }
}