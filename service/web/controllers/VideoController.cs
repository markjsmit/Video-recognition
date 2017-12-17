using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    public class VideoController : ApiController
    {
        StorageServices.VideoStorageService StorageService = new StorageServices.VideoStorageService();
        // GET: api/Video
        public IEnumerable<string> Get()
        {
            return StorageService.GetVideos();
        }

        // GET: api/Video/5
        public Byte[] Get(string id)
        {
            return StorageService.GetOriginalVideo(id);
        }

        // POST: api/Video
        public void Post(string id, [FromBody]Beans.Bestand value)
        {
            StorageService.SaveVideo(value.Content, id);
        }





    }


}
