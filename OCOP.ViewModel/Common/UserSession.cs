using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Common
{
    public class UserSession
    {
        public Guid AppUserId { get; set; }

        public string Ten { get; set; }

        public string UserName { get; set; }

        public string ImagePath { get; set; }

        public IList<string> Roles { get; set; }
    }
}
