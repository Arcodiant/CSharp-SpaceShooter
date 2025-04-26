using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Console;

namespace Craft_Batcher
{
    internal class MenuBox : Box
    {

        private (int x, int y) Position;
        private int Width;
        private int Height;
        private int SelectedIndex;
        private string Title;

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

        public void RenderMenu(bool wordLength)
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
                BackgroundColor = ConsoleColor.DarkGray;
                if (i % 2 != 0 && i != 0 && count < MenuBasic.Count())
                {
                    if (count == SelectedIndex)
                        BackgroundColor = ConsoleColor.Red;

                    SetCursorPosition(Position.x + 1, Position.y + i);
                    Write(new string(' ', length));

                    SetCursorPosition(Position.x + 1, Position.y + i);
                    Write(' ' + MenuBasic[count]);

                    count += 1;
                }
            }

            ResetColor();
        }


        public void RenderMenu(bool wordLength, bool textOnRight)
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

                    SetCursorPosition(Position.x + 1, Position.y + i);
                    if (length + MenuAdvanced.ElementAt(count).Value.Length < Width && wordLength)
                    {
                        if (count == SelectedIndex)
                        {
                            BackgroundColor = ConsoleColor.Red;
                            ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            BackgroundColor = ConsoleColor.DarkGray;
                            ForegroundColor = ConsoleColor.Black;
                        }

                        Write(' ' + MenuAdvanced.ElementAt(count).Key);
                        Write(new string(' ', length - MenuAdvanced.ElementAt(count).Key.Length));

                        if (!textOnRight)
                        {
                            SetCursorPosition(Position.x + length + 2, Position.y + i);
                            BackgroundColor = ConsoleColor.Gray;
                            ForegroundColor = ConsoleColor.DarkGray;
                            Write("| " + MenuAdvanced.ElementAt(count).Value);
                        }
                    }
                    else
                        Write(' ' + MenuAdvanced.ElementAt(count).Key);
                    count += 1;
                }
            }

            if (textOnRight)
            {
                count = 1;
                SetCursorPosition(Position.x + length + 3, Position.y + count);
                BackgroundColor = ConsoleColor.Gray;
                ForegroundColor = ConsoleColor.DarkGray;
                foreach(KeyValuePair<string,string> kvp in MenuAdvanced)
                {
                    Write(kvp.Value);
                    count++;
                    SetCursorPosition(Position.x + length + 3, Position.y + count);
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
