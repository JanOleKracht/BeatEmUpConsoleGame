using BeatEmUpGame.Character_Creation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Character_Creation
{
    /// <summary>
    /// Factory class responsible for creating and storing all available characters.
    /// </summary>
    public class CharacterFactory
    {
        /// <summary>
        /// Internal list holding all character definitions.
        /// </summary>
        private readonly List<Character> characters = new();

        /// <summary>
        /// Initializes the factory and pre-loads all characters.
        /// </summary>
        public CharacterFactory()
        {
            InitializeCharacters();
        }

        /// <summary>
        /// Adds a new character to the list and applies race bonuses.
        /// </summary>
        public void AddCharacterToList(Character character)
        {
            if (character != null)
            {
                character.ApplayRaceBonus();
                characters.Add(character);
            }
        }

        /// <summary>
        /// Returns all available characters as a list.
        /// </summary>
        public List<Character> GetAllCharacters()
        {
            return characters;
        }

        /// <summary>
        /// Returns a character by its ID, or throws if not found.
        /// </summary>
        /// <param name="id">Character ID to search for.</param>
        public Character GetCharacterById(int id)
        {
            foreach (var character in characters)
            {
                if (character.ID == id)
                {
                    return character;
                }
            }

            throw new InvalidOperationException($"No character found with ID {id}.");
        }

        /// <summary>
        /// Initializes and adds all predefined characters to the game.
        /// </summary>
        public void InitializeCharacters()
        {
            // Adds a human fighter
            AddCharacterToList(new Character
            {
                ID = 1,
                Name = "Akira",
                Race = "Human",
                HP = 130,
                AttackPower = 18,
                Defense = 7,
                CriticalChance = 20,
                Medikit = 0,
                RaceCharacteristic = new Human(),
                ConsoleColor = ConsoleColor.Blue,
            });

            // Adds a samurai fighter
            AddCharacterToList(new Character
            {
                ID = 2,
                Name = "Gruumsh",
                Race = "Samurai",
                HP = 115,
                AttackPower = 20,
                Defense = 7,
                CriticalChance = 30,
                Medikit = 1,
                RaceCharacteristic = new Samurai(),
                ConsoleColor = ConsoleColor.Cyan
            });

            // Adds a ninja fighter
            AddCharacterToList(new Character
            {
                ID = 3,
                Name = "Elowen",
                Race = "Ninja",
                HP = 140,
                AttackPower = 15,
                Defense = 12,
                CriticalChance = 25,
                Medikit = 0,
                RaceCharacteristic = new Ninja(),
                ConsoleColor = ConsoleColor.DarkGreen
            });

            // Adds an android fighter
            AddCharacterToList(new Character
            {
                ID = 4,
                Name = "Delta-7",
                Race = "Android",
                HP = 125,
                AttackPower = 17,
                Defense = 10,
                CriticalChance = 30,
                Medikit = 1,
                RaceCharacteristic = new Android(),
                ConsoleColor = ConsoleColor.Yellow
            });
        }
    }
}