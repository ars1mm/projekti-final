# Dokumentacioni Teknik — Edin Store

## Përshkrim i Projektit

**Edin Store** është një aplikacion web i ndërtuar me **ASP.NET Core MVC (.NET 9)** që ofron funksionalitetin e një dyqani online. Projekti mundëson menaxhimin e produkteve, vërtetimin e përdoruesve, shfletimin e katalogut, dhe shtimin e produkteve në shportë.

## Teknologjitë e Përdorura

| Teknologjia | Versioni | Qëllimi |
|---|---|---|
| ASP.NET Core MVC | .NET 9 | Framework kryesor i aplikacionit |
| Entity Framework Core | 9.0.0 | ORM për komunikim me databazën |
| SQLite | — | Databaza relacionale |
| ASP.NET Core Identity | 9.0.0 | Autentikimi dhe autorizimi |
| Bootstrap 5 | 5.2.3 | Dizajni dhe ndërfaqja vizuale |
| Bootstrap Icons | 1.5.0 | Ikonat e ndërfaqes |

---

## Struktura e Folderëve

```
projekti-final/
│
├── Controllers/          → Kontrolluesit MVC dhe API
│   └── API/              → Web API REST kontrolluesit
├── Models/               → Klasat e modelit (databaza + logjika)
├── Views/                → Faqet Razor (HTML + C#)
│   ├── Home/             → Faqja kryesore
│   ├── Products/         → Faqet e katalogut të produkteve
│   ├── Cart/             → Faqet e shportës
│   └── Shared/           → Layout dhe komponentët e përbashkët
├── Data/                 → Konteksti i databazës (EF Core)
├── Services/             → Shërbimet e biznesit (logjika e shportës)
├── Migrations/           → Migrimet e databazës (EF Core)
├── Areas/Identity/       → Faqet e autentikimit (login/register)
├── wwwroot/              → Skedarët statikë (CSS, JS, imazhe)
├── Properties/           → Konfigurimi i nisjes (launchSettings.json)
├── Program.cs            → Pika hyrëse dhe konfigurimi i aplikacionit
├── appsettings.json      → Cilësimet e aplikacionit
└── dokumentacioni/       → Dokumentacioni teknik shqip
```

---

## Funksionalitetet Kryesore

1. **CRUD për Produkte** — Krijim, lexim, ndryshim dhe fshirje produktesh
2. **Web API REST** — Endpoint-et JSON për produktet (`/api/products`)
3. **Autentikimi** — Regjistrim dhe hyrje me ASP.NET Core Identity
4. **Katalog Produktesh** — Listim dhe detaje produktesh
5. **Shporta** — Shtim, heqje dhe porositje produktesh bazuar në sesion

---

## Shiko Dokumentacionin e Detajuar

- [Controllers](./controllers.md)
- [Models](./models.md)
- [Views](./views.md)
- [Data](./data.md)
- [Services](./services.md)
- [Program.cs](./program.md)
- [wwwroot](./wwwroot.md)
