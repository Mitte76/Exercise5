# Övning 5: Garage 1.0

Detta är en robust konsolapplikation i C# som simulerar ett enkelt hanteringssystem för ett fysiskt garage. Projektet är byggt för att demonstrera objektorienterad programmering (OOP), arv, polymorfism och hantering av egna samlingar.

## 🚀 Funktioner

* **Dynamiskt Garage:** Skapa ett garage med en användarspecificerad kapacitet.
* **Fordonshantering:** Parkera, hämta ut och lista fordon.
* **Avancerad sökning:** Sök efter specifika fordon via skiftlägesokänsligt registreringsnummer (t.ex. `abc123`) eller kombinera egenskaper (t.ex. "alla svarta fordon med 4 hjul").
* **Statistik:** Visa en översikt av hur många fordon av varje typ som står i garaget för tillfället.
* **Förpopulering:** Möjlighet att starta applikationen med ett antal färdigparkerade fordon för enklare testning.
* **Robust felhantering:** Applikationen validerar all användarindata och kraschar inte vid felaktiga inmatningar.

## 🛠️ Arkitektur & Krav

Projektet följer en strikt uppdelning av ansvarsområden (Separation of Concerns):

* **Användargränssnitt (UI):** En textbaserad huvudmeny med undermenyer som hanterar all interaktion och ger tydlig feedback till användaren.
* **Garage-klassen:** Hanterar samlingen av fordon internt. **Viktigt:** Använder en strikt, privat `Vehicle[]`-array i stället för `List<Vehicle>` enligt kravspecifikationen.
* **Vehicle (Basmodell):** Innehåller gemensamma egenskaper för alla fordon (unikt registreringsnummer, färg, antal hjul).
* **Subklasser (Fordonstyper):** 
  * `Airplane` (Egen egenskap: Antal motorer)
  * `Motorcycle` (Egen egenskap: Cylindervolym)
  * `Car` (Egen egenskap: Bränsletyp)
  * `Bus` (Egen egenskap: Antal sittplatser)
  * `Boat` (Egen egenskap: Längd)

## 💻 Komma igång

1. Öppna lösningsfilen (`.sln`) i Visual Studio.
2. Starta projektet (`F5` eller klicka på *Play*).
3. Följ instruktionerna på skärmen för att skapa ditt första garage och börja parkera fordon.
