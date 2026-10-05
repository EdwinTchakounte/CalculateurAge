# CalculateurAge

TP1 - Calculateur d'age : Routing et MVVM (.NET MAUI).

## Historique par phase

| Etape | Contenu |
|-------|---------|
| Etape 0 | Projet .NET MAUI cree sans contenu d'exemple |
| Phase A | Version code-behind (`x:Name`, `Clicked`) |
| Phase B | Seconde page `Views/ResultatPage` et navigation Shell avec parametres d'URL |
| Phase C | Reecriture MVVM : `BaseViewModel`, `RelayCommand`, `CalculateurViewModel` |
| Activite 6 | Fonctionnalites supplementaires (ci-dessous) |

## Fonctionnalites ajoutees (Activite 6)

Toutes respectent le MVVM : aucune logique dans le code-behind.

1. **Refus d'une date future** : `CalculerCommand` reste grise si la date est apres aujourd'hui, et le `DatePicker` est borne par `DateMax`.
2. **Majeur / Mineur** : propriete `Statut` calculee par le ViewModel.
3. **Prochain anniversaire** : nombre de jours restants (gere le 29 fevrier).
4. **Effacer** : `EffacerCommand` remet tous les champs a zero.
5. **Historique des calculs** : `ObservableCollection<string>` affichee avec `BindableLayout`.
6. **Page de detail par ViewModel** : `VoirDetailCommand` ouvre `ResultatPage` en lui passant un `ResultatViewModel` (objet de navigation, plus de parametres dans l'URL). La navigation passe par `INavigationService`, ce qui garde les ViewModels independants de Shell.

## Organisation

```
CalculateurAge/
  Views/        ResultatPage.xaml(.cs)
  ViewModels/   BaseViewModel, RelayCommand, CalculateurViewModel, ResultatViewModel
  Services/     INavigationService, ShellNavigationService
  MainPage.xaml(.cs), AppShell.xaml(.cs), App.xaml(.cs), MauiProgram.cs
```

## Lancer

Ouvrir `CalculateurAge.sln` dans Visual Studio (workload .NET MAUI, .NET 9) et lancer sur l'emulateur Android,
ou en ligne de commande : `dotnet build -t:Run -f net9.0-android`.
