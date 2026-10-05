using CalculateurAge.ViewModels;

namespace CalculateurAge.Views;

// Relie l objet "vm" passe a la navigation a la propriete ViewModel.
[QueryProperty(nameof(ViewModel), "vm")]
public partial class ResultatPage : ContentPage
{
	// Rempli par la navigation, APRES le constructeur.
	public ResultatViewModel ViewModel
	{
		set => BindingContext = value;
	}

	// Construit l arbre visuel decrit par le XAML.
	public ResultatPage() => InitializeComponent();
}
