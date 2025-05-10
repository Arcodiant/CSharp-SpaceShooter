using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    struct ConsoleChar
    {
        public char Character;
        public ConsoleColor ForegroundColor;
        public ConsoleColor BackgroundColor;

        public ConsoleChar(char character, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Character = character;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
    }
}
