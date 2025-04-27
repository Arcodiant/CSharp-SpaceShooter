using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Space_Shooter
{
    class Planet
    {
        private int MoveRate = 40;
        private int DeathAnimationRate = 20;
        public int NextAnimationFrame = 0;
        public int NextMove = 0;
        public int HP = 100;
        private int VelocityX = -1;

        public bool IsDying = false;
        public static bool HasSpawned = false;
        public bool IsInAnimation = false;

        private int HitTick;
        public static int TickSinceDeath = 1000;

        public (int x, int y) Position;
        public (int x, int y) RandomPos;
        CollisionManager Manager;
        Textures TextureManager = new Textures();
        GridBuffer Buffer;
        Animator AnimationManager;
        Random rnd = new Random();

        public string[] Texture;
        public int Width;
        public int Height;

        public Planet(CollisionManager manager, GridBuffer buffer, (int x, int y) position)
        {
            Buffer = buffer;
            Manager = manager;
            
            Texture = File.ReadAllText("saturn.txt").Replace("\r", "").Split('\n');
            Height = Texture.Count();
            Width = Texture[0].Length;

            Position = position;

            AnimationManager = new Animator(Buffer);
        }

        public void Move()
        {
            Position = (Position.x + VelocityX, Position.y);
        }

        public void TakeHit(int currentTick,int bulletX, int bulletY)
        {
            if (HP <= 0 && !IsDying)
            {
                Die(currentTick);
            }
            else
            {
                HP--;
                if (!IsInAnimation)
                {
                    RandomPos = (bulletX - 3, bulletY);
                    HitTick = currentTick;
                    HitAnimation(currentTick, RandomPos);
                    IsInAnimation = true;
                }
            }
        }

        public void Die(int currentTick)
        {
            IsDying = true;
        }

        public void HitAnimation(int currentTick, (int x, int y) position)
        {
            int frame = 0;

            frame = AnimationManager.Animation(currentTick, HitTick, 4, 
            position, TextureManager.ExplosionFrames, ConsoleColor.Red);
            if (frame == 3)
                IsInAnimation = false;
        }

        public void DeathAnimation(int currentTick)
        {
            if (!IsDying)
            {
                return;
            }
            else
            {
                Buffer.DrawPlanet(Texture, Position);
                if (currentTick >= NextAnimationFrame)
                {
                    Position.y += 1;
                    Position.x += rnd.Next(-1, 1);

                    if (Position.y > Buffer.Height)
                    {
                        Manager.Planets.Remove(this);
                        HasSpawned = false;
                    }

                    UpdateAnimation(currentTick);
                }
                
            }
        }

        public void HandleCollision(int current_tick)
        {
            Manager.HandleCollision(this, current_tick);
        }

        public void UpdateMove(int tick)
        {
            NextMove = tick + MoveRate;
        }

        public void UpdateAnimation(int tick)
        {
            NextAnimationFrame = tick + DeathAnimationRate;
        }
    }
}
