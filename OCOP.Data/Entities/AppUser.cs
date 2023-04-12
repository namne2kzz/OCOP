using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {       
        public string ImagePath { get; set; }

        public DateTime NgayTao { get; set; }

        public bool TrangThai { get; set; }

        public virtual ThanhVien ThanhVien { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }          

    }
}
