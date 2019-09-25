using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Runpath.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumProcessor _albumProcessor;

        public AlbumsController(IAlbumProcessor albumProcessor)
        {
            _albumProcessor = albumProcessor;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var albums = _albumProcessor.GetAllAlbums().ToList();

            if (albums.Count == 0) return NotFound();

            return Ok(albums);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userAlbums = _albumProcessor.GetAlbumsByUser(id).ToList();

            if (userAlbums.Count == 0)
            {
                return NotFound();
            }

            return Ok(userAlbums);
        }

        
    }
}
