using LogisticCore.Application.DTO;
using LogisticCore.Application.Interface;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LogisticCore.Application.Service
{
    public class ValidationService : IValidationService
    {
        public ValidationResult IsFormFileValid(IFormFile[] files, FileValidationDTO model)
        {
            if (files.Count() > model.MaxImageCount)
            {
                return new ValidationResult(GetLimitExceedErrorMessage(model.MaxImageCount, model.FileType));
            }
            else if (files.Count() == 0)
            {
                return new ValidationResult(GetImageUploadErrorMessage());
            }
            foreach (var file in files)
            {
                if (file.Length > model.MaxFileSize)
                {
                    return new ValidationResult(GetSizeErrorMessage(model));
                }
                else if (!model.FileExtension.Contains(Path.GetExtension(file.FileName)))
                {
                    return new ValidationResult(GetExtenionErrorMessage());
                }
            }

            return new ValidationResult("Success");
        }
        public string GetImageUploadErrorMessage()
        {
            return $"Please upload atleast one image.";
        }
        public string GetLimitExceedErrorMessage(int MaxImageCount, string fileType)
        {
            string errorMessage = string.Empty;
            if (fileType == "accidentlog" || fileType == "complain")
            {
                errorMessage = $"Please upload {MaxImageCount} or less images";
            }
            else if (fileType == "loanclose")
            {
                errorMessage = $"Please upload only one image";
            }

            return errorMessage;
        }
        public string GetSizeErrorMessage(FileValidationDTO model)
        {
            return $"Maximum allowed file size is {model.MaxFileSize} bytes.";
        }
        public string GetExtenionErrorMessage()
        {
            return $"Invalid file format. Upload .jpeg, .png or .jpg format images!";
        }
    }
}
