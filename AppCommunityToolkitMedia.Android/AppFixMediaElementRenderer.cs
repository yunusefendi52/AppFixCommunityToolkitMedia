using System;
using DDebug = System.Diagnostics.Debug;
using Android.Content;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Android.Views;
using Android.Widget;
using AppCommunityToolkitMedia.Droid.Renderers;

[assembly: ExportRenderer(typeof(MediaElement), typeof(AppFixMediaElementRenderer))]
namespace AppCommunityToolkitMedia.Droid.Renderers
{
    /// <summary>
    /// This is only trying to demonstrate "loading" behavior and will override existing <see cref="FormsVideoView"/>
    /// and also uses reflection, if you can access the source code directly, fix there directly
    /// </summary>
    public class AppFixMediaElementRenderer : MediaElementRenderer
	{
		public AppFixMediaElementRenderer(Context context) : base(context)
		{
			EventHandler getMetdataHandler()
			{
				var handler = (EventHandler)Delegate.CreateDelegate(
					typeof(EventHandler), this, "MetadataRetrieved");
				return handler;
			}

			var prevViewIndex = IndexOfChild(view);
			if (prevViewIndex != -1)
			{
				var handler = getMetdataHandler();
				view.MetadataRetrieved -= handler;
				RemoveView(view);
			}
			view = new AppFormsVideoView(context);
			view.SetZOrderMediaOverlay(true);
			view.SetOnCompletionListener(this);
			view.SetOnInfoListener(this);
			view.SetOnPreparedListener(this);
			view.SetOnErrorListener(this);
			var metadataRetrieved = getMetdataHandler();
			view.MetadataRetrieved += metadataRetrieved;

			SetForegroundGravity(GravityFlags.Center);

			AddView(view, -1, -1);

			controller = new MediaController(Context);
			controller.SetAnchorView(this);
			view.SetMediaController(controller);
		}

		protected override void UpdateSource()
		{
			try
			{
				var t = view.GetType();
				base.UpdateSource();
			}
			catch (Exception ex)
			{
				DDebug.WriteLine(ex);
			}
		}
	}
}
