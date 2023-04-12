using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCOP.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Huyen",
                columns: table => new
                {
                    HuyenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHuyen = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huyen", x => x.HuyenId);
                });

            migrationBuilder.CreateTable(
                name: "MoHinhSX",
                columns: table => new
                {
                    MoHinhSXId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMoHinhSX = table.Column<string>(maxLength: 255, nullable: false),
                    Mota = table.Column<string>(nullable: false),
                    TrangThai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoHinhSX", x => x.MoHinhSXId);
                });

            migrationBuilder.CreateTable(
                name: "Nganh",
                columns: table => new
                {
                    NganhId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNganh = table.Column<string>(maxLength: 255, nullable: false),
                    Mota = table.Column<string>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nganh", x => x.NganhId);
                });

            migrationBuilder.CreateTable(
                name: "ThanhVien",
                columns: table => new
                {
                    AppUserId = table.Column<Guid>(nullable: false),
                    Ten = table.Column<string>(maxLength: 255, nullable: false),
                    MaNhanVien = table.Column<string>(maxLength: 10, nullable: false),
                    DonViCongTac = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhVien", x => x.AppUserId);
                    table.ForeignKey(
                        name: "FK_ThanhVien_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Xa",
                columns: table => new
                {
                    XaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HuyenId = table.Column<int>(nullable: false),
                    TenXa = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xa", x => x.XaId);
                    table.ForeignKey(
                        name: "FK_Xa_Huyen_HuyenId",
                        column: x => x.HuyenId,
                        principalTable: "Huyen",
                        principalColumn: "HuyenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nhom",
                columns: table => new
                {
                    NhomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NganhId = table.Column<int>(nullable: false),
                    TenNhom = table.Column<string>(maxLength: 255, nullable: false),
                    Mota = table.Column<string>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nhom", x => x.NhomId);
                    table.ForeignKey(
                        name: "FK_Nhom_Nganh_NganhId",
                        column: x => x.NganhId,
                        principalTable: "Nganh",
                        principalColumn: "NganhId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    AppUserId = table.Column<Guid>(nullable: false),
                    MoHinhSXId = table.Column<int>(nullable: false),
                    XaId = table.Column<int>(nullable: false),
                    TenNhaSanXuat = table.Column<string>(maxLength: 255, nullable: false),
                    SoDangKyKinhDoanh = table.Column<string>(maxLength: 10, nullable: false),
                    DiaChiChiTiet = table.Column<string>(nullable: false),
                    TenNguoiDaiDien = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.AppUserId);
                    table.ForeignKey(
                        name: "FK_NguoiDung_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NguoiDung_MoHinhSX_MoHinhSXId",
                        column: x => x.MoHinhSXId,
                        principalTable: "MoHinhSX",
                        principalColumn: "MoHinhSXId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NguoiDung_Xa_XaId",
                        column: x => x.XaId,
                        principalTable: "Xa",
                        principalColumn: "XaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhanNhom",
                columns: table => new
                {
                    PhanNhomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhomId = table.Column<int>(nullable: false),
                    TenPhanNhom = table.Column<string>(nullable: false),
                    Mota = table.Column<string>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanNhom", x => x.PhanNhomId);
                    table.ForeignKey(
                        name: "FK_PhanNhom_Nhom_NhomId",
                        column: x => x.NhomId,
                        principalTable: "Nhom",
                        principalColumn: "NhomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoSo",
                columns: table => new
                {
                    HoSoId = table.Column<Guid>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    PhanNhomId = table.Column<int>(nullable: false),
                    TenHoSo = table.Column<string>(maxLength: 255, nullable: false),
                    TenSanPham = table.Column<string>(maxLength: 255, nullable: false),
                    GiayDangKyYTuongSanPham = table.Column<string>(maxLength: 50, nullable: false),
                    GiayPhuongAnKeHoachKinhDoanh = table.Column<string>(maxLength: 50, nullable: false),
                    GiayGioiThieuBoMayToChuc = table.Column<string>(maxLength: 50, nullable: false),
                    GiayDangKyKinhDoanh = table.Column<string>(maxLength: 50, nullable: false),
                    GiayDieuKienSanXuat = table.Column<string>(maxLength: 50, nullable: true),
                    GiayCongBoChatLuong = table.Column<string>(maxLength: 50, nullable: true),
                    GiayTieuChuanSanPham = table.Column<string>(maxLength: 50, nullable: true),
                    GiayAnToanVSTP = table.Column<string>(maxLength: 50, nullable: true),
                    GiaySoHuuTriTue = table.Column<string>(maxLength: 50, nullable: true),
                    GiayNguonGocNguyenLieu = table.Column<string>(maxLength: 50, nullable: true),
                    GiayKeHoachBaoVeMT = table.Column<string>(maxLength: 50, nullable: true),
                    GiayQLChatLuong = table.Column<string>(maxLength: 50, nullable: true),
                    GiayHoatDongKeToan = table.Column<string>(maxLength: 50, nullable: true),
                    GiayPhatTrienThiTruong = table.Column<string>(maxLength: 50, nullable: true),
                    GiayCauChuyenSanPham = table.Column<string>(maxLength: 50, nullable: true),
                    TaiLieuThanhTich = table.Column<string>(maxLength: 50, nullable: true),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DiemTBHuyen = table.Column<int>(nullable: false),
                    DanhGiaHuyen = table.Column<int>(nullable: false),
                    DiemTBTinh = table.Column<int>(nullable: false),
                    DanhGiaTinh = table.Column<int>(nullable: false),
                    KetQua = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSo", x => x.HoSoId);
                    table.ForeignKey(
                        name: "FK_HoSo_NguoiDung_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "NguoiDung",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoSo_PhanNhom_PhanNhomId",
                        column: x => x.PhanNhomId,
                        principalTable: "PhanNhom",
                        principalColumn: "PhanNhomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TieuChi",
                columns: table => new
                {
                    TieuChiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhanNhomId = table.Column<int>(nullable: false),
                    TenTieuChi = table.Column<string>(maxLength: 255, nullable: false),
                    DiemCanDuoi = table.Column<int>(nullable: false),
                    DiemCanTren = table.Column<int>(nullable: false),
                    GhiChu = table.Column<string>(maxLength: 500, nullable: true),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TieuChi", x => x.TieuChiId);
                    table.ForeignKey(
                        name: "FK_TieuChi_PhanNhom_PhanNhomId",
                        column: x => x.PhanNhomId,
                        principalTable: "PhanNhom",
                        principalColumn: "PhanNhomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoiDong",
                columns: table => new
                {
                    HoSoId = table.Column<Guid>(nullable: false),
                    TenHoiDong = table.Column<string>(maxLength: 500, nullable: false),
                    CapBac = table.Column<string>(maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoiDong", x => x.HoSoId);
                    table.ForeignKey(
                        name: "FK_HoiDong_HoSo_HoSoId",
                        column: x => x.HoSoId,
                        principalTable: "HoSo",
                        principalColumn: "HoSoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TapTinDinhKem",
                columns: table => new
                {
                    TapTinId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoSoId = table.Column<Guid>(nullable: false),
                    TenTapTin = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TapTinDinhKem", x => x.TapTinId);
                    table.ForeignKey(
                        name: "FK_TapTinDinhKem_HoSo_HoSoId",
                        column: x => x.HoSoId,
                        principalTable: "HoSo",
                        principalColumn: "HoSoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoSoTieuChi",
                columns: table => new
                {
                    HoSoId = table.Column<Guid>(nullable: false),
                    TieuChiId = table.Column<int>(nullable: false),
                    Diem = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoTieuChi", x => new { x.HoSoId, x.TieuChiId });
                    //table.ForeignKey(
                    //    name: "FK_HoSoTieuChi_HoSo_HoSoId",
                    //    column: x => x.HoSoId,
                    //    principalTable: "HoSo",
                    //    principalColumn: "HoSoId",
                    //    onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_HoSoTieuChi_TieuChi_TieuChiId",
                    //    column: x => x.TieuChiId,
                    //    principalTable: "TieuChi",
                    //    principalColumn: "TieuChiId",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TieuChiChiTiet",
                columns: table => new
                {
                    TieuChiChiTietId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuChiId = table.Column<int>(nullable: false),
                    Mota = table.Column<string>(maxLength: 500, nullable: false),
                    Diem = table.Column<int>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TieuChiChiTiet", x => x.TieuChiChiTietId);
                    table.ForeignKey(
                        name: "FK_TieuChiChiTiet_TieuChi_TieuChiId",
                        column: x => x.TieuChiId,
                        principalTable: "TieuChi",
                        principalColumn: "TieuChiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoiDongThanhVien",
                columns: table => new
                {
                    HoiDongId = table.Column<Guid>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoiDongThanhVien", x => new { x.HoiDongId, x.AppUserId });
                    //table.ForeignKey(
                    //    name: "FK_HoiDongThanhVien_ThanhVien_AppUserId",
                    //    column: x => x.AppUserId,
                    //    principalTable: "ThanhVien",
                    //    principalColumn: "AppUserId",
                    //    onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_HoiDongThanhVien_HoiDong_HoiDongId",
                    //    column: x => x.HoiDongId,
                    //    principalTable: "HoiDong",
                    //    principalColumn: "HoSoId",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "b914c1f4-f1ce-4531-8e0e-2a337827b0ee", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImagePath", "LockoutEnabled", "LockoutEnd", "NgayTao", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TrangThai", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "c1f0db34-9e59-4d7c-848d-d1021558862f", "hpnama18099@cusc.ctu.edu.vn", true, "avatar.jpg", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hpnama18099@cusc.ctu.edu.vn", "admin", "AQAAAAEAACcQAAAAEAFg38iRhKG8AYeHEaSn4Y1GnrGKLINfy/dnk7NRnrx6qhwOXnYnCBGgp9wigc1dQA==", null, false, "", false, false, "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), new Guid("8d04dce2-969a-435d-bba4-df3f325983dc") });

            migrationBuilder.InsertData(
                table: "ThanhVien",
                columns: new[] { "AppUserId", "DonViCongTac", "MaNhanVien", "Ten" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), "OCOP Administrator co.ltd", "ABCD1234", "Nguyen Van Admin" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_HoiDongThanhVien_AppUserId",
            //    table: "HoiDongThanhVien",
            //    column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HoSo_AppUserId",
                table: "HoSo",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HoSo_PhanNhomId",
                table: "HoSo",
                column: "PhanNhomId");

            migrationBuilder.CreateIndex(
                name: "IX_HoSoTieuChi_TieuChiId",
                table: "HoSoTieuChi",
                column: "TieuChiId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_MoHinhSXId",
                table: "NguoiDung",
                column: "MoHinhSXId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_XaId",
                table: "NguoiDung",
                column: "XaId");

            migrationBuilder.CreateIndex(
                name: "IX_Nhom_NganhId",
                table: "Nhom",
                column: "NganhId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanNhom_NhomId",
                table: "PhanNhom",
                column: "NhomId");

            migrationBuilder.CreateIndex(
                name: "IX_TapTinDinhKem_HoSoId",
                table: "TapTinDinhKem",
                column: "HoSoId");

            migrationBuilder.CreateIndex(
                name: "IX_TieuChi_PhanNhomId",
                table: "TieuChi",
                column: "PhanNhomId");

            migrationBuilder.CreateIndex(
                name: "IX_TieuChiChiTiet_TieuChiId",
                table: "TieuChiChiTiet",
                column: "TieuChiId");

            migrationBuilder.CreateIndex(
                name: "IX_Xa_HuyenId",
                table: "Xa",
                column: "HuyenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRole");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "HoiDongThanhVien");

            migrationBuilder.DropTable(
                name: "HoSoTieuChi");

            migrationBuilder.DropTable(
                name: "TapTinDinhKem");

            migrationBuilder.DropTable(
                name: "TieuChiChiTiet");

            migrationBuilder.DropTable(
                name: "ThanhVien");

            migrationBuilder.DropTable(
                name: "HoiDong");

            migrationBuilder.DropTable(
                name: "TieuChi");

            migrationBuilder.DropTable(
                name: "HoSo");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "PhanNhom");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "MoHinhSX");

            migrationBuilder.DropTable(
                name: "Xa");

            migrationBuilder.DropTable(
                name: "Nhom");

            migrationBuilder.DropTable(
                name: "Huyen");

            migrationBuilder.DropTable(
                name: "Nganh");
        }
    }
}
