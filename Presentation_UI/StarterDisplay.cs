using BeatEmUpGame.Character_Creation;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Presentation_UI
{
    public class StarterDisplay
    {
        /// <summary>
        /// Displays introductory text and instructions at the beginning of the game.
        /// </summary>
        public void IntroText()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== WELCOME TO !BEATemUP! ===");
            Console.ResetColor();

            Console.WriteLine("This is a small Beat 'em Up console game with turn-based RPG elements.\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("At the top of the screen, you can see the available fighters to choose from.");
            Console.WriteLine("Each turn, you can either ATTACK or use a MEDIKIT.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("After making your choice, you’ll roll TWO dice:");
            Console.WriteLine("- The FIRST die determines the STRENGTH of your attack.");
            Console.WriteLine("- The SECOND die determines whether you land a CRITICAL HIT, based on your character’s stats.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Combine luck with strategy to defeat your enemies.");
            Console.WriteLine("Good luck, and fight smart!\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Prompts the player to select a character by ID.
        /// </summary>
        /// <param name="playerNumber">The number of the player (1 or 2).</param>
        public void ShowPlayerSelectionPrompt(int playerNumber)
        {
            Console.WriteLine($"\nPlayer {playerNumber}, please enter the ID of your fighter:");
        }

        /// <summary>
        /// Displays the name and race of the selected character.
        /// </summary>
        /// <param name="character">The selected character.</param>
        public void ShowSelectedCharacter(Character character)
        {
            Console.Write("You chose the ");
            Console.ForegroundColor = character.ConsoleColor;
            Console.Write($"{character.Race} {character.Name}");
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a versus matchup between two characters.
        /// </summary>
        /// <param name="character1">First character.</param>
        /// <param name="character2">Second character.</param>
        public void ShowMatchup(Character character1, Character character2)
        {
            Console.WriteLine();
            Console.ForegroundColor = character1.ConsoleColor;
            Console.Write($"{character1.Name} The {character1.Race} ");
            Console.ResetColor();
            Console.Write("VS ");
            Console.ForegroundColor = character2.ConsoleColor;
            Console.Write($"{character2.Name} THE {character2.Race}");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("LETS GO! LETS FIGHT!\n");
        }

        /// <summary>
        /// Prompts a player to roll the dice.
        /// </summary>
        /// <param name="character">The current character.</param>
        public void ShowRollPrompt(Character character)
        {
            Console.ForegroundColor = character.ConsoleColor;
            Console.Write(character.Name);
            Console.ResetColor();
            Console.WriteLine(" please press 'R' to roll the dice");
        }

        /// <summary>
        /// Displays feedback if player input is invalid when asked to roll.
        /// </summary>
        /// <param name="character">The current character.</param>
        public void ShowInvalidInput(Character character)
        {
            Console.ForegroundColor = character.ConsoleColor;
            Console.Write(character.Name);
            Console.ResetColor();
            Console.WriteLine(" you didn't press 'R'. Please press 'R' to roll the dice");
        }

        /// <summary>
        /// Shows the number rolled by the player.
        /// </summary>
        /// <param name="character">The player rolling the dice.</param>
        /// <param name="rolledNumber">The rolled dice value (1–20).</param>
        public void ShowRolledNumber(Character character, int rolledNumber)
        {
            Console.ForegroundColor = character.ConsoleColor;
            Console.Write(character.Name);
            Console.ResetColor();
            Console.Write(" you rolled a ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(rolledNumber);
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Informs both players when they roll the same number.
        /// </summary>
        public void ShowEqualRollsMessage()
        {
            Console.WriteLine("You rolled the same number. Please roll again.");
        }

        /// <summary>
        /// Announces the player who will start the game.
        /// </summary>
        /// <param name="character">The character who won the roll and starts.</param>
        public void ShowStarterResult(Character character)
        {
            Console.ForegroundColor = character.ConsoleColor;
            Console.Write(character.Name);
            Console.ResetColor();
            Console.WriteLine(" rolled the higher number and attacks first.");
        }
    }
}