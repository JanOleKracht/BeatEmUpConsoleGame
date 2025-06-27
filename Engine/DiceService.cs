using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatEmUpGame.Engine
{
    public class DiceService
    {
        public int RollGameDice()
        {
            Random random = new Random();
            return random.Next(1, 21);
        }

        public int RollDiceCriticalChanceDice()
        {
            Random random = new Random();
            return (random.Next(1, 101));
        }
    }
}