# 🥊 BEATemUP – Console Turn-Based Fighting Game

**BEATemUP** is a simple two-player turn-based fighting game built in C#.  
It runs in the console and combines dice mechanics, RPG-style stats, and basic strategy.

This project was created as a **beginner-level exercise** to strengthen understanding of:

- ✅ Object-Oriented Programming (OOP)
- ✅ Interfaces (e.g. `IRaces`)
- ✅ Dependency Injection (constructor-based)
- ✅ Separation of Concerns
- ✅ Console-based UI design

---

## 🚀 Features

- 🧍 Choose from 4 unique characters with individual stats and race bonuses
- 🎲 Dice mechanics for attack strength and critical hit chance
- 💢 Rage Mode activated at low HP to boost critical chance
- 💊 Use medikits to restore HP
- 👥 Two-player mode via local console
- 🌈 Color-coded console output based on characters

---

## 📚 Technologies Used

- C# (.NET 6 or newer)
- Object-Oriented Architecture
- Console Application
- Dependency Injection
- Interfaces
- Game loop and logic encapsulation

---

## 🧪 How to Run the Game

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download) or later
- A terminal or console window

### Running the Game

```bash
dotnet build
dotnet run
```

---

## 🎮 Gameplay Overview

1. **Character Selection**: Both players choose their fighter from a list.
2. **Roll for Starter**: A dice roll determines who begins.
3. **Gameplay Loop**: Players alternate turns, choosing to:
   - Attack (base and critical chance via dice)
   - Use a medikit (heal based on dice roll)
4. **Victory Condition**: The game ends when one player’s HP drops to 0.

---

## 📁 Project Structure

```
/BEATemUP
├── Program.cs                  # Application entry point
├── Character.cs                # Character model with stats
├── CharacterFactory.cs         # Handles character creation
├── GameManager.cs              # Core gameplay logic
├── DiceService.cs              # Dice roll logic (1–20 and 1–100)
├── DamageCalculation.cs        # Attack & critical damage logic
├── HealthPointCalculation.cs   # Healing and HP management
├── DisplayCharacters.cs        # Displays all available characters
├── GameplayDisplay.cs          # Turn-based game feedback
├── StarterDisplay.cs           # UI before gameplay starts
└── README.md                   # Project documentation
```

---

## 🔍 OOP Concepts Demonstrated

| Concept              | Where it's Used                         |
|----------------------|------------------------------------------|
| Classes & Objects    | `Character`, `GameManager`, `Factory`    |
| Interface            | `IRaces` for race-specific bonuses       |
| Dependency Injection | Passed via constructor in `GameManager`  |
| Encapsulation        | Private fields with public accessors     |
| Separation of Concerns | Display, logic, data separated cleanly |

---

## 💡 Future Improvements

- Add single-player mode with basic AI
- Expand combat system with skills or special effects
- Use persistent save files or a database
- Build a GUI (WPF or Unity)
- Add leveling/progression system

---

## 📜 License

This project is licensed under the **MIT License**.  
See the [`LICENSE`](LICENSE) file for details.

---

## 👨‍💻 Author

**Your Name Here**  
> This project was created to practice and improve object-oriented programming skills using C#, including interfaces and dependency injection.
