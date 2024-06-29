﻿using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class DmTrangThaiViecLam
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string? MaTrangThaiViecLam { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string? TenTrangThaiViecLam { get; set; }
        public string? MoTa { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
