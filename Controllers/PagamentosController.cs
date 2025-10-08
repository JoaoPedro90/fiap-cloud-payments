using Microsoft.AspNetCore.Mvc;
using PaymentsService.DTOs;
using PaymentsService.Models;
using PaymentsService.Repositories;
using PaymentsService.Services;

namespace PaymentsService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosController : ControllerBase
    {
        private readonly IPagamentoService _pagamentoService;
        private readonly IEventStore _eventStore;


        public PagamentosController(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        [HttpPost]
        public IActionResult ProcessarPagamento([FromBody] PagamentoRequestDto pagamentoDto)
        {
            var transacao = _pagamentoService.ProcessarPagamento(pagamentoDto);
            return Ok(transacao);
        }

        //[HttpPost]
        //public IActionResult CriarPagamento(PagamentoRequestDto dto)
        //{
        //    var transacao = Transacao.Criar(Guid.NewGuid().ToString(), dto.Valor);

        //    foreach (var evento in transacao.EventosPendentes)
        //        _eventStore.SalvarEvento(evento);

        //    return Ok(new { mensagem = "Pagamento criado com sucesso" });
        //}
    }
}
