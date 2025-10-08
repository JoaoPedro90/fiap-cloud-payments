using PaymentsService.DTOs;
using PaymentsService.Models;
using Stripe;

namespace PaymentsService.Services
{
    public class StripePagamentoService : IPagamentoService
    {
        public Transacao ProcessarPagamento(PagamentoRequestDto pagamento)
        {
            var options = new ChargeCreateOptions
            {
                Amount = (long)(pagamento.Valor * 100), // Stripe cobra em centavos
                Currency = "usd", // ou sua moeda
                Source = "tok_visa", // usar fonte de teste, ex: cartão de teste
                Description = $"Pagamento do jogo {pagamento.JogoId} para usuário {pagamento.UsuarioId}"
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);

            var status = charge.Status == "succeeded" ? "Aprovado" : "Recusado";

            return new Transacao
            {
                UsuarioId = pagamento.UsuarioId,
                JogoId = pagamento.JogoId,
                Valor = pagamento.Valor,
                MetodoPagamento = "Stripe",
                Status = status,
                Data = DateTime.UtcNow
            };
        }
    }
}
