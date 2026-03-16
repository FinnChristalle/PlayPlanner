# Field-Implementierungsleitfaden

Dieses Dokument beschreibt Schritt f\u00fcr Schritt, wie du das `Field`-Konzept aus `FieldDesign.md` **in deinem .NET-Backend** umsetzt. Es fokussiert auf Struktur und Vorgehen, nicht auf fertigen Code.

---

## 1. Ziele

- Ein **Dom\u00e4nenobjekt `Field`** im Projekt `PlayPlanner.Domain` bereitstellen.
- Das Objekt in **EF Core / Postgres** (oder Sqlite f\u00fcr Dev) abbilden.
- Eine **`fields`-Tabelle** per Migration anlegen.
- Erste **API-Endpunkte** in `PlayPlanner.Api` bereitstellen, um Fields zu verwalten.

---

## 2. Domain: `Field`-Klasse anlegen

1. Wechsle ins Projekt `PlayPlanner.Domain`.
2. Lege einen Ordner an, z. B. `Entities` oder `Fields`.
3. Erstelle eine Klasse `Field` mit mindestens folgenden Eigenschaften:
   - `Id` (Guid)
   - `Name` (string, nicht null)
   - `Sport` (Enum `SportType` oder string)
   - `WidthMeters` (decimal)
   - `HeightMeters` (decimal)
   - `MetaJson` (string?, optional)
4. Dokumentiere in XML-Kommentaren oder README:
   - Koordinatenkonvention: (0,0) = links unten, (1,1) = rechts oben.
   - Invarianten: `WidthMeters > 0`, `HeightMeters > 0`.
5. Optional: 
   - F\u00fcge Audit-Felder hinzu (`CreatedAt`, `UpdatedAt`).
   - Nutze einen Enum `SportType` f\u00fcr typsichere Sportarten.

> Hinweis: Du kannst sp\u00e4ter Factory-Methoden oder Konstruktoren mit Validierung erg\u00e4nzen, um ung\u00fcltige Felder zu verhindern.

---

## 3. Infrastructure: `AppDbContext` und Mapping

1. Wechsle ins Projekt `PlayPlanner.Infrastructure`.
2. Stelle sicher, dass dort ein `AppDbContext` existiert (falls nicht, lege ihn an).
3. F\u00fcge eine `DbSet<Field>`-Eigenschaft hinzu, z. B. `public DbSet<Field> Fields { get; set; }`.
4. Entscheide, ob du **Fluent API** f\u00fcr das Mapping nutzen willst:
   - Erstelle eine Klasse `FieldConfiguration` (z. B. im Ordner `Persistence/Configurations`).
   - Konfiguriere dort Tabelle `fields`, Spaltennamen und Constraints (z. B. maximale L\u00e4nge f\u00fcr `Name`).
5. Registriere die Konfiguration im `OnModelCreating` des `AppDbContext` (z. B. \"modelBuilder.ApplyConfiguration(new FieldConfiguration());\").

> Achte darauf, dass der `AppDbContext` im `Program.cs`/`Startup` deiner API registriert ist und die ConnectionString-Einstellung aus `appsettings` zieht.

---

## 4. DB-Provider und ConnectionStrings

1. Definiere in `appsettings.Development.json` einen ConnectionString `Default`:
   - Entweder f\u00fcr **Sqlite** (schneller Start): `Data Source=playplanner_dev.db`.
   - Oder f\u00fcr **Postgres** (n\u00e4her an Prod): `Host=localhost;Port=5432;Database=...`.
2. In `Program.cs` (in `PlayPlanner.Api`):
   - H\u00e4nge den `AppDbContext` mit dem gew\u00e4hlten Provider ein (`UseSqlite` oder `UseNpgsql`).
   - Optional: Umgebung pr\u00fcfen (Development vs. Production) und Provider unterscheiden.

---

## 5. Migration erstellen und anwenden

1. Installiere ggf. das EF Core Tools-Paket im API- oder Infrastructure-Projekt (falls noch nicht geschehen).
2. F\u00fchre im Projektordner (meist `PlayPlanner.Api` oder `PlayPlanner.Infrastructure`) den Befehl zur Migrationserstellung aus, z. B.:
   - `dotnet ef migrations add AddFields` (genauer Befehl h\u00e4ngt von deinem Setup ab).
3. Pr\u00fcfe die generierte Migration:
   - Sie sollte eine Tabelle `fields` mit den in `FieldDesign.md` beschriebenen Spalten erstellen.
4. Wende die Migration auf die Dev-Datenbank an:
   - `dotnet ef database update`.
5. Verifiziere in der DB (z. B. via DB-Tool oder CLI), dass die Tabelle `fields` existiert.

---

## 6. API-Schicht: DTOs und Controller

1. Wechsle ins Projekt `PlayPlanner.Api`.
2. Lege einen Ordner an, z. B. `Contracts/Fields` oder `Dtos/Fields`.
3. Definiere dort DTOs:
   - `FieldDto` f\u00fcr Antworten (Id, Name, SportType, WidthMeters, HeightMeters, MetaJson).
   - `CreateFieldRequest` f\u00fcr POST (ohne Id, mit Pflichtfeldern).
   - Optional `UpdateFieldRequest` f\u00fcr PUT.
4. Implementiere eine Mapping-Hilfsklasse oder Extension-Methoden:
   - Aufgabe: `Field` \u2194 `FieldDto` abbilden.
5. Erstelle einen `FieldsController` (z. B. im Ordner `Controllers`):
   - `GET /api/fields` \u2192 Liste aller Felder.
   - `GET /api/fields/{id}` \u2192 einzelnes Feld.
   - `POST /api/fields` \u2192 neues Feld anlegen.
6. Stelle sicher, dass:
   - Eingabedaten validiert werden (ModelState, ggf. DataAnnotations).
   - Fehler (nicht gefunden, Validierungsfehler) sinnvoll als HTTP-Statuscodes zur\u00fcckgegeben werden.

---

## 7. Tests & Qualit\u00e4tsaspekte

1. Schreibe Unit-Tests f\u00fcr die Dom\u00e4nenlogik von `Field` (falls du dort Validierungen oder Factory-Methoden einbaust).
2. Schreibe Integrationstests f\u00fcr die wichtigsten API-Endpunkte von `FieldsController`:
   - `POST` legt ein Feld an und Persistenz funktioniert.
   - `GET` liefert angelegte Felder korrekt zur\u00fcck.
3. Pr\u00fcfe die Performance & Sicherheit:
   - Begrenze die L\u00e4nge von `Name` (z. B. max. 200 Zeichen).
   - Begrenze die Gr\u00f6\u00dfe/Komplexit\u00e4t von `MetaJson` (z. B. per Validierung), damit niemand riesige JSON-Objekte speichert.

---

## 8. N\u00e4chste Schritte nach Field

Sobald Fields sauber funktionieren, kannst du das gleiche Muster f\u00fcr weitere Entit\u00e4ten anwenden:

- `Team` (inkl. `SportType` und optional `DefaultFieldId`).
- `Player` (mit Referenz auf `Team`).
- `Play` (mit Referenzen auf `Team` und `Field`).

Damit steht das Fundament, um sp\u00e4ter die Timeline- und PlaySystem-Logik darauf aufzubauen.

