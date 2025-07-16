using BeatEmUpGame.Character_Creation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Presentation_UI
{
    public class GameplayDisplay
    {
        /// <summary>
        /// Prompts the player to choose an action (attack or use medikit).
        /// </summary>
        /// <param name="character">The character whose turn it is.</param>
        /// <param name="medikit">Number of medikits left.</param>
        public void ShowQuestionForPlayerInputChoice(Character character, int medikit)
        {
            Console.WriteLine("");
            Console.ForegroundColor = character.ConsoleColor;
            Console.WriteLine($"{character.Name} it's your Turn");
            Console.ResetColor();
            Console.WriteLine($"Press (1) to Attack: AttackPower: {character.AttackPower}");
            Console.WriteLine($"Press (2) to Use Medikit: Medikits Left: {medikit}");
        }

        /// <summary>
        /// Displays the rolled dice and corresponding attack power factor.
        /// </summary>
        /// <param name="character">Attacking character.</param>
        /// <param name="gameDice">The rolled game dice value (1–20).</param>
        public void ShowAttackPowerWithFactor(Character character, int gameDice)
        {
            double factor;
            ConsoleColor attackColor;

            // Determine factor and color based on dice value
            if (gameDice <= 5)
            {
                factor = 0;
                attackColor = ConsoleColor.Green;
            }
            else if (gameDice <= 10)
            {
                factor = 0.5;
                attackColor = ConsoleColor.DarkGreen;
            }
            else if (gameDice <= 15)
            {
                factor = 1.0;
                attackColor = ConsoleColor.Red;
            }
            else
            {
                factor = 1.25;
                attackColor = ConsoleColor.DarkRed;
            }

            double attack = character.AttackPower * factor;

            Console.Write("You rolled a ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(gameDice);
            Console.ResetColor();

            Console.Write(". Your Attack Power is ");
            Console.ForegroundColor = attackColor;
            Console.WriteLine(attack);
            Console.ResetColor();
        }

        /// <summary>
        /// Indicates that the player's attack power was zero.
        /// </summary>
        /// <param name="character">The attacking character.</param>
        public void ShowIfAttackIsZero(Character character)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" Your turn is Over.");
            Console.ResetColor();
            Console.WriteLine("");
        }

        /// <summary>
        /// Informs the player that Rage Mode is active.
        /// </summary>
        /// <param name="character">The character in rage mode.</param>
        public void ShowIfRageModeIsActive(Character character)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("RAGE MODE ACTIVE!");
            Console.ResetColor();

            Console.WriteLine($"Your Healthpoints are under 35. Your Critical Chance Stat is now *2. It is: {character.CriticalChance * 2}");
        }

        /// <summary>
        /// Displays result of critical chance roll and whether a critical hit occurred.
        /// </summary>
        /// <param name="character">The attacking character.</param>
        /// <param name="criticalChanceDice">Rolled value for critical chance (1–100).</param>
        /// <param name="rageMode">Whether rage mode is active.</param>
        public void ShowCriticalChanceAttackPower(Character character, int criticalChanceDice, bool rageMode)
        {
            Console.Write("You rolled a ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(criticalChanceDice);
            Console.ResetColor();

            var criticalChanceStat = character.CriticalChance;
            if (rageMode)
                criticalChanceStat *= 2;

            // Check if critical hit was successful
            if (criticalChanceDice <= criticalChanceStat)
            {
                Console.Write("This is within your Critical Attack Chance stat: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(criticalChanceStat);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Critical Attack SUCCESSFUL!");
                Console.ResetColor();
            }
            else
            {
                Console.Write("This is outside your Critical Attack Chance stat: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(criticalChanceStat);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Critical Attack FAILED.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Describes the outcome of an attack round including damage dealt and remaining health.
        /// </summary>
        public void ShowRoundDescriptionAttack(Character character1, Character character2, double attack, double damage, double lifeLeft, bool rageMode)
        {
            Console.WriteLine();

            Console.ForegroundColor = character1.ConsoleColor;
            Console.Write($"{character1.Name} ");
            Console.ResetColor();
            Console.Write("attacked with ");
            Console.ForegroundColor = rageMode ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine($"{attack}");
            Console.ResetColor();

            Console.ForegroundColor = character2.ConsoleColor;
            Console.Write($"{character2.Name} ");
            Console.ResetColor();
            Console.WriteLine($"defended with {character2.Defense}");

            Console.ForegroundColor = character1.ConsoleColor;
            Console.Write($"{character1.Name} ");
            Console.ResetColor();
            Console.Write("deals ");
            Console.ForegroundColor = damage > 0 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.Write($"{damage}");
            Console.ResetColor();
            Console.Write(" to ");
            Console.ForegroundColor = character2.ConsoleColor;
            Console.WriteLine($"{character2.Name}");
            Console.ResetColor();

            Console.ForegroundColor = character2.ConsoleColor;
            Console.Write($"{character2.Name}' ");
            Console.ResetColor();
            Console.Write("Life before: ");

            // Display health color-coded
            if (lifeLeft > 70)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (lifeLeft >= 40)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (lifeLeft >= 20)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine($"{lifeLeft}");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the updated health after damage has been dealt.
        /// </summary>
        /// <param name="character">The damaged character.</param>
        /// <param name="lifeLeft">Remaining health points.</param>
        public void ShowLifeLeftAfterDamageDealt(Character character, double lifeLeft)
        {
            Console.ForegroundColor = character.ConsoleColor;
            Console.Write($"{character.Name}'");
            Console.ResetColor();
            Console.Write("Life is now ");

            if (lifeLeft > 70)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (lifeLeft >= 40)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (lifeLeft >= 20)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine($"{lifeLeft}");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays character's health and medikit count before using a medikit.
        /// </summary>
        public void ShowMedikitUsageBefore(Character character, double lifeLeftPlayer, int medikitCountPlayer)
        {
            Console.Write("Life Before: ");
            if (lifeLeftPlayer > 70)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (lifeLeftPlayer > 50)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (lifeLeftPlayer > 20)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine(lifeLeftPlayer);
            Console.ResetColor();

            Console.Write("Medikits Before: ");
            Console.ForegroundColor = medikitCountPlayer > 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(medikitCountPlayer);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays character's health and medikit count after using a medikit.
        /// </summary>
        public void ShowMedikitUsageAfter(Character character, double lifeLeftPlayer, int medikitCountPlayer)
        {
            Console.Write("Life After: ");
            if (lifeLeftPlayer > 70)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (lifeLeftPlayer > 50)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (lifeLeftPlayer > 20)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine(lifeLeftPlayer);
            Console.ResetColor();

            Console.Write("Medikits After: ");
            Console.ForegroundColor = medikitCountPlayer > 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(medikitCountPlayer);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the winner of the game based on remaining HP.
        /// </summary>
        public void ShowEndOfGame(Character character1, Character character2, double lifeLeftPlayer1, double lifeLeftPlayer2)
        {
            if (lifeLeftPlayer1 <= 0)
            {
                Console.ForegroundColor = character2.ConsoleColor;
                Console.Write(character2.Name);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" WON! with ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{lifeLeftPlayer2}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" Health Points Left");
                Console.ResetColor();
            }
            else if (lifeLeftPlayer2 <= 0)
            {
                Console.ForegroundColor = character1.ConsoleColor;
                Console.Write(character1.Name);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" WON! with ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{lifeLeftPlayer1}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" Health Points Left");
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
}