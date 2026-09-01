namespace VanderPet.Models;

public class Agendamento
{
	public int Id { get; set; }
	public Pet Pet { get; set; } = new Pet();

	// Usamos o DTO para travar o preço exato do momento da compra
	public ServicoExibicao Servico { get; set; } = new ServicoExibicao();

	public DateTime Data { get; set; }
	public TimeSpan Horario { get; set; }
	public string Observacao { get; set; } = string.Empty;
	public string Status { get; set; } = "Confirmado";
}