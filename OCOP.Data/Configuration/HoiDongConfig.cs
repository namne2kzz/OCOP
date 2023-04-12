using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class HoiDongConfig : IEntityTypeConfiguration<HoiDong>
    {        
        public void Configure(EntityTypeBuilder<HoiDong> builder)
        {
            builder.ToTable("HoiDong");
            builder.HasKey(x => x.HoiDongId);
            builder.Property(x => x.TenHoiDong).IsRequired().HasMaxLength(500);

            builder.HasOne(x => x.HoSo).WithMany(x => x.HoiDongs).HasForeignKey(x => x.HoSoId);                       
        }
    }
}
