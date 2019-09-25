using Moq;
using NUnit.Framework;
using Runpath.Api;
using Runpath.Api.Models;
using System.Collections.Generic;
using Shouldly;
using Runpath.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Runpath.Tests
{
    public class AlbumsControllerTests
    {
        private Mock<IAlbumProcessor> _albumProcessor;
        private AlbumsController controller;

        [SetUp]
        public void setUp()
        {
            _albumProcessor = new Mock<IAlbumProcessor>();
            controller = new AlbumsController(_albumProcessor.Object);
        }

        [Test]
        public void GetShouldReturnAllAlbumsAndPhotos()
        {
            //ARRANGE
            _albumProcessor.Setup(x => x.GetAllAlbums()).Returns(new List<Album>
            {
                new Album
                {
                    Id = 1,
                    UserId = 1,
                    Title = "lorem ipsum",
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Id = 1,
                            AlbumId = 1,
                            Title = "illum velit nesciunt soluta",
                            Url = "https://via.placeholder.com/600/512ff0",
                            ThumbnailUrl = "https://via.placeholder.com/150/512ff0"
                        },
                        new Photo
                        {
                            Id = 2,
                            AlbumId = 1,
                            Title = "eos eum nam quasi atque suscipit",
                            Url = "https://via.placeholder.com/600/9902f3",
                            ThumbnailUrl = "https://via.placeholder.com/150/9902f3"
                        }
                    }
                },
                new Album
                {
                    Id = 2,
                    UserId = 2,
                    Title = "lorem ipsum"
                }
            });

            //ACT
            var result = controller.Get();
            var statusCodeResult = result as OkObjectResult;
            var objectResult = statusCodeResult.Value as List<Album>;
            //ASSERT
            statusCodeResult.StatusCode.ShouldBe(200);
            objectResult.Count.ShouldBe(2);
        }

        [Test]
        public void GetShouldReturnNotFoundIfNoAlbumsFound()
        {
            //ARRANGE
            _albumProcessor.Setup(x => x.GetAllAlbums()).Returns(new List<Album>());

            //ACT
            var result = controller.Get();
            var statusCodeResult = result as NotFoundResult;
            //ASSERT
            statusCodeResult.StatusCode.ShouldBe(404);
        }

        [Test]
        public void GetByIdShouldReturnAllAlbumsAndPhotosForUser()
        {
            //ARRANGE
            var userId = 1;
            _albumProcessor.Setup(x => x.GetAlbumsByUser(It.IsAny<int>())).Returns(new List<Album>
            {
                new Album
                {
                    Id = 1,
                    UserId = userId,
                    Title = "lorem ipsum",
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Id = 1,
                            AlbumId = 1,
                            Title = "illum velit nesciunt soluta",
                            Url = "https://via.placeholder.com/600/512ff0",
                            ThumbnailUrl = "https://via.placeholder.com/150/512ff0"
                        },
                        new Photo
                        {
                            Id = 2,
                            AlbumId = 1,
                            Title = "eos eum nam quasi atque suscipit",
                            Url = "https://via.placeholder.com/600/9902f3",
                            ThumbnailUrl = "https://via.placeholder.com/150/9902f3"
                        }
                    }
                },
                new Album
                {
                    Id = 2,
                    UserId = userId,
                    Title = "lorem ipsum"
                }
            });

            //ACT
            var result = controller.Get(userId);
            var statusCodeResult = result as OkObjectResult;
            var objectResult = statusCodeResult.Value as List<Album>;
            //ASSERT
            statusCodeResult.StatusCode.ShouldBe(200);
            objectResult.Count.ShouldBe(2);
        }

        [Test]
        public void GetByIdShouldReturnNotFoundIfNoAlbumsFound()
        {
            //ARRANGE
            var userId = 1;
            _albumProcessor.Setup(x => x.GetAlbumsByUser(It.IsAny<int>())).Returns(new List<Album>());

            //ACT
            var result = controller.Get(userId);
            var statusCodeResult = result as NotFoundResult;
            //ASSERT
            statusCodeResult.StatusCode.ShouldBe(404);
        }
    }
}
