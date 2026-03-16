# Field-Design für PlayPlanner

Dieses Dokument beschreibt, **was ein Spielfeld (Field)** in PlayPlanner speichern soll und warum. Ziel ist ein Modell, das:

- mehrere Sportarten (z. B. Basketball, Football, Handball) unterstützt,
- ein **normiertes 2D-Koordinatensystem** (0–1) für die Visualisierung erlaubt,
- später um zusätzliche Layout-Details erweitert werden kann, ohne das Grundmodell zu brechen.

---

## 1. Verantwortung des Field-Objekts

Ein `Field` beschreibt **den Raum**, in dem sich Spieler und Ball bewegen. Es speichert:

- **Sportart / Typ** → damit klar ist, welche Geometrie und Regeln ungefähr gelten.
- **Geometrie** → Seitenverhältnis / Abmessungen, damit die 2D-Ansicht korrekt skaliert wird.
- **Layout-Metadaten** → optionale Infos für Linien, Zonen, 3-Punkte-Linie, Tore etc.

Ein Field speichert **keine** team- oder play-spezifischen Daten (keine Spieler, keine Keyframes).

---

## 2. Minimale Pflicht-Attribute (v1)

Diese Eigenschaften sollten in der ersten Version auf jeden Fall vorhanden sein:

1. **Id**
   - Typ: GUID / UUID
   - Zweck: eindeutige Identifikation in DB und API.

2. **Name**
   - Beispiel: "Basketball Full Court", "Soccer 11v11 Standard"
   - Wird in der UI angezeigt, Auswahl im Frontend.

3. **SportType**
   - Enum oder String (z. B. `basketball`, `football`, `handball`)
   - Dient dazu, passende Renderer und Standardwerte zu wählen.

4. **Abmessungen**
   - Beispiel-Eigenschaften:
     - `WidthMeters` (decimal)
     - `HeightMeters` (decimal)
   - Werden genutzt, um das normierte Koordinatensystem auf die echte Feldgröße abzubilden.

5. **Koordinaten-Konvention** (implizit, aber wichtig)
   - Wir legen fest:
     - (0,0) = **linke untere Ecke**
     - (1,1) = **rechte obere Ecke**
   - Diese Konvention wird im Code und in der Doku beschrieben, aber nicht als eigene Spalte gespeichert.

6. **MetaJson** (optional, für v1 schon vorsehen)
   - Typ: JSON/JSONB-Spalte in Postgres, String in C#
   - Inhalt: strukturierte Infos für Feldlayout, z. B.:
     - Linien (Mittelkreis, Dreipunktelinie, Strafraum, Zonen)
     - Zonen-Namen ("Left Corner", "High Post", "Slot" usw.)
   - Vorteil: neue Layoutdetails können hinzugefügt werden, ohne das DB-Schema zu ändern.

---

## 3. Optionale/erweiterbare Attribute

Diese Punkte kannst du später ergänzen, ohne das Grundmodell zu zerstören:

1. **Orientation / Default-Angreifende Richtung**
   - Z. B. welches Team typischerweise nach oben/unten angreift.
   - Nützlich für Visualisierungen oder wenn Plays immer in eine Richtung geplant werden.

2. **DefaultPlayerCount**
   - Wie viele Spieler pro Team auf dem Feld sind (z. B. 5 bei Basketball, 11 bei Fußball).
   - Hilft dem Editor, Standard-Setups vorzuschlagen.

3. **Versionierung / isActive**
   - Falls du Varianten eines Spielfeldtyps pflegen willst (z. B. unterschiedliche Ligenregeln).

---

## 4. Datenbank-Mapping (Postgres)

Empfohlene Tabelle `fields` (vereinfacht):

```sql
CREATE TABLE fields (
    id            uuid PRIMARY KEY,
    name          text      NOT NULL,
    sport_type    text      NOT NULL,
    width_m       numeric(5,2) NOT NULL,
    height_m      numeric(5,2) NOT NULL,
    meta          jsonb     NULL
);
```

- `sport_type` kann z. B. Werte wie `basketball`, `football` etc. haben.
- `meta` bleibt flexibel für Layoutdaten.

---

## 5. Vorschlag für C#-Domänenmodell

Zur Orientierung (du passt die Details selbst an):

```csharp
public enum SportType { Basketball, Football, Handball, Custom }

public class Field {
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public SportType Sport { get; set; }
    public decimal WidthMeters { get; set; }
    public decimal HeightMeters { get; set; }
    public string? MetaJson { get; set; }
}
```

Wichtig: In Kommentaren und/oder Doku festhalten, dass **alle Player- und Ball-Positionen** im Bereich `0..1` auf diesem Feld definiert werden und der Renderer die Umrechnung auf Pixel übernimmt.

---

## 6. Nächste Schritte

- `Field`-Klasse im `PlayPlanner.Domain`-Projekt anlegen.
- Im `Infrastructure`-Projekt das Mapping in den `AppDbContext` integrieren (`DbSet<Field> Fields`).
- Erste Migration erstellen, damit die `fields`-Tabelle in der DB angelegt wird.
- Einen `FieldsController` in der API entwerfen, der die Basis-CRUD-Operationen bereitstellt.

