using BeatEmUpGame.Character_Creation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Character_Creation
{
    public class CharacterFactory
    {
        private readonly List<Character> characters = new();

        public CharacterFactory()
        {
            InitializeCharacters();
        }

        public void AddCharacterToList(Character character)
        {
            if (character != null)
            {
                character.ApplayRaceBonus();
                characters.Add(character);
            }
        }

        public List<Character> GetAllCharacters()
        {
            return characters;
        }

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

        public void InitializeCharacters()
        {
            AddCharacterToList(new Character
            {
                ID = 1,
                Name = "Akira",
                Race = "Human",
                HP = 130,
                AttackPower = 20,
                Defense = 7,
                Speed = 6,
                CriticalChance = 80,
                SpecialMeter = 10,
                Potion = 0,
                RaceCharacteristic = new Human()
            });

            AddCharacterToList(new Character
            {
                ID = 2,
                Name = "Gruumsh",
                Race = "Samurai",
                HP = 115,
                AttackPower = 15,
                Defense = 7,
                Speed = 7,
                CriticalChance = 80,
                SpecialMeter = 10,
                Potion = 1,
                RaceCharacteristic = new Samurai()
            });

            AddCharacterToList(new Character
            {
                ID = 3,
                Name = "Elowen",
                Race = "Ninja",
                HP = 140,
                AttackPower = 23,
                Defense = 12,
                Speed = 4,
                CriticalChance = 20,
                SpecialMeter = 0,
                Potion = 0,
                RaceCharacteristic = new Ninja()
            });

            AddCharacterToList(new Character
            {
                ID = 4,
                Name = "Delta-7",
                Race = "Android",
                HP = 125,
                AttackPower = 17,
                Defense = 4,
                Speed = 5,
                CriticalChance = 20,
                SpecialMeter = 0,
                Potion = 1,
                RaceCharacteristic = new Android()
            });
        }
    }
}