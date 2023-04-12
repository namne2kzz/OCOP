using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class HoiDongThanhVienConfig : IEntityTypeConfiguration<HoiDongThanhVien>
    {
        public void Configure(EntityTypeBuilder<HoiDongThanhVien> builder)
        {
            builder.ToTable("HoiDongThanhVien");
            builder.HasKey(x => x.HoiDongThanhVienId);          

            //builder.HasOne(x => x.ThanhVien).WithMany(x => x.HoiDongThanhViens).HasForeignKey(x => x.AppUserId);
            //builder.HasOne(x => x.HoiDong).WithMany(x => x.HoiDongThanhViens).HasForeignKey(x => x.HoiDongId);
        }
    }
}
