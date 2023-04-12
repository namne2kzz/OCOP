using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class TieuChiConfig : IEntityTypeConfiguration<TieuChi>
    {
        public void Configure(EntityTypeBuilder<TieuChi> builder)
        {
            builder.ToTable("TieuChi");
            builder.HasKey(x => x.TieuChiId);
            builder.Property(x => x.TieuChiId).UseIdentityColumn();
            builder.Property(x => x.TenTieuChi).IsRequired().HasMaxLength(255);
            builder.Property(x => x.GhiChu).HasMaxLength(500);

            builder.HasOne(x => x.PhanNhom).WithMany(x => x.TieuChis).HasForeignKey(x => x.PhanNhomId);
            builder.HasOne(x => x.LoaiTieuChi).WithMany(x => x.TieuChis).HasForeignKey(x => x.LoaiTieuChiId);

        }
    }
}
