using Microsoft.AspNetCore.Http;
using OCOP.Business.Common;
using OCOP.Data.Context;
using OCOP.Data.Entities;
using OCOP.Utility;
using OCOP.ViewModel.Catalog.HoSos;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.HoSos
{
    public class HoSoService : IHoSoService
    {
        private readonly OCOPDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public HoSoService(OCOPDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _fileStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ResponseResult<bool>> CreateHoSo(HoSoCreateRequest request)
        {
            var hoSo = new HoSo()
            {
                HoSoId = Guid.NewGuid(),
                AppUserId = request.AppUserId,
                TenHoSo = request.TenHoSo,
                TenSanPham = request.TenSanPham,
                PhanNhomId = request.PhanNhomId,
                GiayDangKyYTuongSanPham = await this.SaveFile(request.GiayDangKyYTuongSanPham),
                GiayPhuongAnKeHoachKinhDoanh = await this.SaveFile(request.GiayPhuongAnKeHoachKinhDoanh),
                GiayGioiThieuBoMayToChuc = await this.SaveFile(request.GiayGioiThieuBoMayToChuc),
                GiayDangKyKinhDoanh = await this.SaveFile(request.GiayDangKyKinhDoanh),
                TrangThai = SystemConstants.HoSo_TrangThai_Pending_Huyen,
                NgayTao = DateTime.Now
            };
            if (request.GiayDieuKienSanXuat != null)
            {
                hoSo.GiayDieuKienSanXuat = await this.SaveFile(request.GiayDieuKienSanXuat);
            }
            if (request.GiayCongBoChatLuong != null)
            {
                hoSo.GiayCongBoChatLuong = await this.SaveFile(request.GiayCongBoChatLuong);
            }
            if (request.GiayTieuChuanSanPham != null)
            {
                hoSo.GiayTieuChuanSanPham = await this.SaveFile(request.GiayTieuChuanSanPham);
            }
            if (request.GiayAnToanVSTP != null)
            {
                hoSo.GiayAnToanVSTP = await this.SaveFile(request.GiayAnToanVSTP);
            }
            if (request.GiaySoHuuTriTue != null)
            {
                hoSo.GiaySoHuuTriTue = await this.SaveFile(request.GiaySoHuuTriTue);
            }
            if (request.GiayNguonGocNguyenLieu != null)
            {
                hoSo.GiayNguonGocNguyenLieu = await this.SaveFile(request.GiayNguonGocNguyenLieu);
            }
            if (request.GiayKeHoachBaoVeMT != null)
            {
                hoSo.GiayKeHoachBaoVeMT = await this.SaveFile(request.GiayKeHoachBaoVeMT);
            }
            if (request.GiayQLChatLuong != null)
            {
                hoSo.GiayQLChatLuong = await this.SaveFile(request.GiayQLChatLuong);
            }
            if (request.GiayHoatDongKeToan != null)
            {
                hoSo.GiayHoatDongKeToan = await this.SaveFile(request.GiayHoatDongKeToan);
            }
            if (request.GiayPhatTrienThiTruong != null)
            {
                hoSo.GiayPhatTrienThiTruong = await this.SaveFile(request.GiayPhatTrienThiTruong);
            }
            if (request.GiayCauChuyenSanPham != null)
            {
                hoSo.GiayCauChuyenSanPham = await this.SaveFile(request.GiayCauChuyenSanPham);
            }
            if (request.TaiLieuThanhTich != null)
            {
                hoSo.TaiLieuThanhTich = await this.SaveFile(request.TaiLieuThanhTich);
            }
            _context.HoSos.Add(hoSo);
            var result = await _context.SaveChangesAsync();
            if (result > 0) return new ResponseSuccessResult<bool>();
            return new ResponseErrorResult<bool>("Tạo hồ sơ không thành công");
        }

        public async Task<ResponseResult<List<HoSoViewModel>>> GetListHoSo()
        {
            var hoSos = _context.HoSos.ToList();
            var result = new List<HoSoViewModel>();
            foreach (var item in hoSos)
            {
                var phanNhom = await _context.PhanNhoms.FindAsync(item.PhanNhomId);
                var hoSoViewModel = new HoSoViewModel()
                {
                    AppUserId = item.AppUserId,
                    DanhGiaHuyen = item.DanhGiaHuyen,
                    DanhGiaTinh = item.DanhGiaTinh,
                    HoSoId = item.HoSoId,
                    KetQua = item.KetQua,
                    NgayTao = item.NgayTao,
                    PhanNhom = phanNhom.TenPhanNhom,
                    TenHoSo = item.TenHoSo,
                    TenSanPham = item.TenSanPham,
                    TrangThai = item.TrangThai
                };
                result.Add(hoSoViewModel);
            }
            return new ResponseSuccessResult<List<HoSoViewModel>>(result);
        }

        public async Task<ResponseResult<List<HoSoViewModel>>> GetListHoSoPending()
        {
            var hoSos = _context.HoSos.Where(x => x.TrangThai == SystemConstants.HoSo_TrangThai_Pending_Huyen || x.TrangThai == SystemConstants.HoSo_TrangThai_Pending_Tinh).ToList();
            var result = new List<HoSoViewModel>();
            foreach (var item in hoSos)
            {
                var hoiDongHuyen = _context.HoiDongs.Where(x => x.HoSoId == item.HoSoId && x.MaCapBac == SystemConstants.MaCapHuyen).SingleOrDefault();
                var hoiDongTinh = _context.HoiDongs.Where(x => x.HoSoId == item.HoSoId && x.MaCapBac == SystemConstants.MaCapTinh).SingleOrDefault();
                var phanNhom = await _context.PhanNhoms.FindAsync(item.PhanNhomId);
                var hoSoViewModel = new HoSoViewModel()
                {
                    AppUserId = item.AppUserId,
                    DanhGiaHuyen = item.DanhGiaHuyen,
                    DanhGiaTinh = item.DanhGiaTinh,
                    HoSoId = item.HoSoId,
                    KetQua = item.KetQua,
                    NgayTao = item.NgayTao,
                    PhanNhom = phanNhom.TenPhanNhom,
                    TenHoSo = item.TenHoSo,
                    TenSanPham = item.TenSanPham,
                    TrangThai = item.TrangThai,
                    IsExistHoiDongHuyen = hoiDongHuyen != null ? true : false,
                    IsExistHoiDongTinh = hoiDongTinh != null ? true : false
                };
                result.Add(hoSoViewModel);
            }
            return new ResponseSuccessResult<List<HoSoViewModel>>(result);
        }

        public async Task<ResponseResult<List<HoSoViewModel>>> GetListHoSoPended()
        {
            var hoSos = _context.HoSos.Where(x => x.TrangThai != SystemConstants.HoSo_TrangThai_Pending_Huyen || x.TrangThai != SystemConstants.HoSo_TrangThai_Pending_Tinh).ToList();
            var result = new List<HoSoViewModel>();
            foreach (var item in hoSos)
            {
                var hoiDong = await _context.HoiDongs.FindAsync(item.HoSoId);
                var phanNhom = await _context.PhanNhoms.FindAsync(item.PhanNhomId);
                var hoSoViewModel = new HoSoViewModel()
                {
                    AppUserId = item.AppUserId,
                    DanhGiaHuyen = item.DanhGiaHuyen,
                    DanhGiaTinh = item.DanhGiaTinh,
                    HoSoId = item.HoSoId,
                    KetQua = item.KetQua,
                    NgayTao = item.NgayTao,
                    PhanNhom = phanNhom.TenPhanNhom,
                    TenHoSo = item.TenHoSo,
                    TenSanPham = item.TenSanPham,
                    TrangThai = item.TrangThai,
                };
                result.Add(hoSoViewModel);
            }
            return new ResponseSuccessResult<List<HoSoViewModel>>(result);
        }

        public async Task<ResponseResult<HoSoDetailViewModel>> GetHoSoDetail(Guid hoSoId)
        {
            var hoSos = await _context.HoSos.FindAsync(hoSoId);
            var phanNhom = await _context.PhanNhoms.FindAsync(hoSos.PhanNhomId);
            var hoSoDetail = new HoSoDetailViewModel()
            {
                AppUserId = hoSos.AppUserId,
                HoSoId = hoSos.HoSoId,
                NgayTao = hoSos.NgayTao,
                PhanNhom = phanNhom.TenPhanNhom,
                TenHoSo = hoSos.TenHoSo,
                TenSanPham = hoSos.TenSanPham,
                GiayAnToanVSTP = hoSos.GiayAnToanVSTP,
                GiayCauChuyenSanPham = hoSos.GiayCauChuyenSanPham,
                GiayCongBoChatLuong = hoSos.GiayCongBoChatLuong,
                GiayDangKyKinhDoanh = hoSos.GiayDangKyKinhDoanh,
                GiayDangKyYTuongSanPham = hoSos.GiayDangKyYTuongSanPham,
                GiayDieuKienSanXuat = hoSos.GiayDieuKienSanXuat,
                GiayGioiThieuBoMayToChuc = hoSos.GiayGioiThieuBoMayToChuc,
                GiayHoatDongKeToan = hoSos.GiayHoatDongKeToan,
                GiayKeHoachBaoVeMT = hoSos.GiayKeHoachBaoVeMT,
                GiayNguonGocNguyenLieu = hoSos.GiayNguonGocNguyenLieu,
                GiayPhatTrienThiTruong = hoSos.GiayPhatTrienThiTruong,
                GiayPhuongAnKeHoachKinhDoanh = hoSos.GiayPhuongAnKeHoachKinhDoanh,
                GiayQLChatLuong = hoSos.GiayQLChatLuong,
                GiaySoHuuTriTue = hoSos.GiaySoHuuTriTue,
                GiayTieuChuanSanPham = hoSos.GiayTieuChuanSanPham,
                TaiLieuThanhTich = hoSos.TaiLieuThanhTich,
                TapTinDinhKems = _context.TapTinDinhKems.Where(x => x.HoSoId == hoSoId).Select(x => x.TenTapTin).ToList()
            };
            return new ResponseSuccessResult<HoSoDetailViewModel>(hoSoDetail);
        }

        public async Task<ResponseResult<HoSoViewModel>> GetHoSo(Guid hoSoId)
        {
            var hoSos = await _context.HoSos.FindAsync(hoSoId);
            var phanNhom = await _context.PhanNhoms.FindAsync(hoSos.PhanNhomId);
            var hoSo = new HoSoViewModel()
            {
                AppUserId = hoSos.AppUserId,
                HoSoId = hoSos.HoSoId,
                NgayTao = hoSos.NgayTao,
                PhanNhom = phanNhom.TenPhanNhom,
                TenHoSo = hoSos.TenHoSo,
                TenSanPham = hoSos.TenSanPham,
            };
            return new ResponseSuccessResult<HoSoViewModel>(hoSo);
        }

        public async Task<ResponseResult<List<HoSoReportViewModel>>> GetReportHoSoByTrangThai()
        {
            var listHoSoReport = new List<HoSoReportViewModel>();
            var hoSoPending = new HoSoReportViewModel()
            {
                Label = "Đang chờ duyệt",
                CountHoSoByTrangThai = _context.HoSos.Count(x => x.TrangThai == SystemConstants.HoSo_TrangThai_Pending_Huyen || x.TrangThai == SystemConstants.HoSo_TrangThai_Pending_Tinh)
            };
            listHoSoReport.Add(hoSoPending);
            var hoSoDoing = new HoSoReportViewModel()
            {
                Label = "Đang đánh giá",
                CountHoSoByTrangThai = _context.HoSos.Count(x => x.TrangThai == SystemConstants.HoSo_TrangThai_Doing_Huyen || x.TrangThai == SystemConstants.HoSo_TrangThai_Doing_Tinh)
            };
            listHoSoReport.Add(hoSoDoing);
            var hoSoPassHuyen = new HoSoReportViewModel()
            {
                Label = "Đậu huyện",
                CountHoSoByTrangThai = _context.HoSos.Count(x => x.TrangThai == SystemConstants.HoSo_TrangThai_Fail_Tinh)
            };
            listHoSoReport.Add(hoSoPassHuyen);
            var hoSoPassTinh = new HoSoReportViewModel()
            {
                Label = "Đậu tỉnh",
                CountHoSoByTrangThai = _context.HoSos.Count(x => x.TrangThai == SystemConstants.HoSo_TrangThai_Pass_Tinh)
            };
            listHoSoReport.Add(hoSoPassTinh);
            var hoSoFail = new HoSoReportViewModel()
            {
                Label = "Trượt",
                CountHoSoByTrangThai = _context.HoSos.Count(x => x.TrangThai == SystemConstants.HoSo_TrangThai_Fail_Huyen || x.TrangThai == SystemConstants.HoSo_TrangThai_Fail_Tinh || x.TrangThai == SystemConstants.HoSo_TrangThai_Fail)
            };
            listHoSoReport.Add(hoSoFail);
            return new ResponseSuccessResult<List<HoSoReportViewModel>>(listHoSoReport);
        }

        public async Task<ResponseResult<List<HoSoViewModel>>> GetListHoSoByAppuser(Guid appUserId)
        {
            var hoSos = _context.HoSos.Where(x => x.AppUserId == appUserId).ToList();
            var result = new List<HoSoViewModel>();
            foreach (var item in hoSos)
            {
                var phanNhom = await _context.PhanNhoms.FindAsync(item.PhanNhomId);
                var hoSoViewModel = new HoSoViewModel()
                {
                    AppUserId = item.AppUserId,
                    DanhGiaHuyen = item.DanhGiaHuyen,
                    DanhGiaTinh = item.DanhGiaTinh,
                    HoSoId = item.HoSoId,
                    KetQua = item.KetQua,
                    NgayTao = item.NgayTao,
                    PhanNhom = phanNhom.TenPhanNhom,
                    TenHoSo = item.TenHoSo,
                    TenSanPham = item.TenSanPham,
                    TrangThai = item.TrangThai
                };
                result.Add(hoSoViewModel);
            }
            return new ResponseSuccessResult<List<HoSoViewModel>>(result);
        }

        public async Task<ResponseResult<bool>> AddFileToHoSo(Guid hoSoId, IFormFile file)
        {
            var tapTinDinhKem = new TapTinDinhKem()
            {
                HoSoId = hoSoId,
                TenTapTin = await this.SaveFile(file)
            };
            _context.TapTinDinhKems.Add(tapTinDinhKem);
            var result = await _context.SaveChangesAsync();
            if (result > 0) return new ResponseSuccessResult<bool>();
            return new ResponseErrorResult<bool>("Thêm tài liệu cho hồ sơ thất bại");
        }
    }
}
