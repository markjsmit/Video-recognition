using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace App1.Abstract
{
    interface IListItem
    {

        string ImagePath { get; set; }
        string Title { get; set; }
        string Subtitle { get; set; }
        void Start(Frame context);
    }
}
