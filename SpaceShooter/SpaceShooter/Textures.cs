using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Space_Shooter
{
    class Textures
    {
        public string IntroAstronaut = @"

██████████████████████████████████▓▓▓▓▓▒▒▓▓▓▓▓█████████████████████████████████
████████████████████████████▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░░░░░▒▒▓███████████████████████████
█████████████████████████▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▒▒░░▒▒░░░░▒▓█████████████████████████
████████████████████████▒░▒▒▒▒▓▓▓▓▓▓▓▒▓▓▒▒▓▓▓▓▓▓▓▒▒▒▒▒▓████████████████████████
██████████████████████▓░░░▒▒▒▒▓▓▓▓▓▓██████████▓▓▓▓▓▓▓▒▒▒███████████████████████
█████████████████████▒░░▒▓████▓▓▓▒▒▒▒▓▓▓▓▓▓▓▓████████▓▒▒▒██████████████████████
████████████████████▒░░▒▓▓▒▒▒▒▓▓▓████████████▓▓▓▓▓▓▓███▓▒▒█████████████████████
███████████████████▒▒▓█▒▒▓██████████████████████████▓▓▓██▓▓████████████████████
███████████████████░▓▓▒███████████████████████████████████▓▓███████████████████
██████████████████▓▓███████▓▒▓█████████████████████████████▓███████████████████
█████████████████▒▓███████░░▒███████████████████████████████▓▓▓████████████████
████████████████▓▓███████▒░░▓████████████████████████████████▓█████████████████
████████████████▓▒▓█████▓░░▒█████████████████████████████████▓▓████████████████
████████████████▓▒▒█████▒░░▓██████████████████████████████████▓████████████████
████████████████▒▓▓███▓█▒░░▓███████████████████████████████████████████████████
████████████████▒▓▓█████▓▒▒▓███████████████████████████████████████████████████
████████████████▓▓█▓███████████████████████████████████████████████████████████
████████████████████▓▓█████████████████████████████████████████████████████████
█████████████████████▓█████████████████████████████████████████████████████████
████████████████████▒░▒██████████████████████████████████▓█████████████████████
██████████████████▓▒▓▒░▓▓▒▒▒▓██████████████████████████████████████████████████
█████████████████▓▒█▓░░░▓▓▒▒▓███████████████████████▓▓█████████████████████████
████████████████▓▒██▓▒▒▒▓▓▓▓▓▓█████████▓▓▓▓▓▓████████████▓█████████████████████
███████████████▓▓█▓░▒▓▒░▒░░▒▓███▓▓██▓▓▓▓██████████████████▓████████████████████
██████████████▓▒▒░░░░▒▓▓▒▒▒▒▒▓▓▓██▓███████████████████████▓▒▒▓█████████████████
███████████▓▒░░░░░░░░░░▓▓▒▓▓▒▒▓▓▓▓▓▓█████████████████████▓▓▒▒▒▒▒▓██████████████
█████████▒░░░░▒▒▒▒▒▒░▒▒░▒▓█▓▒▓██▓▓▓▓▓▓███████████████████▓▓▓▒▒▒▒▒▒▓████████████
███████▒░░░░░░░▒▓▓▓▒▒░█▒▒▒▓▓███████████████████████████████▓▒▓▒▒▒▒▒▒▓██████████
█████▒░░░░░░░░░▒▓▓▒▓▒░▓▓▒▓▓▒▒▓▓███████████████████████████▓▓▓▒▒▓█▓▓▓▓▓▓████████
████▒░░░░▒▒░░░░▒▓██▓▓▓▓█▓▒▓▓▒▒▓▓▓▓██▓█████████▓▓█▓▓█████████▓▒▓▓▓▓█▓▒▓▓████████
███▓▒░░▒▓▒░░░▒░░▓███▓▓▓▓▓▒▓▓▓▒▓▓▓▒▒▓▓▓▓▓▓▓████▓██▓▓██████████▓▓▓██▓▓▓▓█████████
███▓░░▓▓▓▒▒▒▓▓▒░▓▓▓▓█▓▓██▓▓███████▓████▒▓▓▓▓▓██▓▒▒▒▓▓█████▓▓▓▓▓▓▓███▓▓█▓▓▒█████
███▒▒▒▓██▓░▓▒░░▒▒▓███▓▓▓▓▓▒██▓██▓▓▓▓▒▒▒▒▒▓▓▒▒▒▒▒▒▒▒▒▒█████▓▓▓██▓▓▓████▓▓▓▒▒▓███
██▒▒▒▒▓███▒▒▒▓████████▓▓▓▓▒▓████████████▓█▓▓▓▓▓▓▓▓▓▓▓████▓▓█████▓████▓▓▓▒▒▒▒▓██
█▓▒▒▒▓█████▒▒▓█████████▓▓▓▒▓█████████████████████████████████▓▓███████▓▓▓▓▓▓▓██
█▓▓▓▒█▓▓███▓▓▓█████████▓▓█▓▓████████████████████████████████████████████▓▓▒▓▓▓█
▒▓▒▒▒▓▒▒████▓▓█████████████▓███████████████████████████████████████████████████
▓█▓▓▓▒▓██████▓▓████████████▓██▓▓██████████████████████████████████████████████▓";

        public string MenuTitle = @"
 ██▓███  ▓█████  ██▀███  ▓█████   ██████ ▄▄▄█████▓ ██▀███   ▒█████   ██▓ ██ ▄█▀▄▄▄
▓██░  ██▒▓█   ▀ ▓██ ▒ ██▒▓█   ▀ ▒██    ▒ ▓  ██▒ ▓▒▓██ ▒ ██▒▒██▒  ██▒▓██▒ ██▄█▒▒████▄
▓██░ ██▓▒▒███   ▓██ ░▄█ ▒▒███   ░ ▓██▄   ▒ ▓██░ ▒░▓██ ░▄█ ▒▒██░  ██▒▒██▒▓███▄░▒██  ▀█▄
▒██▄█▓▒ ▒▒▓█  ▄ ▒██▀▀█▄  ▒▓█  ▄   ▒   ██▒░ ▓██▓ ░ ▒██▀▀█▄  ▒██   ██░░██░▓██ █▄░██▄▄▄▄██ 
▒██▒ ░  ░░▒████▒░██▓ ▒██▒░▒████▒▒██████▒▒  ▒██▒ ░ ░██▓ ▒██▒░ ████▓▒░░██░▒██▒ █▄▓█   ▓██▒
▒▓▒░ ░  ░░░ ▒░ ░░ ▒▓ ░▒▓░░░ ▒░ ░▒ ▒▓▒ ▒ ░  ▒ ░░   ░ ▒▓ ░▒▓░░ ▒░▒░▒░ ░▓  ▒ ▒▒ ▓▒▒▒   ▓▒█░
░▒ ░      ░ ░  ░  ░▒ ░ ▒░ ░ ░  ░░ ░▒  ░ ░    ░      ░▒ ░ ▒░  ░ ▒ ▒░  ▒ ░░ ░▒ ▒░ ▒   ▒▒ ░";

        public string CCCP_Flag = @"                           ▓██▓                   
                             ▓██▓                 
                   ████████▓  ▓███▓               
                 ▓███████▓     ▓███▓              
               ▓███████▓        ▓███▓             
             ▓█████████▓         ████▓            
             ▓█████▓▓████▓       ▓███▓            
               ▓██▓   █████▓     ▓████            
                       ▓█████▓   █████            
                         ▓█████▓▓████▓            
                 ▓██▓      ▓█████████             
               ▓████████▓▓▓█████████              
             ▓████▓ █████████████████▓            
          ▓█████      ▓▓███████▓▓██████▓          
         ▓█████                   ▓█████▓         
         ▓███▓                      ▓██▓          ";

        public string Saturn = @"                      ▒▒
                    ▒▒ ▒▒
        ▄▄▄▄▄▄▄▄▄▒▒▒ ▒▒▒
     ▄▄▄▓▓░▓▓▓▓░#██▒▒▒
    ▐▒▒▒▓▓▓▓░░░░#▒▒▒█
   ▐▒▒▓▓▓░░░░###▒▒##░▌
  ▐▒▒▓▓░░░▓▓##▒▒▒#░░░▓▌
  ▐▓▓▓░░░▓▓▓▒▒▒##░░░▓▓▌
  ▐▓▓░░░#▓▒▒▒###░░░▓▓▓▌
   ▐▓░░#▒▒▒░░▒░░░▓▓▓▓▌
   ▒▐░░▒▒░░░▒▒▒▒▓▓▓░▌
  ▒▒ ▒▒▒░░▓▒▒▒▓▓▓▀▀▀
 ▒ ▒▒▒  ▀▀▀▀▀▀▀▀▀
▒▒▒▒
▒▒";

        public List<ConsoleChar> Spaceship;

        public List<ConsoleChar> Asteroid;

        public List<ConsoleChar> Comet;

        public List<ConsoleChar> Bullet;

        public List<char[]> ExplosionFrame1;

        public List<char[]> ExplosionFrame2;

        public List<char[]> ExplosionFrame3;

        public List<string> HPBar;

        public List<List<char[]>> ExplosionFrames;

        public Textures()
        {
            Spaceship = new List<ConsoleChar>();
            Spaceship.Add(new ConsoleChar('#', ConsoleColor.Yellow, ConsoleColor.Black));
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
                new char[] {'.',' ','.'},
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

        public void Draw((int x, int y) position, string texture, ConsoleColor background, ConsoleColor foreground)
        {
            int i = 0;

            foreach (string line in texture.Split('\n'))
            {
                BackgroundColor = background;
                ForegroundColor = foreground;

                SetCursorPosition(position.x, position.y + i);
                Write(line);
                i++;
            }
            ResetColor();
        }

        public void Draw((int x, int y) position, string texture, ConsoleColor background, ConsoleColor foreground, 
            ConsoleColor shadowForeground, ConsoleColor shadowBackground)
        {
            int i = 0;
            int width = 1;

            foreach (string line in texture.Split('\n'))
            {
                width = line.Length;
                BackgroundColor = background;
                ForegroundColor = foreground;


                SetCursorPosition(position.x, position.y + i);
                WriteLine(line);

                i++;

                SetCursorPosition(position.x + width-1, position.y + i);
                if (i != 0)
                {
                    BackgroundColor = shadowBackground;
                    ForegroundColor = shadowForeground;
                    Write('█');
                }
                else
                {
                    BackgroundColor = shadowBackground;
                    ForegroundColor = shadowForeground;
                    Write('▄');
                }
            }

            SetCursorPosition(position.x + 1, position.y + i);
            WriteLine(new string('▀', width));
            ResetColor();
        }
    }
}
