using App1.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.Items.Items.Add(new ListItem<UploadVideo> { Title = "Upload", Subtitle = "Upload een video", ImagePath="Assets/MainScreen/upload.png" });
            this.Items.Items.Add(new ListItem<AddTrick> { Title = "Trick maken", Subtitle = "Beheer de tricks", ImagePath = "Assets/MainScreen/trick.png"});
            this.Items.Items.Add(new ListItem<Teach> { Title = "Leren", Subtitle = "Leer het systeem", ImagePath = "Assets/MainScreen/teach.png"});
            this.Items.Items.Add(new ListItem<Analyze> { Title = "Analyseer", Subtitle = "Analyiseer een video", ImagePath = "Assets/MainScreen/run.jpg"});
        }

        private void Items_ItemClick(object sender, ItemClickEventArgs e)
        {

            var item = (IListItem)e.ClickedItem;
            item.Start(this.Frame);
         
        }

        private void Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
