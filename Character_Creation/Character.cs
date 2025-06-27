using BeatEmUpGame.Character_Creation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Character_Creation
{
    public class Character
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value <= 0 ? -1 : value; }
        }

        public string Name { get; set; }

        public string Race { get; set; }

        private int hp;

        public int HP
        {
            get { return hp; }
            set { hp = value < 0 || value > 200 ? -1 : value; }
        }

        private int attackPower;

        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = value < 0 || value > 100 ? -1 : value; }
        }

        private int defense;

        public int Defense
        {
            get { return defense; }
            set { defense = value < 0 || value > 50 ? -1 : value; }
        }

        private int speed;

        public int Speed
        {
            get { return speed; }
            set { speed = value < 0 || value > 50 ? -1 : value; }
        }

        private int criticalChance;

        public int CriticalChance
        {
            get { return criticalChance; }
            set { criticalChance = value < 0 ? -1 : value; }
        }

        private int specialMeter;

        public int SpecialMeter
        {
            get { return specialMeter; }
            set { specialMeter = value < 0 ? -1 : value; }
        }

        private int potion;

        public int Potion
        {
            get { return potion; }
            set { potion = value < 0 ? -1 : value; }
        }

        public IRaces RaceCharacteristic { get; set; }

        public Character()
        { }

        public Character(int id, string name, string race, int hp, int attackPower, int defense, int speed, int criticalChance, int specialMeter, int potion)
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
            this.potion = potion;
        }

        public void ApplayRaceBonus()
        {
            HP += RaceCharacteristic.GetHPBonus();
            AttackPower += RaceCharacteristic.GetAttackPowerBonus();
            Defense += RaceCharacteristic.GetDefenseBonus();
            CriticalChance += RaceCharacteristic.GetCriticalChanceBonus();
            SpecialMeter += RaceCharacteristic.GetSpecialMeterBonus();
            Speed += RaceCharacteristic.GetSpeedBonus();
            Potion += RaceCharacteristic.GetPotionBonus();
        }
    }
}