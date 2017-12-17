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
    [ServiceController("NeuralNetwork")]
    public class NetworkService : RestService
    {

        public void Run ( Callback<IList<double>> callback, string trick, string video)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("trick", trick);
            parameters.Add("video", video);
            Request<IList<double>>(callback, HttpMethod.Get, parameters);
        }

        public void Teach(Callback callback, string video, string trick, IList<int> geland, int cicles)
        {
            Teach teach = new Teach { Positions = geland.ToArray(), Trick = trick, Video = video, Cicles = cicles };
            Request(callback, HttpMethod.Post, null, teach);
        }
    }
}
