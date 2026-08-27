# Dokumentacioni — Folder: Services

Folderi `Services` përmban shërbimet e biznesit — klasa që zbatojnë logjikën e aplikacionit të pavarur nga kontrolluesit dhe databaza.

---

## CartService.cs

**Vendndodhja:** `Services/CartService.cs`  
**Namespace:** `Projekti_Final.Services`  
**Varësia:** `IHttpContextAccessor` (injektuar)  
**Regjistrim:** `AddScoped<CartService>()` në `Program.cs`

Menaxhon shportën e blerjeve duke ruajtur artikujt si JSON brenda **sesionit HTTP** të përdoruesit. Çdo sesion ka shportën e tij të pavarur.

### Çelësi i Sesionit

`"ShoppingCart"` — ky string përdoret si çelës për ruajtjen e të dhënave JSON.

### Metodat

| Metoda | Kthimi | Përshkrim |
|---|---|---|
| `GetItems()` | `List<CartItem>` | Lexon dhe deserializon shportën nga sesioni |
| `AddItem(product, quantity)` | `void` | Shton produkt ose rrit sasinë nëse ekziston |
| `RemoveItem(productId)` | `void` | Heq artikullin me ID-në e dhënë |
| `Clear()` | `void` | Fshin të gjithë shportën nga sesioni |
| `GetTotal()` | `decimal` | Kthen shumën totale të të gjithë artikujve |
| `GetCount()` | `int` | Kthen numrin total të artikujve (duke llogaritur sasinë) |

### Metoda private

| Metoda | Përshkrim |
|---|---|
| `Save(items)` | Serializon listën si JSON dhe e ruan në sesion |

### Logjika e AddItem

```
Nëse produkti ekziston në shportë → Rrit Quantity me 1
Nëse produkti nuk ekziston       → Shton CartItem të ri
Ruan shportën e përditësuar
```

### Shënime
- Shporta humbet kur sesioni skadohet (pas 30 minutash pasiviteti, sipas konfigurimit).
- Çmimet kopjohen në momentin e shtimit — nuk ndikohen nga ndryshimet e mëvonshme.
- Shërbimi regjistrohet si `Scoped` — krijohet një instancë për çdo kërkesë HTTP.
