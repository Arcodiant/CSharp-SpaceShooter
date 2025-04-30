using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    class Asteroid : Entity
    {

        public Asteroid(CollisionManager manager, GridBuffer buffer, (int x, int y) position, List<ConsoleChar> texture) : base(manager, buffer)
        {
            Position = position;
            Texture = texture;
            // Inherited Variables
            MoveRate = 6;
            NextMove = 0;
            VelocityX = -1;
        }

        public override void Move(ConsoleKeyInfo? keyinfo)
        {
            Position = (Position.x + VelocityX, Position.y);
        }

        public override void UpdateMove(int tick)
        {
            NextMove = tick + MoveRate;
        }
    }
}
