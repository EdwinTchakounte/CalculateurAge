using CalculateurAge.ViewModels;
using CalculateurAge.Views;

namespace CalculateurAge.Services;

// Implementation MAUI de la navigation, basee sur Shell.
public class ShellNavigationService : INavigationService
{
	// Le ViewModel voyage comme objet : rien ne passe dans l URL.
	public Task AllerVersResultatAsync(ResultatViewModel resultat)
		=> Shell.Current.GoToAsync(nameof(ResultatPage),
			new Dictionary<string, object> { ["vm"] = resultat });

	public Task RetourAsync() => Shell.Current.GoToAsync("..");
}
