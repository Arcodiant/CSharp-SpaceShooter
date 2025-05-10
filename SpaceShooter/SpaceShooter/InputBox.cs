using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

namespace Space_Shooter
{
    class InputBox : Box
    {
        private (int x, int y) Position;
        private int Width;
        private int Height;
        private string Title;

        private (int x, int y) CurrentPos;

        ConsoleColor box_foreground = ConsoleColor.Black;
        ConsoleColor box_background = ConsoleColor.DarkGray;
        public InputBox(int width, int height, (int x, int y) position, string title) : base(width, height, position, title)
        {
            Position = position;
            Width = width;
            Height = height;
            Title = title;
        }

        public string CreateInputBox(string message, GridBuffer buffer)
        {
            int i = 1;
            int length;
            string[] message_array = message.Split('\n');

            length = LongestMenuItem(message_array.ToList()) + 1;

            if (message_array.Length > Height - 3)
                throw new ArgumentOutOfRangeException("Message is too big for the box");

            box_background = ConsoleColor.DarkGray;
            box_foreground = ConsoleColor.Black;
            foreach (string line in message_array)
            {
                i+=1;
                CurrentPos = (Position.x + 1, Position.y + i);
                buffer.DrawText(' ' + line.Trim(), CurrentPos, box_foreground, box_background);
                buffer.DrawText(new string(' ', length - line.Trim().Length), (CurrentPos.x + line.Trim().Length, CurrentPos.y),
                    box_foreground, box_background);
            }
            box_background = ConsoleColor.Black;
            box_foreground = ConsoleColor.White;
            CurrentPos = (Position.x + 1, Position.y + i + 2);
            buffer.DrawText(new string(' ', Width - 4), CurrentPos, box_foreground, box_background);

            CursorVisible = true;
            SetCursorPosition(Position.x + 1, Position.y + i + 2);
            return ReadLine();
        }
    }
}
