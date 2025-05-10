using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Console;

namespace Space_Shooter
{
    internal class MenuBox : Box
    {

        private (int x, int y) Position;
        private int Width;
        private int Height;
        private int SelectedIndex;
        private string Title;

        private (int x, int y) CurrentPos;

        ConsoleColor menu_foreground = ConsoleColor.Black;
        ConsoleColor menu_background = ConsoleColor.DarkGray;

        List<string> MenuBasic;
        Dictionary<string, string> MenuAdvanced;
        public MenuBox(int width, int height, (int x, int y) position, string title) : base(width, height, position, title)
        {
            Position = position;
            Width = width;  
            Height = height;
            Title = title;
            // Defaults
            SelectedIndex = 0;
            MenuBasic = null;
            MenuAdvanced = null;
        }

        public void InitializeMenu(List<string> menu)
        {
            MenuBasic = menu;
        }

        public void InitializeMenu(Dictionary<string,string> menu)
        {
            MenuAdvanced = menu;
        }

        public void RenderMenu(bool wordLength, GridBuffer buffer)
        {
            int count = 0;
            int length;

            // Find out longest string
            if (wordLength)
            {
                length = LongestMenuItem(MenuBasic) + 2;
            }
            else
            {
                length = Width - 2;
            }

            for (int i = 0; i < Height; i++)
            {
                menu_background = ConsoleColor.DarkGray;
                if (i % 2 != 0 && i != 0 && count < MenuBasic.Count())
                {
                    if (count == SelectedIndex)
                        menu_background = ConsoleColor.Red;

                    CurrentPos = (Position.x + 1, Position.y + i);
                    buffer.DrawText(new string(' ', length), CurrentPos, ConsoleColor.White, menu_background);
                    buffer.DrawText((' ' + MenuBasic[count]), CurrentPos, ConsoleColor.White, menu_background);

                    count += 1;
                }
            }

            ResetColor();
        }


        public void RenderMenu(bool wordLength, bool textOnRight, GridBuffer buffer)
        {
            int count = 0;
            int length;

            // Find out longest string
            if (wordLength)
            {
                length = LongestMenuItem(MenuAdvanced) + 2;
            }
            else
            {
                length = Width - 2;
            }


            for (int i = 0; i < Height; i++)
            {

                if (i % 2 != 0 && i != 0 && count < MenuAdvanced.Count())
                {

                    CurrentPos = (Position.x + 1, Position.y + i);
                    if (length + MenuAdvanced.ElementAt(count).Value.Length < Width && wordLength)
                    {
                        if (count == SelectedIndex)
                        {
                            menu_background = ConsoleColor.Red;
                            menu_foreground = ConsoleColor.White;
                        }
                        else
                        {
                            menu_background = ConsoleColor.DarkGray;
                            menu_foreground = ConsoleColor.Black;
                        }

                        buffer.DrawText(' ' + MenuAdvanced.ElementAt(count).Key, CurrentPos, menu_foreground, menu_background);
                        buffer.DrawText(new string(' ', length - MenuAdvanced.ElementAt(count).Key.Length), 
                            (CurrentPos.x + MenuAdvanced.ElementAt(count).Key.Length + 1, CurrentPos.y), menu_foreground, menu_background
                            );

                        if (!textOnRight)
                        {
                            CurrentPos = (Position.x + length + 2, Position.y + i);
                            menu_background = ConsoleColor.Gray;
                            menu_foreground = ConsoleColor.DarkGray;
                            buffer.DrawText("| " + MenuAdvanced.ElementAt(count).Value, CurrentPos, menu_foreground, menu_background);
                        }
                    }
                    else
                        buffer.DrawText(' ' + MenuAdvanced.ElementAt(count).Key, CurrentPos, menu_foreground, menu_background);
                    count += 1;
                }
            }

            if (textOnRight)
            {
                count = 1;
                CurrentPos = (Position.x + length + 3, Position.y + count);
                menu_background = ConsoleColor.Gray;
                menu_foreground = ConsoleColor.DarkGray;
                foreach(KeyValuePair<string,string> kvp in MenuAdvanced)
                {
                    buffer.DrawText(kvp.Value, CurrentPos, menu_foreground, menu_background);
                    count++;
                    CurrentPos = (Position.x + length + 3, Position.y + count);
                }

            }

            ResetColor();
        }
        public void SelectMenu(ConsoleKeyInfo? keyinfo)
        {
            int count = 1;

            if (MenuBasic != null)
            {
                count = MenuBasic.Count;
            }
            else if (MenuAdvanced != null) 
            {
                count = MenuAdvanced.Count;
            }
            else
            {
                return;
            }

            if (keyinfo.Value.Key == ConsoleKey.UpArrow)
                SelectedIndex = (SelectedIndex - 1 + count) % count;
            else if (keyinfo.Value.Key == ConsoleKey.DownArrow)
                SelectedIndex = (SelectedIndex + 1) % count;
        }

        public int GetIndex()
        {
            return SelectedIndex;
        }
    }
}
