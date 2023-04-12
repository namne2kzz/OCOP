using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class XaConfig : IEntityTypeConfiguration<Xa>
    {
        public void Configure(EntityTypeBuilder<Xa> builder)
        {
            builder.ToTable("Xa");
            builder.HasKey(x => x.XaId);
            builder.Property(x => x.XaId).UseIdentityColumn();
            builder.Property(x => x.TenXa).IsRequired().HasMaxLength(255);

            builder.HasOne(x => x.Huyen).WithMany(x => x.Xas).HasForeignKey(x => x.HuyenId);
        }

    }
}
