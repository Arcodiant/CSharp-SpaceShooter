using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    class EnemyManager
    {
        CollisionManager Manager;
        Textures Texture;
        GridBuffer Buffer;

        Random rnd = new Random();

        private int AsteroidSpawnRate = 60;
        public int NextSpawnAsteroid = 0;
        private int CometSpawnRate = 120;
        public int NextSpawnComet = 0;
        private int PlanetSpawnRate = 1000;
        public int NextSpawnPlanet = 0;

        public EnemyManager(CollisionManager manager, GridBuffer buffer)
        {
            Manager = manager;
            Buffer = buffer;
            Texture = new Textures();
        }
        public void SpawnAsteroid()
        {
            Manager.Asteroids.Add(new Asteroid(Manager, Buffer,(78, rnd.Next(6, 23)), Texture.Asteroid));
        }

        public void SpawnComet()
        {
            Manager.Asteroids.Add(new Comet(Manager, Buffer, (78, rnd.Next(6, 23)), Texture.Comet));
        }

        public void SpawnPlanet()
        {
            Manager.Planets.Add(new Planet(Manager, Buffer, (78, rnd.Next(0, 15))));
        }

        public void UpdateSpawnAsteroid(int tick)
        {
            NextSpawnAsteroid = tick + AsteroidSpawnRate;
        }

        public void UpdateSpawnComet(int tick)
        {
            NextSpawnComet = tick + CometSpawnRate;
        }

        public void UpdateSpawnPlanet(int tick)
        {
            NextSpawnPlanet = tick + PlanetSpawnRate;
        }
    }
}
