using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
    {

        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRole");

            builder.Property(x => x.Description).IsRequired().HasMaxLength(255);
        }
    }
}