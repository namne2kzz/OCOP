using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OCOP.ViewModel.System.AppUsers
{
    public class ThanhVienUpdateRequest
    {
        public Guid AppUserid { get; set; }

        public string Ten { get; set; }

        public string MaNhanVien { get; set; }

        public string DonViCongTac { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile ThumbnailImage { get; set; }

        public string ImagePath { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
       
    }
}
