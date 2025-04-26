using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Craft_Batcher
{
    class GridBuffer
    {
        private int Width, Height;
        public List<ConsoleChar[]> Buffer;

        int overlayWidth = 76;  // Width of the interior
        int overlayHeight = 20;  // Height of the interior
        int startX = 1;          // Overlay starts at column 5
        int startY = 4;          // Top border at row 5

        Random rnd = new Random();

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
                    Buffer[bufferY][bufferX] = new ConsoleChar(img[y].ToCharArray()[x]);
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

        public void DrawOverlay(int score)
        {
            int scoreY = 3;          // Score at row 3

            string scoreText = "Score: " + score;
            string overlayTop = "╔" + new string('═', overlayWidth) + "╗";
            string overlayBottom = "╚" + new string('═', overlayWidth) + "╝";

            // Draw the score at row 3, starting at column 3
            for (int x = 0; x < scoreText.Length; x++)
            {
                int bufferX = 3 + x;
                if (bufferX < Width)  // Prevent out-of-bounds
                    Buffer[scoreY][bufferX] = new ConsoleChar(scoreText[x]);
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
