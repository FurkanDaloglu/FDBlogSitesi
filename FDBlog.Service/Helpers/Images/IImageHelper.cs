using FDBlog.Entity.Dtos.Images;
using FDBlog.Entity.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDBlog.Service.Helpers.Images
{
    public interface IImageHelper
    {
        Task<ImageUploadDto> Upload(string name, IFormFile imageFile,ImageType ImageType, string folderName=null);
        void Delete(string imageName);
    }
}
