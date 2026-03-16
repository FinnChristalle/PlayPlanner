# Leitfaden für konsistente EF-Migrationen

Dieser Guide beschreibt, wie du **Entity Framework Core Migrationen** in deinem Projekt konsistent durchführst.
Er ist bewusst technologie-fokussiert, aber ohne konkreten Code – du füllst die Befehle in deinem Terminal selbst aus.

---

## 1. Grundprinzipien

- **Ein DbContext pro Datenbank** (hier: `AppDbContext` im Infrastructure-Projekt).
- **Migrationen gehören ins Infrastructure-Projekt**, nicht ins API-Projekt.
- **Startup-Projekt ist die API**, da dort `Program.cs`/`Main` liegt und der DbContext registriert wird.
- Migrationen werden nur über `dotnet ef` erzeugt, **nicht** manuell in SQL-Dateien geschrieben.

---

## 2. Projekte und Rollen

- **Migrations-Projekt**
  - Enthält den `AppDbContext`.
  - Beispiel: `backend/PlayPlanner.Infrastructure/PlayPlanner.Infrastructure.csproj`.

- **Startup-Projekt**
  - Enthält die `Program.cs`/Startup-Konfiguration und registriert den DbContext.
  - Beispiel: `backend/PlayPlanner.Api/PlayPlanner.Api.csproj`.

Diese Trennung erlaubt dir, mehrere Entry-Points (z. B. API, Worker) auf derselben Datenbank aufzubauen.

---

## 3. Vorbereitungen

Bevor du Migrationen erzeugst:

1. Stelle sicher, dass
   - der `AppDbContext` existiert und den `Field`-Typ (und weitere Entitäten) kennt,
   - das Infrastructure-Projekt eine Referenz auf das Domain-Projekt hat.
2. Stelle sicher, dass
   - im Infrastructure-Projekt die EF Core-Pakete installiert sind,
   - im API-Projekt `AddDbContext<AppDbContext>` mit einem ConnectionString konfiguriert ist.
3. Prüfe, dass `appsettings.Development.json` einen gültigen `Default`-ConnectionString enthält
   (für SQLite oder Postgres, je nach Entscheidung).

---

## 4. Universeller Befehl zum Erzeugen einer Migration

Grundmuster:

- Verwende immer das **Infrastructure-Projekt** als `--project` (dort liegen DbContext und Migrationen).
- Verwende immer das **Api-Projekt** als `--startup-project` (dort startet die Anwendung).
- Lege Migrationen in einem klaren Ordner an, z. B. `Persistence/Migrations`.

**Universelles Befehlsmuster:**

- `dotnet ef migrations add <MigrationName> \`
- `  --project <Pfad-zum-Infrastructure-Projekt> \`
- `  --startup-project <Pfad-zum-Api-Projekt> \`
- `  --output-dir <Migrations-Ordner-relativ-zum-Infrastructure-Projekt>`

Die Platzhalter setzt du wie folgt ein:

- `<MigrationName>`: kurzer, beschreibender Name, z. B. `InitialCreate`, `AddFields`, `AddTeamsAndPlayers`.
- `<Pfad-zum-Infrastructure-Projekt>`: Pfad zur `.csproj` des Infrastructure-Projekts relativ zum Arbeitsverzeichnis.
- `<Pfad-zum-Api-Projekt>`: Pfad zur `.csproj` der API relativ zum Arbeitsverzeichnis.
- `<Migrations-Ordner-relativ-zum-Infrastructure-Projekt>`: z. B. `Persistence/Migrations`.

Wichtig: Du kannst den Befehl von überall im Repository ausführen, solange die Pfade zu den Projekten stimmen.

---

## 5. Universeller Befehl zum Anwenden einer Migration

Wenn eine Migration erzeugt wurde, musst du sie auf die Ziel-Datenbank anwenden.

**Universelles Befehlsmuster:**

- `dotnet ef database update \`
- `  --project <Pfad-zum-Infrastructure-Projekt> \`
- `  --startup-project <Pfad-zum-Api-Projekt>`

Die Platzhalter sind die gleichen wie oben.

Damit wird der `AppDbContext` aus dem Infrastructure-Projekt verwendet, und die Konfiguration (ConnectionString, Provider) aus dem Startup-Projekt.

---

## 6. Namens- und Ablaufkonventionen

Um Migrationen langfristig verständlich zu halten, helfen Konventionen:

1. **Migrationen klein und fokussiert halten**
   - Pro Migration eine klar umrissene Änderung (z. B. "AddFields", "AddTeams", "AddPlays").

2. **Benennung**
   - Nutze sprechende Namen, die sowohl Entitäten als auch Aktionen enthalten können:
     - `AddFieldsTable`
     - `AddTeamsAndPlayers`
     - `AddPlayTimelineTables`

3. **Reihenfolge**
   - Felder (`fields`) zuerst,
   - dann Teams und Players,
   - dann Plays und später Timeline-spezifische Tabellen.

4. **Review jeder Migration**
   - Vor `database update` kurz in die generierte Migration schauen und prüfen:
     - Werden nur die erwarteten Tabellen/Spalten/Constraints angelegt oder geändert?

---

## 7. Typische Workflows

### 7.1 Neue Entität (z. B. Field) hinzufügen

1. Domain-Klasse ergänzen (z. B. `Field`).
2. DbContext (und ggf. Konfiguration) anpassen.
3. Migration mit sprechendem Namen erzeugen.
4. Migration auf Dev-Datenbank anwenden.
5. API-Endpunkte und Tests ergänzen.

### 7.2 Änderung an bestehender Entität

1. Domain und DbContext-Konfiguration anpassen.
2. Neue Migration mit klarem Namen erzeugen (z. B. `AlterFieldsAddRuleSet`).
3. Migration prüfen.
4. `database update` ausführen.
5. Relevante Stellen im Code und Tests nachziehen.

---

## 8. Hinweise für verschiedene Umgebungen

- **Development**
  - Du kannst häufiger Migrationen erzeugen und anwenden.
  - Datenbank kann bei Bedarf neu erstellt werden (z. B. während der frühen Entwicklung).

- **Staging/Production**
  - Migrationen sollten **reviewt, getestet und versioniert** sein.
  - Niemals ungetestete Migrationen direkt in Prod anwenden.
  - Idealerweise laufen Migrationen im Rahmen der CI/CD-Pipeline kontrolliert durch.

Dieser Guide soll dir helfen, deine Migrationen von Anfang an konsistent und skalierbar zu organisieren.

