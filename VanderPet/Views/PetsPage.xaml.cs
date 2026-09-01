using VanderPet.Models;

namespace VanderPet.Views;

public partial class PetsPage : ContentPage
{
	public PetsPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		BindableLayout.SetItemsSource(vslListaPets, null);
		BindableLayout.SetItemsSource(vslListaPets, App.lstPets);
	}

	// CORREÇÃO: TappedEventArgs aplicado aqui também
	private async void OnEditarTapped(object? sender, TappedEventArgs e)
	{
		if (sender is Border border)
		{
			var petSelecionado = (Pet)border.BindingContext;
			await Navigation.PushModalAsync(new CadastroPetPage(petSelecionado));
		}
	}

	private async void OnExcluirTapped(object? sender, TappedEventArgs e)
	{
		if (sender is Border border)
		{
			var petSelecionado = (Pet)border.BindingContext;

			// CORREÇÃO: Substituição para DisplayAlertAsync
			bool resposta = await DisplayAlertAsync("Confirmação", $"Tem certeza que deseja excluir o pet {petSelecionado.Nome}?", "Sim, excluir", "Cancelar");

			if (resposta)
			{
				App.lstPets.Remove(petSelecionado);

				BindableLayout.SetItemsSource(vslListaPets, null);
				BindableLayout.SetItemsSource(vslListaPets, App.lstPets);
			}
		}
	}
}