using System.Collections.ObjectModel;
using CalculateurAge.Services;

namespace CalculateurAge.ViewModels;

// Contient l ETAT de l ecran et les ACTIONS possibles.
public class CalculateurViewModel : BaseViewModel
{
	private readonly INavigationService? _navigation;

	// Champs prives : la vraie donnee.
	private string _nom = "";
	private DateTime _dateNaissance
		= DateTime.Today.AddYears(-20);
	private string _resultat = "";
	private bool _resultatVisible;
	private string _statut = "";
	private string _prochainAnniversaire = "";

	// Age de la majorite civile.
	private const int AgeMajorite = 18;

	// Proprietes publiques : ce que le XAML voit.
	public string Nom
	{
		get => _nom;
		set { if (SetField(ref _nom, value))
				  CalculerCommand.Rafraichir(); }
	}

	public DateTime DateNaissance
	{
		get => _dateNaissance;
		set { if (SetField(ref _dateNaissance, value))
				  CalculerCommand.Rafraichir(); }
	}

	// Borne superieure du DatePicker : on ne nait pas dans le futur.
	public DateTime DateMax => DateTime.Today;

	public string Resultat
	{
		get => _resultat;
		set => SetField(ref _resultat, value);
	}

	public bool ResultatVisible
	{
		get => _resultatVisible;
		set { if (SetField(ref _resultatVisible, value))
				  VoirDetailCommand.Rafraichir(); }
	}

	// "Majeur" ou "Mineur", calcule par le ViewModel.
	public string Statut
	{
		get => _statut;
		set => SetField(ref _statut, value);
	}

	// Nombre de jours restants avant le prochain anniversaire.
	public string ProchainAnniversaire
	{
		get => _prochainAnniversaire;
		set => SetField(ref _prochainAnniversaire, value);
	}

	// Historique des calculs, le plus recent en premier.
	// ObservableCollection previent la vue a chaque ajout.
	public ObservableCollection<string> Historique { get; } = new();

	// Liee a Button.Command dans le XAML.
	public RelayCommand CalculerCommand { get; }

	// Remet tous les champs a zero.
	public RelayCommand EffacerCommand { get; }

	// Ouvre ResultatPage avec un ResultatViewModel deja rempli.
	public RelayCommand VoirDetailCommand { get; }

	public CalculateurViewModel(INavigationService? navigation = null)
	{
		_navigation = navigation;
		CalculerCommand = new RelayCommand(
			Calculer,
			PeutCalculer);
		EffacerCommand = new RelayCommand(Effacer);
		VoirDetailCommand = new RelayCommand(
			VoirDetail,
			() => ResultatVisible && _navigation != null);
	}

	// Calcul possible seulement avec un nom et une date non future.
	private bool PeutCalculer()
	{
		return !string.IsNullOrWhiteSpace(Nom)
			&& DateNaissance.Date <= DateTime.Today;
	}

	// La logique metier : aucun controle d interface ici.
	private void Calculer()
	{
		int age = DateTime.Today.Year
				  - DateNaissance.Year;
		if (DateNaissance.Date >
			DateTime.Today.AddYears(-age)) age--;

		Resultat = $"{Nom}, vous avez {age} ans";
		Statut = age >= AgeMajorite ? "Majeur" : "Mineur";

		int jours = JoursAvantAnniversaire(DateNaissance);
		ProchainAnniversaire = jours == 0
			? "Joyeux anniversaire !"
			: $"Prochain anniversaire dans {jours} jour(s)";
		ResultatVisible = true;

		Historique.Insert(0,
			$"{DateTime.Now:HH:mm} - {Nom} : {age} ans ({Statut})");
	}

	private async void VoirDetail()
	{
		if (_navigation == null) return;
		await _navigation.AllerVersResultatAsync(new ResultatViewModel(
			Resultat, Statut, ProchainAnniversaire, _navigation));
	}

	private void Effacer()
	{
		Nom = "";
		DateNaissance = DateTime.Today.AddYears(-20);
		Resultat = "";
		Statut = "";
		ProchainAnniversaire = "";
		ResultatVisible = false;
	}

	// AddYears gere le 29 fevrier (ramene au 28 les annees non bissextiles).
	private static int JoursAvantAnniversaire(DateTime naissance)
	{
		DateTime aujourdhui = DateTime.Today;
		DateTime prochain = naissance.Date
			.AddYears(aujourdhui.Year - naissance.Year);
		if (prochain < aujourdhui)
			prochain = naissance.Date
				.AddYears(aujourdhui.Year - naissance.Year + 1);
		return (prochain - aujourdhui).Days;
	}
}
