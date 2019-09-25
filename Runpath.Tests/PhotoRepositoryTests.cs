using NUnit.Framework;
using Runpath.Api.Repo;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var client = new PhotosRepository("http://jsonplaceholder.typicode.com");
            var result = client.GetAlbums();
            Assert.Pass();
        }
    }
}