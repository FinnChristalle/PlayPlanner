# Modulares PlaySystem – Idee & Architektur

Dieses Dokument beschreibt eine **modulare und erweiterbare Architektur** für ein System, das Spielabläufe (Plays) für Sportteams speichert, visualisiert und abspielt.

Ziel: Neue Sportarten, neue Aktionen (z. B. Pass, Block, Screen), neue Visualisierungen sollen ohne große Umbauten ergänzbar sein.

---

## 1. Kernideen

- **Trennung von Domäne, Engine, Visualisierung und Persistenz**
- **Koordinatensystem normiert (0–1)**, damit jede Sportart eigene Feldgröße haben kann
- **Timeline-basiertes Modell** mit Keyframes und Events
- **Plugin-fähige Module** für Sportarten und Aktionstypen

---

## 2. Domänenmodell (fachlich)

### 2.1 Basis-Konzepte

- **Team**: Gruppe von Spielern
- **Player**: Name, Nummer, Standardposition, Sportart-bezogenes Profil
- **Field**: Typ (z. B. Basketball, Fußball), Seitenverhältnis, Linienlayout
- **Play**: Ein geplanter Ablauf für ein Team
  - Metadaten: Name, Beschreibung, Dauer, Sportart, Team
  - Inhalt: Bewegungen der Spieler und Aktionen (Pässe, Screens, Würfe)

### 2.2 Timeline-Struktur

- **Actor**: Abstrakte Entität, die sich bewegt (Spieler, Ball, ggf. Coach)
- **Track** pro Actor:
  - Liste von Keyframes: `{ timeSec, x, y, orientation? }`
- **Event**:
  - Zeitbezogenes Ereignis (Pass, Screen, Wurf, Cut, etc.)
  - Enthält Typ + Referenzen auf beteiligte Actoren

Diese Grundstruktur funktioniert für viele Sportarten und ist leicht erweiterbar.

---

## 3. Architektur-Schichten

### 3.1 Core Domain (Backend & Frontend geteilt als Konzept)

- Reine Domänen-Modelle (+ Interfaces), **ohne Framework-Abhängigkeiten**
- Verantwortlich für:
  - Definition von `Play`, `Track`, `Event`, `Field`, `Actor`
  - Geschäftsregeln (z. B. Validierung von Events)
  - Basal: Zeit- und Koordinatenkonventionen

### 3.2 Play Engine (Timeline-Engine)

- Aufgabe: Aus einem `Play` den Zustand zu einem Zeitpunkt `t` berechnen
- Funktionen:
  - Interpolation zwischen Keyframes für Spieler und Ball
  - Auswertung von Events (Pass, Screen, Wurf…) zur Zeit `t`
  - Bereitstellung eines API:
    - `getStateAtTime(play, t) -> FrameState`
    - `simulate(play, stepSize) -> FrameState[]` (für Previews oder Export)
- Implementierung kann als **Backend-Bibliothek** (.NET) und/oder **Frontend-Service** (Angular) existieren.

### 3.3 Visualization Layer

- **Field Renderer** pro Sportart:
  - Kennt die Geometrie und das Layout (Linien, Zonen)
  - Wandelt normierte Koordinaten (0–1) in Pixel um
- **Overlay Renderer**:
  - Zeichnet Actoren (Spieler, Ball) und Symbole (z. B. Pass-Pfeile)
- Pluggable Design:
  - Neue Sportart = neues Field-Theme + ggf. zusätzliche Overlays

### 3.4 Persistence & API Layer

- **Repository-Schnittstellen**:
  - `PlayRepository`: Speichern/Laden kompletter Plays
  - Optional spezifische Repositories für Teams, Spieler, Vorlagen
- **Datenspeicher**:
  - Start: JSON-basierte Speicherung des kompletten Plays in einer Spalte
  - Später erweiterbar auf normalisierte Tabellen (Play, Track, Keyframe, Event)
- **REST-API**:
  - CRUD auf Plays (`/api/plays`)
  - Endpunkte liefern/erwarten ein standardisiertes DTO für `Play`

---

## 4. Angular-Frontend – Modularer Aufbau

Empfohlene Module (z. B. als Libraries oder Feature-Module):

- `play-core`:
  - Typdefinitionen für `Play`, `Track`, `Event`, `Field` (spiegeln Core Domain)
  - `PlayPlayerService` (Steuerung: Play/Pause/Seek/Speed)
- `play-viewer`:
  - Komponenten für Spielfeld-Ansicht, Actor-Overlays, Event-Anzeige
  - Nutzt `PlayPlayerService` zur Darstellung
- `play-editor`:
  - UI zum Erstellen/Bearbeiten von Plays
  - Timeline-Editor (Drag & Drop von Keyframes, Bearbeiten von Events)

So bleibt der Viewer nutzbar, auch wenn der Editor noch nicht fertig ist.

---

## 5. Erweiterbarkeitsszenarien

### 5.1 Neue Sportart hinzufügen

1. Neues **Field-Layout** definieren (z. B. Handballfeld mit Torzonen)
2. Renderer für dieses Feld implementieren (Farben, Linien, Maße)
3. Optional spezielle Events (z. B. "Pick & Roll" im Basketball) hinzufügen
4. UI-Konfiguration, damit Plays dieser Sportart zugeordnet werden können

### 5.2 Neuen Event-Typ einführen (z. B. Screen)

1. Neuen Event-Typ als Enum/ID definieren (`"screen"`)
2. Datenstruktur um spezifische Felder erweitern (wer stellt Screen für wen?)
3. Engine-Logik: Wie wirkt sich das auf die Darstellung / Interpretation aus?
4. Visualization: Symbolik und ggf. besondere Hervorhebung zeichnen
5. Editor: Eingabefelder und Tooling zum Anlegen dieses Event-Typs

---

## 6. Praktischer Start

Empfohlene Reihenfolge für die Umsetzung:

1. **Core-Domänenmodelle** für `Play`, `Track`, `Event`, `Field`, `Actor` definieren
2. **Einfache Engine**: `getStateAtTime(play, t)` mit linearer Interpolation bauen
3. **Basis-Field-Renderer** für eine Sportart (z. B. Basketball) umsetzen
4. **Viewer**: Spielfeld + einfache Timeline mit Play/Pause/Seek
5. **Persistenz/REST**: Plays als JSON speichern/laden
6. Danach Schritt für Schritt neue Sportarten und Events als Erweiterungen hinzufügen

