using BeatEmUpGame.Character_Creation;
using BeatEmUpGame.Presentation_UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BeatEmUpGame.Engine
{
    /// <summary>
    /// Contains logic for calculating damage, rage mode, and critical hits.
    /// </summary>
    public class DamageCalculation
    {
        /// <summary>
        /// Returns a damage factor multiplier based on the dice roll.
        /// </summary>
        /// <param name="rolledDice">A number between 1 and 20</param>
        /// <returns>Damage multiplier (0.0 to 1.25)</returns>
        public double DamageFactor(int rolledDice)
        {
            if (rolledDice < 1 || rolledDice > 20)
                throw new ArgumentOutOfRangeException(nameof(rolledDice), "Dice roll must be between 1 and 20.");

            return rolledDice switch
            {
                <= 5 => 0.0,
                <= 10 => 0.5,
                <= 15 => 1.0,
                <= 20 => 1.25
            };
        }

        /// <summary>
        /// Calculates base attack value from attack power and damage factor.
        /// </summary>
        /// <param name="characterAttackPower">Character's base attack stat</param>
        /// <param name="damageFactor">Multiplier returned by DamageFactor()</param>
        public double CalculateBaseAttack(int characterAttackPower, double damageFactor)
        {
            return characterAttackPower * damageFactor;
        }

        /// <summary>
        /// Returns true if rage mode is active (HP <= 35).
        /// </summary>
        /// <param name="healthPointsDefender">The player's remaining HP</param>
        public bool RageMode(double healthPointsDefender)
        {
            return healthPointsDefender <= 35;
        }

        /// <summary>
        /// Calculates total attack power, factoring in critical chance and rage mode.
        /// </summary>
        /// <param name="baseAttack">Attack value before critical calculation</param>
        /// <param name="criticalChanceStat">Critical chance %</param>
        /// <param name="dice">Rolled value for critical chance</param>
        /// <param name="rageMode">Whether rage mode is active</param>
        public double CalculateCriticalChanceAttack(double baseAttack, int criticalChanceStat, int dice, bool rageMode)
        {
            // Rage mode doubles the critical chance stat
            if (rageMode)
            {
                criticalChanceStat *= 2;
            }

            // If the rolled dice is within the critical chance, attack is doubled
            return dice <= criticalChanceStat ? baseAttack * 2 : baseAttack;
        }

        /// <summary>
        /// Calculates total damage after subtracting the defender's defense.
        /// </summary>
        /// <param name="totalAttack">Attack power after critical chance</param>
        /// <param name="defenseDefender">Defense stat of the opponent</param>
        public double CalculateTotalDamage(double totalAttack, int defenseDefender)
        {
            double damageTotal = totalAttack - defenseDefender;

            // Damage can't go below 0
            return Math.Max(damageTotal, 0);
        }

        /// <summary>
        /// Performs a complete damage calculation: base → critical → total.
        /// </summary>
        /// <param name="character1">Attacker</param>
        /// <param name="character2">Defender</param>
        /// <param name="gameDice">Game dice roll (1–20)</param>
        /// <param name="criticalChanceDice">Critical hit roll (1–100)</param>
        /// <param name="healthLeft">Attacker's remaining HP (used for rage mode)</param>
        /// <returns>Final damage to apply</returns>
        public double CalculateFinalDamage(Character character1, Character character2, int gameDice, int criticalChanceDice, double healthLeft)
        {
            var damageFactor = DamageFactor(gameDice);
            var baseAttack = CalculateBaseAttack(character1.AttackPower, damageFactor);
            bool rageMode = RageMode(healthLeft);
            var totalAttack = CalculateCriticalChanceAttack(baseAttack, character1.CriticalChance, criticalChanceDice, rageMode);
            return CalculateTotalDamage(totalAttack, character2.Defense);
        }
    }
}