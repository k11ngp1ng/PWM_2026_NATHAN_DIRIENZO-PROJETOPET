using System;

namespace VanderPet.Models;

public class Servicos
{
	public int Id { get; set; }
	public string Servico { get; set; } = string.Empty;
	public string Descricao { get; set; } = string.Empty;

	// Tempo e Preço base, antes de calcular o peso/porte do animal
	public int DuracaoMinutosBase { get; set; }
	public decimal PrecoBase { get; set; }
}