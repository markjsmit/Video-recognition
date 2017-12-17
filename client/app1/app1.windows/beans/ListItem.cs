using App1.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace App1
{
    class ListItem<T>:IListItem where T:IActionStarter, new()
    {
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }

        public void Start(Frame context)
        {

            IActionStarter starter=new  T();
            starter.Start(context);
        }
    }
}
