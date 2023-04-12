using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class HoSoConfig : IEntityTypeConfiguration<HoSo>
    {
        public void Configure(EntityTypeBuilder<HoSo> builder)
        {
            builder.ToTable("HoSo");
            builder.HasKey(x => x.HoSoId);
           
            builder.Property(x => x.TenHoSo).IsRequired().HasMaxLength(255);
            builder.Property(x => x.TenSanPham).IsRequired().HasMaxLength(255);
            builder.Property(x => x.GiayDangKyYTuongSanPham).IsRequired().HasMaxLength(50);
            builder.Property(x => x.GiayPhuongAnKeHoachKinhDoanh).IsRequired().HasMaxLength(50);
            builder.Property(x => x.GiayGioiThieuBoMayToChuc).IsRequired().HasMaxLength(50);
            builder.Property(x => x.GiayDangKyKinhDoanh).IsRequired().HasMaxLength(50);
            builder.Property(x => x.GiayDieuKienSanXuat).HasMaxLength(50);
            builder.Property(x => x.GiayCongBoChatLuong).HasMaxLength(50);
            builder.Property(x => x.GiayTieuChuanSanPham).HasMaxLength(50);
            builder.Property(x => x.GiayAnToanVSTP).HasMaxLength(50);
            builder.Property(x => x.GiaySoHuuTriTue).HasMaxLength(50);
            builder.Property(x => x.GiayNguonGocNguyenLieu).HasMaxLength(50);
            builder.Property(x => x.GiayKeHoachBaoVeMT).HasMaxLength(50);
            builder.Property(x => x.GiayQLChatLuong).HasMaxLength(50);
            builder.Property(x => x.GiayHoatDongKeToan).HasMaxLength(50);
            builder.Property(x => x.GiayPhatTrienThiTruong).HasMaxLength(50);
            builder.Property(x => x.GiayCauChuyenSanPham).HasMaxLength(50);
            builder.Property(x => x.TaiLieuThanhTich).HasMaxLength(50);

            builder.HasOne(x => x.NguoiDung).WithMany(x => x.HoSos).HasForeignKey(x => x.AppUserId);
            builder.HasOne(x => x.PhanNhom).WithMany(x => x.HoSos).HasForeignKey(x => x.PhanNhomId);
        }
    }
}
