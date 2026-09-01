using VanderPet.Models;

namespace VanderPet.Views;

public partial class ConfirmacaoPage : ContentPage
{
	private Pet _pet;
	private ServicoExibicao _servico;
	private DateTime _data;
	private HorarioExibicao _horario;

	public ConfirmacaoPage(Pet pet, ServicoExibicao servico, DateTime data, HorarioExibicao horario)
	{
		InitializeComponent();
		_pet = pet;
		_servico = servico;
		_data = data;
		_horario = horario;

		PreencherResumo();
	}

	private void PreencherResumo()
	{
		lblPet.Text = _pet.Nome;
		lblServico.Text = _servico.Servico;
		lblData.Text = _data.ToString("dd/MM/yyyy");
		lblHorario.Text = _horario.HoraTexto;
		lblTotal.Text = _servico.PrecoCalculado.ToString("C"); // Formata automaticamente para Moeda Local (R$)
	}

	private async void OnConfirmarFinalTapped(object? sender, EventArgs e)
	{
		Agendamento novoAgendamento = new Agendamento
		{
			Id = App.lstAgendamentos.Count + 1,
			Pet = _pet,
			Servico = _servico,
			Data = _data,
			Horario = _horario.Hora,
			Observacao = edtObservacoes.Text ?? string.Empty
		};

		App.lstAgendamentos.Add(novoAgendamento);

		await DisplayAlertAsync("Sucesso!", "Seu agendamento foi confirmado com sucesso. Te esperamos lá!", "OK");

		// Retorna para a tela de agendamento original após a confirmação
		await Navigation.PopAsync();
	}

	private async void OnVoltarTapped(object? sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}