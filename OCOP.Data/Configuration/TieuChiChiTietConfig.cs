using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Configuration
{
    public class TieuChiChiTietConfig : IEntityTypeConfiguration<TieuChiChiTiet>
    {
        public void Configure(EntityTypeBuilder<TieuChiChiTiet> builder)
        {
            builder.ToTable("TieuChiChiTiet");
            builder.HasKey(x => x.TieuChiChiTietId);
            builder.Property(x => x.TieuChiChiTietId).UseIdentityColumn();
            builder.Property(x => x.Mota).IsRequired().HasMaxLength(500);        

            builder.HasOne(x => x.TieuChi).WithMany(x => x.TieuChiChiTiets).HasForeignKey(x => x.TieuChiId);
        }
    }
}
