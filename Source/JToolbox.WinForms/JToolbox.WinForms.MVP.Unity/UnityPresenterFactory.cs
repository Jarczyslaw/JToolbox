using System;
using System.Collections.Generic;
using Unity;

namespace JToolbox.WinForms.MVP.Unity
{
    public abstract class UnityPresenterFactory : PresenterFactory
    {
        private readonly IUnityContainer container;

        protected UnityPresenterFactory(IUnityContainer container)
        {
            this.container = container;
        }

        protected virtual Dictionary<string, Type> Views { get; }

        protected override TPresenter ResolvePresenter<TPresenter, TView>()
        {
            return container.Resolve<TPresenter>();
        }

        protected override IView ResolveView(string viewKey)
        {
            if (Views.TryGetValue(viewKey, out Type viewType))
            {
                return container.Resolve(viewType) as IView;
            }
            return null;
        }
    }
}