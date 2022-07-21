using Android.Content;
using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Droid.Core;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PageBase), typeof(PageBaseRenderer))]

namespace JToolbox.XamarinForms.Droid.Core
{
    public class PageBaseRenderer : PageRenderer
    {
        public PageBaseRenderer(Context context) : base(context)
        {
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            if (Element is PageBase pageBase)
            {
                pageBase.OnAppeared();
            }
        }
    }
}