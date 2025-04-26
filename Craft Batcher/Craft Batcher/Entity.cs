using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craft_Batcher
{
    class Entity
    {
        public bool IsDying = false;
        public int DeathTick = 0;

        public (int x, int y) Position;
        public int VelocityX;
        public int MoveRate;
        public int NextMove;

        public List<ConsoleChar> Texture;

        CollisionManager Manager;
        Textures TextureManager = new Textures();
        GridBuffer Buffer;

        public Entity(CollisionManager manager, GridBuffer buffer)
        {
            Buffer = buffer;
            Manager = manager;
        }
        public virtual void Move(ConsoleKeyInfo? keyinfo)
        {

        }
        
        public void Die(int currentTick)
        {
            IsDying = true;
            DeathTick = currentTick;
        }

        public void DeathAnimation(int currentTick)
        {
            if (!IsDying)
            {
                return;
            }
            else
            {
                int ticksSinceDeath = currentTick - DeathTick;
                int animationFrame = ticksSinceDeath / 4;
                if (animationFrame < 3)
                {
                    Buffer.DrawCharList(TextureManager.ExplosionFrames[animationFrame], (Position.x - 1, Position.y - 1), ConsoleColor.Red, ConsoleColor.Black);
                }
                else
                {
                    Manager.Asteroids.Remove(this);
                }
            }
        }

        public void HandleCollision(int current_tick)
        {
            Manager.HandleCollision(this, current_tick);
        }

        public virtual void UpdateMove(int tick)
        {
            NextMove = tick + MoveRate;
        }
    }
}
