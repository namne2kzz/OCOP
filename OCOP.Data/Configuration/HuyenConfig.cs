using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class HuyenConfig : IEntityTypeConfiguration<Huyen>
    {
        public void Configure(EntityTypeBuilder<Huyen> builder)
        {
            builder.ToTable("Huyen");
            builder.HasKey(x => x.HuyenId);
            builder.Property(x => x.HuyenId).UseIdentityColumn();
            builder.Property(x => x.TenHuyen).IsRequired().HasMaxLength(255);
        }
    }
}
