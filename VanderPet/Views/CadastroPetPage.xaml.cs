using VanderPet.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace VanderPet.Views;

public partial class CadastroPetPage : ContentPage
{
	private Pet? _petEmEdicao;

	public CadastroPetPage(Pet? petParaEditar = null)
	{
		InitializeComponent();

		pckEspecie.ItemsSource = App.lstEspecies;
		dtpNascimento.MaximumDate = DateTime.Today;

		_petEmEdicao = petParaEditar;

		if (_petEmEdicao != null)
		{
			lblTitulo.Text = "Editar Pet";
			lblSubtitulo.Text = "Atualize as informações do seu companheiro.";

			entNome.Text = _petEmEdicao.Nome;
			pckSexo.SelectedItem = _petEmEdicao.Sexo;
			dtpNascimento.Date = _petEmEdicao.Nascimento;
			entPeso.Text = _petEmEdicao.Peso.ToString();
			edtObservacoes.Text = _petEmEdicao.Observacoes;

			pckEspecie.SelectedItem = App.lstEspecies.FirstOrDefault(e => e.Id == _petEmEdicao.Especie?.Id);

			if (pckRaca.ItemsSource != null)
			{
				var listaRacasAtual = (IEnumerable<Racas>)pckRaca.ItemsSource;
				pckRaca.SelectedItem = listaRacasAtual.FirstOrDefault(r => r.Id == _petEmEdicao.Raca?.Id);
			}
		}
	}

	private void OnEspecieChanged(object sender, EventArgs e)
	{
		if (pckEspecie.SelectedItem is Especies especieSelecionada)
		{
			var racasFiltradas = App.lstRacas.Where(r => r.EspecieId == especieSelecionada.Id).ToList();
			pckRaca.ItemsSource = racasFiltradas;
			brdRaca.IsVisible = racasFiltradas.Count > 0;

			if (racasFiltradas.Count == 1)
			{
				pckRaca.SelectedIndex = 0;
			}
		}
	}

	private async void OnSalvarClicked(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(entNome.Text) || pckEspecie.SelectedItem == null)
		{
			await DisplayAlertAsync("Atenção", "Por favor, informe pelo menos o Nome e a Espécie do pet.", "OK");
			return;
		}

		double peso = 0;
		if (!string.IsNullOrWhiteSpace(entPeso.Text) && double.TryParse(entPeso.Text, out peso) && peso < 0)
		{
			await DisplayAlertAsync("Atenção", "O peso não pode ser negativo.", "OK");
			return;
		}

		if (_petEmEdicao != null)
		{
			_petEmEdicao.Nome = entNome.Text;
			_petEmEdicao.Especie = (Especies)pckEspecie.SelectedItem;
			_petEmEdicao.Raca = pckRaca.SelectedItem as Racas;
			_petEmEdicao.Sexo = pckSexo.SelectedItem?.ToString();
			_petEmEdicao.Nascimento = Convert.ToDateTime(dtpNascimento.Date);
			_petEmEdicao.Peso = peso;
			_petEmEdicao.Observacoes = edtObservacoes.Text ?? string.Empty;

			await DisplayAlertAsync("Sucesso", "Os dados do pet foram atualizados!", "OK");
		}
		else
		{
			Pet novoPet = new Pet
			{
				Id = App.lstPets.Count + 1,
				Nome = entNome.Text,
				Especie = (Especies)pckEspecie.SelectedItem,
				Raca = pckRaca.SelectedItem as Racas,
				Sexo = pckSexo.SelectedItem?.ToString(),
				Nascimento = Convert.ToDateTime(dtpNascimento.Date),
				Peso = peso,
				Observacoes = edtObservacoes.Text ?? string.Empty
			};
			App.lstPets.Add(novoPet);

			await DisplayAlertAsync("Sucesso", $"{novoPet.Nome} foi cadastrado com sucesso!", "OK");
		}

		// CORREÇÃO: Usando a navegação padrão para fechar a tela corretamente
		await Navigation.PopAsync();
	}

	private async void OnCancelarClicked(object sender, EventArgs e)
	{
		// CORREÇÃO: Usando a navegação padrão para cancelar e voltar
		await Navigation.PopAsync();
	}
}