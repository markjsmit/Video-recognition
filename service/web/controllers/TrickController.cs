using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    public class TrickController : ApiController
    {
        StorageServices.TrickStorageService StorageService = new StorageServices.TrickStorageService();

        public IEnumerable<string> Get()
        {
            return StorageService.GetTricks();
        }



        // POST api/values
        public void Post([FromBody]Beans.Trick trick)
        {
            StorageService.SaveTrick(trick.Naam);
        }




    }
}
