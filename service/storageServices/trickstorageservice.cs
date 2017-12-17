using Domain;
using System;
using System.Net;
using VideoService;



namespace StorageServices
{
    public class TrickStorageService
    {
        static NeuralNetworkService.NeuralNetworkService Service = new NeuralNetworkService.NeuralNetworkService();
        public static  string STORAGE_FOLDER = "c:\\data\\tricks";

        public void SaveTrick(Trick trick){
            var serialized = FileHelper.Serialize(trick);
            FileHelper.SaveFileData(serialized, STORAGE_FOLDER + "/" + trick.TrickName, "trick.bin");
            if (trick.Network != null)
            {
                trick.Network.Save();
            }
        }

        public void SaveTrick(string name)
        {
            Trick trick = new Trick(name, STORAGE_FOLDER + "/" + name+"/network.bin");
            SaveTrick(trick); 
        }

        public Trick GetTrick(string trick)
        {
            var data = FileHelper.GetFileData(STORAGE_FOLDER + "/" + trick+"/trick.bin");
            var trickObj= (Trick)FileHelper.Unserialize(data);
            trickObj.InitNetowrk();
            return trickObj;
        }



        public string[] GetTricks() {
            return FileHelper.GetAllFolders(STORAGE_FOLDER);
        }
    
    
    }

}
