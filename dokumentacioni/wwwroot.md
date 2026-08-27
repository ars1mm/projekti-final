# Dokumentacioni — Folder: wwwroot

Folderi `wwwroot` është rrënja e skedarëve statikë të aplikacionit. Skedarët këtu shërbehn direkt nga serveri pa u procesuar nga ASP.NET Core.

---

## Struktura

```
wwwroot/
├── css/
│   └── styles.css        → Stilet kryesore (Start Bootstrap Creative theme)
├── js/
│   └── scripts.js        → JavaScript i personalizuar
└── assets/
    ├── favicon.ico       → Ikona e faqes (shfaqet në tab të shfletuesit)
    └── img/
        ├── gymbanner.jpg → Imazhi hero i faqes kryesore
        └── portfolio/    → Imazhet e koleksioneve
```

---

## css/styles.css

Stylesheet kryesor i ndërtuar mbi **Start Bootstrap Creative** theme. Mbulon:
- Stilimin e navbar-it dhe hero section-it
- Ngjyrat, fontet dhe hapësirat
- Animacionet e scroll-it
- Stilet e personalizuara për kartat e produkteve dhe shportën

Fontet e jashtme ngarkohen nga CDN (Google Fonts: Merriweather, Merriweather Sans).

---

## js/scripts.js

Skripti JavaScript i personalizuar. Mbulon:
- Aktivizimin e SimpleLightbox për galeri imazhesh
- Validimin e formularit të kontaktit nga ana e klientit
- Efektet vizuale të navbar-it gjatë scroll-it

---

## assets/

Përmban burimet vizuale statike:

| Skedari | Përshkrim |
|---|---|
| `favicon.ico` | Ikona 32×32 px e shfaqur në tab të shfletuesit |
| `img/gymbanner.jpg` | Imazhi kryesor i hero section-it (241 KB) |
| `img/portfolio/` | Imazhet e galerie së koleksioneve në faqen kryesore |

---

## index.html

Faqe HTML statike e rezervuar (nuk përdoret aktivisht nga aplikacioni MVC).

---

## Shënime

- Skedarët statikë aktivizohen nëpërmjet `app.UseStaticFiles()` në `Program.cs`.
- Për referenca në Razor views, përdoret prefix `~/` (p.sh. `~/css/styles.css`).
- CDN-të e jashtme (Bootstrap, SimpleLightbox) ngarkohen direkt nga interneti dhe nuk ruhen lokalisht.
