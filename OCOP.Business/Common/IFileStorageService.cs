﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OCOP.Business.Common
{
    public interface IFileStorageService
    {
         string GetFileUrl(string fileName);

         Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

         Task DeleteFileAsync(string fileName);
    }
}
