using Examples.Desktop.MVP.Forms;
using Examples.Desktop.MVP.Presenters;
using JToolbox.WinForms.MVP.Unity;
using System;
using System.Collections.Generic;
using Unity;

namespace Examples.Desktop.MVP
{
    public class AppPresenterFactory : UnityPresenterFactory
    {
        protected override Dictionary<Type, Type> ViewPresenterPairs { get; } = new Dictionary<Type, Type>
        {
            { typeof(MainPresenter), typeof(MainForm) },
            { typeof(ResultPresenter), typeof(ResultForm) }
        };

        public AppPresenterFactory(IUnityContainer container)
            : base(container)
        {
        }
    }
}