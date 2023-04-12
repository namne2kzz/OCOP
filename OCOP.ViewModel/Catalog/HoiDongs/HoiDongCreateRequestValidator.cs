using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Catalog.HoiDongs
{
    public class HoiDongCreateRequestValidator : AbstractValidator<HoiDongCreateRequest>
    {
        public HoiDongCreateRequestValidator()
        {
            RuleFor(x => x.TenHoiDong).NotEmpty().WithMessage("Tên hội đồng không được rỗng").MaximumLength(255);          
        }
    }
}
