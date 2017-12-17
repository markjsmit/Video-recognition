using Domain;
using Services.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [ServiceController("Trick")]
    public class TrickService : RestService
    {

        public void GetAll(Callback<IList<string>> callback)
        {
            Request<IList<string>>(callback, HttpMethod.Get);
        }

        public void Make(Callback callback, string naam)
        {
            var trick = new Trick(naam);
            Request(callback,HttpMethod.Post,null,trick);
        }


    }
}
