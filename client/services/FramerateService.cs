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
    [ServiceController("Framerate")]
    public class FramerateService:RestService
    {


        public void Get(Callback<string> callback, string naam)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Id", naam);
            Request<string>(callback,HttpMethod.Get,parameters);
        }
    }
}
