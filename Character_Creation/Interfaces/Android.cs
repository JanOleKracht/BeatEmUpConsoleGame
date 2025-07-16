using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Character_Creation.Interfaces
{
    public class Android : IRaces
    {
        public int GetHPBonus() => 10;

        public int GetAttackPowerBonus() => 10;

        public int GetDefenseBonus() => 10;

        public int GetCriticalChanceBonus() => 10;

        public int GetMediKitBonus() => 1;
    }
}