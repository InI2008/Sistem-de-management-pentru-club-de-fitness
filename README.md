# Sistem de management pentru club de fitness

Proiect academic pentru ziua 4: structura proiectului, schema bazei de date si definirea aplicatiei pentru utilizator si administrator.

Design de referinta:
[Modern Fitness Management System UI](https://www.figma.com/make/grObhsuEdcDS8UoGW8VVPb/Modern-Fitness-Management-System-UI?fullscreen=1&t=FTrwyoiHkzoJriVf-1)

## Structura proiectului

```text
.
|-- apps
|   |-- api
|   |   `-- README.md
|   `-- web
|       |-- app.js
|       |-- index.html
|       |-- styles.css
|       `-- assets
|-- database
|   `-- schema.sql
|-- docs
|   |-- database-schema.md
|   `-- project-structure.md
`-- README.md
```

## Ce include proiectul

- `apps/web` contine o interfata demonstrativa pentru:
  - utilizator: programari, abonament, antrenori, progres;
  - administrator: membri, clase, plati, KPI si administrare operationala.
- `database/schema.sql` contine query-ul complet pentru baza de date.
- `docs/database-schema.md` contine schema vizuala si explicarea relatiilor.
- `docs/project-structure.md` explica rolul fiecarui folder.

## Cum se deschide partea vizuala

Deschide fisierul `apps/web/index.html` in browser.

## GitHub

Repo public: [InI2008/Sistem-de-management-pentru-club-de-fitness](https://github.com/InI2008/Sistem-de-management-pentru-club-de-fitness)

La prezentare poti arata:

1. structura directoarelor;
2. interfata pentru user si administrator;
3. schema vizuala din `docs/database-schema.md`;
4. query-ul integral din `database/schema.sql`.
