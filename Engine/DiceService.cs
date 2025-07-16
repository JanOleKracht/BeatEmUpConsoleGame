using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Engine
{
    /// <summary>
    /// Provides methods to simulate game-related dice rolls.
    /// </summary>
    public class DiceService
    {
        private readonly Random random = new Random();

        /// <summary>
        /// Rolls a 20-sided dice used for determining attack strength or medikit healing.
        /// </summary>
        public int RollGameDice()
        {
            return random.Next(1, 21); // Returns a value between 1 and 20
        }

        /// <summary>
        /// Rolls a 100-sided dice used for evaluating critical attack chances.
        /// </summary>
        public int RollDiceCriticalChanceDice()
        {
            return random.Next(1, 101); // Returns a value between 1 and 100
        }
    }
}