using Moq;
using NUnit.Framework;
using Runpath.Api;
using Runpath.Api.Models;
using Runpath.Api.Repo;
using System.Collections.Generic;
using Shouldly;

namespace Runpath.Tests
{
    public class AlbumProcessorTests
    {
        private Mock<IPhotoRepository> _photoRepo;
        private AlbumProcessor albumProcessor;

        [SetUp]
        public void Setup()
        {
            _photoRepo = new Mock<IPhotoRepository>();
            albumProcessor = new AlbumProcessor(_photoRepo.Object);
        }


        [Test]
        public void GetAllAlbumsShouldReturnsAllAlbumsAndPhotos()
        {
            //ARRANGE
            var albumId = 1;
            _photoRepo.Setup(x => x.GetAlbums()).Returns(new List<Album>
            {
                new Album
                {
                    Id = albumId,
                    UserId = 1,
                    Title = "lorem ipsum"
                }
            });
            _photoRepo.Setup(x => x.GetPhotos()).Returns(new List<Photo>
            {
                new Photo
                {
                    Id = 1,
                    AlbumId = albumId,
                    Title = "illum velit nesciunt soluta",
                    Url = "https://via.placeholder.com/600/512ff0",
                    ThumbnailUrl = "https://via.placeholder.com/150/512ff0"
                }
            });

            //ACT
            var result = albumProcessor.GetAllAlbums();

            //ASSERT
            result.Count.ShouldBe(1);
            result.Find(x => x.Id == albumId).Photos.Count.ShouldBe(1);
        }

        [Test]
        public void GetAllAlbumsShouldReturnsAlbumsWithNoPhotos()
        {
            //ARRANGE
            var albumIdWithPhotos = 1;
            var albumIdWithoutPhotos = 2;
            _photoRepo.Setup(x => x.GetAlbums()).Returns(new List<Album>
            {
                new Album
                {
                    Id = albumIdWithPhotos,
                    UserId = 1,
                    Title = "lorem ipsum"
                },
                new Album
                {
                    Id = albumIdWithoutPhotos,
                    UserId = 1,
                    Title = "lorem ipsum"
                }
            });
            _photoRepo.Setup(x => x.GetPhotos()).Returns(new List<Photo>
            {
                new Photo
                {
                    Id = 1,
                    AlbumId = albumIdWithPhotos,
                    Title = "illum velit nesciunt soluta",
                    Url = "https://via.placeholder.com/600/512ff0",
                    ThumbnailUrl = "https://via.placeholder.com/150/512ff0"
                }
            });

            //ACT
            var result = albumProcessor.GetAllAlbums();

            //ASSERT
            result.Count.ShouldBe(2);
            result.Find(x => x.Id == albumIdWithPhotos).Photos.Count.ShouldBe(1);
            result.Find(x => x.Id == albumIdWithoutPhotos).Photos.Count.ShouldBe(0);
        }

        [Test]
        public void GetAllAlbumsShouldReturnsAllPhotosWithValidAlbumId()
        {
            //ARRANGE
            var albumId = 1;
            _photoRepo.Setup(x => x.GetAlbums()).Returns(new List<Album>
            {
                new Album
                {
                    Id = albumId,
                    UserId = 1,
                    Title = "lorem ipsum"
                }
            });
            _photoRepo.Setup(x => x.GetPhotos()).Returns(new List<Photo>
            {
                new Photo
                {
                    Id = 1,
                    AlbumId = albumId,
                    Title = "illum velit nesciunt soluta",
                    Url = "https://via.placeholder.com/600/512ff0",
                    ThumbnailUrl = "https://via.placeholder.com/150/512ff0"
                },
                new Photo
                {
                    Id = 2,
                    AlbumId = 2,
                    Title = "eos eum nam quasi atque suscipit",
                    Url = "https://via.placeholder.com/600/9902f3",
                    ThumbnailUrl = "https://via.placeholder.com/150/9902f3"
                }
            });

            //ACT
            var result = albumProcessor.GetAllAlbums();

            //ASSERT
            result.Count.ShouldBe(1);
            result.Find(x => x.Id == albumId).Photos.Count.ShouldBe(1);
        }

