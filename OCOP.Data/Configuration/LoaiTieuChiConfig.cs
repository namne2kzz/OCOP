using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class LoaiTieuuChiConfig : IEntityTypeConfiguration<LoaiTieuChi>
    {
        public void Configure(EntityTypeBuilder<LoaiTieuChi> builder)
        {
            builder.ToTable("LoaiTieuChi");
            builder.HasKey(x => x.LoaiTieuChiId);
            builder.Property(x => x.TenLoaiTieuChi).IsRequired().HasMaxLength(50);           
        }
    }
}
