using Natural.Core.IServices;
using Natural.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Naturals.Service.Service
{
    public class MapService : IMapService
    {
        private readonly IHttpClientWrapper _httpClient;

        public MapService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Getlat>> GetLatLung()
        {
            var Allnotification = await _httpClient.GetAsync<List<Getlat>>("/ExecutiveGPS");
            return Allnotification;

        }
    }
}
