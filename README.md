# Sistem de management pentru club de fitness

Proiect academic realizat ca aplicatie C# pentru Visual Studio, cu interfata desktop, logo FP, schema bazei de date si separare intre zona de utilizator si zona de administrator.

Design de referinta:
[Modern Fitness Management System UI](https://www.figma.com/make/grObhsuEdcDS8UoGW8VVPb/Modern-Fitness-Management-System-UI?fullscreen=1&t=FTrwyoiHkzoJriVf-1)

## Structura proiectului

```text
.
|-- FitnessClubManagement.sln
|-- src
|   `-- FitnessClubManagement
|       |-- Assets
|       |   `-- fp-logo.png
|       |-- Data
|       |   `-- SampleRepository.cs
|       |-- Models
|       |   `-- DashboardModels.cs
|       |-- FitnessClubManagement.csproj
|       |-- MainForm.cs
|       `-- Program.cs
|-- database
|   `-- schema.sql
|-- docs
|   |-- database-schema.md
|   `-- project-structure.md
`-- README.md
```

## Ce include proiectul

- `FitnessClubManagement.sln` se deschide direct in Visual Studio.
- `src/FitnessClubManagement` contine aplicatia C# Windows Forms.
- logo-ul FP este inclus in `Assets/fp-logo.png`.
- formularul principal afiseaza:
  - portalul pentru utilizator;
  - panoul pentru administrator;
  - sumarul modulelor si legatura cu baza de date.
- `database/schema.sql` contine query-ul complet pentru baza de date.
- `docs/database-schema.md` contine schema vizuala si explicarea relatiilor.
- `docs/project-structure.md` explica rolul fiecarui folder.

## Cum se deschide aplicatia

Deschide solutia `FitnessClubManagement.sln` in Visual Studio si ruleaza proiectul `FitnessClubManagement`.

## GitHub

Repo public:
[InI2008/Sistem-de-management-pentru-club-de-fitness](https://github.com/InI2008/Sistem-de-management-pentru-club-de-fitness)

La prezentare poti arata:

1. structura directoarelor;
2. interfata C# pentru user si administrator;
3. schema vizuala din `docs/database-schema.md`;
4. query-ul integral din `database/schema.sql`.
