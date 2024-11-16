using System.Text;
using System.Text.RegularExpressions;

namespace WebMVC.Helper
{
    public class MyUtil
    {
       
        public static void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
            {
                Directory.CreateDirectory(path);
            }
        }

      public static string ToTitleCase(string str)
{
    string result = str;
    if (!string.IsNullOrEmpty(result))
    {
        var words = str.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            var s = words[i];
            if (s.Length > 0)
            {
                words[i] = s[0].ToString().ToUpper() + s.Substring(1);

            }
        }
        result = string.Join(" ", words);
    }
    return result;
}

        public static async Task<string> UploadFile(Microsoft.AspNetCore.Http.IFormFile file,  string newName)
        {
            try
            {
                if (newName == null) { newName = file.FileName; }
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagep");
                CreateIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagep", newName);
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif", "jfif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower()))
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newName;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
    }
}