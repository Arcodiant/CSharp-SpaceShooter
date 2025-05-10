using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
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
        Animator AnimationManager;

        SoundManager SoundManager = new SoundManager();

        public Entity(CollisionManager manager, GridBuffer buffer)
        {
            Buffer = buffer;
            Manager = manager;

            AnimationManager = new Animator(Buffer);
        }
        public virtual void Move(ConsoleKeyInfo? keyinfo)
        {

        }
        
        public virtual void Die(int currentTick)
        {
            IsDying = true;
            DeathTick = currentTick;
        }

        public virtual void DeathAnimation(int currentTick)
        {
            if (!IsDying)
            {
                return;
            }
            else
            {
                int frame = 0;

                frame = AnimationManager.Animation(currentTick, DeathTick, 3, (Position.x - 1, Position.y - 1),
                TextureManager.ExplosionFrames, ConsoleColor.Red);

                if (frame == 3)
                    Manager.Asteroids.Remove(this);
            }
        }

        public virtual void HandleCollision(int current_tick)
        {
            Manager.HandleCollision(this, current_tick);
        }

        public virtual void UpdateMove(int tick)
        {
            NextMove = tick + MoveRate;
        }
    }
}
