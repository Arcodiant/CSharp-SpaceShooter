using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Space_Shooter
{
    class GridBuffer
    {
        public int Width, Height;
        public List<ConsoleChar[]> Buffer;
        Textures Texture = new Textures();

        int overlayWidth = 76;  // Width of the interior
        int overlayHeight = 20;  // Height of the interior
        int startX = 1;          // Overlay starts at column 5
        int startY = 4;          // Top border at row 5

        public GridBuffer(int width, int height)
        {
            Width = width;
            Height = height;
            Buffer = new List<ConsoleChar[]>(Height);
            for (int  y = 0; y < Height; y++)
            {
                Buffer.Add(new ConsoleChar[Width]);
                for (int x= 0;  x < Width; x++)
                {
                    Buffer[y][x] = new ConsoleChar(' ');
                }
            }
        }

        public List<ConsoleChar[]> ResetBuffer()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Buffer[y][x] = new ConsoleChar(' ');
                }
            }
            return Buffer;
        }

        public void DrawText(string text, (int x, int y) position, ConsoleColor foreground, ConsoleColor background)
        {
            for (int x = 0; x < text.Length; x++)
            {
                int bufferX = position.x + x;
                if (bufferX < 0 || bufferX >= Width || position.y < 0 || position.y >= Height)
                    continue;
                Buffer[position.y][bufferX] = new ConsoleChar(text.ToCharArray()[x],foreground, background);
            }
        }

        public void DrawMenuTexture(string[] img, (int x, int y) position, ConsoleColor foreground, ConsoleColor background)
        {
            for (int y = 0; y < img.Count(); y++)
            {
                int bufferY = position.y + y;
                if (bufferY < 0 || bufferY >= Height)
                    continue;
                for (int x = 0; x < img[y].Length; x++)
                {
                    int bufferX = position.x + x;
                    if (bufferX < 0 || bufferX >= Width)
                        continue;
                    if (img[y][x] != ' ')
                        Buffer[bufferY][bufferX] = new ConsoleChar(img[y][x], foreground, background);
                }
            }
        }

        public void DrawPlanetGradient(string[] img, (int x, int y) position)
        {
            for (int y = 0; y < img.Count(); y++)
            {
                int bufferY = position.y + y;
                if (bufferY < 0 || bufferY >= Height)
                    continue;
                for (int x = 0; x < img[y].Length; x++)
                {
                    int bufferX = position.x + x;
                    if (bufferX < 0 || bufferX >= Width)
                        continue;
                    if (img[y][x] != ' ')
                        if (img[y][x] == ',' || img[y][x] == '-' || img[y][x] == ':' || img[y][x] == '^')
                            Buffer[bufferY][bufferX] = new ConsoleChar(img[y][x], ConsoleColor.DarkGray);
                        else if (img[y][x] == ';' || img[y][x] == '/' || img[y][x] == '~' || img[y][x] == '[')
                            Buffer[bufferY][bufferX] = new ConsoleChar(img[y][x], ConsoleColor.Gray);
                        else if (img[y][x] == 'M' || img[y][x] == 'd' || img[y][x] == 'o' || img[y][x] == '@' || img[y][x] == 'P' || img[y][x] == 'Y')
                            Buffer[bufferY][bufferX] = new ConsoleChar(img[y][x], ConsoleColor.White);
                        else
                            Buffer[bufferY][bufferX] = new ConsoleChar(img[y][x], ConsoleColor.White);
                }       
            }
        }

        public void Draw(string[] img, (int x, int y) position)
        {
            for (int y = 0; y < img.Count(); y++)
            {
                int bufferY = position.y + y;
                if (bufferY < startY || bufferY >= overlayHeight + 5)
                    continue;
                for (int x = 0; x < img[y].Length; x++)
                {
                    int bufferX = position.x + x;
                    if (bufferX < startX || bufferX >= overlayWidth) 
                        continue;
                    if (img[y][x] != ' ')
                        Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x]);
                }
            }
        }

        public void DrawPlanet(string[] img, (int x, int y) position, bool IsSaturn, bool blackhole)
        {
            for (int y = 0; y < img.Count(); y++)
            {
                int bufferY = position.y + y;
                if (bufferY < startY || bufferY >= overlayHeight + 5)
                    continue;
                for (int x = 0; x < img[y].Length; x++)
                {
                    int bufferX = position.x + x;
                    if (bufferX < startX || bufferX >= overlayWidth)
                        continue;
                    if (img[y][x] != ' ')
                    {
                        if (IsSaturn && !blackhole) // for sure is a cleaner way
                        {
                            if (img[y][x] == '▒')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.DarkYellow, ConsoleColor.Yellow);
                            else if (img[y][x] == '▓')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.DarkYellow);
                            else if (img[y][x] == '░')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.Yellow, ConsoleColor.DarkYellow);
                            else if (img[y][x] == '#')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.DarkYellow, ConsoleColor.Yellow);
                            else if (img[y][x] == '@')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.Yellow, ConsoleColor.DarkYellow);
                            else
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.DarkYellow, ConsoleColor.Black);
                        }
                        else if (!IsSaturn && !blackhole)
                        {
                            if (img[y][x] == '▒')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.DarkBlue, ConsoleColor.Blue);
                            else if (img[y][x] == '▓')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.DarkBlue, ConsoleColor.Blue);
                            else if (img[y][x] == '░')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.Cyan, ConsoleColor.DarkCyan);
                            else if (img[y][x] == '#')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.Blue, ConsoleColor.DarkBlue);
                            else if (img[y][x] == '@')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.Cyan, ConsoleColor.DarkCyan);
                            else
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.DarkBlue, ConsoleColor.Black);
                        }
                        else
                        {
                            if (img[y][x] == '█')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.Black, ConsoleColor.Black);
                            else if (img[y][x] == '▓')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.Black, ConsoleColor.White);
                            else if (img[y][x] == '░')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.White, ConsoleColor.Black);
                            else if (img[y][x] == '▒')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.Blue, ConsoleColor.DarkBlue);
                            else if (img[y][x] == '@')
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.Cyan, ConsoleColor.DarkCyan);
                            else
                                Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x], ConsoleColor.Black, ConsoleColor.Black);
                        }
                    }
                }
            }
        }

        public void DrawCharList(List<char[]> img, (int x, int y) position, ConsoleColor foreground, ConsoleColor background)
        {
            for (int y = 0; y < img.Count(); y++)
            {
                int bufferY = position.y + y;

                if (bufferY < startY || bufferY >= overlayHeight + 5) // Check if out of bounds
                    continue;

                for (int x = 0; x < img[y].Length; x++)
                {
                    int bufferX = position.x + x;

                    if (bufferX < startX || bufferX >= overlayWidth) // Check if out of bounds
                        continue;
                    if (img[y][x] != ' ')
                        Buffer[bufferY][bufferX] = new ConsoleChar(img[y][x], foreground, background); // write the characters to the buffer
                }
            }
        }

        public void Draw(List<ConsoleChar> img, (int x, int y) position)
        {
            for (int x = 0; x < img.Count(); x++)
            {
                int bufferX = position.x + x;
                if (bufferX < startX || bufferX >= overlayWidth)
                    continue;
                Buffer[position.y][bufferX] = new ConsoleChar(img[x].Character, img[x].ForegroundColor, img[x].BackgroundColor);
            }
        }

        public void Draw(List<ConsoleChar> img, (int x, int y) position, ConsoleColor foreground, ConsoleColor background)
        {
            for (int x = 0; x < img.Count(); x++)
            {
                int bufferX = position.x + x;
                if (bufferX < startX || bufferX >= overlayWidth)
                    continue;
                Buffer[position.y][bufferX] = new ConsoleChar(img[x].Character, foreground, background);
            }
        }

        public void DrawOverlay(int score, int hp)
        {
            int scoreY = 3;          // Score at row 3
            int index = 10;
            ConsoleColor foreground_color;
            ConsoleColor background_color;

            string scoreText = "Score: " + score;
            string HPText;
            if (index - hp < 10)
                HPText = "HP: " + Texture.HPBar[index - hp];
            else
                HPText = "HP: ▒▒▒▒▒▒▒▒▒▒";
            string overlayTop = "╔" + new string('═', overlayWidth) + "╗";
            string overlayBottom = "╚" + new string('═', overlayWidth) + "╝";

            // Draw the score at row 3, starting at column 3
            for (int x = 0; x < scoreText.Length; x++)
            {
                int bufferX = 3 + x;
                if (bufferX < Width)  // Prevent out-of-bounds
                    Buffer[scoreY][bufferX] = new ConsoleChar(scoreText[x]);
            }

            // Draw the HP Bar at row 3, starting at column 10
            for (int x = 0; x < HPText.Length; x++)
            {
                int bufferX = 15 + x;
                if (x < 4)
                {
                    foreground_color = ConsoleColor.White;
                    background_color = ConsoleColor.Black;
                }
                else
                {
                    foreground_color = ConsoleColor.DarkRed;
                    background_color = ConsoleColor.White;
                }
                if (bufferX < Width)  // Prevent out-of-bounds
                    Buffer[scoreY][bufferX] = new ConsoleChar(HPText[x], foreground_color, background_color);
            }

            // Draw the top border at row 5
            for (int x = 0; x < overlayTop.Length; x++)
            {
                int bufferX = startX + x;
                if (bufferX < Width)
                    Buffer[startY][bufferX] = new ConsoleChar(overlayTop[x]);
            }

            // Draw side borders and clear interior from row 6 to 34
            for (int y = startY + 1; y < startY + overlayHeight; y++)
            {
                if (y < Height)  // Prevent out-of-bounds
                {
                    Buffer[y][startX] = new ConsoleChar('║');              // Left border
                    Buffer[y][startX + overlayWidth + 1] = new ConsoleChar('║'); // Right border
                }
            }

            // Draw the bottom border at row 35
            int bottomY = startY + overlayHeight;  // Row 35
            for (int x = 0; x < overlayBottom.Length; x++)
            {
                int bufferX = startX + x;
                if (bufferX < Width && bottomY < Height)
                    Buffer[bottomY][bufferX] = new ConsoleChar(overlayBottom[x]);
            }
        }
    }
}
