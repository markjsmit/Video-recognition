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
    [ServiceController("Video")]
    public class VideoService:RestService
    {
        public void Save(Callback callback, string naam, byte[] content)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Id", naam);
            Bestand bestand = new Bestand(naam, content);
            Request(callback, HttpMethod.Post, parameters, bestand);


        }

        public void GetAll(Callback<IList<string>> callback) { 
              Request<IList<string>>(callback, HttpMethod.Get);
        }

        public void GetVideo(Callback<string> callback, string naam)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Id", naam);
            Request<string>(callback,HttpMethod.Get,parameters);
        }
    }
}
