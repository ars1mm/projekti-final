# Dokumentacioni — Folder: Controllers

Folderi `Controllers` përmban të gjithë kontrolluesit e aplikacionit. Kontrolluesit janë përgjegjës për marrjen e kërkesave HTTP, ekzekutimin e logjikës, dhe kthimin e përgjigjes tek përdoruesi.

---

## HomeController.cs

**Vendndodhja:** `Controllers/HomeController.cs`  
**Namespace:** `Projekti_Final.Controllers`  
**Trashëgon:** `Controller`

Kontrolluesi i faqes kryesore të aplikacionit.

### Metodat

| Metoda | HTTP | Rruga | Përshkrim |
|---|---|---|---|
| `Index()` | GET | `/` | Kthen faqen kryesore të dyqanit |
| `Privacy()` | GET | `/Home/Privacy` | Kthen faqen e politikës së privatësisë |
| `Error()` | GET | `/Home/Error` | Kthen faqen e gabimit; nuk ruhet në cache |

---

## ProductsController.cs

**Vendndodhja:** `Controllers/ProductsController.cs`  
**Namespace:** `Projekti_Final.Controllers`  
**Trashëgon:** `Controller`  
**Varësia:** `ApplicationDbContext` (injektuar nëpërmjet konstruktorit)

Kontrolluesi MVC për menaxhimin e produkteve. Zbaton operacionet CRUD të plota (Create, Read, Update, Delete). Veprimet e shkrimit (Create, Edit, Delete) janë të mbrojtura me atributin `[Authorize]` dhe kërkojnë hyrje në sistem.

### Metodat

| Metoda | HTTP | Rruga | Autorizo | Përshkrim |
|---|---|---|---|---|
| `Index()` | GET | `/Products` | Jo | Liston të gjitha produktet |
| `Details(id)` | GET | `/Products/Details/{id}` | Jo | Tregon detajet e një produkti |
| `Create()` | GET | `/Products/Create` | Po | Tregon formularin e krijimit |
| `Create(product)` | POST | `/Products/Create` | Po | Ruan produktin e ri |
| `Edit(id)` | GET | `/Products/Edit/{id}` | Po | Tregon formularin e ndryshimit |
| `Edit(id, product)` | POST | `/Products/Edit/{id}` | Po | Ruan ndryshimet e produktit |
| `Delete(id)` | GET | `/Products/Delete/{id}` | Po | Tregon konfirmimin e fshirjes |
| `DeleteConfirmed(id)` | POST | `/Products/Delete/{id}` | Po | Fshin produktin nga databaza |

### Shënime
- Të gjitha metodat POST përdorin `[ValidateAntiForgeryToken]` për mbrojtje nga sulmet CSRF.
- `DeleteConfirmed` kontrollon nëse produkti ekziston para se ta fshijë.

---

## CartController.cs

**Vendndodhja:** `Controllers/CartController.cs`  
**Namespace:** `Projekti_Final.Controllers`  
**Trashëgon:** `Controller`  
**Varësia:** `CartService`, `ApplicationDbContext`

Kontrolluesi i shportës së blerjeve. Menaxhon paraqitjen, shtimin, heqjen dhe porosinë e produkteve.

### Metodat

| Metoda | HTTP | Rruga | Përshkrim |
|---|---|---|---|
| `Index()` | GET | `/Cart` | Tregon të gjitha artikujt në shportë dhe totalin |
| `Add(productId, returnUrl)` | POST | `/Cart/Add` | Shton një produkt në shportë |
| `Remove(productId)` | POST | `/Cart/Remove` | Heq një produkt nga shporta |
| `Checkout()` | POST | `/Cart/Checkout` | Përfundon porosinë dhe pastron shportën |

### Shënime
- `Add` gjen produktin nga databaza dhe e kalon te `CartService`.
- `Checkout` simulon dërgimin e porosisë — pastron sesionin dhe ridrejton tek faqja kryesore.
- Mesazhet e suksesit transmetohen nëpërmjet `TempData["Message"]`.

---

## API/ProductsApiController.cs

**Vendndodhja:** `Controllers/API/ProductsApiController.cs`  
**Namespace:** `Projekti_Final.Controllers.API`  
**Trashëgon:** `ControllerBase`  
**Rruga bazë:** `/api/products`

Web API RESTful për produktet. Kthen dhe pranon të dhëna në format JSON. Veprimet e shkrimit kërkojnë autentikimin.

### Endpoint-et

| Metoda | HTTP | Rruga | Autorizo | Përshkrim |
|---|---|---|---|---|
| `Get()` | GET | `/api/products` | Jo | Kthen listën e të gjitha produkteve |
| `Get(id)` | GET | `/api/products/{id}` | Jo | Kthen një produkt sipas ID-së |
| `Post(product)` | POST | `/api/products` | Po | Krijon produkt të ri |
| `Put(id, product)` | PUT | `/api/products/{id}` | Po | Përditëson produktin ekzistues |
| `Delete(id)` | DELETE | `/api/products/{id}` | Po | Fshin produktin sipas ID-së |

### Shënime
- Endpoint-et publike (GET) mund të thirren pa autentikim.
- Endpoint-et e modifikimit (POST, PUT, DELETE) kërkojnë token autentikimi.
