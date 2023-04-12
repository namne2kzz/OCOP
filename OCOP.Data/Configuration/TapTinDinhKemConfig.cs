using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class TapTinDinhKemConfig : IEntityTypeConfiguration<TapTinDinhKem>
    {
        public void Configure(EntityTypeBuilder<TapTinDinhKem> builder)
        {
            builder.ToTable("TapTinDinhKem");
            builder.HasKey(x => x.TapTinId);
            builder.Property(x => x.TapTinId).UseIdentityColumn();
            builder.Property(x => x.TenTapTin).IsRequired().IsUnicode(false);

            builder.HasOne(x => x.HoSo).WithMany(x => x.TapTinDinhKems).HasForeignKey(x => x.HoSoId);

        }
    }
}
