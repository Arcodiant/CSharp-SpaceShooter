using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Craft_Batcher;
using static System.Console;

namespace Space_Shooter
{
    class Game
    {
        ConsoleKeyInfo? KeyInfo;
        Render Renderer;
        Stopwatch Timer;
        CollisionManager Manager;

        Dictionary<string, string> Menu = new Dictionary<string, string>()
        {
            {"New Game", "PERESTROIKA - Game made by DataCrab."},
            {"Load Game", "Programmed in C#."},
            {"Options", " ───────────────────────────────── "},
            {"Quit", "Thanks for Playing!"}

        };

        GridBuffer GameBuffer;
        Textures GameTextures;
        GameObjDraw Drawer;
        Player User;

        (int x, int y) MainGameSize = (80, 25);
        private int TickRate = 10;
        private bool GameStarted = false;

        public Game()
        {
            KeyInfo = new ConsoleKeyInfo?();

            GameBuffer = new GridBuffer(MainGameSize.x, MainGameSize.y);
            Renderer = new Render((MainGameSize.x, MainGameSize.y), GameBuffer);
            Drawer = new GameObjDraw(GameBuffer);

            Timer = new Stopwatch();
            GameTextures = new Textures();

            User = new Player(GameTextures.Spaceship, GameBuffer);
            Manager = new CollisionManager(GameBuffer, User);


            Title = "=== Space Shooter ==="; 
            CursorVisible = false;
        }

        public void MainGame()
        {
            SetWindowSize(MainGameSize.x, MainGameSize.y);
            SetBufferSize(MainGameSize.x, MainGameSize.y);
            CursorVisible = false;
            int tickcount = 0;
            int frames_rendered = 0;
            int fps = 0;
            DateTime _lastTime = DateTime.Now;

            while (true) // Basic Tick loop
            {
                Timer.Start();

                if (KeyAvailable) // Check for input
                {
                    KeyInfo = ReadKey(true);
                }
                if (Timer.ElapsedMilliseconds >= TickRate) // Update each Tick
                {
                    tickcount++;
                    frames_rendered++;


                    if ((DateTime.Now - _lastTime).TotalSeconds >= 1)
                    {
                        // one second has elapsed 

                        fps = frames_rendered;
                        frames_rendered = 0;
                        _lastTime = DateTime.Now;
                    }

                    if (GameStarted)
                    {
                        Manager.Update(tickcount);
                    }
                    Renderer.RenderGame(User, Manager, tickcount, fps, GameStarted);
                    

                    //if (KeyInfo != null && KeyInfo.Value.Key == ConsoleKey.A) // for input movement
                    if (KeyInfo != null && User.Win)
                    {
                        if (KeyInfo.Value.Key != ConsoleKey.Spacebar && GameStarted)
                            User.Move(KeyInfo);
                        else if (KeyInfo.Value.Key == ConsoleKey.Spacebar)
                        {
                            if (!GameStarted)
                                GameStarted = true;
                            User.Attack(GameTextures.Bullet, Manager);
                        }
                    }


                    KeyInfo = null; // Reset Key input stream
                    Timer.Restart();
                }

            }
        }
    }
}
