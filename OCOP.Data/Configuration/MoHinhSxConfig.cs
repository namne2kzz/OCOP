using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class MoHinhSxConfig : IEntityTypeConfiguration<MoHinhSX>
    {
        public void Configure(EntityTypeBuilder<MoHinhSX> builder)
        {
            builder.ToTable("MoHinhSX");
            builder.HasKey(x => x.MoHinhSXId);
            builder.Property(x => x.MoHinhSXId).UseIdentityColumn();
            builder.Property(x => x.TenMoHinhSX).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Mota).IsRequired();
        }
    }
}
