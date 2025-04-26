using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Craft_Batcher
{
    internal class Box
    {
        private (int x, int y) Position;
        private int Width;
        private int Height;
        private string Title;

        public Box(int width, int height, (int x, int y) position, string title)
        {
            Position = position;
            Width = width;
            Height = height;
            Title = title;
        }
        public void InitBox(bool title_bar)
        {
            for (int i = 0; i < Height; i++)
            {
                SetCursorPosition(Position.x, Position.y + i);
                if (title_bar && i == 0)
                {
                    Height += 1;
                    BackgroundColor = ConsoleColor.Red;
                    ForegroundColor = ConsoleColor.White;
                    Write(Title);
                    ResetColor();
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(new string('█', Width - Title.Length));
                }
                else
                {
                    ForegroundColor = ConsoleColor.Gray;
                    WriteLine(new string('█', Width));
                }
                SetCursorPosition(Position.x + Width, Position.y + i);
                if (i != 0)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    Write('█');
                }
                else
                {
                    if (title_bar)
                        ForegroundColor = ConsoleColor.DarkRed;
                    else
                        ForegroundColor = ConsoleColor.DarkGray;
                    Write('▄');
                }
            }
            SetCursorPosition(Position.x + 1, Position.y + Height);
            WriteLine(new string('▀', Width));

            // TODO: Read from file and make into options that get selected with arrow buttons + enter
            ResetColor();
        }

        public int LongestMenuItem(List<string> menu)
        {
            int length = 0;
            foreach (string item in menu)
            {
                if (item.Length > length)
                    length = item.Length;
            }
            return length;
        }

        public int LongestMenuItem(Dictionary<string,string> menu)
        {
            int length = 0;
            foreach (KeyValuePair<string,string> kvp in menu)
            {
                if (kvp.Key.Length > length)
                    length = kvp.Key.Length;
            }
            return length;
        }
    }
}
