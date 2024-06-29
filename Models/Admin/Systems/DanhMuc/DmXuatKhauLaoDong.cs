﻿using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class DmXuatKhauLaoDong
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string? MaXuatKhauLaoDong { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string? TenXuatKhauLaoDong { get; set; }
        public string? MoTa { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
