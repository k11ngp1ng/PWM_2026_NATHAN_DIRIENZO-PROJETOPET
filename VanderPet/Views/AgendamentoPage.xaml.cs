using System;
using System.Collections.ObjectModel;
using VanderPet.Models;

namespace VanderPet.Views;

public partial class AgendamentoPage : ContentPage
{
	public ObservableCollection<ServicoExibicao> ListaServicos { get; set; } = new();
	public ObservableCollection<HorarioExibicao> ListaHorarios { get; set; } = new();

	private ServicoExibicao? _servicoSelecionado;

	public AgendamentoPage()
	{
		InitializeComponent();
		BindingContext = this;
		CarregarDadosFixos();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		pckPet.ItemsSource = null;
		pckPet.ItemsSource = App.lstPets;

		if (App.lstPets.Count == 1)
		{
			pckPet.SelectedIndex = 0;
		}

		AtualizarServicos();
	}

	private void CarregarDadosFixos()
	{
		dtpData.MinimumDate = DateTime.Today;

		ListaHorarios.Add(new HorarioExibicao { Hora = new TimeSpan(8, 0, 0), HoraTexto = "08:00 - Manhã" });
		ListaHorarios.Add(new HorarioExibicao { Hora = new TimeSpan(9, 30, 0), HoraTexto = "09:30 - Manhã" });
		ListaHorarios.Add(new HorarioExibicao { Hora = new TimeSpan(11, 0, 0), HoraTexto = "11:00 - Manhã" });
		ListaHorarios.Add(new HorarioExibicao { Hora = new TimeSpan(14, 0, 0), HoraTexto = "14:00 - Tarde" });
		ListaHorarios.Add(new HorarioExibicao { Hora = new TimeSpan(15, 30, 0), HoraTexto = "15:30 - Tarde" });
		ListaHorarios.Add(new HorarioExibicao { Hora = new TimeSpan(17, 0, 0), HoraTexto = "17:00 - Tarde" });

		pckHorario.ItemsSource = ListaHorarios;
	}

	private void OnPorteChanged(object sender, EventArgs e)
	{
		AtualizarServicos();
	}

	private void AtualizarServicos()
	{
		string porte = pckPorte.SelectedItem?.ToString() ?? "Pequeno";
		decimal multPreco = 1.0m;
		double multTempo = 1.0;

		if (porte == "Médio")
		{
			multPreco = 1.2m;
			multTempo = 1.2;
		}
		else if (porte == "Grande")
		{
			multPreco = 1.5m;
			multTempo = 1.5;
		}

		int? idSelecionado = _servicoSelecionado?.Id;
		_servicoSelecionado = null;
		ListaServicos.Clear();

		foreach (var baseSvc in App.lstServicos)
		{
			var novoSvc = new ServicoExibicao
			{
				Id = baseSvc.Id,
				Servico = baseSvc.Servico,
				Descricao = baseSvc.Descricao,
				PrecoCalculado = baseSvc.PrecoBase * multPreco,
				DuracaoMinutosFinal = (int)(baseSvc.DuracaoMinutosBase * multTempo)
			};

			if (idSelecionado.HasValue && novoSvc.Id == idSelecionado.Value)
			{
				novoSvc.IsSelected = true;
				_servicoSelecionado = novoSvc;
			}

			ListaServicos.Add(novoSvc);
		}
	}

	private void OnServicoTapped(object sender, TappedEventArgs e)
	{
		if (e.Parameter is ServicoExibicao servicoClicado)
		{
			foreach (var s in ListaServicos)
			{
				s.IsSelected = false;
			}

			servicoClicado.IsSelected = true;
			_servicoSelecionado = servicoClicado;
		}
	}

	private async void OnNovoPetTapped(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new CadastroPetPage());
	}

	private async void OnAvancarClicked(object sender, EventArgs e)
	{
		if (pckPet.SelectedItem == null)
		{
			await DisplayAlertAsync("Atenção", "Selecione qual pet receberá o serviço.", "OK");
			return;
		}

		if (pckPorte.SelectedItem == null)
		{
			await DisplayAlertAsync("Atenção", "Selecione o porte do pet para este serviço.", "OK");
			return;
		}

		if (_servicoSelecionado == null)
		{
			await DisplayAlertAsync("Atenção", "Selecione um serviço tocando em um dos cards.", "OK");
			return;
		}

		if (pckHorario.SelectedItem == null)
		{
			await DisplayAlertAsync("Atenção", "Selecione um horário disponível.", "OK");
			return;
		}

		Pet petEscolhido = (Pet)pckPet.SelectedItem;
		string porteEscolhido = pckPorte.SelectedItem?.ToString() ?? string.Empty;
		HorarioExibicao horarioEscolhido = (HorarioExibicao)pckHorario.SelectedItem;

		await Navigation.PushAsync(new ConfirmacaoPage(petEscolhido, _servicoSelecionado, porteEscolhido, Convert.ToDateTime(dtpData.Date), horarioEscolhido));
	}

	private async void OnVoltarTapped(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}