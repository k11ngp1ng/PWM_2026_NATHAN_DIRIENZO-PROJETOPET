namespace VanderPet.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void OnEntrarClicked(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(entEmail.Text) || string.IsNullOrWhiteSpace(entSenha.Text))
		{
			await DisplayAlert("Atenção", "Por favor, preencha o e-mail e a senha.", "OK");
			return;
		}

		// TODO: Implementar lógica real de autenticação
		Application.Current.MainPage = new AppShell();
	}

	private async void OnCriarContaClicked(object sender, EventArgs e)
	{
		// Navega para a tela de Cadastro
		await Navigation.PushAsync(new CadastroPage());
	}

	private async void OnEsqueciSenhaTapped(object sender, EventArgs e)
	{
		// Navega para a tela de Esqueci a Senha
		await Navigation.PushAsync(new EsqueciSenhaPage());
	}
}