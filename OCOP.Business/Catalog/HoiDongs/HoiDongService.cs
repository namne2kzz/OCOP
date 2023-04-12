using Newtonsoft.Json;
using OCOP.Data.Context;
using OCOP.Data.Entities;
using OCOP.Utility;
using OCOP.ViewModel.Catalog.HoiDongs;
using OCOP.ViewModel.Catalog.TieuChis;
using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Catalog.HoiDongs
{
    public class HoiDongService : IHoiDongService
    {
        private readonly OCOPDbContext _context;
        public HoiDongService(OCOPDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<bool>> AddThanhVien(HoiDongThanhVienAssignRequest request)
        {
            var listThanhVienInHoiDong = _context.HoiDongThanhViens.Where(x => x.HoiDongId == request.HoiDongId).Select(x => x.AppUserId.ToString().ToLower()).ToList();

            foreach (var item in request.ThanhViens)
            {
                if (!listThanhVienInHoiDong.Contains(item.Id) && item.Selected == true)
                {
                    var hdtv = new HoiDongThanhVien()
                    {
                        AppUserId = new Guid(item.Id),
                        HoiDongThanhVienId = Guid.NewGuid(),
                        HoiDongId = request.HoiDongId,
                        ThanhvienAppUserId = Guid.NewGuid(),
                    };
                    _context.HoiDongThanhViens.Add(hdtv);
                }
                if (listThanhVienInHoiDong.Contains(item.Id) && item.Selected == false)
                {
                    var hdtv = _context.HoiDongThanhViens.Where(x => x.AppUserId == (new Guid(item.Id)) && x.HoiDongId == request.HoiDongId).SingleOrDefault();
                    _context.HoiDongThanhViens.Remove(hdtv);
                }
            }
            var result = await _context.SaveChangesAsync();
            if (result > 0) return new ResponseSuccessResult<bool>();
            return new ResponseErrorResult<bool>("Thêm thành viên không thành công");
        }

        public async Task<ResponseResult<bool>> CheckHoSoAnyHoiDong(Guid hoSoId, int maCapBac)
        {
            var result = _context.HoiDongs.Where(x => x.HoSoId == hoSoId && x.MaCapBac == maCapBac).SingleOrDefault();
            if (result == null) return new ResponseSuccessResult<bool>();
            return new ResponseErrorResult<bool>("Hồ sơ này đã tồn tại hội đồng");
        }

        public async Task<ResponseResult<bool>> CreateHoiDong(HoiDongCreateRequest request)
        {
            var hoiDong = new HoiDong()
            {
                HoiDongId = Guid.NewGuid(),
                MaCapBac = request.MaCapBac,
                HoSoId = request.HoSoId,
                TenHoiDong = request.TenHoiDong,
                TrangThai = SystemConstants.HoiDong_DangHoanThien,
                NgayTao = DateTime.Now,
                NgayKetThuc = DateTime.Now.AddDays(30)
            };
            _context.HoiDongs.Add(hoiDong);
            var result = await _context.SaveChangesAsync();
            if (result > 0) return new ResponseSuccessResult<bool>();
            return new ResponseErrorResult<bool>("Tạo hội dồng thất bại");
        }

        public async Task<ResponseResult<HoiDongViewModel>> GetHoiDong(Guid hoiDongId)
        {
            var hoiDong = await _context.HoiDongs.FindAsync(hoiDongId);
            var hoSo = await _context.HoSos.FindAsync(hoiDong.HoSoId);
            var hoiDongViewModel = new HoiDongViewModel()
            {
                CapBac = hoiDong.MaCapBac == 1 ? "Hội đồng cấp Huyện" : "Hội dồng cấp Tỉnh",
                MaCapBac = hoiDong.MaCapBac,
                HoSoId = hoiDong.HoSoId,
                NgayKetThuc = hoiDong.NgayKetThuc,
                NgayTao = hoiDong.NgayTao,
                TenHoiDong = hoiDong.TenHoiDong,
                TrangThai = hoiDong.TrangThai,
                TenHoSo = hoSo.TenHoSo,
                HoiDongId = hoiDong.HoiDongId
            };
            return new ResponseSuccessResult<HoiDongViewModel>(hoiDongViewModel);
        }

        public async Task<ResponseResult<HoiDongViewModel>> GetHoiDongByHoSoAndCapBac(Guid hoSoId, int maCapBac)
        {
            var hoiDong = _context.HoiDongs.Where(x => x.HoSoId == hoSoId && x.MaCapBac == maCapBac).SingleOrDefault();
            if (hoiDong == null) return new ResponseErrorResult<HoiDongViewModel>();
            var hoSo = await _context.HoSos.FindAsync(hoiDong.HoSoId);

            var hoiDongViewModel = new HoiDongViewModel()
            {
                CapBac = hoiDong.MaCapBac == 1 ? "Hội đồng cấp Huyện" : "Hội dồng cấp Tỉnh",
                MaCapBac = hoiDong.MaCapBac,
                HoSoId = hoiDong.HoSoId,
                NgayKetThuc = hoiDong.NgayKetThuc,
                NgayTao = hoiDong.NgayTao,
                TenHoiDong = hoiDong.TenHoiDong,
                TrangThai = hoiDong.TrangThai,
                HoiDongId = hoiDong.HoiDongId,
                TenHoSo = hoSo.TenHoSo
            };
            return new ResponseSuccessResult<HoiDongViewModel>(hoiDongViewModel);
        }

        public async Task<ResponseResult<List<HoiDongViewModel>>> GetListHoiDong()
        {
            var listHoiDong = _context.HoiDongs.ToList();
            var listHoiDongViewModel = new List<HoiDongViewModel>();
            foreach (var item in listHoiDong)
            {
                var hoSo = await _context.HoSos.FindAsync(item.HoSoId);
                var hoiDongViewModel = new HoiDongViewModel()
                {
                    HoiDongId = item.HoiDongId,
                    CapBac = item.MaCapBac == 1 ? "Hội đồng cấp Huyện" : "Hội dồng cấp Tỉnh",
                    MaCapBac = item.MaCapBac,
                    HoSoId = item.HoSoId,
                    NgayKetThuc = item.NgayKetThuc,
                    NgayTao = item.NgayTao,
                    TenHoiDong = item.TenHoiDong,
                    TrangThai = item.TrangThai,
                    TenHoSo = hoSo.TenHoSo
                };
                listHoiDongViewModel.Add(hoiDongViewModel);
            }
            return new ResponseSuccessResult<List<HoiDongViewModel>>(listHoiDongViewModel);
        }

        public async Task<ResponseResult<List<HoiDongViewModel>>> GetListHoiDongByAppUser(Guid appUserId)
        {
            var listHoiDong = _context.HoiDongThanhViens.Where(x => x.AppUserId == appUserId).ToList();
            var listHoiDongViewModel = new List<HoiDongViewModel>();
            foreach (var item in listHoiDong)
            {
                var hoidong = await _context.HoiDongs.FindAsync(item.HoiDongId);
                var hoSo = await _context.HoSos.FindAsync(hoidong.HoSoId);
                var hoiDongViewModel = new HoiDongViewModel()
                {
                    HoiDongId = hoidong.HoiDongId,
                    CapBac = hoidong.MaCapBac == SystemConstants.MaCapHuyen ? "Hội đồng cấp Huyện" : "Hội đồng cấp Tỉnh",
                    MaCapBac = hoidong.MaCapBac,
                    HoSoId = hoidong.HoSoId,
                    NgayKetThuc = hoidong.NgayKetThuc,
                    NgayTao = hoidong.NgayTao,
                    TenHoiDong = hoidong.TenHoiDong,
                    TrangThai = hoidong.TrangThai,
                    TenHoSo = hoSo.TenHoSo
                };
                listHoiDongViewModel.Add(hoiDongViewModel);
            }
            return new ResponseSuccessResult<List<HoiDongViewModel>>(listHoiDongViewModel);
        }

        public async Task<ResponseResult<bool>> LockHoiDong(Guid hoiDongId)
        {
            var hoiDong = await _context.HoiDongs.FindAsync(hoiDongId);
            if (hoiDong == null) return new ResponseErrorResult<bool>("Không tìm thấy hội đồng");
            hoiDong.TrangThai = SystemConstants.HoiDong_DangDanhGia;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var hoSo = await _context.HoSos.FindAsync(hoiDong.HoSoId);
                var listTieuChi = _context.TieuChis.Where(x => x.PhanNhomId == hoSo.PhanNhomId).ToList();
                foreach (var item in listTieuChi)
                {
                    var hoiDongTieuChi = new HoiDongTieuChi()
                    {
                        Diem = 0,
                        GhiChu = String.Empty,
                        HoiDongId = hoiDongId,
                        HoiDongTieuChiId = Guid.NewGuid(),
                        TieuChiId = item.TieuChiId
                    };
                    _context.HoiDongTieuChis.Add(hoiDongTieuChi);
                }
                var affectRow = await _context.SaveChangesAsync();
                if (affectRow > 0) return new ResponseSuccessResult<bool>();
                return new ResponseErrorResult<bool>("Lỗi không thể gán tiêu chí chấm điểm cho hội đồng");
            }
            return new ResponseErrorResult<bool>("Lỗi không thể chuyển trạng thái");
        }

        public async Task<ResponseResult<HoSo>> MarkHoSoByThanhVien(TieuChiMarkRequest request)
        {
            var hoiDongThanhVien = _context.HoiDongThanhViens.Where(x => x.AppUserId == request.AppUserId && x.HoiDongId == request.HoiDongId).SingleOrDefault();
            var diemNhomA = 0;
            var diemNhomB = 0;
            var diemNhomC = 0;
            foreach (var item in request.DanhGias)
            {
                var tieuChi = await _context.TieuChis.FindAsync(item.TieuChiId);
                if (tieuChi.LoaiTieuChiId == SystemConstants.TieuChi_NhomA_SucManhCongDong)
                {
                    diemNhomA += item.Diem;
                }
                else if (tieuChi.LoaiTieuChiId == SystemConstants.TieuChi_NhomB_KhaNangTiepThi)
                {
                    diemNhomB += item.Diem;
                }
                else
                {
                    diemNhomC += item.Diem;
                }
            }
            var jsonDanhGia = JsonConvert.SerializeObject(request.DanhGias);

            hoiDongThanhVien.DanhGia = jsonDanhGia;
            hoiDongThanhVien.DiemChatLuongSanPhan = diemNhomC;
            hoiDongThanhVien.DiemKhaNangTiepThi = diemNhomB;
            hoiDongThanhVien.DiemSucManhCongDong = diemNhomA;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                await this.UpdateDiemInHoiDongTieuChi(request);
                await this.CheckHoiDongDoneToUpdateDiemHoSo(request.HoiDongId);
                var hoiDong = await _context.HoiDongs.FindAsync(request.HoiDongId);
                var hoSo = await _context.HoSos.FindAsync(hoiDong.HoSoId);
                return new ResponseSuccessResult<HoSo>(hoSo);
            }
            return new ResponseErrorResult<HoSo>("Cập nhật điểm không thành công");
        }

        private async Task<bool> UpdateDiemInHoiDongTieuChi(TieuChiMarkRequest request)
        {
            var countMember = _context.HoiDongThanhViens.Where(x => x.HoiDongId == request.HoiDongId).Count();
            foreach (var item in request.DanhGias)
            {
                var hoiDongTieuChi = _context.HoiDongTieuChis.Where(x => x.HoiDongId == request.HoiDongId && x.TieuChiId == item.TieuChiId).SingleOrDefault();
                if (string.IsNullOrEmpty(hoiDongTieuChi.GhiChu))
                {
                    hoiDongTieuChi.GhiChu = item.GhiChu;
                }
                else if (!string.IsNullOrEmpty(item.GhiChu))
                {
                    hoiDongTieuChi.GhiChu = hoiDongTieuChi.GhiChu + "-AND-" + item.GhiChu;
                }
                hoiDongTieuChi.Diem += (decimal)item.Diem / (decimal)countMember;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> CheckHoiDongDoneToUpdateDiemHoSo(Guid hoiDongId)
        {
            var thanhVienInHoiDong = _context.HoiDongThanhViens.Where(x => x.HoiDongId == hoiDongId).ToList();
            var hoiDong = await _context.HoiDongs.FindAsync(hoiDongId);
            var hoSo = await _context.HoSos.FindAsync(hoiDong.HoSoId);
            var diemSumNhomA = 0;
            var diemSumNhomB = 0;
            var diemSumNhomC = 0;
            foreach (var item in thanhVienInHoiDong)
            {
                if (string.IsNullOrEmpty(item.DanhGia)) return false;
                hoiDong.TrangThai = SystemConstants.HoiDong_HoanThanh;
                diemSumNhomA += item.DiemSucManhCongDong;
                diemSumNhomB += item.DiemKhaNangTiepThi;
                diemSumNhomC += item.DiemChatLuongSanPhan;
            }
            if (hoiDong.MaCapBac == SystemConstants.MaCapHuyen)
            {
                hoSo.DiemTBHuyen = (int)((diemSumNhomA + diemSumNhomB + diemSumNhomC) / thanhVienInHoiDong.Count);
                if (hoSo.DiemTBHuyen < 30)
                {
                    hoSo.DanhGiaHuyen = 1;
                }
                else if (hoSo.DiemTBHuyen > 30 && hoSo.DiemTBHuyen < 50)
                {
                    hoSo.DanhGiaHuyen = 2;
                }
                else if (hoSo.DiemTBHuyen > 50 && hoSo.DiemTBHuyen < 70)
                {
                    hoSo.DanhGiaHuyen = 3;
                }
                else if (hoSo.DiemTBHuyen > 70 && hoSo.DiemTBHuyen < 90)
                {
                    hoSo.DanhGiaHuyen = 4;
                }
                else
                {
                    hoSo.DanhGiaHuyen = 5;
                }
                if (hoSo.DiemTBHuyen > 50)
                {
                    hoSo.TrangThai = SystemConstants.HoSo_TrangThai_Pending_Tinh;
                }
                else
                {
                    hoSo.TrangThai = SystemConstants.HoSo_TrangThai_Fail_Huyen;
                }
                hoSo.KetQua = hoSo.TrangThai;
            }
            else
            {
                hoSo.DiemTBTinh = (int)((diemSumNhomA + diemSumNhomB + diemSumNhomC) / thanhVienInHoiDong.Count);
                if (hoSo.DiemTBTinh < 30)
                {
                    hoSo.DanhGiaTinh = 1;
                }
                else if (hoSo.DiemTBTinh > 30 && hoSo.DiemTBTinh < 50)
                {
                    hoSo.DanhGiaTinh = 2;
                }
                else if (hoSo.DiemTBTinh > 50 && hoSo.DiemTBTinh < 70)
                {
                    hoSo.DanhGiaTinh = 3;
                }
                else if (hoSo.DiemTBTinh > 70 && hoSo.DiemTBTinh < 90)
                {
                    hoSo.DanhGiaTinh = 4;
                }
                else
                {
                    hoSo.DanhGiaTinh = 5;
                }
                if (hoSo.DiemTBTinh > 50)
                {
                    hoSo.TrangThai = SystemConstants.HoSo_TrangThai_Pass_Tinh;
                }
                else
                {
                    hoSo.TrangThai = SystemConstants.HoSo_TrangThai_Fail_Tinh;
                }
                hoSo.KetQua = hoSo.TrangThai;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ResponseResult<List<HoiDongDanhGiaViewModel>>> GetDanhGiaByHoiDong(Guid hoiDongId)
        {
            var listDanhGia = _context.HoiDongTieuChis.Where(x => x.HoiDongId == hoiDongId).ToList();
            var listHoiDongDanhGia = new List<HoiDongDanhGiaViewModel>();
            foreach (var item in listDanhGia)
            {
                var tieuChi = await _context.TieuChis.FindAsync(item.TieuChiId);
                var hoiDongDanhGia = new HoiDongDanhGiaViewModel()
                {
                    TieuChiId = item.TieuChiId,
                    Diem = item.Diem,
                    DiemToiDa = tieuChi.DiemCanTren,
                    GhiChu = item.GhiChu,
                    HoiDongId = item.HoiDongId,
                    HoiDongTieuChiId = item.HoiDongTieuChiId,
                    TenTieuChi = tieuChi.TenTieuChi
                };
                listHoiDongDanhGia.Add(hoiDongDanhGia);
            }
            return new ResponseSuccessResult<List<HoiDongDanhGiaViewModel>>(listHoiDongDanhGia);
        }

        public async Task<ResponseResult<List<HoiDongReportViewModel>>> GetReportHoiDongByTrangThai()
        {
            var listHoiDongReport = new List<HoiDongReportViewModel>();
            var hoiDongDangHoanThienReport = new HoiDongReportViewModel()
            {
                TrangThai = SystemConstants.HoiDong_DangHoanThien,
                Label = "Đang hoàn thiện",
                CountHoiDongByTrangThai = _context.HoiDongs.Count(x => x.TrangThai == SystemConstants.HoiDong_DangHoanThien)
            };
            listHoiDongReport.Add(hoiDongDangHoanThienReport);
            var hoiDongDangDanhGiaReport = new HoiDongReportViewModel()
            {
                TrangThai = SystemConstants.HoiDong_DangDanhGia,
                Label = "Đang đánh giá",
                CountHoiDongByTrangThai = _context.HoiDongs.Count(x => x.TrangThai == SystemConstants.HoiDong_DangDanhGia)
            };
            listHoiDongReport.Add(hoiDongDangDanhGiaReport);
            var hoiDongHoanThanhReport = new HoiDongReportViewModel()
            {
                TrangThai = SystemConstants.HoiDong_HoanThanh,
                Label = "Hoàn thành",
                CountHoiDongByTrangThai = _context.HoiDongs.Count(x => x.TrangThai == SystemConstants.HoiDong_HoanThanh)
            };
            listHoiDongReport.Add(hoiDongHoanThanhReport);
            return new ResponseSuccessResult<List<HoiDongReportViewModel>>(listHoiDongReport);
        }
    }
}
