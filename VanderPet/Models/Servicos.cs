namespace VanderPet.Models;

// O serviço puro, como cadastrado no banco de dados do Pet Shop
public class Servicos
{
	public int Id { get; set; }
	public string Servico { get; set; } = string.Empty;
	public string Descricao { get; set; } = string.Empty;
	public int DuracaoMinutosBase { get; set; }
	public decimal PrecoBase { get; set; }
}

// DTO: Objeto temporário usado apenas para desenhar a tela com os valores já recalculados
public class ServicoExibicao
{
	public int Id { get; set; }
	public string Servico { get; set; } = string.Empty;
	public string Descricao { get; set; } = string.Empty;
	public decimal PrecoCalculado { get; set; }
	public int DuracaoMinutosFinal { get; set; }
	public string DuracaoFormatada { get; set; } = string.Empty;
}