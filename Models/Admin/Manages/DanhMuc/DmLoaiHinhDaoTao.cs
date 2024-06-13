﻿using System.ComponentModel.DataAnnotations;

namespace QLViecLam.Models.Admin.Manages.DanhMuc
{
    public class DmLoaiHinhDaoTao
    {
        [Key]
        public int Id { get; set; }
        public string? MaLoaiHinhDaoTao { get; set; }
        public string? TenLoaiHinhDaoTao { get; set; }
        public string? MoTa { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
