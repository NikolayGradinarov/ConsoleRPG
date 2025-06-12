# 🧝 Console RPG: Player vs Monsters

A console-based role-playing game where the player chooses a character class and battles monsters in a 10x10 grid arena. 
The game includes stat-based combat, procedural monster generation, and logging via Entity Framework Core.

---

## 📜 Game Overview

The game takes place in a 10x10 matrix battlefield where the player moves and fights against randomly spawning monsters. Players can select from three different character classes, each with unique stats and abilities.

---

## 🧙 Character Classes

| Class   | Strength | Agility | Intelligence | Range | Symbol |
|---------|----------|---------|---------------|-------|--------|
| Mage    | 2        | 1       | 3             | 3     | `*`    |
| Warrior | 3        | 3       | 0             | 1     | `@`    |
| Archer  | 2        | 4       | 0             | 2     | `#`    |

---

## 👾 Monsters

Stats: Randomized Strength, Agility, Intelligence (values between 1 and 3)

Range: Always 1

Symbol: 0

Implements the same Setup() method for stat calculations.

---

## 🎮 Gameplay

Map
10x10 matrix displayed with the ▒ character

Player starts at position (1,1)

Movement Controls
Key	Action |
W - Move up |
S - Move down |
D - Move right |
A - Move left |
E - Diagonal up-right |
Q - Diagonal up-left |
X - Diagonal down-right |
Z - Diagonal down-left |

Combat System
Player chooses to Move or Attack

Monsters always move toward the player

A new monster appears each round in a random cell

---

## ⚠️ Input Validation

All inputs are verified:

Valid character selection

Proper stat point allocation

Correct movement/attack commands

No over-allocations or invalid entries

---

## ✅ Features Summary

 3 Unique player classes with customizable stats

 Turn-based movement and combat system

 Range-based attacks with tactical positioning

 Monster that moves toward the player

 Console-rendered 10x10 battlefield

 Game logs stored via Entity Framework Core

 Full input validation throughout

---

## 🛠 Technologies Used

.NET (C#)

Entity Framework Core

Console Application

---

## 📌 Notes

Use Ctrl + C to exit at any time.

Make sure the database connection is configured before running the app.

Use EF Core Migrations to initialize and update the database.
