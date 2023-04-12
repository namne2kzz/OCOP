using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Common
{
    public class ResponseSuccessResult<T> : ResponseResult<T>
    {
        public ResponseSuccessResult()
        {
            IsSuccessed = true;
            Message = "Thao tác thành công";
        }

        public ResponseSuccessResult(T resultObj)
        {
            IsSuccessed = true;
            Message = "Thao tác thành công";
            ResultObj = resultObj;
        }
    }
}
