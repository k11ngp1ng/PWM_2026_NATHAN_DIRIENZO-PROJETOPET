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
		new Racas() { Id = 1, EspecieId = 1, Raca = "SRD (Sem Raça Definida)" },
		new Racas() { Id = 2, EspecieId = 1, Raca = "Poodle" },
		new Racas() { Id = 3, EspecieId = 1, Raca = "Pinscher" },
		new Racas() { Id = 4, EspecieId = 1, Raca = "Shih Tzu" },
		new Racas() { Id = 5, EspecieId = 1, Raca = "Yorkshire" },
		new Racas() { Id = 6, EspecieId = 1, Raca = "Pug" },
		new Racas() { Id = 7, EspecieId = 1, Raca = "Bulldog Francês" },
		new Racas() { Id = 8, EspecieId = 1, Raca = "Golden Retriever" },
		new Racas() { Id = 9, EspecieId = 1, Raca = "Labrador" },
		new Racas() { Id = 10, EspecieId = 1, Raca = "Spitz Alemão" },
		new Racas() { Id = 11, EspecieId = 1, Raca = "Pitbull" },
		new Racas() { Id = 12, EspecieId = 1, Raca = "Border Collie" },

		new Racas() { Id = 13, EspecieId = 2, Raca = "SRD (Sem Raça Definida)" },
		new Racas() { Id = 14, EspecieId = 2, Raca = "Siamês" },
		new Racas() { Id = 15, EspecieId = 2, Raca = "Persa" },
		new Racas() { Id = 16, EspecieId = 2, Raca = "Maine Coon" },
		new Racas() { Id = 17, EspecieId = 2, Raca = "Angorá" },
		new Racas() { Id = 18, EspecieId = 2, Raca = "Sphynx" },
		new Racas() { Id = 19, EspecieId = 2, Raca = "Bengal" },

		new Racas() { Id = 20, EspecieId = 3, Raca = "Não se aplica / SRD" },
		new Racas() { Id = 21, EspecieId = 3, Raca = "Calopsita" },
		new Racas() { Id = 22, EspecieId = 3, Raca = "Papagaio" },
		new Racas() { Id = 23, EspecieId = 3, Raca = "Canário" },

		new Racas() { Id = 24, EspecieId = 4, Raca = "Não se aplica / SRD" },
		new Racas() { Id = 25, EspecieId = 4, Raca = "Tartaruga" },
		new Racas() { Id = 26, EspecieId = 4, Raca = "Iguana" },

		new Racas() { Id = 27, EspecieId = 5, Raca = "Não se aplica / SRD" },
		new Racas() { Id = 28, EspecieId = 5, Raca = "Coelho" },
		new Racas() { Id = 29, EspecieId = 5, Raca = "Hamster" },
		new Racas() { Id = 30, EspecieId = 5, Raca = "Porquinho da Índia" },

		new Racas() { Id = 31, EspecieId = 6, Raca = "Não se aplica / SRD" }
	};

	public static ObservableCollection<Pet> lstPets = new ObservableCollection<Pet>()
	{
		new Pet() {
			Id = 1,
			Nome = "Thor",
			Especie = lstEspecies[0],
			Nascimento = new DateTime(2021, 5, 10),
			Peso = 15.5
		}
	};

	public static ObservableCollection<Agendamento> lstAgendamentos = new ObservableCollection<Agendamento>();

	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		// Cria a janela baseando-se na tela de Login inicial
		var window = new Window(new NavigationPage(new LoginPage()));

		// MÁGICA: Se o aplicativo estiver rodando no Windows (Desktop), forçamos o tamanho
#if WINDOWS
        window.Width = 420;  // Largura típica de smartphone
        window.Height = 780; // Altura típica de smartphone
#endif

		return window;
	}
}