using System.Collections.ObjectModel;
using VanderPet.Models;
using VanderPet.Views;

namespace VanderPet;

public partial class App : Application
{
	public static List<Especies> lstEspecies = new List<Especies>()
	{
		new Especies() { Id = 1, Especie = "Cão" },
		new Especies() { Id = 2, Especie = "Gato" },
		new Especies() { Id = 3, Especie = "Ave" },
		new Especies() { Id = 4, Especie = "Réptil" },
		new Especies() { Id = 5, Especie = "Roedor" },
		new Especies() { Id = 6, Especie = "Outro" }
	};

	public static List<Servicos> lstServicos = new List<Servicos>()
	{
		new Servicos() { Id = 1, Servico = "Banho", Descricao = "Higienização completa com produtos premium.", DuracaoMinutosBase = 60, PrecoBase = 50.00m },
		new Servicos() { Id = 2, Servico = "Tosa", Descricao = "Corte na máquina ou tesoura, padrão da raça.", DuracaoMinutosBase = 60, PrecoBase = 70.00m },
		new Servicos() { Id = 3, Servico = "Banho + Tosa", Descricao = "Pacote completo de higiene e estética.", DuracaoMinutosBase = 90, PrecoBase = 100.00m },
		new Servicos() { Id = 4, Servico = "Hidratação", Descricao = "Tratamento profundo para recuperação dos pelos.", DuracaoMinutosBase = 40, PrecoBase = 45.00m },
		new Servicos() { Id = 5, Servico = "Corte de Unhas", Descricao = "Apara segura e lixamento das unhas.", DuracaoMinutosBase = 15, PrecoBase = 20.00m }
	};

	public static List<Racas> lstRacas = new List<Racas>()
	{
        // ... (Para economizar espaço visual aqui no prompt, mantenha todas aquelas raças de 1 a 30 que criamos antes)
        new Racas() { Id = 1, Raca = "SRD (Sem Raça Definida)", EspecieId = 1 },
		new Racas() { Id = 11, Raca = "SRD (Sem Raça Definida)", EspecieId = 2 }
	};

	public static System.Collections.ObjectModel.ObservableCollection<Pet> lstPets = new System.Collections.ObjectModel.ObservableCollection<Pet>()
	{
		new Pet() {
			Id = 1,
			Nome = "Thor",
			Especie = lstEspecies[0],
			Nascimento = new DateTime(2021, 5, 10),
			Peso = 15.5,
			Porte = "Médio"
		}
	};

	// NOVA LISTA GLOBAL: Para guardar o histórico de agendamentos
	public static ObservableCollection<Agendamento> lstAgendamentos = new ObservableCollection<Agendamento>();

	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new NavigationPage(new LoginPage()));
	}
}