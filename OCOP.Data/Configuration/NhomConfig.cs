using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class NhomConfig : IEntityTypeConfiguration<Nhom>
    {
        public void Configure(EntityTypeBuilder<Nhom> builder)
        {
            builder.ToTable("Nhom");
            builder.HasKey(x => x.NhomId);
            builder.Property(x => x.NhomId).UseIdentityColumn();
            builder.Property(x => x.TenNhom).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Mota).IsRequired();

            builder.HasOne(x => x.Nganh).WithMany(x => x.Nhoms).HasForeignKey(x => x.NganhId);

        }
    }
}
