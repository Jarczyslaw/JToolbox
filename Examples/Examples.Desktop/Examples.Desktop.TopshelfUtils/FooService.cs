using JToolbox.Core.Abstraction;
using JToolbox.Misc.Logging;
using JToolbox.Misc.TopshelfUtils;

namespace Examples.Desktop.TopshelfUtils
{
    internal class FooService : LocalService
    {
        private readonly ILoggerService _loggerService = new LoggerService();

        public override string ServiceName => "FooService";

        public override bool Continue()
        {
            _loggerService.Info(nameof(Continue));
            return base.Continue();
        }

        public override bool Pause()
        {
            _loggerService.Info(nameof(Pause));
            return base.Pause();
        }

        public override bool Start()
        {
            _loggerService.Info(nameof(Start));
            return base.Start();
        }

        public override bool Stop()
        {
            _loggerService.Info(nameof(Stop));
            return base.Stop();
        }
    }
}