using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class ThanhVienConfig : IEntityTypeConfiguration<ThanhVien>
    {
        public void Configure(EntityTypeBuilder<ThanhVien> builder)
        {
            builder.ToTable("ThanhVien");
            builder.HasKey(x => x.AppUserId);
            builder.Property(x => x.Ten).IsRequired().HasMaxLength(255);
            builder.Property(x => x.DonViCongTac).IsRequired().HasMaxLength(500);
            builder.Property(x => x.MaNhanVien).IsRequired().HasMaxLength(10);
           
            builder.HasOne<AppUser>(x => x.AppUser).WithOne(x => x.ThanhVien).HasForeignKey<ThanhVien>(x => x.AppUserId);
        }
    }
}
