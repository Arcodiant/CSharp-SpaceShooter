using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craft_Batcher
{
    class Player
    {
        public int Score;
        public (int x, int y) Position;

        public List<ConsoleChar> Texture;
        GridBuffer Buffer;

        public Player(List<ConsoleChar> texture, GridBuffer buffer)
        {
            Texture = texture;
            Score = 0;
            Position = (2, 15);

            Buffer = buffer;
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
            // TODO
        }

        public void Attack(List<ConsoleChar> texture, CollisionManager manager)
        {
            manager.Bullets.Add(new Bullet((Position.x + 2, Position.y), texture, manager));
        }
    }
}
