using VanderPet.Models;

namespace VanderPet.Views;

public partial class CadastroPetPage : ContentPage
{
	private Pet? _petEmEdicao;

	public CadastroPetPage(Pet? petParaEditar = null)
	{
		InitializeComponent();

		pckEspecie.ItemsSource = App.lstEspecies;
		pckRaca.ItemsSource = App.lstRacas;
		dtpNascimento.MaximumDate = DateTime.Today;

		_petEmEdicao = petParaEditar;

		if (_petEmEdicao != null)
		{
			lblTitulo.Text = "Editar Pet";
			lblSubtitulo.Text = "Atualize as informações do seu companheiro.";

			entNome.Text = _petEmEdicao.Nome;
			pckSexo.SelectedItem = _petEmEdicao.Sexo;
			pckPorte.SelectedItem = _petEmEdicao.Porte; // Carrega o porte salvo
			dtpNascimento.Date = _petEmEdicao.Nascimento;
			entPeso.Text = _petEmEdicao.Peso.ToString();
			edtObservacoes.Text = _petEmEdicao.Observacoes;

			pckEspecie.SelectedItem = App.lstEspecies.FirstOrDefault(e => e.Id == _petEmEdicao.Especie?.Id);
			pckRaca.SelectedItem = App.lstRacas.FirstOrDefault(r => r.Id == _petEmEdicao.Raca?.Id);
		}
	}

	private void OnEspecieChanged(object sender, EventArgs e)
	{
		if (pckEspecie.SelectedItem is Especies especieSelecionada)
		{
			var racasFiltradas = App.lstRacas.Where(r => r.EspecieId == especieSelecionada.Id).ToList();
			pckRaca.ItemsSource = racasFiltradas;

			if (especieSelecionada.Id == 1 || especieSelecionada.Id == 2)
			{
				brdRaca.IsVisible = true;
			}
			else
			{
				brdRaca.IsVisible = false;
			}
		}
	}

	private async void OnSalvarClicked(object sender, EventArgs e)
	{
		// Tornamos o Porte obrigatório para a inteligência de negócios funcionar
		if (string.IsNullOrWhiteSpace(entNome.Text) || pckEspecie.SelectedItem == null || pckPorte.SelectedItem == null)
		{
			await DisplayAlertAsync("Atenção", "Por favor, informe pelo menos o Nome, Espécie e Porte do pet.", "OK");
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
			_petEmEdicao.Raca = (Racas)pckRaca.SelectedItem;
			_petEmEdicao.Sexo = pckSexo.SelectedItem?.ToString();
			_petEmEdicao.Porte = pckPorte.SelectedItem?.ToString(); // Salva a edição do porte
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
				Raca = (Racas)pckRaca.SelectedItem,
				Sexo = pckSexo.SelectedItem?.ToString(),
				Porte = pckPorte.SelectedItem?.ToString(), // Salva o novo porte
				Nascimento = Convert.ToDateTime(dtpNascimento.Date),
				Peso = peso,
				Observacoes = edtObservacoes.Text ?? string.Empty
			};
			App.lstPets.Add(novoPet);

			await DisplayAlertAsync("Sucesso", $"{novoPet.Nome} foi cadastrado com sucesso!", "OK");
		}

		await Navigation.PopModalAsync();
	}

	private async void OnCancelarClicked(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}
}