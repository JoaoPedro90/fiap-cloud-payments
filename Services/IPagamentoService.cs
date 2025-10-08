using PaymentsService.DTOs;
using PaymentsService.Models;

namespace PaymentsService.Services
{
    public interface IPagamentoService
    {
        Transacao ProcessarPagamento(PagamentoRequestDto pagamento);
    }
}
