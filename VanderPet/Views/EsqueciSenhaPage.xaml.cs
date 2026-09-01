namespace VanderPet.Views;

public partial class EsqueciSenhaPage : ContentPage
{
	public EsqueciSenhaPage()
	{
		InitializeComponent();
	}

	private async void OnEnviarClicked(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(entEmail.Text))
		{
			await DisplayAlert("Atenção", "Por favor, informe o seu e-mail.", "OK");
			return;
		}

		// Simulação do envio de e-mail (mock)
		await DisplayAlert("Sucesso", "Se o e-mail estiver cadastrado, você receberá um link de recuperação em instantes.", "OK");

		// Retorna para o login após o "envio"
		await Navigation.PopAsync();
	}

	private async void OnVoltarTapped(object sender, EventArgs e)
	{
		// Retorna para a tela de Login sem fazer nada
		await Navigation.PopAsync();
	}
}