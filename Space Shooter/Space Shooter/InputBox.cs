using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

namespace Craft_Batcher
{
    class InputBox : Box
    {
        private (int x, int y) Position;
        private int Width;
        private int Height;
        private string Title;
        public InputBox(int width, int height, (int x, int y) position, string title) : base(width, height, position, title)
        {
            Position = position;
            Width = width;
            Height = height;
            Title = title;
        }

        public string CreateInputBox(string message)
        {
            int i = 1;
            int length;
            string[] message_array = message.Split('\n');

            length = LongestMenuItem(message_array.ToList()) + 1;

            if (message_array.Length > Height - 3)
                throw new ArgumentOutOfRangeException("Message is too big for the box");

            BackgroundColor = ConsoleColor.DarkGray;
            ForegroundColor = ConsoleColor.Black;
            foreach (string line in message_array)
            {
                i+=1;
                SetCursorPosition(Position.x + 1, Position.y + i);
                Write(' ' + line.Trim());
                Write(new string(' ', length - line.Trim().Length));
            }
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
            SetCursorPosition(Position.x + 1, Position.y + i + 2);
            Write(new string(' ', Width - 4));

            CursorVisible = true;
            SetCursorPosition(Position.x + 1, Position.y + i + 2);
            return ReadLine();
        }
    }
}
