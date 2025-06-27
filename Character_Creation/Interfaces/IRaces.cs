using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Character_Creation.Interfaces
{
    public interface IRaces
    {
        int GetHPBonus();

        int GetAttackPowerBonus();

        int GetDefenseBonus();

        int GetSpeedBonus();

        int GetCriticalChanceBonus();

        int GetSpecialMeterBonus();

        int GetPotionBonus();
    }
}