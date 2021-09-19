using Examples.Desktop.MVP.Forms;
using JToolbox.WinForms.MVP.Unity;
using System;
using System.Collections.Generic;
using Unity;

namespace Examples.Desktop.MVP
{
    public class AppPresenterFactory : UnityPresenterFactory
    {
        public AppPresenterFactory(IUnityContainer container)
            : base(container)
        {
        }

        protected override Dictionary<string, Type> Views { get; } = new Dictionary<string, Type>
        {
            { ViewKeys.Main, typeof(MainForm) },
            { ViewKeys.Result, typeof(ResultForm) }
        };
    }
}