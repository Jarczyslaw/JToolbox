using JToolbox.XamarinForms.Core.Base;
using Prism.Navigation;
using System;

namespace JToolbox.XamarinForms.Core.Navigation
{
    public class NavigationParams<TArg, TResult>
    {
        public static string CallbackKey => nameof(CallbackKey);
        public static string SourceViewModelKey => nameof(SourceViewModelKey);
        public static string ValueKey => nameof(ValueKey);
        public TArg Argument { get; set; }
        public Action<TResult> Callback { get; set; }
        public ViewModelBase SourceViewModel { get; set; }

        public static NavigationParams<TArg, TResult> CreateFromNavigationParameters(INavigationParameters navigationParameters)
        {
            if (navigationParameters.ContainsKey(SourceViewModelKey)
                && navigationParameters.ContainsKey(ValueKey))
            {
                return new NavigationParams<TArg, TResult>
                {
                    SourceViewModel = (ViewModelBase)navigationParameters[SourceViewModelKey],
                    Argument = (TArg)navigationParameters[ValueKey],
                    Callback = (Action<TResult>)navigationParameters[CallbackKey],
                };
            }
            return null;
        }

        public INavigationParameters CreateNavigationParameters()
        {
            return new NavigationParameters
            {
                { SourceViewModelKey, SourceViewModel },
                { ValueKey, Argument },
                { CallbackKey, Callback },
            };
        }
    }
}