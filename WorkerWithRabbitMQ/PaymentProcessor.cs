using Microsoft.Extensions.Logging;
using System;

namespace WorkerWithRabbitMQ
{
    public class PaymentProcessor
    {
        private readonly ILogger<PaymentProcessor> _logger;

        public PaymentProcessor(ILogger<PaymentProcessor> logger)
        {
            _logger = logger;
        }

        public void ProcessPayment(string paymentId)
        {
            // Lógica para processar o pagamento
            _logger.LogInformation($"Processando pagamento com ID: {paymentId}");

            // Exibir notificação para o usuário (exemplo simplificado)
            Console.WriteLine($"Pagamento {paymentId} processado com sucesso!");
        }
    }
}