        [Test]
        public void GetAllAlbumsShouldReturnsEmptyListIfNoAlbumsFound()
        {
            //ARRANGE
            var albumId = 1;
            _photoRepo.Setup(x => x.GetAlbums()).Returns(new List<Album>());
            _photoRepo.Setup(x => x.GetPhotos()).Returns(new List<Photo>
            {
                new Photo
                {
                    Id = 1,
                    AlbumId = albumId,
                    Title = "illum velit nesciunt soluta",
                    Url = "https://via.placeholder.com/600/512ff0",
                    ThumbnailUrl = "https://via.placeholder.com/150/512ff0"
                }
            });

            //ACT
            var result = albumProcessor.GetAllAlbums();

            //ASSERT
            result.Count.ShouldBe(0);
        }

        [Test]
        public void GetAlbumsByUserShouldReturnAllUsersAlbums()
        {
            //ARRANGE
            var albumId = 1;
            var userId = 1;
            _photoRepo.Setup(x => x.GetAlbums()).Returns(new List<Album>
            {
                new Album
                {
                    Id = albumId,
                    UserId = userId,
                    Title = "lorem ipsum"
                }
            });

            _photoRepo.Setup(x => x.GetPhotos()).Returns(new List<Photo>
            {
                new Photo
                {
                    Id = 1,
                    AlbumId = albumId,
                    Title = "illum velit nesciunt soluta",
                    Url = "https://via.placeholder.com/600/512ff0",
                    ThumbnailUrl = "https://via.placeholder.com/150/512ff0"
                }
            });


            //ACT
            var result = albumProcessor.GetAlbumsByUser(userId);

            //ASSERT
            result.Count.ShouldBe(1);
            result.Find(x => x.Id == albumId).Photos.Count.ShouldBe(1);
        }

        [Test]
        public void GetAlbumsByUserShouldNotReturnAlbumsOfOtherUsers()
        {
            //ARRANGE
            var albumId = 1;
            var userId = 1;
            _photoRepo.Setup(x => x.GetAlbums()).Returns(new List<Album>
            {
                new Album
                {
                    Id = albumId,
                    UserId = userId,
                    Title = "lorem ipsum"
                },

                new Album
                {
                    Id = 2,
                    UserId = 2,
                    Title = "lorem illum"
                }
            });

            _photoRepo.Setup(x => x.GetPhotos()).Returns(new List<Photo>
            {
                new Photo
                {
                    Id = 1,
                    AlbumId = albumId,
                    Title = "illum velit nesciunt soluta",
                    Url = "https://via.placeholder.com/600/512ff0",
                    ThumbnailUrl = "https://via.placeholder.com/150/512ff0"
                },
                new Photo
                {
                    Id = 2,
                    AlbumId = 2,
                    Title = "eos eum nam quasi atque suscipit",
                    Url = "https://via.placeholder.com/600/9902f3",
                    ThumbnailUrl = "https://via.placeholder.com/150/9902f3"
                }
            });


            //ACT
            var result = albumProcessor.GetAlbumsByUser(userId);

            //ASSERT
            result.TrueForAll(x => x.UserId == userId).ShouldBeTrue();
        }

        [Test]
        public void GetAlbumsByUserShouldReturnEmptyListIfUserHasNoAlbums()
        {
            //ARRANGE
            var albumId = 1;
            var userId = 1;
            _photoRepo.Setup(x => x.GetAlbums()).Returns(new List<Album>());


            //ACT
            var result = albumProcessor.GetAlbumsByUser(userId);

            //ASSERT
            result.Count.ShouldBe(0);
        }
    }
}