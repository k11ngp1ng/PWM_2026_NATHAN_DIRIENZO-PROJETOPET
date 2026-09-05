using System;

namespace VanderPet.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private void OnEntrarClicked(object sender, EventArgs e)
	{
		// Troca a tela inicial do aplicativo para o Shell sem gerar aviso de obsolescência
		Application.Current.Windows[0].Page = new AppShell();
	}

	private async void OnCriarContaClicked(object sender, EventArgs e)
	{
		// Navega para a tela de cadastro do tutor
		await Navigation.PushAsync(new CadastroPage());
	}

	private async void OnEsqueciSenhaTapped(object sender, EventArgs e)
	{
		await DisplayAlertAsync("Recuperação", "A funcionalidade de 'Esqueci minha senha' será implementada em breve.", "OK");
	}
}