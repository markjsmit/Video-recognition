using StorageServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Beans;

namespace Web.Controllers
{
    public class NeuralNetworkController : ApiController
    {
        TrickStorageService TrickService = new TrickStorageService();
        VideoStorageService VideoStorageService = new VideoStorageService();

        // GET: api/NeuralNetwork/5
        public double[] Get(string trick, string video)
        {
            var videoObj = VideoStorageService.GetFrames(video);
            var trickObj = TrickService.GetTrick(trick);
            var list = new List<double>();

            foreach (var frame in videoObj.Frames)
            {
                list.Add(trickObj.Network.Run(frame.SimpleDoubleArray()) * 100);
            }

            return list.ToArray();

        }

        // POST: api/NeuralNetwork
        public void Post([FromBody]Beans.Teach teach)
        {
            string trick = teach.Trick;
            string video = teach.Video;
            int cicles = teach.Cicles;

            int[] positions = teach.Positions;


            var videoObj = VideoStorageService.GetFrames(video);
            var trickObj = TrickService.GetTrick(trick);

    

            for (int c = 0; c < cicles; c++)
            {

                var list = new List<double>();
                var LinkedPositions = new LinkedList<int>(positions.OrderBy(x => x).Select(x => x - 10 > 0 ? (int)x : 0));
                int i = 0;

                var nextPos = LinkedPositions.First;
                var calculatePos = 999;



                foreach (var frame in videoObj.Frames)
                {
                    var val = (double)0;
                    if (nextPos != null)
                    {
                        if (i > nextPos.Value)
                        {
                            calculatePos = -10;
                            nextPos = nextPos.Next;
                        }

                        val = ((-1 * Math.Pow(-0.1 * calculatePos, 2)) + 1);
                        if (!(val >= 0 && i > 0))
                        {
                            val = 0;
                        }
                    }


              
                    trickObj.Network.Teach( frame.SimpleDoubleArray(), val);

                    i++;

                    calculatePos++;

                }
            }
            TrickService.SaveTrick(trickObj);
        }




    }
}
