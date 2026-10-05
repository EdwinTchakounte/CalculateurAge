using CalculateurAge.Services;

namespace CalculateurAge.ViewModels;

// Etat de la page de resultat, prepare par CalculateurViewModel.
public class ResultatViewModel : BaseViewModel
{
	private readonly INavigationService? _navigation;

	public string Message { get; }
	public string Statut { get; }
	public string ProchainAnniversaire { get; }

	public RelayCommand RetourCommand { get; }

	public ResultatViewModel(string message, string statut,
		string prochainAnniversaire,
		INavigationService? navigation = null)
	{
		Message = message;
		Statut = statut;
		ProchainAnniversaire = prochainAnniversaire;
		_navigation = navigation;
		RetourCommand = new RelayCommand(Retour);
	}

	private async void Retour()
	{
		if (_navigation != null)
			await _navigation.RetourAsync();
	}
}
