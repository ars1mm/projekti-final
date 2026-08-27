# Dokumentacioni — Program.cs

**Vendndodhja:** `Program.cs`  
**Funksioni:** Pika hyrëse e aplikacionit dhe konfigurimi i të gjitha shërbimeve.

`Program.cs` është skedari ku ndërtohet dhe konfigurohet aplikacioni ASP.NET Core. Ndahet në dy faza: **regjistrimi i shërbimeve** dhe **konfigurimi i pipeline-it HTTP**.

---

## Faza 1 — Regjistrimi i Shërbimeve (`builder.Services`)

| Shërbimi | Metoda | Përshkrim |
|---|---|---|
| MVC Controllers + Views | `AddControllersWithViews()` | Aktivizon kontrolluesit MVC dhe renderimin Razor |
| Entity Framework + SQLite | `AddDbContext<ApplicationDbContext>()` | Lidh aplikacionin me databazën SQLite (`products.db`) |
| ASP.NET Core Identity | `AddDefaultIdentity<IdentityUser>()` | Autentikimi — login, register, logout |
| Razor Pages | `AddRazorPages()` | Kërkohet nga Identity UI (faqet e login/register) |
| Distributed Cache | `AddDistributedMemoryCache()` | Cache në memorie — parakusht për sesionin |
| Session | `AddSession()` | Aktivizon sesionin HTTP (timeout: 30 min) |
| HTTP Context Accessor | `AddHttpContextAccessor()` | Lejon shërbimet të aksesojnë `HttpContext` |
| Cart Service | `AddScoped<CartService>()` | Regjistron shërbimin e shportës |

### Konfigurimi i Sesionit

```csharp
options.IdleTimeout = TimeSpan.FromMinutes(30); // Sesioni skadon pas 30 min
options.Cookie.HttpOnly = true;                  // Cookie nuk aksesohëm nga JavaScript
options.Cookie.IsEssential = true;               // Kërkohet edhe pa cookie consent
```

### Konfigurimi i Identity

```csharp
options.SignIn.RequireConfirmedAccount = false   // Nuk kërkohet konfirmim emaili
```

---

## Faza 2 — Konfigurimi i Pipeline-it HTTP (`app.Use...`)

Middlewaret ekzekutohen në rendin e mëposhtëm:

| Middleware | Funksioni |
|---|---|
| `UseExceptionHandler` | Trajton gabimet në production (`/Home/Error`) |
| `UseHsts` | Kërkon HTTPS nëpërmjet header-it HSTS |
| `UseHttpsRedirection` | Ridrejton HTTP → HTTPS |
| `UseStaticFiles` | Shërben skedarët nga `wwwroot/` |
| `UseRouting` | Analizon URL-në dhe gjen kontrolluesin |
| `UseAuthentication` | Kontrollon nëse përdoruesi është i identifikuar |
| `UseAuthorization` | Kontrollon nëse ka leje për aksionin e kërkuar |
| `UseSession` | Aktivizon sesionin HTTP (duhet pas Authorization) |

### Konfigurimi i Rrugës Default

```csharp
pattern: "{controller=Home}/{action=Index}/{id?}"
```
Shembull: `/Products/Edit/5` → `ProductsController.Edit(5)`

---

## Shënime të Rëndësishme

- `UseSession()` duhet vendosur **pas** `UseAuthentication()` dhe `UseAuthorization()`.
- `AddDistributedMemoryCache()` duhet thirret **para** `AddSession()`.
- Identity kërkon `AddRazorPages()` dhe `MapRazorPages()` për faqet e login/register.
