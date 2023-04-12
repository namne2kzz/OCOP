using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OCOP.Data.Configuration;
using OCOP.Data.Entities;
using OCOP.Data.Seeding;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Context
{
    public class OCOPDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public OCOPDbContext(DbContextOptions options)
          : base(options)
        {
        }

        public virtual DbSet<HoiDong> HoiDongs { get; set; }
        public virtual DbSet<HoiDongThanhVien> HoiDongThanhViens { get; set; }
        public virtual DbSet<HoSo> HoSos { get; set; }
        public virtual DbSet<HoiDongTieuChi> HoiDongTieuChis { get; set; }
        public virtual DbSet<Huyen> Huyens { get; set; }
        public virtual DbSet<MoHinhSX> MoHinhSXes { get; set; }
        public virtual DbSet<Nganh> Nganhs { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<Nhom> Nhoms { get; set; }
        public virtual DbSet<PhanNhom> PhanNhoms { get; set; }
        public virtual DbSet<TapTinDinhKem> TapTinDinhKems { get; set; }
        public virtual DbSet<ThanhVien> ThanhViens { get; set; }
        public virtual DbSet<TieuChi> TieuChis { get; set; }
        public virtual DbSet<Xa> Xas { get; set; }
        public virtual DbSet<LoaiTieuChi> LoaiTieuChis { get; set; }
        public virtual DbSet<TieuChiChiTiet> TieuChiChiTiets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HoiDongConfig());
            modelBuilder.ApplyConfiguration(new HoiDongThanhVienConfig());
            modelBuilder.ApplyConfiguration(new HoSoConfig());
            modelBuilder.ApplyConfiguration(new HoiDongTieuChiConfig());
            modelBuilder.ApplyConfiguration(new HuyenConfig());
            modelBuilder.ApplyConfiguration(new MoHinhSxConfig());
            modelBuilder.ApplyConfiguration(new NganhConfig());
            modelBuilder.ApplyConfiguration(new NguoiDungConfig());
            modelBuilder.ApplyConfiguration(new NhomConfig());
            modelBuilder.ApplyConfiguration(new PhanNhomConfig());
            modelBuilder.ApplyConfiguration(new TapTinDinhKemConfig());
            modelBuilder.ApplyConfiguration(new ThanhVienConfig());
            modelBuilder.ApplyConfiguration(new TieuChiChiTietConfig());
            modelBuilder.ApplyConfiguration(new TieuChiConfig());
            modelBuilder.ApplyConfiguration(new XaConfig());
            modelBuilder.ApplyConfiguration(new LoaiTieuuChiConfig());

            modelBuilder.ApplyConfiguration(new AppUserConfig());
            modelBuilder.ApplyConfiguration(new AppRoleConfig());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            //data seeding
            modelBuilder.Seed();
        }
    }
}
