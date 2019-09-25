using Runpath.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Runpath.Api.Repo
{
    public interface IPhotoRepository
    {
        IEnumerable<Album> GetAlbums();
        IEnumerable<Photo> GetPhotos();
    }
}
