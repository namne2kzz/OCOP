﻿using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.System.Roles
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }

        public List<SelectedItem> Roles { get; set; } = new List<SelectedItem>();
    }
}
