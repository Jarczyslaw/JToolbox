using JToolbox.Core.Abstraction;
using JToolbox.Misc.Logging;
using JToolbox.Misc.TopshelfUtils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Examples.Desktop.TopshelfUtils
{
    internal class FooService : LocalService
    {
        private readonly ILoggerService _loggerService = new LoggerService();
        private WebApplication _webApplication;

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

        public override bool Start(bool launchedInConsole)
        {
            var builder = WebApplication.CreateBuilder();

#if DEBUG
            builder.Environment.EnvironmentName = Environments.Development;
#endif

            builder.WebHost.UseUrls("http://localhost:5000/");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowAll");

            app.UseSwaggerUI();
            app.UseSwagger();

            app.Services.GetRequiredService<IHostApplicationLifetime>()
                .ApplicationStopping.Register(() =>
                {
                });

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Start();

            _webApplication = app;

            _loggerService.Info(nameof(Start));
            return base.Start(launchedInConsole);
        }

        public override bool Stop()
        {
            _webApplication?.StopAsync().Wait();

            _loggerService.Info(nameof(Stop));
            return base.Stop();
        }
    }
}