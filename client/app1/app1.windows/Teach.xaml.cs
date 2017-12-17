using App1.Abstract;
using App1.Common;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace App1
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Teach : Page, IActionStarter
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private TrickService TrickService = new TrickService();
        private VideoService VideoService = new VideoService();
        private NetworkService NetworkService = new NetworkService();
        private FramerateService FramerateService = new FramerateService();
        private FramecountService FramecountService = new FramecountService();


        public IList<int> FramesLanded;

        private bool VideoGekozen = false;
        private bool TrickGekozen = false;

        private string GekozenTrick = "";
        private string GekozenVideo = "";



        IList<double> Waardes;

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public Teach()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {


            TrickService.GetAll(delegate(bool success, IList<string> result)
            {
                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                   {
                       if (result != null)
                       {
                           foreach (var trick in result)
                           {
                               TrickList.Items.Add(new Beans.Trick { Naam = trick });
                           }
                       }
                   });
            });


            VideoService.GetAll(delegate(bool success, IList<string> result)
            {
                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    if (result != null)
                    {
                        foreach (var video in result)
                        {
                            VideoList.Items.Add(new Beans.Video { Naam = video });
                        }
                    }
                });
            });

        }



        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        public void Start(Frame context)
        {
            context.Navigate(this.GetType());
        }

        private void pageTitle_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void VideoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Beans.Video)e.AddedItems.First();
            VideoService.GetVideo(delegate(bool success, string result)
            {
                if (success)
                {

                    FramerateService.Get(delegate(bool succes, string fps)
                    {
                        try
                        {
                            var fpsd = Double.Parse(fps);
                            Fps = fpsd;

                        }
                        catch (Exception ex) { }
                    }, selectedItem.Naam);



                    byte[] data = Convert.FromBase64String(result);
                    StorageFolder folder = ApplicationData.Current.LocalFolder;
                    folder.CreateFileAsync("temp.avi", CreationCollisionOption.ReplaceExisting).AsTask().ContinueWith(
                            resultFile =>
                            {
                                StorageFile file = resultFile.Result;
                                file.OpenStreamForWriteAsync().ContinueWith(streamResult =>
                                {
                                    var stream = streamResult.Result;
                                    stream.Write(data, 0, data.Length);
                                    stream.Dispose();
                                });

                                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {
                                    MediaPlayer.Source = new Uri(folder.Path + "\\temp.avi");
                                    MediaPlayer.AutoPlay = false;
                                    VideoGekozen = true;
                                    GekozenVideo = selectedItem.Naam;
                                });

                            }
                        );

                }
            }, selectedItem.Naam);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TrickGekozen && VideoGekozen)
            {
                MediaPlayer.Play();
            }
            else
            {
                MessageDialog dialog = new MessageDialog("KiesEerstEenTrickEnvideo");
                dialog.ShowAsync();
            }
        }

        private void TrickList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Beans.Trick)e.AddedItems.First();
            TrickGekozen = true;
            GekozenTrick = selectedItem.Naam;
            FramesLanded = new List<int>();
        }

        private void MediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {

        }

        public double Fps { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var fpms = TimeSpan.FromSeconds(1 / Fps).TotalMilliseconds;
            var pastMs = MediaPlayer.Position.TotalMilliseconds;
            var pastFrames = pastMs / fpms;
            var frame = (int)(pastFrames);

            FramesLanded.Add(frame);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int cicles = 1;
            try
            {
                cicles = int.Parse(CiclesText.Text);
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog("Het aantal cicles is geen nummer");
                dialog.ShowAsync();
            }


            NetworkService.Teach(delegate(bool success)
            {
                MessageDialog dialog = new MessageDialog("Trick succesvol opgeslagen");
                dialog.ShowAsync();
            }, GekozenVideo, GekozenTrick, FramesLanded,cicles);
        }


    }
}
