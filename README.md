# OdooScope — Internal Diagnostic Tool

![OdooScope Logo](wwwroot/logo.png)

> Application interne de sélection automatique d'applications Odoo — TPI 2026 — Normand Aymon — blackbox tech Sàrl

---

## Contexte

Chez **blackbox tech Sàrl**, société de digitalisation et intégrateur Odoo, la sélection des applications à installer chez un client est une étape cruciale mais chronophage. OdooScope automatise ce processus via un questionnaire dynamique qui génère automatiquement une liste personnalisée d'applications Odoo selon le profil du client.

---

## Fonctionnalités

- **Nouveau client** — Création d'un client et génération automatique de sa liste d'applications via un questionnaire dynamique
- **Client existant** — Modification des informations client, mise à jour du questionnaire et consultation du résultat
- **Consultation** — Visualisation des questions, secteurs d'activité et applications Odoo disponibles

---

## Logique de sélection (Moulinette)

La liste d'applications est générée selon trois critères combinés :

1. **Applications conditionnelles** — liées aux réponses Oui du questionnaire via la table `QuestionApplication`
2. **Applications essentielles** — marquées `EstEssentiel = true`, filtrées par secteur d'activité
3. **Filtre employés** — certaines applications nécessitent un nombre minimum d'employés (`EmployeMin`)

Les doublons sont supprimés automatiquement avec `Distinct()`.

---

## Stack technique

| Catégorie | Technologies |
|---|---|
| **Langage** | C#, HTML, CSS, JavaScript, SQL |
| **Framework** | ASP.NET Core MVC |
| **ORM** | Entity Framework Core (Database First) |
| **Base de données** | Microsoft SQL Server (MSSQL) via Docker |
| **Frontend** | Bootstrap 5, jQuery, AJAX, DataTables.js |
| **Validation** | jQuery Validation, jQuery Validation Unobtrusive |
| **Sérialisation** | Newtonsoft.Json |
| **Outils** | Visual Studio 2022, SSMS, DB Designer, Docker |

---

## Structure de la base de données

```
Client ──────────── SecteurActivite
   │
   ├── Repondre ─── Question ──── QuestionApplication ─── ApplicationOdoo
   │
   └── Resultat ─── CreationListe ── ApplicationOdoo
```

**8 tables** organisées en 3ème forme normale :

| Table | Description | Enregistrements |
|---|---|---|
| `SecteurActivite` | 26 secteurs d'activité | 26 |
| `ApplicationOdoo` | Applications Odoo disponibles | 64 |
| `Question` | Questions du questionnaire (hiérarchie parent/enfant) | 64 |
| `QuestionApplication` | Liaisons question → application | 59 |
| `Client` | Clients enregistrés | — |
| `Repondre` | Réponses du client au questionnaire | — |
| `Resultat` | Résultat généré par client | — |
| `CreationListe` | Applications sélectionnées par résultat | — |

---

## Architecture du projet

```
OdooScope/
├── OdooScopeEntities/          # Projet EF Core
│   ├── Entities/               # Classes générées par scaffolding
│   └── Models/                 # MetaData de validation (ClientMetaData.cs)
│
└── OdooScopeWeb/               # Projet MVC
    ├── Controllers/
    │   ├── ClientController.cs
    │   ├── QuestionController.cs
    │   ├── ResultatController.cs
    │   ├── ApplicationOdooController.cs
    │   ├── SecteurActiviteController.cs
    │   └── MainMenuController.cs
    ├── Views/
    │   ├── Client/
    │   ├── Question/
    │   ├── Resultat/
    │   ├── ApplicationOdoo/
    │   ├── SecteurActivite/
    │   ├── MainMenu/
    │   └── Shared/
    └── wwwroot/
        ├── css/site.css
        └── logo.png
```

---

## Installation

### Prérequis

- Visual Studio 2022
- Docker Desktop
- SQL Server Management Studio (SSMS)
- .NET 10

### 1. Base de données

Lancez un container SQL Server avec Docker :

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=VotreMotDePasse" \
  -p 1433:1433 --name mssql \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

Exécutez le script SQL d'import dans SSMS pour créer les tables et insérer les données de référence.

### 2. Configuration

Créez le fichier `secrets.json` via **clic droit sur OdooScopeWeb → Manage User Secrets** :

```json
{
  "ConnectionStrings:DefaultConnection": "Server=localhost,1433;User ID=sa;Password=VotreMotDePasse;Database=TPINormand;TrustServerCertificate=True;"
}
```

### 3. Lancer l'application

Ouvrez la solution dans Visual Studio et lancez `OdooScopeWeb` avec `F5`.

---

## Points techniques notables

- **Questionnaire dynamique** — Les questions sont sérialisées en JSON côté serveur et manipulées par JavaScript côté client sans rechargement de page. Les questions enfants apparaissent/disparaissent dynamiquement selon les réponses.
- **AJAX** — L'envoi des réponses au controller se fait via AJAX pour préserver l'état du questionnaire dynamique.
- **Validation** — Les règles de validation sont définies dans `ClientMetaData.cs` avec des annotations C# et appliquées côté client via jQuery Validation.
- **DataTables** — Les listes de consultation utilisent DataTables.js pour la recherche, le tri et la pagination.



## Auteur

**Normand Aymon** 
TPI IDEC 2026 — Mention très bien ⭐

---

*blackbox tech Sàrl — [bkbx.ch](https://www.bkbx.ch)*
