using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Helper
{
    public static class FileManager
    {
        public static string Save(IFormFile file,string rootPath,string folder)
        {
            string newFileName=Guid.NewGuid().ToString()+(file.FileName.Length>64?file.FileName.Substring(file.FileName.Length-64):file.FileName);
            string path=Path.Combine(rootPath,folder,newFileName);
            using(FileStream stream=new FileStream(path,FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return newFileName;
        }
        public static void Delete(string rootPath,string folder,string fileName)
        {

            string path =Path.Combine(rootPath,folder,fileName);
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
