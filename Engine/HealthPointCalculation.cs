using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Engine
{
    public class HealthPointCalculation
    {
        // Calculation for Health after Attack has benn calculated
        public double HealthCalculation(double damage, double healthPoints)
        {
            var healthLeft = healthPoints - damage;
            return healthLeft;
        }

        //public double HealthCalculation(double attackPower, int defense, double healthPoints)
        //{
        //    var damage = attackPower - defense;
        //    if (damage <= 0)
        //    {
        //        damage = 0;
        //    }
        //    var healthLeft = healthPoints - damage;
        //    return healthLeft;
        //}

        // Restores 50 Health Points when used
        public double UseMedikit(double healthpoints, int rolledDice)
        {
            double newHealthPoints = 0;

            if (rolledDice <= 5)
            {
                newHealthPoints = healthpoints + 20;
            }
            else if (rolledDice <= 10)
            {
                newHealthPoints = healthpoints + 30;
            }
            else if (rolledDice <= 15)
            {
                newHealthPoints = healthpoints + 40;
            }
            else if (rolledDice <= 20)
            {
                newHealthPoints = healthpoints + 50;
            }

            return newHealthPoints;
        }
    }
}