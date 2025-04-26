using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using System.Data;
using Microsoft.Win32;
using System.Runtime.CompilerServices;

namespace Craft_Batcher
{
    class Render
    {
        GridBuffer Buffer;
        string[] ArrayBackground;

        private int Width;
        private int Height;

        private string Background;
        private (int x, int y) Background1pos;
        private (int x, int y) Background2pos;
        private int Background1_velocity;
        private int Background2_velocity;   

        public Render((int width, int height) size, GridBuffer buffer)
        {
            Buffer = buffer;
            Width = size.width;
            Height = size.height;

            Background = File.ReadAllText("Stars.txt").Replace("\r", "");
            ArrayBackground = Background.Split('\n');

            Background1pos = (6, 6);
            Background2pos = (135, 6);
            Background1_velocity = 1;
            Background2_velocity = 1;
            
        }
        public void RenderGame(Player player, CollisionManager manager, int current_tick) // Render whole buffer
        {
            Buffer.Buffer = Buffer.ResetBuffer();

            RenderBackground();
            RenderEntities(player, manager.Asteroids, manager.Bullets, current_tick);
            RenderOverlay(player.Score);
            

            SetCursorPosition(0, 0);
            ConsoleColor currentForeground = ConsoleColor.White;
            ConsoleColor currentBackground = ConsoleColor.Black;

            for (int y=0; y < Height; y++)
            {
                StringBuilder row = new StringBuilder();
                for (int x = 0; x < Width; x++)
                {
                    ConsoleChar c = Buffer.Buffer[y][x];

                    if (x == Width - 1 || c.ForegroundColor != currentForeground || c.BackgroundColor != currentBackground)
                    {
                        if (row.Length > 0)
                        {
                            ForegroundColor = currentForeground;
                            BackgroundColor = currentBackground;
                            Write(row.ToString());
                            row.Clear();
                        }
                        currentBackground = c.BackgroundColor;
                        currentForeground = c.ForegroundColor;
                    }
                    row.Append(c.Character);
                }

                if (row.Length > 0)
                {
                    ForegroundColor = currentForeground;
                    BackgroundColor = currentBackground;
                    Write(row.ToString());
                }

                if (y < Height - 1)
                    Write('\n');
            }
        }

        private void RenderBackground()
        {
            int backgroundWidth = ArrayBackground[0].Length;

            Buffer.Draw(ArrayBackground, Background1pos);
            Buffer.Draw(ArrayBackground, Background2pos);

            Background1pos = (Background1pos.x - Background1_velocity, Background1pos.y);
            Background2pos = (Background2pos.x - Background2_velocity, Background2pos.y);

            if (Background1pos.x <= (-backgroundWidth))
                Background1pos.x = backgroundWidth;
            if (Background2pos.x <= (-backgroundWidth))
                Background2pos.x = backgroundWidth;
        }

        private void RenderOverlay(int score)
        {
            Buffer.DrawOverlay(score);
        }

        public void RenderEntities(Player player, List<Entity> asteroids, List<Bullet> bullets, int current_tick) // Entity layer
        {
            Buffer.Draw(player.Texture, player.Position);
            foreach (var a in asteroids)
            {
                if (a.IsDying)
                    a.DeathAnimation(current_tick);
                else
                    Buffer.Draw(a.Texture, a.Position);
            }
            foreach (var b in bullets)
            {
                Buffer.Draw(b.Texture, b.Position);
            }
        }
    }
}
