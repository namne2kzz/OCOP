using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.System.AppUsers
{
    public class NguoiDungCreateRequestValidator : AbstractValidator<NguoiDungCreateRequest>
    {
        public NguoiDungCreateRequestValidator()
        {
            RuleFor(x => x.TenNhaSanXuat).NotEmpty().WithMessage("Tên nhà sản xuất không được rỗng")
                .MaximumLength(255).WithMessage("Tên không quá 255 kí tự");

            RuleFor(x => x.TenNguoiDaiDien).NotEmpty().WithMessage("Tên người đại diện xuất không được rỗng")
                .MaximumLength(255).WithMessage("Tên không quá 255 kí tự");

            RuleFor(x => x.SoDangKyKinhDoanh).NotEmpty().WithMessage("Số đăng kí kinh doanh không được rỗng")
                .MaximumLength(10).WithMessage("Họ không quá 10 kí tự");
         
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được rỗng")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$").WithMessage("Email không hợp lệ");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại không được rỗng")
                .MaximumLength(15).WithMessage("Số điện thoại không quá 15 kí tự").Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$").WithMessage("Số điện thoại không hợp lệ");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Tên đăng nhập không được rỗng")
                .MinimumLength(6).WithMessage("Tên đăng nhập phải ít nhất 6 kí tự");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được rỗng. Yêu cầu ít nhất 1 chữ hoa, 1 chữ thường và 1 số")
                .MinimumLength(8).WithMessage("Mật khẩu phải ít nhất 8 kí tự");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Xác nhận mật khẩu không đúng");
                }
            });

            RuleFor(x => x.DiaChiChiTiet).NotEmpty().WithMessage("Địa chỉ chi tiết không được rỗng");
            RuleFor(x => x.MoHinhSXId).NotNull().WithMessage("Chọn mô hình sản xuất");
            RuleFor(x => x.XaId).NotNull().WithMessage("Xã không được rỗng");
        }
    }
}
