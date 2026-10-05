namespace CalculateurAge.ViewModels;

// Contient l ETAT de l ecran et les ACTIONS possibles.
public class CalculateurViewModel : BaseViewModel
{
	// Champs prives : la vraie donnee.
	private string _nom = "";
	private DateTime _dateNaissance
		= DateTime.Today.AddYears(-20);
	private string _resultat = "";
	private bool _resultatVisible;
	private string _statut = "";

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
		set => SetField(ref _resultatVisible, value);
	}

	// "Majeur" ou "Mineur", calcule par le ViewModel.
	public string Statut
	{
		get => _statut;
		set => SetField(ref _statut, value);
	}

	// Liee a Button.Command dans le XAML.
	public RelayCommand CalculerCommand { get; }

	public CalculateurViewModel()
	{
		CalculerCommand = new RelayCommand(
			Calculer,
			PeutCalculer);
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
		ResultatVisible = true;
	}
}
