# Dokumentacioni — Folder: Data

Folderi `Data` përmban kontekstin e databazës i cili menaxhon komunikimin ndërmjet aplikacionit dhe SQLite nëpërmjet Entity Framework Core.

---

## ApplicationDbContext.cs

**Vendndodhja:** `Data/ApplicationDbContext.cs`  
**Namespace:** `Projekti_Final.Data`  
**Trashëgon:** `IdentityDbContext`

Konteksti kryesor i databazës. Duke trashëguar `IdentityDbContext`, kombinon tabelat e ASP.NET Core Identity (përdorues, role, etj.) me tabelat e aplikacionit.

### Pronat (DbSet)

| DbSet | Tipi | Tabela në DB | Përshkrim |
|---|---|---|---|
| `Products` | `DbSet<Product>` | `Products` | Tabela e produkteve të dyqanit |

### Konstruktori

Pranon `DbContextOptions<ApplicationDbContext>` dhe i kalon klasës bazë — lejon konfigurimin e jashtëm (string lidhjes, provider, etj.) nëpërmjet `Program.cs`.

### Shënime
- Tabelat e Identity (`AspNetUsers`, `AspNetRoles`, etj.) krijohen automatikisht nga klasa bazë.
- Migrimet menaxhohen nëpërmjet EF Core Tools (`dotnet ef migrations`).

---

## Folder: Migrations/

Përmban skedarët e gjeneruar automatikisht nga Entity Framework Core `dotnet ef migrations add`.

| Skedari | Përshkrim |
|---|---|
| `20260112221915_InitialCreate.cs` | Migrimi i parë — krijon tabelën `Products` |
| `20260128194653_AddIdentity.cs` | Migrimi i dytë — shton tabelat e Identity |
| `ApplicationDbContextModelSnapshot.cs` | Fotografia aktuale e modelit (EF Core e përdor për krahasim) |

Migrimet aplikohen me komandën: `dotnet ef database update`
