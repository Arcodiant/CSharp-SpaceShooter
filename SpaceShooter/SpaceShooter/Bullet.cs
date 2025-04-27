using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    class Bullet
    {
        private int VelocityX = 1;
        public (int x, int y) Position;
        public List<ConsoleChar> Texture;
        public bool HitPlanet = false;

        CollisionManager Manager;

        public Bullet((int x, int y) position, List<ConsoleChar> texture, CollisionManager manager)
        {
            Position = position;
            Texture = texture;
            Manager = manager;
        }

        public void Move(ConsoleKeyInfo? keyinfo)
        {
            Position = (Position.x + VelocityX, Position.y);
        }

        public void HandleCollision(int current_tick)
        {
            Manager.HandleCollisionBullet(this, current_tick);
        }
    }
}
