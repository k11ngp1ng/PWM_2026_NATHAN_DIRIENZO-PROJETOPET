using System;

namespace VanderPet.Models;

public class Agendamento
{
	public int Id { get; set; }
	public Pet Pet { get; set; } = new();
	public Servicos Servico { get; set; } = new();

	// NOVA PROPRIEDADE: O porte agora pertence ao evento do serviço
	public string Porte { get; set; } = string.Empty;

	public DateTime Data { get; set; }
	public TimeSpan Horario { get; set; }
	public string Status { get; set; } = string.Empty;
	public string Observacao { get; set; } = string.Empty;
}