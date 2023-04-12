using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Common
{
    public class ResponseResult<T>
    {
        public bool IsSuccessed { get; set; }

        public string Message { get; set; }

        public T ResultObj { get; set; }
    }
}
