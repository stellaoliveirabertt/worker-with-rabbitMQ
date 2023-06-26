using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace WorkerWithRabbitMQ
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConnectionFactory _connectionFactory;
        private readonly PaymentProcessor _paymentProcessor;

        public Worker(ILogger<Worker> logger, IConnectionFactory connectionFactory, PaymentProcessor paymentProcessor)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
            _paymentProcessor = paymentProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueName = "pagamentos";

            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, eventArgs) =>
                {
                    var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                    _logger.LogInformation($"Mensagem recebida: {message}");

                    // Processar a mensagem do pagamento
                    _paymentProcessor.ProcessPayment(message);

                    // Exibir notificação para o usuário
                    _logger.LogInformation($"Pagamento {message} processado com sucesso!");

                    channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
                };

                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                }
            }
        }
    }
}
