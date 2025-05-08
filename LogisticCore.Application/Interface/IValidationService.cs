using LogisticCore.Application.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Application.Interface
{
    public interface IValidationService
    {
        ValidationResult IsFormFileValid(IFormFile[] files, FileValidationDTO model);
    }
}
