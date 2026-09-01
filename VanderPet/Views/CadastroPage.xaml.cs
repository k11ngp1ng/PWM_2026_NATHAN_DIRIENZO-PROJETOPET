namespace VanderPet.Views;

public partial class CadastroPage : ContentPage
{
	public CadastroPage()
	{
		InitializeComponent();
	}

	private async void OnContinuarClicked(object sender, EventArgs e)
	{
		// Validações básicas incluindo o novo campo CPF
		if (string.IsNullOrWhiteSpace(entNome.Text) ||
			string.IsNullOrWhiteSpace(entCpf.Text) ||
			string.IsNullOrWhiteSpace(entEmail.Text) ||
			string.IsNullOrWhiteSpace(entTelefone.Text) ||
			string.IsNullOrWhiteSpace(entSenha.Text))
		{
			await DisplayAlert("Atenção", "Preencha todos os campos para continuar.", "OK");
			return;
		}

		// Validação simples de tamanho de CPF para o nível acadêmico
		if (entCpf.Text.Length != 11)
		{
			await DisplayAlert("Atenção", "O CPF deve conter exatamente 11 números.", "OK");
			return;
		}

		if (entSenha.Text.Length < 6)
		{
			await DisplayAlert("Atenção", "A senha deve conter no mínimo 6 caracteres.", "OK");
			return;
		}

		if (entSenha.Text != entConfirmarSenha.Text)
		{
			await DisplayAlert("Atenção", "As senhas não coincidem.", "OK");
			return;
		}

		// TODO: Salvar o usuário no banco de dados posteriormente

		await DisplayAlert("Sucesso", "Conta criada! Vamos cadastrar seu primeiro pet.", "OK");

		// Temporário até criarmos a próxima tela:
		// await Navigation.PushAsync(new CadastroPetPage());
	}

	private async void OnVoltarLoginTapped(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}