using FDBlog.Entity.Dtos.Images;
using FDBlog.Entity.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDBlog.Service.Helpers.Images
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string wwwroot;
        private const string imgFoler = "images";
        private const string articleImagesFolder = "article-images";
        private const string UserImageFolder = "user-images";
        public ImageHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            wwwroot = webHostEnvironment.WebRootPath;
        }

        //yazılan makalenin ismini alıp image ismini çeviren kod 
        private string ReplaceInvalidChars(string fileName)
        {
            return fileName.Replace("İ", "I")
                 .Replace("ı", "i")
                 .Replace("Ğ", "G")
                 .Replace("ğ", "g")
                 .Replace("Ü", "U")
                 .Replace("ü", "u")
                 .Replace("ş", "s")
                 .Replace("Ş", "S")
                 .Replace("Ö", "O")
                 .Replace("ö", "o")
                 .Replace("Ç", "C")
                 .Replace("ç", "c")
                 .Replace("é", "")
                 .Replace("!", "")
                 .Replace("'", "")
                 .Replace("^", "")
                 .Replace("+", "")
                 .Replace("%", "")
                 .Replace("/", "")
                 .Replace("(", "")
                 .Replace(")", "")
                 .Replace("=", "")
                 .Replace("?", "")
                 .Replace("_", "")
                 .Replace("*", "")
                 .Replace("æ", "")
                 .Replace("ß", "")
                 .Replace("@", "")
                 .Replace("€", "")
                 .Replace("<", "")
                 .Replace(">", "")
                 .Replace("#", "")
                 .Replace("$", "")
                 .Replace("½", "")
                 .Replace("{", "")
                 .Replace("[", "")
                 .Replace("]", "")
                 .Replace("}", "")
                 .Replace(@"\", "")
                 .Replace("|", "")
                 .Replace("~", "")
                 .Replace("¨", "")
                 .Replace(",", "")
                 .Replace(";", "")
                 .Replace("`", "")
                 .Replace(".", "")
                 .Replace(":", "")
                 .Replace(" ", "");
        }

       
        public async Task<ImageUploadDto> Upload(string name, IFormFile imageFile,ImageType imageType, string folderName = null)
        {
            folderName ??= imageType == ImageType.User ? UserImageFolder : articleImagesFolder;

            if (!Directory.Exists($"{wwwroot}/{imgFoler}/{folderName}"))
                Directory.CreateDirectory($"{wwwroot}/{imgFoler}/{folderName}");

            string oldFileName=Path.GetFileNameWithoutExtension(imageFile.FileName);
            string fileExtension=Path.GetExtension(imageFile.FileName);

            name=ReplaceInvalidChars(name);

            DateTime dateTime = DateTime.Now;

            string newFileName=$"{name}_{dateTime.Millisecond}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/{imgFoler}/{folderName}", newFileName);
            await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024,useAsync:false);
            await imageFile.CopyToAsync(stream);
            await stream.FlushAsync();

            string message = imageType == ImageType.User 
                ? $"{newFileName} isimli kullanıcı resmi başarı ile eklenmiştir." 
                : $"{newFileName} isimli makale resmi başarı ile eklenmiştir.";

            return new ImageUploadDto()
            {
                FullName = $"{folderName}/{newFileName}"
            };
        }

        public void Delete(string imageName)
        {
            var filetoDelete = Path.Combine($"{wwwroot}/{imgFoler}/{imageName}");
            if(File.Exists(filetoDelete))
                File.Delete(filetoDelete);
        }

    }
}
