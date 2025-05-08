using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Application.DTO
{
    public class FileValidationDTO
    {
        public int MaxImageCount { get; set; }
        public int MaxFileSize { get; set; }
        public string FileExtension { get; set; }
        public string FileType { get; set; }
    }
}
