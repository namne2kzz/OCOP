using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Common
{
    public class ResponseErrorResult<T> : ResponseResult<T>
    {
        public string[] ValidationErrors { get; set; }

        public ResponseErrorResult()
        {
            IsSuccessed = false;
            Message = "Thao tác thất bại";
        }

        public ResponseErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ResponseErrorResult(string[] validationErrors)
        {
            IsSuccessed = false;
            Message = "Thao tác thất bại";
            ValidationErrors = validationErrors;
        }
    }
}
