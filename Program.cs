using BeatEmUpGame.Character_Creation;
using BeatEmUpGame.Engine;
using BeatEmUpGame.Presentation_UI;

namespace BeatEmUpGame
{
    /// <summary>
    /// Entry point of the application. Sets up game services, handles user flow,
    /// and manages the main game loop.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main method which initiates the game, manages character selection,
        /// and loops the gameplay until players decide to exit.
        /// </summary>
        private static void Main(string[] args)
        {
            // Setup all required components and services
            CharacterFactory characterFactory = new CharacterFactory();
            DisplayCharacters displayCharacters = new DisplayCharacters(characterFactory);
            DiceService diceService = new DiceService();
            DamageCalculation damageCalculation = new DamageCalculation();
            HealthPointCalculation healthPointCalculation = new HealthPointCalculation();
            StarterDisplay starterDisplay = new StarterDisplay();
            GameplayDisplay gameplayDisplay = new GameplayDisplay();

            // Instantiate GameManager with all dependencies
            GameManager gameManager = new GameManager(
                characterFactory,
                diceService,
                damageCalculation,
                healthPointCalculation,
                starterDisplay,
                gameplayDisplay
            );

            bool playAgain = true;

            // Main game loop
            while (playAgain)
            {
                // Show all available characters to both players
                displayCharacters.ShowAllCharacters();

                // Display introductory explanation of game rules
                starterDisplay.IntroText();

                // === Fighter Selection ===

                // Player 1 chooses character
                var player1 = 1;
                starterDisplay.ShowPlayerSelectionPrompt(player1);
                string input = Console.ReadLine();
                var character1 = gameManager.CharacterSelection(input);
                starterDisplay.ShowSelectedCharacter(character1);

                // Player 2 chooses character
                var player2 = 2;
                starterDisplay.ShowPlayerSelectionPrompt(player2);
                input = Console.ReadLine();
                var character2 = gameManager.CharacterSelection(input);
                starterDisplay.ShowSelectedCharacter(character2);

                // Display VS screen
                starterDisplay.ShowMatchup(character1, character2);

                // Roll dice to determine who starts
                var starter = gameManager.DetermineStarter(character1, character2);

                // Run the game loop
                playAgain = gameManager.GamePlay(character1, character2, starter);
            }
        }
    }
}