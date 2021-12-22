using System;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.Core;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace AppCommunityToolkitMedia
{
    public partial class MediaPage
    {
        public MediaPage()
        {
            InitializeComponent();

            var mediaElement = new MediaElement()
            {
                Source = MediaSource.FromUri("https://ia800201.us.archive.org/12/items/BigBuckBunny_328/BigBuckBunny_512kb.mp4"),
            };
            Content = mediaElement;
        }
    }
}
