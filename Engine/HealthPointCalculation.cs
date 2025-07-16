using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Engine
{
    /// <summary>
    /// Provides methods to handle character health changes,
    /// such as applying damage and healing from medikits.
    /// </summary>
    public class HealthPointCalculation
    {
        /// <summary>
        /// Calculates remaining health after taking damage.
        /// </summary>
        /// <param name="damage">Amount of damage dealt.</param>
        /// <param name="healthPoints">Current health before damage.</param>
        /// <returns>New health after damage is subtracted.</returns>
        public static double HealthCalculation(double damage, double healthPoints)
        {
            return healthPoints - damage;
        }

        /// <summary>
        /// Restores health based on medikit dice result.
        /// </summary>
        /// <param name="healthpoints">Current health before healing.</param>
        /// <param name="rolledDice">Dice result from medikit roll (1–20).</param>
        /// <returns>Updated health after applying healing bonus.</returns>
        public double UseMedikit(double healthpoints, int rolledDice)
        {
            int healAmount = rolledDice switch
            {
                <= 5 => 20,
                <= 10 => 30,
                <= 15 => 40,
                _ => 50
            };

            Console.WriteLine($"You rolled a {rolledDice}. Plus {healAmount} Healthpoints");
            return healthpoints + healAmount;
        }
    }
}