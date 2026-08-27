# Dokumentacioni — Folder: Views

Folderi `Views` përmban të gjitha faqet Razor (`.cshtml`) të aplikacionit. Razor kombinon HTML me kod C# për të gjeneruar HTML dinamike. Çdo nënfolder korrespondon me një kontrollues.

---

## Views/Shared/

Përmban komponentët e përbashkët të ndërfaqes.

### _Layout.cshtml

**Faqja bazë** e aplikacionit. Të gjitha faqet tjera e trashëgojnë këtë template nëpërmjet `_ViewStart.cshtml`. Përmban:

- **Navbar** — me lidhje te Home, Products, Cart (Shporta) dhe Login/Logout
- **@RenderBody()** — vendi ku injektohet permbajtja e çdo faqeje
- **Footer** — me informacion mbi të drejtat e autorit
- Lidhjet CDN për Bootstrap 5, Bootstrap Icons, dhe Google Fonts

### _LoginPartial.cshtml

Shfaq gjendjen e autentikimit:
- Nëse përdoruesi është **i identifikuar**: tregon emrin dhe butonin "Logout"
- Nëse **nuk është i identifikuar**: tregon butonat "Register" dhe "Login"

### _ValidationScriptsPartial.cshtml

Skriptet e validimit nga ana e klientit (jQuery Validation). Përfshihet vetëm në faqet me formularë.

### Error.cshtml

Tregon mesazhin e gabimit kur ndodh një gabim i papritur. Tregon `RequestId` nëse është i disponueshëm.

---

## Views/Home/

### Index.cshtml

Faqja kryesore e dyqanit. Përmban:
- **Hero section** — titulli, slogan dhe butoni "Explore"
- **About section** — përshkrim i dyqanit
- **Services section** — 4 karta me veçoritë (Premium Equipment, Performance, Pricing, Support)
- **Portfolio section** — grilë me imazhe të koleksioneve
- **Contact section** — formular kontakti me emër, email, telefon dhe mesazh

### Privacy.cshtml

Faqe e thjeshtë me politikën e privatësisë.

---

## Views/Products/

### Index.cshtml

Lista e të gjitha produkteve. Çdo produkt shfaqet si kartë Bootstrap me:
- Emrin, çmimin dhe përshkrimin
- Butonat: **View**, **Edit**, **Delete** (CRUD)
- Butoni **Shto në Shportë** — dërgon POST të `/Cart/Add`
- Mesazhi i suksesit nga `TempData["Message"]` (kur shtohet artikull)
- Butoni i shpejtë për të shkuar te shporta

### Create.cshtml

Formulari për shtimin e produktit të ri. Kërkon hyrje. Validon të dhënat para dërgimit.

### Edit.cshtml

Formulari për ndryshimin e produktit ekzistues. Parafushet plotësohen me të dhënat aktuale.

### Details.cshtml

Tregon të gjitha detajet e një produkti të vetëm (vetëm lexim).

### Delete.cshtml

Faqe konfirmimi para fshirjes. Tregon të dhënat e produktit dhe kërkon konfirmim.

---

## Views/Cart/

### Index.cshtml

Faqja e shportës. Tregon:
- **Tabelë** me të gjithë artikujt, çmimet, sasitë dhe totalin individual
- **Total i porosisë** në fund të tabelës
- Buton **Hiq** për çdo artikull (POST tek `/Cart/Remove`)
- Buton **Vazhdo Blerjen** (kthehet tek Products)
- Buton **Porosit Tani** (POST tek `/Cart/Checkout`)
- Mesazh zbrazëtie me ikonë kur shporta është bosh

---

## _ViewStart.cshtml

Vendos `_Layout.cshtml` si template bazë për të gjitha faqet automatikisht.

## _ViewImports.cshtml

Importon namespace-et dhe Tag Helpers të përdorura globalisht:
- `@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`
- `@using Projekti_Final.Models`
