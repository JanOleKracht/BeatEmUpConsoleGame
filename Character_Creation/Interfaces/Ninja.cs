using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Character_Creation.Interfaces
{
    public class Ninja : IRaces
    {
        public int GetHPBonus() => 10;

        public int GetAttackPowerBonus() => 10;

        public int GetDefenseBonus() => 10;

        public int GetSpeedBonus() => 10;

        public int GetCriticalChanceBonus() => 10;

        public int GetSpecialMeterBonus() => 10;

        public int GetPotionBonus() => 1;
    }
}