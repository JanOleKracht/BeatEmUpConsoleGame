using BeatEmUpGame.Character_Creation;
using BeatEmUpGame.Engine;
using BeatEmUpGame.Presentation_UI;

namespace BeatEmUpGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CharacterFactory characterFactory = new CharacterFactory();
            DisplayCharacters displayCharacters = new DisplayCharacters(characterFactory);
            DiceService diceService = new DiceService();
            DamageCalculation damageCalculation = new DamageCalculation();
            HealthPointCalculation healthPointCalculation = new HealthPointCalculation();
            GameplayDisplay gameplayDisplay = new GameplayDisplay();
            GameManager gameManager = new GameManager(characterFactory, diceService, damageCalculation, healthPointCalculation, gameplayDisplay);

            // Show Characters plus Intro Text
            displayCharacters.ShowAllCharacters();
            gameplayDisplay.IntroText();

            // Fighter Selection
            // Player 1
            var player1 = 1;
            gameplayDisplay.ShowPlayerSelectionPrompt(player1);
            string input = Console.ReadLine();
            var character1 = gameManager.CharacterSelection(input);
            gameplayDisplay.ShowSelectedCharacter(character1);

            // Player 2
            var player2 = 2;
            gameplayDisplay.ShowPlayerSelectionPrompt(player2);
            input = Console.ReadLine();
            var character2 = gameManager.CharacterSelection(input);
            gameplayDisplay.ShowSelectedCharacter(character2);

            gameplayDisplay.ShowMatchup(character1, character2);

            // Roll Dice to determine what Player starts
            var starter = gameManager.DetermineStarter(character1, character2);

            if (starter == 1)
            {
                Console.WriteLine("PLAYER1");
            }
            else if (starter == 2)
            {
                Console.WriteLine("PLAYER2");
            }

            gameManager.GamePlay(character1, character2, starter);

            // Roll Dice Who starts

            //// !!TEST!!
            //displayCharacters.ShowAllCharacters();

            //var attacker = 1;
            //var defender = 3;

            //displayCharacters.ShowChoosenCharacter(attacker);
            //Console.WriteLine("");
            //displayCharacters.ShowChoosenCharacter(defender);

            //gameManager.Testing(attacker, defender);
        }
    }
}