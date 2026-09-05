using System;

namespace VanderPet.Models;

public class Pet
{
	public int Id { get; set; }
	public string Nome { get; set; } = string.Empty;
	public Especies? Especie { get; set; }
	public Racas? Raca { get; set; }
	public string? Sexo { get; set; }
	public DateTime Nascimento { get; set; }
	public double Peso { get; set; }
	public string Observacoes { get; set; } = string.Empty;
}