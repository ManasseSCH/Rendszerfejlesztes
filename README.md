### Blog

Ez a projekt a Rendszerfejlesztés óra beadandója

- Stefán Balázs (GCJ8PO)
- Schnéberger Manassé (ERK7L9)
- Dimitrov Bernát (UABH63)

---

### Futtatás szűz Windows felületen

1. **Szükséges programok**: Visual Studio, .NET, ASP.net, Entity Framework, adatbázis-kezelő szoftver 

2. **Az adatbázis üzembehelyezése**:
    - Nyisd meg a NuGet Package Manager-t.
    - Állítsd át a Default project mező "Rendszerfejl" elemét "Server"-re.
    - Ha még nincs migration mappa, hozz létre egyet az alábbi paranccsal: `Add-Migration [név]`
    - Futtasd a következő parancsot: `Update-Database`

    Példa adatokkal való feltöltés:
    Másold be az "sqlcode másolata" nevű .txt fájlban található kódot query-ként és futtasd azokat az adatbázison.

4. **Indítás**: Több projekt indítása

5. Bejelentkezés példa: user1 / password1
 
---
