using Domain;
using System;
using System.Net;
using VideoService;



namespace StorageServices
{
    public class VideoStorageService
    {
        static VideoService.VideoService VideoService = new VideoService.VideoService();
        public static  string STORAGE_FOLDER = "c:\\data\\Videos";
        public void SaveVideo(Byte[] content, string name)
        {
            var folder = STORAGE_FOLDER + "/" + name;
            FileHelper.SaveFileData(content, folder, "original.avi");
            var video = VideoService.GetVideo(folder+"/"+ "original.avi");
            var frames = VideoService.ProcessVideo(video);
            var serialized= FileHelper.Serialize(frames);
            FileHelper.SaveFileData(serialized, folder, "frames.bin");
        }

        public VideoFrames GetFrames(string name)
        {
            var data = FileHelper.GetFileData(STORAGE_FOLDER + "/" + name + "/frames.bin");
            
            return (VideoFrames)FileHelper.Unserialize(data);
        }
        public Byte[] GetOriginalVideo(string name)
        {
            return FileHelper.GetFileData(STORAGE_FOLDER + "/" + name + "/original.avi");
        }

        public string[] GetVideos() {
            return FileHelper.GetAllFolders(STORAGE_FOLDER);
        }



        public double GetFramerate(string name)
        {
            var data = FileHelper.GetFileData(STORAGE_FOLDER + "/" + name + "/frames.bin");
            var frames = (VideoFrames)FileHelper.Unserialize(data); ;
            return frames.OriginalFps;
        }
    }

}
