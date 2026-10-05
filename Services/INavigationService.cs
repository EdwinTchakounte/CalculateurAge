using CalculateurAge.ViewModels;

namespace CalculateurAge.Services;

// Abstraction de la navigation : le ViewModel demande d aller
// quelque part sans connaitre Shell ni les pages.
public interface INavigationService
{
	Task AllerVersResultatAsync(ResultatViewModel resultat);
	Task RetourAsync();
}
