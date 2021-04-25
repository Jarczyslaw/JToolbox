using System.Web.Mvc;

namespace JToolbox.MVC.Core
{
    public static class ViewEnginesConfig
    {
        public static void Setup()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}