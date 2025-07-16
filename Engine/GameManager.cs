using BeatEmUpGame.Character_Creation;
using BeatEmUpGame.Presentation_UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;

namespace BeatEmUpGame.Engine
{
    /// <summary>
    /// Manages the core gameplay flow, including character selection, turn logic,
    /// damage calculations, and win conditions.
    /// </summary>
    public class GameManager : Character
    {
        private readonly CharacterFactory characterFactory;
        private readonly DiceService diceService;
        private readonly DamageCalculation damageCalculation;
        private readonly HealthPointCalculation healthPointCalculation;
        private readonly StarterDisplay starterDisplay;
        private readonly GameplayDisplay gameplayDisplay;

        /// <summary>
        /// Initializes the game manager with required services and display handlers.
        /// </summary>
        /// <param name="characterFactory">Provides character creation and access.</param>
        /// <param name="diceService">Handles dice-based randomness.</param>
        /// <param name="damageCalculation">Performs all attack and damage calculations.</param>
        /// <param name="healthPointCalculation">Handles HP and healing logic.</param>
        /// <param name="starterDisplay">Manages display of starter-related prompts.</param>
        /// <param name="gameplayDisplay">Handles in-game turn-by-turn feedback and UI.</param>
        public GameManager(CharacterFactory characterFactory, DiceService diceService, DamageCalculation damageCalculation, HealthPointCalculation healthPointCalculation, StarterDisplay starterDisplay, GameplayDisplay gameplayDisplay)
        {
            this.characterFactory = characterFactory;
            this.diceService = diceService;
            this.damageCalculation = damageCalculation;
            this.healthPointCalculation = healthPointCalculation;
            this.starterDisplay = starterDisplay;
            this.gameplayDisplay = gameplayDisplay;
        }

