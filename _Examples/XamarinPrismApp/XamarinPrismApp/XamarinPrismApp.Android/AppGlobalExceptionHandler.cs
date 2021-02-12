using JToolbox.XamarinForms.Core;
using JToolbox.XamarinForms.Logging;
using Prism.Ioc;
using System;

namespace XamarinPrismApp.Droid
{
    public class AppGlobalExceptionHandler : GlobalExceptionHandler
    {
        protected override void HandleException(string exceptionSource, Exception exception)
        {
            var logger = App.ContainerProvider.Resolve<ILoggerService>();
            logger.Fatal(exceptionSource, exception);
        }
    }
}