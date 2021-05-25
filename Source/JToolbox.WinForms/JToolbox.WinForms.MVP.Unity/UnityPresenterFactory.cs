using System;
using System.Collections.Generic;
using Unity;

namespace JToolbox.WinForms.MVP.Unity
{
    public abstract class UnityPresenterFactory : PresenterFactory
    {
        private readonly IUnityContainer container;

        public UnityPresenterFactory(IUnityContainer container)
        {
            this.container = container;
        }

        protected virtual Dictionary<Type, Type> ViewPresenterPairs { get; }

        protected override TPresenter ResolvePresenter<TPresenter, TView>()
        {
            return container.Resolve<TPresenter>();
        }

        protected override TView ResolveView<TPresenter, TView>()
        {
            return (TView)container.Resolve(ViewPresenterPairs[typeof(TPresenter)]);
        }
    }
}