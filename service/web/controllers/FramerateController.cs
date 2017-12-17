using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    public class FramerateController : ApiController
    {
        StorageServices.VideoStorageService StorageService = new StorageServices.VideoStorageService();

        // GET: api/Framerate/5
        public double Get(string id)
        {
            return StorageService.GetFramerate(id);
        }






    }


}
