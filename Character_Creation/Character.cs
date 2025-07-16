using BeatEmUpGame.Character_Creation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Character_Creation
{
    /// <summary>
    /// Represents a game character with various stats and race-based bonuses.
    /// </summary>
    public class Character
    {
        private int id;

        /// <summary>
        /// Unique identifier for the character.
        /// Returns -1 if set with 0 or a negative number.
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value <= 0 ? -1 : value; }
        }

        /// <summary>
        /// The character's display name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The character's race (used for stat modifiers).
        /// </summary>
        public string Race { get; set; }

        private int hp;

        /// <summary>
        /// Character's health points.
        /// Valid values are from 0 to 200. Returns -1 if out of bounds.
        /// </summary>
        public int HP
        {
            get { return hp; }
            set { hp = value < 0 || value > 200 ? -1 : value; }
        }

        private int attackPower;

        /// <summary>
        /// The character's base attack power.
        /// Must be between 0 and 100. Otherwise set to -1.
        /// </summary>
        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = value < 0 || value > 100 ? -1 : value; }
        }

        private int defense;

        /// <summary>
        /// The character's base defense.
        /// Must be between 0 and 50. Otherwise set to -1.
        /// </summary>
        public int Defense
        {
            get { return defense; }
            set { defense = value < 0 || value > 50 ? -1 : value; }
        }

        private int speed; // Currently unused, but defined for extensibility.

        private int criticalChance;

        /// <summary>
        /// Critical hit chance as a percentage.
        /// Must be >= 0, else set to -1.
        /// </summary>
        public int CriticalChance
        {
            get { return criticalChance; }
            set { criticalChance = value < 0 ? -1 : value; }
        }

        private int specialMeter; // Reserved for special abilities (future extension)

        private int mediKit;

        /// <summary>
        /// Number of available medikits for healing.
        /// Must be >= 0, else set to -1.
        /// </summary>
        public int Medikit
        {
            get { return mediKit; }
            set { mediKit = value < 0 ? -1 : value; }
        }

        /// <summary>
        /// Object responsible for applying race-specific stat bonuses.
        /// </summary>
        public IRaces RaceCharacteristic { get; set; }

        /// <summary>
        /// Console text color used when printing this character.
        /// </summary>
        public ConsoleColor ConsoleColor { get; set; }

        /// <summary>
        /// Default constructor (used when building characters manually).
        /// </summary>
        public Character()
        { }

        /// <summary>
        /// Full constructor for creating a character with all stats and race.
        /// </summary>
        public Character(int id, string name, string race, int hp, int attackPower, int defense, int speed, int criticalChance, int specialMeter, int mediKit, ConsoleColor consoleColor)
        {
            this.id = id;
            Name = name;
            Race = race;
            this.hp = hp;
            this.attackPower = attackPower;
            this.defense = defense;
            this.criticalChance = criticalChance;
            this.specialMeter = specialMeter;
            this.speed = speed;
            this.mediKit = mediKit;
            ConsoleColor = consoleColor;
        }

        /// <summary>
        /// Applies bonuses to the character based on their race trait object.
        /// </summary>
        public void ApplayRaceBonus()
        {
            HP += RaceCharacteristic.GetHPBonus();
            AttackPower += RaceCharacteristic.GetAttackPowerBonus();
            Defense += RaceCharacteristic.GetDefenseBonus();
            CriticalChance += RaceCharacteristic.GetCriticalChanceBonus();
            Medikit += RaceCharacteristic.GetMediKitBonus();
        }
    }
}