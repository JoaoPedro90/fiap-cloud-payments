namespace PaymentsService.DTOs
{
    public class PagamentoRequestDto
    {
        public string UsuarioId { get; set; }
        public string JogoId { get; set; }
        public decimal Valor { get; set; }
        public string MetodoPagamento { get; set; }
    }
}
