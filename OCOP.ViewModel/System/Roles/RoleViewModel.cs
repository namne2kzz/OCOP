using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.System.Roles
{
    public class RoleViewModel
    {
        public Guid RoleId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string NormalizedName { get; set; }
    }
}
