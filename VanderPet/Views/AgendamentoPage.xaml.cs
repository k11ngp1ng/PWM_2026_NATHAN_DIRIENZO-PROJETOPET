using VanderPet.Models;

namespace VanderPet.Views;

public partial class AgendamentoPage : ContentPage
{
	private Pet? _petSelecionado;
	private ServicoExibicao? _servicoSelecionado;
	private HorarioExibicao? _horarioSelecionado;

	public AgendamentoPage()
	{
		InitializeComponent();
		dtpData.MinimumDate = DateTime.Today;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		BindableLayout.SetItemsSource(hslPets, null);
		BindableLayout.SetItemsSource(hslPets, App.lstPets);

		string porteInicial = _petSelecionado?.Porte ?? "Pequeno";
		RecalcularServicos(porteInicial);
		GerarHorarios();
	}

	private void RecalcularServicos(string? porte)
	{
		decimal multiplicador = 1.0m;
		int acrescimoTempo = 0;

		if (porte == "Médio") { multiplicador = 1.2m; acrescimoTempo = 15; }
		else if (porte == "Grande") { multiplicador = 1.5m; acrescimoTempo = 30; }

		var catalogoRecalculado = new List<ServicoExibicao>();

		foreach (var s in App.lstServicos)
		{
			int tempoFinal = s.DuracaoMinutosBase + acrescimoTempo;
			int horas = tempoFinal / 60;
			int minutos = tempoFinal % 60;
			string textoDuracao = horas > 0
				? $"Aprox. {horas}h{(minutos > 0 ? " " + minutos + "min" : "")}"
				: $"Aprox. {minutos}min";

			catalogoRecalculado.Add(new ServicoExibicao
			{
				Id = s.Id,
				Servico = s.Servico,
				Descricao = s.Descricao,
				PrecoCalculado = s.PrecoBase * multiplicador,
				DuracaoMinutosFinal = tempoFinal,
				DuracaoFormatada = textoDuracao
			});
		}

		_servicoSelecionado = null;
		BindableLayout.SetItemsSource(vslServicos, null);
		BindableLayout.SetItemsSource(vslServicos, catalogoRecalculado);
	}

	private void GerarHorarios()
	{
		var horariosManha = new List<HorarioExibicao>();
		var horariosTarde = new List<HorarioExibicao>();

		TimeSpan horaAbertura = new TimeSpan(9, 0, 0);
		TimeSpan horaFechamento = new TimeSpan(18, 0, 0);
		TimeSpan intervalo = new TimeSpan(0, 30, 0);

		int duracaoServico = _servicoSelecionado?.DuracaoMinutosFinal ?? 30;
		DateTime dataEscolhida = Convert.ToDateTime(dtpData.Date);

		for (TimeSpan h = horaAbertura; h < horaFechamento; h = h.Add(intervalo))
		{
			bool disponivel = true;

			if (h.Add(TimeSpan.FromMinutes(duracaoServico)) > horaFechamento) disponivel = false;
			if (dataEscolhida.Date == DateTime.Today.Date && h <= DateTime.Now.TimeOfDay) disponivel = false;
			if (h == new TimeSpan(14, 0, 0)) disponivel = false;

			var novoHorario = new HorarioExibicao
			{
				Hora = h,
				HoraTexto = disponivel ? h.ToString(@"hh\:mm") : $"{h.ToString(@"hh\:mm")} (Ocupado)",
				Disponivel = disponivel,
				CorFundo = disponivel ? "#FFFFFF" : "#F1F5F9",
				CorTexto = disponivel ? "#263238" : "#A0AEC0",
				CorBorda = "#E2E8F0"
			};

			if (h.Hours < 12) horariosManha.Add(novoHorario);
			else horariosTarde.Add(novoHorario);
		}

		_horarioSelecionado = null;
		BindableLayout.SetItemsSource(flxHorariosManha, null);
		BindableLayout.SetItemsSource(flxHorariosManha, horariosManha);

		BindableLayout.SetItemsSource(flxHorariosTarde, null);
		BindableLayout.SetItemsSource(flxHorariosTarde, horariosTarde);
	}

	// LÓGICA DAS ABAS
	private void OnAbaManhaTapped(object? sender, TappedEventArgs e)
	{
		brdAbaManha.BackgroundColor = Color.FromArgb("#3A8D7A");
		brdAbaManha.Stroke = Color.FromArgb("#3A8D7A");
		lblAbaManha.TextColor = Color.FromArgb("#FFFFFF");
		flxHorariosManha.IsVisible = true;

		brdAbaTarde.BackgroundColor = Color.FromArgb("#FFFFFF");
		brdAbaTarde.Stroke = Color.FromArgb("#E2E8F0");
		lblAbaTarde.TextColor = Color.FromArgb("#718096");
		flxHorariosTarde.IsVisible = false;
	}

