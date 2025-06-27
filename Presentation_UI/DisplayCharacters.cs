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
        private CharacterFactory characterFactory;

        public DisplayCharacters(CharacterFactory characterFactory)
        {
            this.characterFactory = characterFactory;
        }

        public void PrintInfo(Character character)
        {
            if (character is null)
            {
                Console.WriteLine("Error: No character was provided.");
                throw new ArgumentNullException(nameof(character));
            }
            Console.WriteLine($"{character.ID}: {character.GetType().Name}");
            Console.WriteLine($"Name: {character.Name}");
            Console.WriteLine($"Race: {character.Race}");
            Console.WriteLine($"HP: {character.HP}");
            Console.WriteLine($"Attack: {character.AttackPower}");
            Console.WriteLine($"Defense: {character.Defense}");
            Console.WriteLine($"Critical Hit Chance: {character.CriticalChance}");
            Console.WriteLine($"Special Meter: {character.SpecialMeter}");
            Console.WriteLine($"Speed: {character.Speed}");
            Console.WriteLine($"Number of Potions: {character.Potion}");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - ");
        }

        public void ShowAllCharacters()
        {
            foreach (var character in characterFactory.GetAllCharacters())
            {
                PrintInfo(character);
            }
        }

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