using System.Collections.ObjectModel;
using System.Linq;
using VanderPet.Models;

namespace VanderPet.Views;

public partial class HistoricoPage : ContentPage
{
	public ObservableCollection<Agendamento> ListaAgendamentos { get; set; } = new();

	public HistoricoPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		CarregarAgendamentosReais();
	}

	private void CarregarAgendamentosReais()
	{
		ListaAgendamentos.Clear();

		if (App.lstAgendamentos != null && App.lstAgendamentos.Count > 0)
		{
			var agendamentosOrdenados = App.lstAgendamentos
				.OrderByDescending(a => a.Data)
				.ThenByDescending(a => a.Horario);

			foreach (var agendamento in agendamentosOrdenados)
			{
				ListaAgendamentos.Add(agendamento);
			}
		}
	}

	// REGRA DE NEGÓCIO: Verifica se o usuário tem pets antes de agendar
	private async void OnNovoAgendamentoClicked(object sender, EventArgs e)
	{
		if (App.lstPets == null || App.lstPets.Count == 0)
		{
			await DisplayAlert("Atenção", "Para realizar um agendamento, você precisa cadastrar seu pet primeiro!", "OK");

			// Redireciona para o cadastro do pet sem reiniciar o fluxo
			await Navigation.PushAsync(new CadastroPetPage());
		}
		else
		{
			// Se já tem pet, vai direto para a tela de escolha de serviços e horários
			await Navigation.PushAsync(new AgendamentoPage());
		}
	}
}