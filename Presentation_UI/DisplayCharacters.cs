using BeatEmUpGame.Character_Creation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Presentation_UI
{
    public class DisplayCharacters
    {
        private readonly CharacterFactory characterFactory;

        /// <summary>
        /// Initializes the display with access to the character factory.
        /// </summary>
        /// <param name="characterFactory">Factory that contains all available characters.</param>
        public DisplayCharacters(CharacterFactory characterFactory)
        {
            this.characterFactory = characterFactory;
        }

        /// <summary>
        /// Prints all information about a character to the console.
        /// </summary>
        /// <param name="character">The character whose info is to be printed.</param>
        public void PrintInfo(Character character)
        {
            if (character is null)
            {
                Console.WriteLine("Error: No character was provided.");
                throw new ArgumentNullException(nameof(character));
            }

            // Show character ID and type with color
            Console.ForegroundColor = character.ConsoleColor;
            Console.WriteLine($"{character.ID}: {character.GetType().Name}");
            Console.ResetColor();

            // Output all character stats
            Console.WriteLine($"Name: {character.Name}");
            Console.WriteLine($"Race: {character.Race}");
            Console.WriteLine($"HP: {character.HP}");
            Console.WriteLine($"Attack: {character.AttackPower}");
            Console.WriteLine($"Defense: {character.Defense}");
            Console.WriteLine($"Critical Hit Chance: {character.CriticalChance}");
            Console.WriteLine($"Number of Potions: {character.Medikit}");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - ");
        }

        /// <summary>
        /// Displays all characters currently available in the game.
        /// </summary>
        public void ShowAllCharacters()
        {
            // Loops over the list from the factory and prints each character
            foreach (var character in characterFactory.GetAllCharacters())
            {
                PrintInfo(character);
            }
        }

        /// <summary>
        /// Displays confirmation of chosen character.
        /// </summary>
        /// <param name="id">The ID of the character to confirm.</param>
        public void ShowChoosenCharacter(int id)
        {
            var character = characterFactory.GetCharacterById(id);

            if (character is null)
            {
                Console.WriteLine("Error: No character was provided.");
                throw new ArgumentNullException(nameof(character));
            }

            Console.WriteLine($"You chose {character.Name} LETS GO!!!!");
        }
    }
}