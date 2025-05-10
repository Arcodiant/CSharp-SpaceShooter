using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Space_Shooter
{
    internal class Box
    {
        private (int x, int y) Position;
        private int Width;
        private int Height;
        private string Title;

        private (int x, int y) CurrentPos;

        public Box(int width, int height, (int x, int y) position, string title)
        {
            Position = position;
            Width = width;
            Height = height;
            Title = title;
        }
        public void InitBox(bool title_bar, GridBuffer buffer)
        {
            for (int i = 0; i < Height; i++)
            {
                CurrentPos = (Position.x, Position.y + i);
                if (title_bar && i == 0)
                {
                    Height += 1;
                    buffer.DrawText(Title, CurrentPos, ConsoleColor.Red, ConsoleColor.White);
                    buffer.DrawText(new string('█', Width - Title.Length), (Position.x+Title.Length, Position.y + i), ConsoleColor.Red, ConsoleColor.Black);
                }
                else
                {
                    buffer.DrawText(new string('█', Width), CurrentPos, ConsoleColor.Gray, ConsoleColor.Black);
                }
                CurrentPos = (Position.x + Width, Position.y + i);
                if (i != 0)
                {
                    buffer.Buffer[CurrentPos.y][CurrentPos.x] = new ConsoleChar('█', ConsoleColor.DarkGray, ConsoleColor.Black);
                }
                else
                {
                    ConsoleColor title_color;
                    if (title_bar)
                        title_color = ConsoleColor.DarkRed;
                    else
                        title_color = ConsoleColor.DarkGray;
                    buffer.Buffer[CurrentPos.y][CurrentPos.x] = new ConsoleChar('▄', title_color, ConsoleColor.Black);
                }
            }
            CurrentPos = (Position.x + 1, Position.y + Height);
            buffer.DrawText(new string('▀', Width), CurrentPos, ConsoleColor.DarkGray, ConsoleColor.Black);

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
