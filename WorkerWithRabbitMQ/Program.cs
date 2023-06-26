using RabbitMQ.Client;
using Microsoft.Extensions.Options;

namespace WorkerWithRabbitMQ
{
  class Program
  {
    static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        var worker = services.GetRequiredService<Worker>();

        worker.StartAsync(default).GetAwaiter().GetResult();

        host.Run();
      }
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, config) =>
            {
              config.AddJsonFile("appsettings.json", optional: true);
              config.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
              services.AddSingleton<Worker>();
              services.AddSingleton<PaymentProcessor>();

              services.AddSingleton<IConnectionFactory>(provider =>
                  {
                    var rabbitMQSettings = provider.GetRequiredService<IOptions<RabbitMQSettings>>().Value;

                    return new ConnectionFactory
                    {
                      HostName = rabbitMQSettings.Hostname,
                      Port = rabbitMQSettings.Port,
                      UserName = rabbitMQSettings.Username,
                      Password = rabbitMQSettings.Password
                    };
                  });

              services.Configure<RabbitMQSettings>(hostContext.Configuration.GetSection("RabbitMQ"));
              services.AddHostedService<Worker>();
            })
            .UseConsoleLifetime();
  }
}
