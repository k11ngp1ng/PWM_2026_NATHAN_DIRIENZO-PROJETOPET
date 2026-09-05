using System;
using VanderPet.Models;

namespace VanderPet.Views;

public partial class ConfirmacaoPage : ContentPage
{
	private Pet _pet;
	private ServicoExibicao _servico;
	private string _porteServico; // Recebe o Porte
	private DateTime _data;
	private HorarioExibicao _horario;

	// Construtor atualizado para exigir o Porte
	public ConfirmacaoPage(Pet pet, ServicoExibicao servico, string porte, DateTime data, HorarioExibicao horario)
	{
		InitializeComponent();

		_pet = pet;
		_servico = servico;
		_porteServico = porte;
		_data = data;
		_horario = horario;

		PreencherResumo();
	}

	private void PreencherResumo()
	{
		// Se você quiser mostrar o porte na tela, adicione um 'lblPorte' no seu ConfirmacaoPage.xaml
		lblPet.Text = _pet.Nome;
		lblServico.Text = _servico.Servico;
		lblData.Text = _data.ToString("dd/MM/yyyy");
		lblHorario.Text = _horario.HoraTexto;
		lblTotal.Text = _servico.PrecoCalculado.ToString("C");
	}

	private async void OnConfirmarFinalTapped(object? sender, EventArgs e)
	{
		Servicos servicoReal = new Servicos
		{
			Id = _servico.Id,
			Servico = _servico.Servico,
			Descricao = _servico.Descricao,
			PrecoBase = _servico.PrecoCalculado
		};

		Agendamento novoAgendamento = new Agendamento
		{
			Id = App.lstAgendamentos.Count + 1,
			Pet = _pet,
			Servico = servicoReal,

			// Salvando o Porte diretamente no Agendamento/Serviço como o professor pediu
			Porte = _porteServico,

			Data = _data,
			Horario = _horario.Hora,
			// Status inicializado para manter o padrão e evitar erros de nulo
			Status = "Confirmado",
			Observacao = "" // Se houver campo de observação, preencha aqui
		};

		App.lstAgendamentos.Add(novoAgendamento);

		await DisplayAlert("Sucesso!", "Seu agendamento foi confirmado com sucesso. Te esperamos lá!", "OK");
		await Navigation.PopToRootAsync(); // Retorna direto para a Home limpa
	}

	private async void OnVoltarTapped(object? sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}