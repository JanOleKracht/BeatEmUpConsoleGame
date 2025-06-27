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
    public class GameManager : Character
    {
        private readonly CharacterFactory characterFactory;
        private readonly DiceService diceService;
        private readonly DamageCalculation damageCalculation;
        private readonly HealthPointCalculation healthPointCalculation;
        private readonly GameplayDisplay gameplayDisplay;

        public GameManager(CharacterFactory characterFactory, DiceService diceService, DamageCalculation damageCalculation, HealthPointCalculation healthPointCalculation, GameplayDisplay gameplayDisplay)
        {
            this.characterFactory = characterFactory;
            this.diceService = diceService;
            this.damageCalculation = damageCalculation;
            this.healthPointCalculation = healthPointCalculation;
            this.gameplayDisplay = gameplayDisplay;
        }

        //
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
                        {
                            return selectedCharacter;
                        }
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

        public int DetermineStarter(Character character1, Character character2)
        {
            int dicePlayer1 = 0;
            int dicePlayer2 = 0;

            while (dicePlayer1 == dicePlayer2)
            {
                //Player 1
                Console.WriteLine($"{character1.Name} please Press 'R' to rolle the Dice");

                while (Console.ReadLine().ToLower() != "r")
                {
                    Console.WriteLine($"{character1.Name} You didn't Press 'R'. Please Press 'R' to rolle the Dice");
                }

                dicePlayer1 = diceService.RollGameDice();
                Console.WriteLine($"{character1.Name} ýou rolled a {dicePlayer1}");

                Console.WriteLine($"{character2.Name} please Press 'R' to rolle the Dice");

                // Player 2
                while (Console.ReadLine().ToLower() != "r")
                {
                    Console.WriteLine($"{character2.Name} You didn't Press 'R'. Please Press 'R' to rolle the Dice");
                }

                dicePlayer2 = diceService.RollGameDice();
                Console.WriteLine($"{character2.Name} ýou rolled a {dicePlayer2}");

                if (dicePlayer1 > dicePlayer2)
                {
                    Console.WriteLine($"{character1.Name} rolled the higher Number and Attacks first.");
                    return 1;
                }
                else if (dicePlayer1 < dicePlayer2)
                {
                    Console.WriteLine($"{character2.Name} rolled the higher Number and Attacks first.");
                    return 2;
                }
                else
                {
                    Console.WriteLine("You rolled same Number. Please roll again");
                }
            }
            return 0;
        }

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

                Console.WriteLine("Invalid input. Please enter 1 or 2.");
                Console.WriteLine("");
            }
        }

        public void AskToRollGameDice(Character character)
        {
            // Roll Dice
            Console.WriteLine($"{character.Name} please Press 'R' to rolle the Game Dice");

            while (Console.ReadLine().ToLower() != "r")
            {
                Console.WriteLine($"{character.Name} You didn't Press 'R'. Please Press 'R' to rolle the Dice");
            }
        }

        public void AskToRollCriticalChanceDice(Character character)
        {
            // Roll Dice
            Console.WriteLine($"{character.Name} please Press 'R' to rolle the CriticalChance Dice");

            while (Console.ReadLine().ToLower() != "r")
            {
                Console.WriteLine($"{character.Name} You didn't Press 'R'. Please Press 'R' to rolle the Dice");
            }
        }

        public void GamePlay(Character character1, Character character2, int starter)
        {
            double lifeLeftPlayer1 = character1.HP;
            int medikitCountPlayer1 = character1.Potion;
            double lifeLeftPlayer2 = character2.HP;
            int medikitCountPlayer2 = character1.Potion;

            if (starter == 1)
            {
                int choice = GetValidPlayerActionChoice(character1, medikitCountPlayer1);
                if (choice == 1)
                {
                    var gameDice = diceService.RollGameDice();
                    AskToRollGameDice(character1);
                    gameplayDisplay.ShowAttackPowerWithFactor(character1, gameDice);
                    var criticalChanceDice = diceService.RollDiceCriticalChanceDice();
                    AskToRollCriticalChanceDice(character1);
                    gameplayDisplay.ShowCriticalChanceAttackPower(character1, criticalChanceDice);
                    var attack = damageCalculation.CalculateFinalDamage(character1, character2, gameDice, criticalChanceDice, lifeLeftPlayer1);
                    Console.WriteLine($"Attack: {attack}");
                    var damage = healthPointCalculation.HealthCalculation(attack, lifeLeftPlayer2);
                    Console.WriteLine($"Damage: {damage}");
                    //lifeLeftPlayer2 = lifeLeftPlayer2 - damage;
                    //gameplayDisplay.ShowRoundDescriptionAttack(character1, character2, attack, damage, lifeLeftPlayer2);
                }
                else if (choice == 2)
                {
                }

                starter = 2;
            }
            else if (starter == 2)
            {
                int choice = GetValidPlayerActionChoice(character2, medikitCountPlayer2);
                if (choice == 1)
                {
                    var gameDice = diceService.RollGameDice();
                    AskToRollGameDice(character2);
                    gameplayDisplay.ShowAttackPowerWithFactor(character2, gameDice);
                    var criticalChanceDice = diceService.RollDiceCriticalChanceDice();
                    AskToRollCriticalChanceDice(character2);
                    gameplayDisplay.ShowCriticalChanceAttackPower(character2, criticalChanceDice);

                    var attack = damageCalculation.CalculateFinalDamage(character2, character1, gameDice, criticalChanceDice, lifeLeftPlayer2);
                    Console.WriteLine($"Attack: {attack}");
                    var damage = healthPointCalculation.HealthCalculation(attack, lifeLeftPlayer1);
                    Console.WriteLine($"Damage: {damage}");
                    //lifeLeftPlayer1 = lifeLeftPlayer1 - damage;
                    //gameplayDisplay.ShowRoundDescriptionAttack(character2, character1, attack, damage, lifeLeftPlayer1);
                }
                else if (choice == 2)
                {
                }

                starter = 1;
            }
        }
    }

    //    public void Testing(int attackerId, int defenderId)
    //    {
    //        var attacker = characterFactory.GetCharacterById(attackerId);
    //        var defender = characterFactory.GetCharacterById(defenderId);
    //        var attackPower = attacker.AttackPower;
    //        var defPower = defender.Defense;
    //        var health = defender.HP;

    //        Console.WriteLine($"Attacker Power:{attackPower}");
    //        Console.WriteLine($"Defender Defense:{defPower}");
    //        Console.WriteLine($"Defender Health:{health}");

    //        var rolledDiceGame = diceService.RollGameDice();
    //        Console.WriteLine($"Rolled Dice: {rolledDiceGame}");

    //        var damageFac = damageCalculation.DamageFactor(rolledDiceGame);
    //        Console.WriteLine($"Damage Factor: {damageFac}");

    //        var attackWithFactor = damageCalculation.CalculateBaseAttack(attackPower, damageFac);
    //        Console.WriteLine($"Attack: {attackWithFactor}");

    //        var rolledDiceCrital = diceService.RollDiceCriticalChanceDice();
    //        Console.WriteLine($"Rolled Dice Crital: {rolledDiceCrital}");

    //        bool rageMode = damageCalculation.RageMode(defender.HP);
    //        var totalAttack = damageCalculation.CalculateCriticalChance(attackWithFactor, attacker.CriticalChance, rolledDiceCrital, rageMode);
    //        Console.WriteLine($"Total Attack: {totalAttack}");

    //        var damageTotal = damageCalculation.CalculateTotalattack(totalAttack, defender.Defense);
    //        Console.WriteLine($"Damage: {damageTotal}");

    //        var healthLeft = healthPointCalculation.HealthCalculation(totalAttack, defPower, health);
    //        Console.WriteLine($"Health Left {healthLeft}");

    //        var healthPotion = healthPointCalculation.UseMedikit(healthLeft, rolledDiceGame);
    //        Console.WriteLine($"Health after Potion: {healthPotion}");
    //    }
    //}
}