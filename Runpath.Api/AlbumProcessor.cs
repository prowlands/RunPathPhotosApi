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

        public List<Album> GetAlbumsByUser(int userId)
        {
            var albumList = _photoRepo.GetAlbums().Where(x => x.UserId == userId).ToList();
            var photoList = _photoRepo.GetPhotos().ToList();

            List<Album> response = AddPhotosToAlbums(albumList, photoList);

            return response;
        }



        public List<Album> GetAllAlbums()
        {
            var albumList = _photoRepo.GetAlbums().ToList();
            var photoList = _photoRepo.GetPhotos().ToList();

            List<Album> response = AddPhotosToAlbums(albumList, photoList);

            return response;
        }

        private static List<Album> AddPhotosToAlbums(List<Album> albumList, List<Photo> photoList)
        {
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