        /// <summary>
        /// Prompts user to select a character based on input.
        /// </summary>
        /// <param name="input">User input (character ID).</param>
        /// <returns>The selected Character object.</returns>
        public Character CharacterSelection(string input)
        {
            List<Character> availableCharacters = characterFactory.GetAllCharacters();
            int listCount = availableCharacters.Count;

            while (true)
            {
                if (int.TryParse(input, out int chosenID))
                {
                    if (chosenID >= 1 && chosenID <= listCount)
                    {
                        var selectedCharacter = availableCharacters.FirstOrDefault(x => x.ID == chosenID);
                        if (selectedCharacter != null)
                            return selectedCharacter;
                    }
                    else
                    {
                        Console.WriteLine($"Please choose an Id from 1 to {listCount}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                Console.Write("Enter character ID: ");
                input = Console.ReadLine();
            }
        }

        /// <summary>
        /// Both players roll a dice to determine who starts first.
        /// </summary>
        /// <param name="character1">Player 1 character.</param>
        /// <param name="character2">Player 2 character.</param>
        /// <returns>1 if player 1 starts, 2 if player 2 starts.</returns>
        public int DetermineStarter(Character character1, Character character2)
        {
            int dicePlayer1 = 0;
            int dicePlayer2 = 0;

            while (dicePlayer1 == dicePlayer2)
            {
                // Player 1 roll
                starterDisplay.ShowRollPrompt(character1);
                while (Console.ReadLine().ToLower() != "r")
                    starterDisplay.ShowInvalidInput(character1);

                dicePlayer1 = diceService.RollGameDice();
                starterDisplay.ShowRolledNumber(character1, dicePlayer1);

                // Player 2 roll
                starterDisplay.ShowRollPrompt(character2);
                while (Console.ReadLine().ToLower() != "r")
                    starterDisplay.ShowInvalidInput(character2);

                dicePlayer2 = diceService.RollGameDice();
                starterDisplay.ShowRolledNumber(character2, dicePlayer2);

                // Compare results
                if (dicePlayer1 > dicePlayer2)
                {
                    starterDisplay.ShowStarterResult(character1);
                    return 1;
                }
                else if (dicePlayer2 > dicePlayer1)
                {
                    starterDisplay.ShowStarterResult(character2);
                    return 2;
                }
                else
                {
                    starterDisplay.ShowEqualRollsMessage();
                }
            }

            return 0;
        }

        /// <summary>
        /// Gets a valid player action (1 = attack, 2 = medikit).
        /// </summary>
        /// <param name="character">Current player's character.</param>
        /// <param name="medikitCount">Remaining medikits.</param>
        /// <returns>Selected action (1 or 2).</returns>
        public int GetValidPlayerActionChoice(Character character, int medikitCount)
        {
            while (true)
            {
                gameplayDisplay.ShowQuestionForPlayerInputChoice(character, medikitCount);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && (choice == 1 || choice == 2))
                {
                    return choice;
                }

                Console.WriteLine("Invalid input. Please enter 1 or 2.\n");
            }
        }

        /// <summary>
        /// Prompts the user to roll the game dice.
        /// </summary>
        /// <param name="character">Character taking the action.</param>
        public void AskToRollGameDice(Character character)
        {
            Console.ForegroundColor = character.ConsoleColor;
            Console.Write(character.Name);
            Console.ResetColor();
            Console.WriteLine(" please Press 'R' to rolle the Game Dice");

            while (Console.ReadLine().ToLower() != "r")
            {
                Console.ForegroundColor = character.ConsoleColor;
                Console.Write(character.Name);
                Console.ResetColor();
                Console.WriteLine(" You didn't Press 'R'. Please Press 'R' to rolle the Dice");
            }
        }

        /// <summary>
        /// Prompts the user to roll the critical chance dice.
        /// </summary>
        /// <param name="character">Character taking the action.</param>
        public void AskToRollCriticalChanceDice(Character character)
        {
            Console.ForegroundColor = character.ConsoleColor;
            Console.Write(character.Name);
            Console.ResetColor();
            Console.WriteLine(" please Press 'R' to rolle the CriticalChance Dice");

            while (Console.ReadLine().ToLower() != "r")
            {
                Console.ForegroundColor = character.ConsoleColor;
                Console.Write(character.Name);
                Console.ResetColor();
                Console.WriteLine(" You didn't Press 'R'. Please Press 'R' to rolle the Dice");
            }
        }

        /// <summary>
        /// Rolls a game dice and calculates base attack.
        /// </summary>
        /// <param name="character">Character performing the attack.</param>
        /// <returns>Tuple of base attack and dice roll result.</returns>
        public (double baseAttack, int gameDice) ChooseAttackBaseAttack(Character character)
        {
            Console.WriteLine("You Chose to Attack");
            var gameDice = diceService.RollGameDice();
            AskToRollGameDice(character);
            var damagefactor = damageCalculation.DamageFactor(gameDice);
            var baseAttack = damageCalculation.CalculateBaseAttack(character.AttackPower, damagefactor);
            gameplayDisplay.ShowAttackPowerWithFactor(character, gameDice);
            return (baseAttack, gameDice);
        }

        /// <summary>
        /// Calculates total damage after applying critical and rage modifiers.
        /// </summary>
        /// <param name="character1">Attacking character.</param>
        /// <param name="character2">Defending character.</param>
        /// <param name="baseAttack">Initial attack value before modifiers.</param>
        /// <param name="lifeLeftPlayer">Remaining HP of the defender before damage.</param>
        /// <param name="rageMode">Whether rage mode is active.</param>
        /// <returns>Total damage dealt.</returns>
        public double ChooseAttackTotalDamage(Character character1, Character character2, double baseAttack, double lifeLeftPlayer, bool rageMode)
        {
            var criticalChanceDice = diceService.RollDiceCriticalChanceDice();
            AskToRollCriticalChanceDice(character1);

            if (rageMode == true)
            {
                gameplayDisplay.ShowIfRageModeIsActive(character1);
            }

            gameplayDisplay.ShowCriticalChanceAttackPower(character1, criticalChanceDice, rageMode);
            var totalattack = damageCalculation.CalculateCriticalChanceAttack(baseAttack, character1.CriticalChance, criticalChanceDice, rageMode);
            var damage = damageCalculation.CalculateTotalDamage(totalattack, character2.Defense);
            gameplayDisplay.ShowRoundDescriptionAttack(character1, character2, totalattack, damage, lifeLeftPlayer, rageMode);
            return damage;
        }

        /// <summary>
        /// Heals the player using a medikit, based on dice roll.
        /// </summary>
        /// <param name="character">Character using the medikit.</param>
        /// <param name="lifeLeftPlayer">Current health points.</param>
        /// <param name="medikitCountPlayer">Remaining medikits.</param>
        /// <returns>Tuple with new health and updated medikit count.</returns>
        public (double lifeLeft, int medikitCountPlayer) ChooseToUseMedikit(Character character, double lifeLeftPlayer, int medikitCountPlayer)
        {
            Console.WriteLine("Please roll the dice to determine the effectiveness of your medkit.");
            AskToRollGameDice(character);
            gameplayDisplay.ShowMedikitUsageBefore(character, lifeLeftPlayer, medikitCountPlayer);
            var gameDiceForMedikit = diceService.RollGameDice();
            lifeLeftPlayer = healthPointCalculation.UseMedikit(lifeLeftPlayer, gameDiceForMedikit);
            medikitCountPlayer--;
            gameplayDisplay.ShowMedikitUsageAfter(character, lifeLeftPlayer, medikitCountPlayer);
            return (lifeLeftPlayer, medikitCountPlayer);
        }

        /// <summary>
        /// Asks the player if they want to play another round.
        /// </summary>
        /// <returns>True if yes, false if no.</returns>
        public bool AskForAnotherRound()
        {
            Console.WriteLine("Do You Want to Play another Round?");
            Console.WriteLine("Yes - Press 1");
            Console.WriteLine("No - Press 2");

            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && (choice == 1 || choice == 2))
                {
                    if (choice == 1)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Thanks for playing. See you next Time!");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1 or 2.\n");
                }
            }
        }

