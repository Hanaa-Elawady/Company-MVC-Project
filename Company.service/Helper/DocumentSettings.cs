
using Microsoft.AspNetCore.Http;

namespace Company.Services.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, String folderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";
            var filePath = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }
    }
}
