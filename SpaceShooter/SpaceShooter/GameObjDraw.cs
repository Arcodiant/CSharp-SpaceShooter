using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Space_Shooter;
using System.IO;

namespace Craft_Batcher
{
    class GameObjDraw
    {
        GridBuffer Buffer;

        private string Background;
        private (int x, int y) Background1pos = (6,6);
        private (int x, int y) Background2pos = (135,6);
        private int Background1_velocity = 1;
        private int Background2_velocity = 1;

        string[] ArrayBackground;

        public int Index = 1;
        private int FrameRate = 50;
        public int NextFrame = 0;

        public GameObjDraw(GridBuffer buffer)
        { 
            Buffer = buffer;

            Background = File.ReadAllText("Stars.txt").Replace("\r", "");
            ArrayBackground = Background.Split('\n');
        }

        public void DrawBackground()
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

        public void DrawOverlay(int score, int hp)
        {
            Buffer.DrawOverlay(score, hp);
        }

        public void DrawEntities(Player player, List<Entity> asteroids, List<Bullet> bullets, List<Planet> planets, int current_tick) // Entity layer
        {
            if (player.Win)
                Buffer.Draw(player.Texture, player.Position);
            else
                player.DeathAnimation(current_tick);
            foreach (var p in planets)
            {
                if (p.IsDying)
                    p.DeathAnimation(current_tick);
                else
                {
                    Buffer.DrawPlanet(p.Texture, p.Position, p.IsSaturn);
                    if (p.IsInAnimation)
                        p.HitAnimation(current_tick, p.RandomPos);
                }
            }
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

        public void FlickerText(string text)
        {
            if (Index % 2 == 0 || Index == 0)
            {
                Buffer.DrawText(text, (Buffer.Width/2-text.Length/2, Buffer.Height/2), ConsoleColor.White, ConsoleColor.Black);
            }
            else
            {
                Buffer.DrawText(new string(' ', text.Length), (Buffer.Width / 2-text.Length/2, Buffer.Height / 2), ConsoleColor.White, ConsoleColor.Black);
            }
        }

        public void UpdateFlicker(int tick)
        {
            NextFrame = tick + FrameRate;
        }
    }
}
