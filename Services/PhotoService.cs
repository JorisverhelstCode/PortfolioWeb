using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWeb.Services
{
    public interface IPhotoService
    {
        public void DeletePicture(string webRootPath, string photoUrl);
    }

    public class PhotoService : IPhotoService
    {
        public void DeletePicture(string webRootPath, string photoUrl)
        {
            if (photoUrl.StartsWith("/"))
            {
                photoUrl = photoUrl.Substring(1);
            }

            string pathName = Path.Combine(webRootPath, photoUrl);
            System.IO.File.Delete(pathName);
        }
    }
}
