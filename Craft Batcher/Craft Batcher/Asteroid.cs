using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craft_Batcher
{
    class Asteroid : Entity
    {
        //public override (int x, int y) Position;
        //public override List<ConsoleChar> Texture;

        //private override int VelocityX = -1;
        //public override int NextMove = 0;

        public Asteroid(CollisionManager manager, GridBuffer buffer, (int x, int y) position, List<ConsoleChar> texture) : base(manager, buffer)
        {
            Position = position;
            Texture = texture;
            // Inherited Variables
            MoveRate = 8;
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
