using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.ViewModel.Common
{
    public class EmailConfig
    {
        public string FromEmailAddress { get; set; }

        public string FromEmailPassword { get; set; }

        public string DisplayEnaimName { get; set; }

        public string SMTPHost { get; set; }

        public string SMTPPort { get; set; }

        public bool EnableSSL { get; set; }
    }
}
