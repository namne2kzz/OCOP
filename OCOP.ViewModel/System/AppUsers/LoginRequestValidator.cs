﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.System.AppUsers
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Tên đăng nhập không được rỗng");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được rỗng");
        }
    }    
}
