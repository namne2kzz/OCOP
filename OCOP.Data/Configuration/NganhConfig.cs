using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class NganhConfig : IEntityTypeConfiguration<Nganh>
    {
        public void Configure(EntityTypeBuilder<Nganh> builder)
        {
            builder.ToTable("Nganh");
            builder.HasKey(x => x.NganhId);
            builder.Property(x => x.NganhId).UseIdentityColumn();
            builder.Property(x => x.TenNganh).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Mota).IsRequired();
        }
    }
}
