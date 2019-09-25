using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Runpath.Api.Models;
using Runpath.Api.Repo;

namespace Runpath.Api
{
    public class AlbumProcessor : IAlbumProcessor
    {
        private readonly IPhotoRepository _photoRepo;

        public AlbumProcessor(IPhotoRepository photoRepo)
        {
            _photoRepo = photoRepo;
        }

        public IEnumerable<Album> GetAlbumsByUser(int userId)
        {
            var albumList = _photoRepo.GetAlbums().Where(x => x.UserId == userId).ToList();
            var photoList = _photoRepo.GetPhotos();

            var response = new List<Album>();
            foreach (var album in albumList)
            {
                album.Photos = photoList.Where(x => x.AlbumId == album.Id).ToList();
                response.Add(album);
            }

            return response;
        }

        public IEnumerable<Album> GetAllAlbums()
        {
            var albumList = _photoRepo.GetAlbums();
            var photoList = _photoRepo.GetPhotos();

            var response = new List<Album>();
            foreach (var album in albumList)
            {
                album.Photos = photoList.Where(x => x.AlbumId == album.Id).ToList();
                response.Add(album);
            }

            return response;
        }
    }
}
