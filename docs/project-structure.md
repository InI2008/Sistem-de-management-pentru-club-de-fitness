# Structura proiectului

## Foldere principale

- `FitnessClubManagement.sln` - solutia principala pentru Visual Studio.
- `src/FitnessClubManagement` - aplicatia C# Windows Forms.
- `src/FitnessClubManagement/Controls` - controlul care deseneaza logo-ul FP si alte componente UI.
- `src/FitnessClubManagement/Data` - date demonstrative si viitor acces la baza de date.
- `src/FitnessClubManagement/Models` - modele C# pentru dashboard.
- `src/FitnessClubManagement/login` - modul si fereastra pentru login.
- `src/FitnessClubManagement/register` - modul si fereastra pentru inregistrare.
- `src/FitnessClubManagement/dashboards` - modul pentru dashboard-uri.
- `src/FitnessClubManagement/clienti` - modul pentru clienti.
- `src/FitnessClubManagement/abonemente` - modul pentru abonamente.
- `src/FitnessClubManagement/prezente` - modul pentru prezente.
- `src/FitnessClubManagement/plati` - modul pentru plati.
- `src/FitnessClubManagement/atrenori` - modul pentru antrenori.
- `src/FitnessClubManagement/darl mode` - modul pentru tema dark.
- `src/FitnessClubManagement/Logout` - modul pentru logout.
- `database` - schema SQL completa a proiectului.
- `docs` - documentatie pentru prezentare si explicarea arhitecturii.

## Rolurile aplicatiei

### Utilizator

- isi vede profilul;
- isi verifica abonamentul;
- face rezervari la clase;
- urmareste progresul si istoricul participarii.

### Administrator

- gestioneaza membrii;
- creeaza abonamente;
- organizeaza clase si programari;
- monitorizeaza platile;
- verifica mentenanta si rapoartele operative.

## Observatie

- folderul `apps` a ramas doar ca material de concept din versiunea initiala; aplicatia ceruta pentru predare este cea din `src/FitnessClubManagement`.
