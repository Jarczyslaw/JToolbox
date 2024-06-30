using System;
using Topshelf;
using Topshelf.HostConfigurators;

namespace JToolbox.Misc.TopshelfUtils
{
    public abstract class LocalService
    {
        public virtual string Description => ServiceName;

        public virtual string DisplayName => ServiceName;

        public abstract string ServiceName { get; }

        public virtual bool StartAutomatically => true;

        public virtual void AdditionalConfiguration(HostConfigurator hostConfigurator)
        {
            hostConfigurator.RunAsLocalSystem();
        }

        public virtual bool Continue() => true;

        public virtual void OnException(Exception exception)
        {
        }

        public virtual bool Pause() => true;

        public void Run()
        {
            HostFactory.Run(x =>
            {
                x.Service<LocalService>(y =>
                {
                    y.ConstructUsing(_ => this);
                    y.WhenStarted(z => z.Start());
                    y.WhenStopped(z => z.Stop());
                    y.WhenPaused(z => z.Pause());
                    y.WhenContinued(z => z.Continue());
                    y.WhenShutdown(z => z.Shutdown());
                });

                x.EnablePauseAndContinue();
                x.EnableShutdown();
                x.OnException(OnException);

                if (StartAutomatically)
                {
                    x.StartAutomaticallyDelayed();
                }

                x.SetDescription(Description);
                x.SetDisplayName(DisplayName);
                x.SetServiceName(ServiceName);

                x.EnableServiceRecovery(ServiceRecovery);

                AdditionalConfiguration(x);
            });
        }

        public virtual void ServiceRecovery(ServiceRecoveryConfigurator serviceRecoveryConfigurator)
        {
            serviceRecoveryConfigurator.TakeNoAction();
        }

        public virtual bool Shutdown() => true;

        public virtual bool Start() => true;

        public virtual bool Stop() => true;
    }
}