﻿using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Manages.DanhMuc
{
    public class DmTonGiao
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string? MaTonGiao { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string? TenTonGiao { get; set; }
        public string? MoTa { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}