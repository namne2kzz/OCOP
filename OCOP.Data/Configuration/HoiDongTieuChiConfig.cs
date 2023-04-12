using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class HoiDongTieuChiConfig : IEntityTypeConfiguration<HoiDongTieuChi>
    {
        public void Configure(EntityTypeBuilder<HoiDongTieuChi> builder)
        {
            builder.ToTable("HoiDongTieuChi");
            builder.HasKey(x => x.HoiDongTieuChiId);
            builder.Property(x => x.GhiChu).HasMaxLength(500);
            builder.Property(x => x.Diem).HasColumnType("decimal(18,2)");
         
        }
    }
}
