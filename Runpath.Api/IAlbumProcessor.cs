using Runpath.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Runpath.Api
{
    public interface IAlbumProcessor
    {
        IEnumerable<Album> GetAllAlbums();
        IEnumerable<Album> GetAlbumsByUser(int userId);
    }
}