        /// <summary>
        /// Controls full turn-by-turn gameplay, alternating between players.
        /// </summary>
        /// <param name="character1">Player 1's character.</param>
        /// <param name="character2">Player 2's character.</param>
        /// <param name="starter">1 if player 1 starts, 2 if player 2 starts.</param>
        /// <returns>True if user wants to replay, false otherwise.</returns>
        public bool GamePlay(Character character1, Character character2, int starter)
        {
            double lifeLeftPlayer1 = character1.HP;
            int medikitCountPlayer1 = character1.Medikit;
            double lifeLeftPlayer2 = character2.HP;
            int medikitCountPlayer2 = character2.Medikit;

            while (lifeLeftPlayer1 >= 0 && lifeLeftPlayer2 >= 0)
            {
                if (starter == 1)
                {
                    bool validActionPerformed = false;

                    while (!validActionPerformed)
                    {
                        int choice = GetValidPlayerActionChoice(character1, medikitCountPlayer1);

                        if (choice == 1)
                        {
                            bool rageMode = damageCalculation.RageMode(lifeLeftPlayer1);
                            var baseAttack = ChooseAttackBaseAttack(character1);

                            if (baseAttack.baseAttack == 0)
                            {
                                gameplayDisplay.ShowIfAttackIsZero(character1);
                            }
                            else
                            {
                                var damage = ChooseAttackTotalDamage(character1, character2, baseAttack.baseAttack, lifeLeftPlayer2, rageMode);
                                lifeLeftPlayer2 -= damage;
                                gameplayDisplay.ShowLifeLeftAfterDamageDealt(character2, lifeLeftPlayer2);
                            }
                            validActionPerformed = true;
                        }
                        else if (choice == 2)
                        {
                            if (medikitCountPlayer1 <= 0)
                            {
                                Console.WriteLine("Sorry you don't have any Medikits left");
                            }
                            else
                            {
                                (lifeLeftPlayer1, medikitCountPlayer1) = ChooseToUseMedikit(character1, lifeLeftPlayer1, medikitCountPlayer1);
                                validActionPerformed = true;
                            }
                        }
                    }

                    starter = 2;
                }
                else if (starter == 2)
                {
                    bool validActionPerformed = false;

                    while (!validActionPerformed)
                    {
                        int choice = GetValidPlayerActionChoice(character2, medikitCountPlayer2);

                        if (choice == 1)
                        {
                            bool rageMode = damageCalculation.RageMode(lifeLeftPlayer2);
                            var baseAttack = ChooseAttackBaseAttack(character2);

                            if (baseAttack.baseAttack == 0)
                            {
                                gameplayDisplay.ShowIfAttackIsZero(character2);
                            }
                            else
                            {
                                var damage = ChooseAttackTotalDamage(character2, character1, baseAttack.baseAttack, lifeLeftPlayer1, rageMode);
                                lifeLeftPlayer1 -= damage;
                                gameplayDisplay.ShowLifeLeftAfterDamageDealt(character1, lifeLeftPlayer1);
                            }

                            validActionPerformed = true;
                        }
                        else if (choice == 2)
                        {
                            if (medikitCountPlayer2 <= 0)
                            {
                                Console.WriteLine("Sorry you don't have any Medikits left");
                            }
                            else
                            {
                                (lifeLeftPlayer2, medikitCountPlayer2) = ChooseToUseMedikit(character2, lifeLeftPlayer2, medikitCountPlayer2);
                                validActionPerformed = true;
                            }
                        }
                    }

                    starter = 1;
                }
            }

            gameplayDisplay.ShowEndOfGame(character1, character2, lifeLeftPlayer1, lifeLeftPlayer2);

            return AskForAnotherRound();
        }
    }
}