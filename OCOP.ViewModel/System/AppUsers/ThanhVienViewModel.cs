using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.System.AppUsers
{
    public class ThanhVienViewModel
    {
        public Guid AppUserId { get; set; }

        public string Ten { get; set; }

        public string MaNhanVien { get; set; }

        public string DonViCongTac { get; set; }

        public string CapBac { get; set; }

       public string ImagePath { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public bool TrangThai { get; set; }
    }
}
