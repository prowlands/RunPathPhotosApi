using Newtonsoft.Json;
using Runpath.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Runpath.Api.Repo
{
    public class PhotosRepository : IPhotoRepository
    {
        private HttpClient _client;
        public PhotosRepository(string baseAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
        }
        public IEnumerable<Album> GetAlbums()
        {
            string action = "albums";
            return CallWebservice<IEnumerable<Album>>(action);
        }

        private T CallWebservice<T>(string action)
        {
            var webResponse = _client.GetAsync(_client.BaseAddress + action).Result;
            if (webResponse.IsSuccessStatusCode)
            {
                var jsonResponse = webResponse.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<T>(jsonResponse);
                return response;
            }
            return default(T);
        }

        public IEnumerable<Photo> GetPhotos()
        {
            string action = "photos";
            return CallWebservice<IEnumerable<Photo>>(action);
        }
    }
}
