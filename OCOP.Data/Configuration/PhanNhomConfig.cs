using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class PhanNhomConfig : IEntityTypeConfiguration<PhanNhom>
    {
        public void Configure(EntityTypeBuilder<PhanNhom> builder)
        {
            builder.ToTable("PhanNhom");
            builder.HasKey(x => x.PhanNhomId);
            builder.Property(x => x.PhanNhomId).UseIdentityColumn();
            builder.Property(x => x.TenPhanNhom).IsRequired();
            builder.Property(x => x.Mota).IsRequired();

            builder.HasOne(x => x.Nhom).WithMany(x => x.PhanNhoms).HasForeignKey(x => x.NhomId);
        }
    }
}
