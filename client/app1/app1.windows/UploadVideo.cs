using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Popups;

namespace App1
{
    class UploadVideo : Abstract.IActionStarter
    {
        private VideoService VideoService = new VideoService();

        public void Start(Frame context)
        {

            var fileOpenPicker = new Windows.Storage.Pickers.FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Clear();
            fileOpenPicker.FileTypeFilter.Add(".avi");
            
            fileOpenPicker.PickSingleFileAsync().AsTask().ContinueWith(
                (task) =>
                {
                    var result = task.Result;
                    var naam =result.DisplayName;

                    FileIO.ReadBufferAsync(result).AsTask().ContinueWith(x =>
                    {
                        var stream = x.Result;
                        var array = stream.ToArray();
                        VideoService.Save(delegate(bool success) {
                            if (success)
                            {
                                MessageDialog dialog = new MessageDialog("Trick succesvol opgeslagen");
                                dialog.ShowAsync();
                            }
                            else
                            {
                                MessageDialog dialog = new MessageDialog("Trick succesvol opgeslagen");
                                dialog.ShowAsync();
                            }
                        },naam, array);
                    });
                }
            );

        }
    }
}