	private void OnAbaTardeTapped(object? sender, TappedEventArgs e)
	{
		brdAbaTarde.BackgroundColor = Color.FromArgb("#3A8D7A");
		brdAbaTarde.Stroke = Color.FromArgb("#3A8D7A");
		lblAbaTarde.TextColor = Color.FromArgb("#FFFFFF");
		flxHorariosTarde.IsVisible = true;

		brdAbaManha.BackgroundColor = Color.FromArgb("#FFFFFF");
		brdAbaManha.Stroke = Color.FromArgb("#E2E8F0");
		lblAbaManha.TextColor = Color.FromArgb("#718096");
		flxHorariosManha.IsVisible = false;
	}

	private async void OnAdicionarPetTapped(object? sender, TappedEventArgs e)
	{
		await Navigation.PushModalAsync(new CadastroPetPage());
	}

	private void OnPetTapped(object? sender, TappedEventArgs e)
	{
		if (sender is Border borderClicado)
		{
			_petSelecionado = (Pet)borderClicado.BindingContext;
			foreach (var view in hslPets.Children)
			{
				if (view is Border b)
				{
					b.Stroke = Color.FromArgb("#E2E8F0");
					b.BackgroundColor = Color.FromArgb("#FFFFFF");
					if (b.Content is Label lbl) lbl.TextColor = Color.FromArgb("#263238");
				}
			}
			borderClicado.Stroke = Color.FromArgb("#3A8D7A");
			borderClicado.BackgroundColor = Color.FromArgb("#E8F3F1");
			if (borderClicado.Content is Label labelSelecionado) labelSelecionado.TextColor = Color.FromArgb("#3A8D7A");

			RecalcularServicos(_petSelecionado.Porte);
			GerarHorarios();
		}
	}

	private void OnServicoTapped(object? sender, TappedEventArgs e)
	{
		if (sender is Border borderClicado)
		{
			_servicoSelecionado = (ServicoExibicao)borderClicado.BindingContext;
			foreach (var view in vslServicos.Children)
			{
				if (view is Border b)
				{
					b.Stroke = Color.FromArgb("#E2E8F0");
					b.BackgroundColor = Color.FromArgb("#FFFFFF");
				}
			}
			borderClicado.Stroke = Color.FromArgb("#3A8D7A");
			borderClicado.BackgroundColor = Color.FromArgb("#E8F3F1");

			GerarHorarios();
		}
	}

	private void OnDataSelecionada(object? sender, DateChangedEventArgs e)
	{
		GerarHorarios();
	}

	private void LimparSelecaoDeHorarios(Microsoft.Maui.Controls.FlexLayout layout)
	{
		foreach (var view in layout.Children)
		{
			if (view is Border b)
			{
				var ctx = (HorarioExibicao)b.BindingContext;
				if (ctx.Disponivel)
				{
					b.Stroke = Color.FromArgb("#E2E8F0");
					b.BackgroundColor = Color.FromArgb("#FFFFFF");
					if (b.Content is Label lbl) lbl.TextColor = Color.FromArgb("#263238");
				}
			}
		}
	}

	private async void OnHorarioTapped(object? sender, TappedEventArgs e)
	{
		if (sender is Border borderClicado)
		{
			var horarioContext = (HorarioExibicao)borderClicado.BindingContext;
			if (!horarioContext.Disponivel)
			{
				await DisplayAlertAsync("Indisponível", "Este horário não está disponível.", "OK");
				return;
			}

			_horarioSelecionado = horarioContext;
			LimparSelecaoDeHorarios(flxHorariosManha);
			LimparSelecaoDeHorarios(flxHorariosTarde);

			borderClicado.Stroke = Color.FromArgb("#3A8D7A");
			borderClicado.BackgroundColor = Color.FromArgb("#3A8D7A");
			if (borderClicado.Content is Label labelSelecionado)
				labelSelecionado.TextColor = Color.FromArgb("#FFFFFF");
		}
	}

	private async void OnConfirmarTapped(object? sender, EventArgs e)
	{
		if (_petSelecionado == null)
		{
			await DisplayAlertAsync("Atenção", "Por favor, selecione um pet antes de agendar.", "OK");
			return;
		}
		if (_servicoSelecionado == null)
		{
			await DisplayAlertAsync("Atenção", "Por favor, selecione um serviço para continuar.", "OK");
			return;
		}
		if (_horarioSelecionado == null)
		{
			await DisplayAlertAsync("Atenção", "Por favor, selecione um horário disponível.", "OK");
			return;
		}

		// NAVEGAÇÃO PARA A ETAPA 9
		await Navigation.PushAsync(new ConfirmacaoPage(_petSelecionado, _servicoSelecionado, Convert.ToDateTime(dtpData.Date), _horarioSelecionado));
	}
}