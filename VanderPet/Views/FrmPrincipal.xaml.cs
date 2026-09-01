using VanderPet.Models;

namespace VanderPet.Views;

public partial class FrmPrincipal : ContentPage
{
	public FrmPrincipal()
	{
		InitializeComponent();

		// Como as listas são 'static' no App.xaml.cs, nós as acessamos diretamente 
		// usando o nome da classe 'App' seguido de ponto.
		pckEspecies.ItemsSource = App.lstEspecies;
		pckServicos.ItemsSource = App.lstServicos;
		pckRacas.ItemsSource = App.lstRacas;
	}
}