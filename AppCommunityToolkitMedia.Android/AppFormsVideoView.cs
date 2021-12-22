using System;
using DDebug = System.Diagnostics.Debug;
using Android.Content;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Android.Widget;
using System.Collections.Generic;
using AUri = Android.Net.Uri;
using AppCommunityToolkitMedia.Droid.Renderers;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(MediaElement), typeof(AppFixMediaElementRenderer))]
namespace AppCommunityToolkitMedia.Droid.Renderers
{
    public class AppFormsVideoView : FormsVideoView
    {
        public AppFormsVideoView(Context context) : base(context)
        {
        }

        bool _Disposed;

        protected override void Dispose(bool disposing)
        {
            if (!_Disposed)
                _Disposed = true;
            base.Dispose(disposing);
        }

        public override async void SetVideoURI(AUri uri, IDictionary<string, string> headers)
        {
            try
            {
                // To simulate the "loading" of the uri source
                await Task.Delay(5000);

                if (uri != null)
                {
                    await SetMetadata(uri, headers);
                }

                /// Uncomment below to fix the <see cref="ObjectDisposedException"/>
                //if (_Disposed)
                //{
                //    DDebug.WriteLine("View is disposed, ignored set video URI");
                //    return;
                //}

                var ptr = typeof(VideoView).GetMethod("SetVideoURI", new[] { typeof(AUri), typeof(IDictionary<string, string>) }).MethodHandle.GetFunctionPointer();
                var baseSetVideoURI = (Action<AUri, IDictionary<string, string>>)Activator.CreateInstance(typeof(Action<AUri, IDictionary<string, string>>), this, ptr);
                baseSetVideoURI?.Invoke(uri, headers);
            }
            catch (Exception ex)
            {
                DDebug.WriteLine(ex);
            }
        }
    }
}
