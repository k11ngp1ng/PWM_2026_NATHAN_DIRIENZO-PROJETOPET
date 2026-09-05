using VanderPet.Views;

namespace VanderPet;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// REGISTRO DE ROTAS:
		// Como o fluxo do professor exige telas que "sobrepõem" o menu (ex: Novo Agendamento),
		// precisamos registrar essas rotas no Shell para que a navegação funcione sem quebrar o app.

		Routing.RegisterRoute(nameof(CadastroPetPage), typeof(CadastroPetPage));
		Routing.RegisterRoute(nameof(AgendamentoPage), typeof(AgendamentoPage));
		Routing.RegisterRoute(nameof(ConfirmacaoPage), typeof(ConfirmacaoPage));
	}
}