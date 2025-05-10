using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    class Player
    {
        public int Score;
        public (int x, int y) Position;
        public bool Win = true;
        public bool IsDying = false;
        public int HP = 10;

        private int DeathTick;

        public List<ConsoleChar> Texture;
        GridBuffer Buffer;
        Textures TextureManager = new Textures();
        Animator Animator;

        public Player(List<ConsoleChar> texture, GridBuffer buffer)
        {
            Texture = texture;
            Score = 0;
            Position = (2, 15);

            Buffer = buffer;
            Animator = new Animator(Buffer);
        }

        public void Move(ConsoleKeyInfo? keyinfo)
        {
            int dx = 0, dy = 0;

            if (keyinfo.Value.Key == ConsoleKey.UpArrow) dy -= 1;
            if (keyinfo.Value.Key == ConsoleKey.DownArrow) dy += 1;
            if (keyinfo.Value.Key == ConsoleKey.LeftArrow) dx -= 1;
            if (keyinfo.Value.Key == ConsoleKey.RightArrow) dx += 1;

            HandleCollision(dx, dy);
        }

        private void HandleCollision(int dx, int dy)
        {
            int bufferX = Position.x + dx;
            int bufferY = Position.y + dy;

            if (bufferX > 1 && bufferX < Buffer.Buffer[bufferY].Length-1)
                Position.x = bufferX;
            if (bufferY > 5 && bufferY < Buffer.Buffer.Count-1)
                Position.y = bufferY;
        }

        public void Die(int current_tick)
        {
            Win = false;
            DeathTick = current_tick;
            IsDying = true;
        }

        public void DeathAnimation(int current_tick)
        {
            int frame;

            frame = Animator.Animation(current_tick, DeathTick, 4, (Position.x-1, Position.y-1),
            TextureManager.ExplosionFrames, ConsoleColor.Yellow);
        }

        public void Attack(List<ConsoleChar> texture, CollisionManager manager)
        {
            if (manager.Bullets.Count < 30)
                manager.Bullets.Add(new Bullet((Position.x + 2, Position.y), texture, manager));
        }
        public void SpecialAttack(List<ConsoleChar> texture, CollisionManager manager)
        {
            if (manager.Bullets.Count < 30)
            {
                manager.Bullets.Add(new Bullet((Position.x + 2, Position.y+1), texture, manager));
                manager.Bullets.Add(new Bullet((Position.x + 2, Position.y), texture, manager));
                manager.Bullets.Add(new Bullet((Position.x + 2, Position.y-1), texture, manager));
            }
        }
    }
}
