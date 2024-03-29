﻿using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation.Exceptions;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JToolbox.XamarinForms.Core.Navigation
{
    public class NavigationMapper
    {
        private static readonly Lazy<NavigationMapper> instance = new Lazy<NavigationMapper>(() => new NavigationMapper());
        private readonly RegisterForNavigationWrapper wrapper = new RegisterForNavigationWrapper();

        public static NavigationMapper Instance => instance.Value;

        public Dictionary<Type, Type> ViewModelsMapping { get; } = new Dictionary<Type, Type>();

        public string ViewModelSuffix => "ViewModel";

        public List<string> ViewsSuffixes => new List<string>
        {
            "View",
            "Page",
            "Window"
        };

        public Type GetPageForViewModel(Type viewModelType)
        {
            CheckType(viewModelType, typeof(ViewModelBase));
            if (ViewModelsMapping.TryGetValue(viewModelType, out var type))
            {
                return type;
            }
            throw new NoPageException(viewModelType.Name);
        }

        public string GetPageName(Type pageType)
        {
            CheckType(pageType, typeof(PageBase));
            foreach (var suffix in ViewsSuffixes)
            {
                var tempName = pageType.Name.Replace(suffix, string.Empty);
                if (tempName.Length != pageType.Name.Length)
                {
                    return tempName;
                }
            }
            throw new Exception($"{pageType.Name} is not a valid page name");
        }

        public bool IsValidViewName(string typeName)
        {
            foreach (var suffix in ViewsSuffixes)
            {
                if (typeName.EndsWith(suffix))
                {
                    return true;
                }
            }
            return false;
        }

        public void Register<TPage, TViewModel>(IContainerRegistry containerRegistry)
            where TPage : PageBase
            where TViewModel : ViewModelBase
        {
            Register(containerRegistry, typeof(TPage), typeof(TViewModel));
        }

        public void Register(IContainerRegistry containerRegistry, Type pageType, Type viewModelType)
        {
            CheckType(pageType, typeof(PageBase));
            CheckType(viewModelType, typeof(ViewModelBase));
            wrapper.RegisterTypes(containerRegistry, pageType, viewModelType);
            ViewModelsMapping.Add(viewModelType, pageType);
        }

        public void Register(IContainerRegistry containerRegistry, Assembly assembly)
        {
            var types = assembly.GetTypes();
            var pages = types.Where(t => IsValidViewName(t.Name) && t.IsSubclassOf(typeof(PageBase)));
            var viewModels = types.Where(t => t.Name.EndsWith(ViewModelSuffix));
            foreach (var page in pages)
            {
                var pageName = GetPageName(page);
                var targetViewModelName = pageName + ViewModelSuffix;
                var pageViewModels = viewModels.Where(v => v.Name == targetViewModelName);
                if (!pageViewModels.Any())
                {
                    throw new NoViewModelException($"No view model found for: {page.Name}");
                }
                else if (pageViewModels.Count() > 1)
                {
                    var invalidViewModels = string.Join(",", pageViewModels.Select(p => p.FullName).ToArray());
                    throw new ToManyViewModelsException($"More than one view model found for: {page.Name}. Found view models: {invalidViewModels}");
                }
                else
                {
                    var viewModel = pageViewModels.First();
                    wrapper.RegisterTypes(containerRegistry, page, viewModel);
                    ViewModelsMapping.Add(viewModel, page);
                }
            }
        }

        private void CheckType(Type type, Type constraint)
        {
            if (type.IsAssignableFrom(constraint))
            {
                throw new InvalidTypeException(type, constraint);
            }
        }
    }
}