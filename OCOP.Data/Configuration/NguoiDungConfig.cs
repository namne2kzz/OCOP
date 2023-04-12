using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class NguoiDungConfig : IEntityTypeConfiguration<NguoiDung>
    {
        public void Configure(EntityTypeBuilder<NguoiDung> builder)
        {
            builder.ToTable("NguoiDung");
            builder.HasKey(x => x.AppUserId);
            builder.Property(x => x.DiaChiChiTiet).IsRequired();
            builder.Property(x => x.SoDangKyKinhDoanh).IsRequired().HasMaxLength(10);
            builder.Property(x => x.TenNguoiDaiDien).IsRequired().HasMaxLength(255);
            builder.Property(x => x.TenNhaSanXuat).IsRequired().HasMaxLength(255);

            builder.HasOne(x => x.Xa).WithMany(x => x.NguoiDungs).HasForeignKey(x => x.XaId);
            builder.HasOne(x => x.MoHinhSX).WithMany(x => x.NguoiDungs).HasForeignKey(x => x.MoHinhSXId);
            builder.HasOne<AppUser>(x => x.AppUser).WithOne(x => x.NguoiDung).HasForeignKey<NguoiDung>(x => x.AppUserId);
        }
    }
}
