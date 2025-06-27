using BeatEmUpGame.Character_Creation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BeatEmUpGame.Engine
{
    public class DamageCalculation
    {
        private Character character = new Character();

        // Calculates DamageFactor based on the Number of the Dice
        public double DamageFactor(int rolledDice)
        {
            if (rolledDice < 1 || rolledDice > 20)
                throw new ArgumentOutOfRangeException(nameof(rolledDice),
                    "Dice roll must be between 1 and 20.");

            return rolledDice switch
            {
                <= 5 => 0.0,
                <= 10 => 0.5,
                <= 15 => 1.0,
                <= 20 => 1.25
            };
        }

        public double CalculateBaseAttack(int characterAttackPower, double damageFactor)
        {
            var totalAttackPower = characterAttackPower * damageFactor;
            return totalAttackPower;
        }

        // Rage Mode. If Healthpoints go down to 25 or under
        public bool RageMode(double healthPointsDefender)
        {
            if (healthPointsDefender <= 25)
            {
                Console.WriteLine("Rage Mode is active!");
                return true;
            }
            else
            {
                return false;
            }
        }

        // Calculating CritcalChance of Attack.
        public double CalculateCriticalChance(double baseAttack, int criticalChanceStat, int dice, bool rageMode)
        {
            double totalattack = 0;

            // If rageMode is true. Critical Chance stat *2
            if (rageMode)
            {
                criticalChanceStat = criticalChanceStat * 2;
            }
            else if (dice <= criticalChanceStat)
            {
                totalattack = baseAttack * 2;
            }
            else
            {
                totalattack = baseAttack;
            }

            return totalattack;
        }

        // Calculates Base Attack & Critical Chance attack combined
        public double CalculateTotalattack(double totalAttack, int defenseDefender)
        {
            var damageTotal = totalAttack - defenseDefender;
            if (damageTotal < 0)
            {
                damageTotal = 0;
            }

            return damageTotal;
        }

        public double CalculateFinalDamage(Character character1, Character character2, int gameDice, int criticalChanceDice, double healthLeft)
        {
            var damageFactor = DamageFactor(gameDice);
            var baseAttack = CalculateBaseAttack(character1.AttackPower, damageFactor);
            bool rageMode = RageMode(healthLeft);
            var totalAttack = CalculateCriticalChance(baseAttack, character1.CriticalChance, criticalChanceDice, rageMode);
            var finaleDamage = CalculateTotalattack(totalAttack, character2.Defense);

            return finaleDamage;
        }
    }
}