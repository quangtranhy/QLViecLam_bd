﻿using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class ViTriViecLam
    {
        [Key]
        public int Id { get; set; }
        public string? MaViTriViecLam { get; set; }
        public string? TenViTriViecLam { get; set; }
        public string? TrangThai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}