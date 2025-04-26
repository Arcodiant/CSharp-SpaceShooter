using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craft_Batcher
{
    class CollisionManager
    {
        public List<Entity> Asteroids = new List<Entity>();
        public List<Bullet> Bullets = new List<Bullet>();

        private int LastAsteroidSpawnTick = 0;

        GridBuffer Buffer;
        Player Player;
        EnemyManager Enemies;

        public CollisionManager(GridBuffer buffer, Player player)
        {
            Buffer = buffer;
            Player = player;
            Enemies = new EnemyManager(this, Buffer);
        }

        public void Update(int current_tick)
        {
            foreach (var b in Bullets.ToList())
            {
                b.Move(null);
                b.HandleCollision(current_tick);
            }
            foreach (var a in Asteroids.ToList())
            {
                if (a.IsDying)
                    a.DeathAnimation(current_tick);
                else
                    a.HandleCollision(current_tick);
            }
            
            foreach (var a in Asteroids.ToList())
            {
                if (current_tick >= a.NextMove)
                {
                    a.Move(null);
                    a.UpdateMove(current_tick);
                }
            }
            if (current_tick >= Enemies.NextSpawnAsteroid)
            {
                Enemies.SpawnAsteroid();
                Enemies.UpdateSpawnAsteroid(current_tick);
            }
            if (current_tick >= Enemies.NextSpawnComet)
            {
                Enemies.SpawnComet();
                Enemies.UpdateSpawnComet(current_tick);
            }
        }
        
        public void HandleCollisionBullet(Bullet bullet, int current_tick)
        {
            var hitAsteroid = Asteroids.FirstOrDefault(a => a.Position.x == bullet.Position.x+1 && a.Position.y == bullet.Position.y);
            if (hitAsteroid != null && !hitAsteroid.IsDying)
            {
                Bullets.Remove(bullet);
                hitAsteroid.Die(current_tick);

                Buffer.Buffer[bullet.Position.y][bullet.Position.x] = new ConsoleChar(' ');
                Buffer.Buffer[hitAsteroid.Position.y][hitAsteroid.Position.x] = new ConsoleChar(' ');

                Player.Score += 1;
            }
        }

        public void HandleCollision(Entity asteroid, int current_tick)
        {
            var hitBullet = Bullets.FirstOrDefault(a => a.Position.x == asteroid.Position.x + 1 && a.Position.y == asteroid.Position.y);
            if (hitBullet != null && !asteroid.IsDying)
            {
                Bullets.Remove(hitBullet);
                asteroid.Die(current_tick);

                Buffer.Buffer[hitBullet.Position.y][hitBullet.Position.x] = new ConsoleChar(' ');
                Buffer.Buffer[asteroid.Position.y][asteroid.Position.x] = new ConsoleChar(' ');

                Player.Score += 1;
            }
        }
    }
}
