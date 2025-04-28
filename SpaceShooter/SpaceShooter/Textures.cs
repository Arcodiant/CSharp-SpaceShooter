using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using System.IO;

namespace Space_Shooter
{
    class Textures
    {
        public List<ConsoleChar> Spaceship;

        public List<ConsoleChar> Asteroid;

        public List<ConsoleChar> Comet;

        public List<ConsoleChar> Bullet;

        public List<char[]> ExplosionFrame1;

        public List<char[]> ExplosionFrame2;

        public List<char[]> ExplosionFrame3;

        public List<string> HPBar;

        public List<List<char[]>> ExplosionFrames;

        private string Astronaut = File.ReadAllText("astronaut.txt").Replace("\r", "");
        public string[] ArrayAstronaut;
        private string SpaceShooter = File.ReadAllText("spaceshooter.txt").Replace("\r", "");
        public string[] ArraySpaceShooter;

        public Textures()
        {
            ArrayAstronaut = Astronaut.Split('\n');
            ArraySpaceShooter = SpaceShooter.Split('\n');

            Spaceship = new List<ConsoleChar>();
            Spaceship.Add(new ConsoleChar('=', ConsoleColor.Yellow, ConsoleColor.Black));
            Spaceship.Add(new ConsoleChar('>', ConsoleColor.Red, ConsoleColor.Black));

            Bullet = new List<ConsoleChar>();
            Bullet.Add(new ConsoleChar('o', ConsoleColor.Yellow, ConsoleColor.Black));
            
            Asteroid = new List<ConsoleChar>();
            Asteroid.Add(new ConsoleChar('▓', ConsoleColor.DarkGray, ConsoleColor.Black));

            Comet = new List<ConsoleChar>();
            Comet.Add(new ConsoleChar('▓', ConsoleColor.Gray, ConsoleColor.Black));

            ExplosionFrames = new List<List<char[]>>();

            ExplosionFrame1 = new List<char[]>()
            {
                new char[] {' ',' ',' '},
                new char[] {' ','░','.'},
                new char[] {' ',' ',' '},
            };

            ExplosionFrame2 = new List<char[]>()
            {
                new char[] {'.',' ','.'},
                new char[] {'.','*','.'},
                new char[] {' ',' ',' '},
            };

            ExplosionFrame3 = new List<char[]>()
            {
                new char[] {'.',' ','.'},
                new char[] {'.',' ',' '},
                new char[] {' ',' ',' '},
            };
            
            ExplosionFrames.Add(ExplosionFrame1);
            ExplosionFrames.Add(ExplosionFrame2);
            ExplosionFrames.Add(ExplosionFrame3);

            HPBar = new List<string>()
            {
                "▓▓▓▓▓▓▓▓▓▓",
                "▓▓▓▓▓▓▓▓▓▒",
                "▓▓▓▓▓▓▓▓▒▒",
                "▓▓▓▓▓▓▓▒▒▒",
                "▓▓▓▓▓▓▒▒▒▒",
                "▓▓▓▓▓▒▒▒▒▒",
                "▓▓▓▓▒▒▒▒▒▒",
                "▓▓▓▒▒▒▒▒▒▒",
                "▓▓▒▒▒▒▒▒▒▒",
                "▓▒▒▒▒▒▒▒▒▒",
            };
        }
    }
}
