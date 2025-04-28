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
using Craft_Batcher;
using System.Runtime.InteropServices;

namespace Space_Shooter
{
    class Render
    {
        GridBuffer Buffer;
        GameObjDraw Drawer;

        private int Width;
        private int Height;

        public Render((int width, int height) size, GridBuffer buffer)
        {
            Buffer = buffer;
            Width = size.width;
            Height = size.height;

            Drawer = new GameObjDraw(Buffer);
            
        }
        public void RenderGame(Player player, CollisionManager manager, int current_tick, int fps, bool gamestarted) // Render whole buffer
        {
            Buffer.Buffer = Buffer.ResetBuffer();

            Drawer.DrawBackground();
            Drawer.DrawEntities(player, manager.Asteroids, manager.Bullets, manager.Planets, current_tick);
            Drawer.DrawOverlay(player.Score, player.HP);
            Buffer.DrawText("Fps: " + fps + "| 60-70 is Normal, that's the Tickrate", (0, 0), ConsoleColor.Black, ConsoleColor.DarkGray);
            if (!gamestarted )
            {
                Drawer.FlickerText("PRESS SPACE TO START THE GAME");
                if (current_tick >= Drawer.NextFrame)
                {
                    Drawer.Index++;
                    Drawer.UpdateFlicker(current_tick);
                }
            }
            if (!player.Win)
            {
                Drawer.FlickerText("YOU LOST");
                if (current_tick >= Drawer.NextFrame)
                {
                    Drawer.Index++;
                    Drawer.UpdateFlicker(current_tick);
                }
            }

            RenderBuffer();

        }

        public void RenderBuffer()
        {
            SetCursorPosition(0, 0);
            ConsoleColor currentForeground = ConsoleColor.White;
            ConsoleColor currentBackground = ConsoleColor.Black;

            for (int y = 0; y < Height; y++)
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

    }
}
