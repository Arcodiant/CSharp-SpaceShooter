using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    class CollisionManager
    {
        public List<Entity> Asteroids = new List<Entity>();
        public List<Bullet> Bullets = new List<Bullet>();
        public List<Planet> Planets = new List<Planet>();

        GridBuffer Buffer;
        Player Player;
        EnemyManager Enemies;

        private Planet HitPlanet;
        private bool SpawnBlack;

        public CollisionManager(GridBuffer buffer, Player player, bool spawnast, bool spawncom, bool spawnpln, bool spawnblack)
        {
            Buffer = buffer;
            Player = player;
            Enemies = new EnemyManager(this, Buffer, spawnast, spawncom, spawnpln);
            SpawnBlack = spawnblack;
            Enemies.SpawnBlackHole = spawnblack;
            if (spawnpln && spawncom)
            {
                Enemies.AsteroidSpawnRate = 40;
            }
            else if (spawnpln && !spawncom)
            {
                Enemies.AsteroidSpawnRate = 50;
                Enemies.CometSpawnRate = 110;
            }
        }

        public void Update(int current_tick)
        {
            HandleCollisionPlayer(Player, current_tick);
            Planet.TickSinceDeath++;
            foreach (var b in Bullets.ToList())
            {
                if (b.HitPlanet)
                {
                    int bufferX = b.Position.x + 1;

                    if (bufferX > 0 && bufferX < Buffer.Width)
                    {
                        ConsoleChar consoleChar = Buffer.Buffer[b.Position.y][b.Position.x+1];
                        if (consoleChar.Character == ' ' || consoleChar.Character == '.' || consoleChar.Character == '\'')
                            b.Move(null);
                        else
                        {
                            HitPlanet.TakeHit(current_tick,b.Position.x, b.Position.y);
                            Bullets.Remove(b);
                            Buffer.Buffer[b.Position.y][b.Position.x] = new ConsoleChar(' ');
                        }
                    }
                    else
                    {
                        Bullets.Remove(b);
                    }
                }
                else
                {
                    b.Move(null);
                    b.HandleCollision(current_tick);
                }
            }
            foreach (var a in Asteroids.ToList())
            {
                if (a.IsDying)
                    a.DeathAnimation(current_tick);
                else
                    a.HandleCollision(current_tick);

                if (current_tick >= a.NextMove)
                {
                    a.Move(null);
                    a.UpdateMove(current_tick);
                }
            }
            foreach (var p in Planets.ToList())
            {
                if (p.IsDying)
                    p.DeathAnimation(current_tick);
                else
                    p.HandleCollision(current_tick);

                if (current_tick >= p.NextMove)
                {
                    p.Move();
                    p.UpdateMove(current_tick);
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
            if ((!Planet.HasSpawned && Planet.TickSinceDeath >= 1500) || (!Planet.HasSpawned && SpawnBlack))
            {
                Enemies.SpawnPlanet();
                Planet.TickSinceDeath = 0;
                Planet.HasSpawned = true;
            }
        }

        public void HandleCollisionPlayer(Player player, int current_tick)
        {
            var hitAsteroid = Asteroids.FirstOrDefault(a => a.Position.x == player.Position.x + 1 && a.Position.y == player.Position.y);
            var hitPlanet = Planets.FirstOrDefault(
            p => p.Position.x+2 <= player.Position.x && p.Position.x + p.Width >= player.Position.x &&
            p.Position.y <= player.Position.y && p.Position.y + p.Height >= player.Position.y
            );

            if (hitAsteroid != null && !hitAsteroid.IsDying)
            {
                player.Die(current_tick);
            }
            if (hitPlanet != null)
            {
                player.Die(current_tick);
            }
        }
        
        public void HandleCollisionBullet(Bullet bullet, int current_tick)
        {
            var hitAsteroid = Asteroids.FirstOrDefault(a => a.Position.x == bullet.Position.x+1 && a.Position.y == bullet.Position.y);
            var hitPlanet = Planets.FirstOrDefault(p => p.Position.x <= bullet.Position.x && p.Position.x + p.Width >= bullet.Position.x &&
            p.Position.y <= bullet.Position.y && p.Position.y + p.Height >= bullet.Position.y
            );

            if (hitAsteroid != null && !hitAsteroid.IsDying && !bullet.HitPlanet)
            {
                Bullets.Remove(bullet);
                hitAsteroid.Die(current_tick);

                Buffer.Buffer[bullet.Position.y][bullet.Position.x] = new ConsoleChar(' ');
                Buffer.Buffer[hitAsteroid.Position.y][hitAsteroid.Position.x] = new ConsoleChar(' ');

                Player.Score += 1;
            }

            if (hitPlanet != null && !hitPlanet.IsDying && !bullet.HitPlanet)
            {
                HitPlanet = hitPlanet;
                bullet.HitPlanet = true;

                Player.Score += 1;
            }

            if (bullet.Position.x > Buffer.Width)
            {
                Bullets.Remove(bullet);
            }
        }

        public void HandleCollision(Entity asteroid, int current_tick)
        {
            var hitBullet = Bullets.FirstOrDefault(a => a.Position.x == asteroid.Position.x + 1 && a.Position.y == asteroid.Position.y);
            if (hitBullet != null && !asteroid.IsDying && !hitBullet.HitPlanet)
            {
                Bullets.Remove(hitBullet);
                asteroid.Die(current_tick);

                if (hitBullet.Position.x < Buffer.Width)
                    Buffer.Buffer[hitBullet.Position.y][hitBullet.Position.x] = new ConsoleChar(' ');
                Buffer.Buffer[asteroid.Position.y][asteroid.Position.x] = new ConsoleChar(' ');

                Player.Score += 1;
            }
            if (asteroid.Position.x < 0)
            {
                if (Player.HP == 1)
                    Player.Win = false;
                else
                    Player.HP--;
                Asteroids.Remove(asteroid);
            }
        }

        public void HandleCollision(Planet planet, int current_tick)
        {
            var hitBullet = Bullets.FirstOrDefault(a => planet.Position.x <= a.Position.x && planet.Position.x + planet.Width >= a.Position.x
            && planet.Position.y <= a.Position.y && planet.Position.y + planet.Height >= a.Position.y);
            if (hitBullet != null && !planet.IsDying && !hitBullet.HitPlanet)
            {
                HitPlanet = planet;
                hitBullet.HitPlanet = true;

                Player.Score += 1;
            }
            if (planet.Position.x + planet.Width < 0)
            {
                Player.Win = false;
                Planets.Remove(planet);
            }
        }
    }
}
