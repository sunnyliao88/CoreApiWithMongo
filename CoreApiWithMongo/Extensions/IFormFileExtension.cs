using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithMongo.Extensions
{
    public static class IFormFileExtension
    {
        public static string ConvertToBase64String(this IFormFile formFile)
        {
            string result = null;
            if (formFile == null)
            {
                return result;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                byte[] bytes = ms.ToArray();
                result = Convert.ToBase64String(bytes);
            }
            return result;
        }




    }
}
