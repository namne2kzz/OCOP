using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OCOP.ViewModel.System.AppUsers
{
    public class ThanhVienCreateRequest
    {
        public string Ten { get; set; }

        public string MaNhanVien { get; set; }

        public string DonViCongTac { get; set; }

        public int MaCapBac { get; set; }
     
        [DataType(DataType.Upload)]
        public IFormFile ThumbnailImage { get; set; }

        public string Email { get; set; }
       
        public string PhoneNumber { get; set; }
       
        public string UserName { get; set; }
      
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
