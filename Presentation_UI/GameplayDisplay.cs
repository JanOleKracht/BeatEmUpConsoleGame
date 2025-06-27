using BeatEmUpGame.Character_Creation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Presentation_UI
{
    public class GameplayDisplay
    {
        public void IntroText()
        {
            Console.WriteLine("Welcome to !BEAT YOU UP! This is a Beat em Up Console Game");
            Console.WriteLine("Above You see the Fighters you can Choose from.");
            Console.WriteLine("");
        }

        public void ShowPlayerSelectionPrompt(int playerNumber)
        {
            Console.WriteLine($"Player {playerNumber}, please enter the ID of your fighter:");
            Console.WriteLine("");
        }

        public void ShowSelectedCharacter(Character character)
        {
            Console.WriteLine($"You chose the {character.Race} {character.Name}");
            Console.WriteLine("");
        }

        public void ShowMatchup(Character character1, Character character2)
        {
            Console.WriteLine($"{character1.Name} The {character1.Race} VS {character2.Name} THE {character2.Race}");
            Console.WriteLine("LETS GO! LETS FIGHT!");
            Console.WriteLine("");
        }

        public void ShowQuestionForPlayerInputChoice(Character character, int medikit)
        {
            Console.WriteLine($"{character.Name} its your Turn");
            Console.WriteLine($"Press (1) to Attack: AttackPower: {character.AttackPower}");
            Console.WriteLine($"Press (2) to Use Medikit: Medikits Left{medikit}");
        }

        public void ShowAttackPowerWithFactor(Character character, int gameDice)
        {
            if (gameDice <= 5)
            {
                var attack = character.AttackPower * 0;
                Console.WriteLine($"You rolled a {gameDice}. Your Attack Power is {attack}");
            }
            else if (gameDice <= 10)
            {
                var attack = character.AttackPower * 0.5;
                Console.WriteLine($"You rolled a {gameDice}. Your Attack Power is {attack}");
            }
            else if (gameDice <= 15)
            {
                var attack = character.AttackPower * 1;
                Console.WriteLine($"You rolled a {gameDice}. Your Attack Power is {attack}");
            }
            else if (gameDice <= 20)
            {
                var attack = character.AttackPower * 1.25;
                Console.WriteLine($"You rolled a {gameDice}. Your Attack Power is {attack}");
            }
        }

        public void ShowCriticalChanceAttackPower(Character character, int criticalChanceDice)
        {
            Console.WriteLine($"You rolled a {criticalChanceDice}.");

            if (character.CriticalChance <= criticalChanceDice)
            {
                Console.WriteLine($"This is outside your Critical Attack Chance stat: {character.CriticalChance}");
                Console.WriteLine("Critical Attack FAILED.");
            }
            else
            {
                Console.WriteLine($"This is within your Critical Attack Chance stat: {character.CriticalChance}");
                Console.WriteLine("Critical Attack SUCCESSFUL.");
            }
        }

        public void ShowRoundDescriptionAttack(Character character1, Character character2, double attack, double damage, double lifeLeft)
        {
            Console.WriteLine("");
            Console.WriteLine($"{character1.Name} attacked with {attack}");
            Console.WriteLine($"{character2.Name} defended with {character2.Defense}");
            Console.WriteLine($"{character1.Name} deals {damage} to {character2.Name}");
            Console.WriteLine($"{character2.Name} Life Left: {lifeLeft}");
        }
    }
}