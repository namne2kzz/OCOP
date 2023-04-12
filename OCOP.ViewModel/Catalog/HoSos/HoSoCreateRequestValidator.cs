using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Catalog.HoSos
{
    public class HoSoCreateRequestValidator : AbstractValidator<HoSoCreateRequest>
    {
        public HoSoCreateRequestValidator()
        {
            RuleFor(x => x.TenHoSo).NotEmpty().WithMessage("Tên hồ sơ không được rỗng").MaximumLength(255);
            RuleFor(x => x.TenSanPham).NotEmpty().WithMessage("Tên sản phẩm không được rỗng").MaximumLength(255);
            RuleFor(x => x.GiayDangKyYTuongSanPham).NotNull().WithMessage("Thông tin bắt buộc");
            RuleFor(x => x.GiayPhuongAnKeHoachKinhDoanh).NotNull().WithMessage("Thông tin bắt buộc");
            RuleFor(x => x.GiayGioiThieuBoMayToChuc).NotNull().WithMessage("Thông tin bắt buộc");
            RuleFor(x => x.GiayDangKyKinhDoanh).NotNull().WithMessage("Thông tin bắt buộc");
        }
    }
}
