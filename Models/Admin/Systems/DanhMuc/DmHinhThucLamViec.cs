﻿using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Systems.DanhMuc
{
    public class DmHinhThucLamViec
    {
        [Key]
        public int Id { get; set; }
        public string? TenDm { get; set; }
        public string? Stt { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}