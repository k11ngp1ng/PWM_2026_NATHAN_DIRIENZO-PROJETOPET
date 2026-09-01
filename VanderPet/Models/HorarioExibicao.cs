namespace VanderPet.Models;

public class HorarioExibicao
{
	public TimeSpan Hora { get; set; }
	public string HoraTexto { get; set; } = string.Empty;
	public bool Disponivel { get; set; }

	// Propriedades visuais acopladas ao DTO para facilitar a mudança de cor sem usar Converters complexos
	public string CorFundo { get; set; } = "#FFFFFF";
	public string CorTexto { get; set; } = "#263238";
	public string CorBorda { get; set; } = "#E2E8F0";
}