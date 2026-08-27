# Dokumentacioni — Folder: Models

Folderi `Models` përmban klasat që përfaqësojnë strukturën e të dhënave të aplikacionit. Këto klasa përdoren nga Entity Framework Core për krijimin e tabelave në databazë dhe për transferimin e të dhënave ndërmjet shtresave.

---

## Product.cs

**Vendndodhja:** `Models/Product.cs`  
**Namespace:** `Projekti_Final.Models`

Modeli kryesor i aplikacionit. Përfaqëson një produkt në katalog dhe ruhet në tabelën `Products` të databazës.

### Pronat

| Prona | Tipi | Kufizime | Përshkrim |
|---|---|---|---|
| `Id` | `int` | Çelës primar, auto-increment | Identifikuesi unik i produktit |
| `Name` | `string` | Required, max 100 karaktere | Emri i produktit |
| `Price` | `decimal` | Required, vlera 0–100000 | Çmimi i produktit |
| `Description` | `string?` | Max 500 karaktere | Përshkrimi i produktit |

### Shënime
- Validimi kryhet nëpërmjet atributeve `[Required]`, `[StringLength]`, dhe `[Range]`.
- `Description` mund të jetë `null` (opsionale).

---

## CartItem.cs

**Vendndodhja:** `Models/CartItem.cs`  
**Namespace:** `Projekti_Final.Models`

Përfaqëson një artikull brenda shportës së blerjeve. Nuk ruhet në databazë — ekziston vetëm gjatë sesionit të përdoruesit (ruhet si JSON në sesion).

### Pronat

| Prona | Tipi | Përshkrim |
|---|---|---|
| `ProductId` | `int` | ID e produktit të zgjedhur |
| `Name` | `string` | Emri i produktit (kopje nga databaza) |
| `Price` | `decimal` | Çmimi i produktit në momentin e shtimit |
| `Quantity` | `int` | Sasia e zgjedhur nga përdoruesi |
| `Total` | `decimal` | Llogaritur: `Price × Quantity` (vetëm lexim) |

### Shënime
- `Total` është pronë e llogaritur (`get` only) dhe nuk serializohet veçmas.
- Të dhënat kopjohen nga `Product` në momentin e shtimit, duke ruajtur çmimin aktual.

---

## ErrorViewModel.cs

**Vendndodhja:** `Models/ErrorViewModel.cs`  
**Namespace:** `Projekti_Final.Models`

Model i thjeshtë i përdorur vetëm nga faqja e gabimit (`/Home/Error`).

### Pronat

| Prona | Tipi | Përshkrim |
|---|---|---|
| `RequestId` | `string?` | ID unike e kërkesës HTTP |
| `ShowRequestId` | `bool` | `true` nëse `RequestId` nuk është bosh |
