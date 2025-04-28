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
        Render MenuRenderer;
        Stopwatch Timer;
        CollisionManager Manager;

        Dictionary<string, string> Menu = new Dictionary<string, string>()
        {
            {"Infinite Mode", "Space Shooter"},
            {"Journey Mode", "Programmed in C#"},
            {"Load Game", "Game made by DataCrab"},
            {"Options", "Thanks for Playing!"}

        };

        GridBuffer GameBuffer;
        GridBuffer MenuBuffer;
        Textures GameTextures;
        MenuBox StartMenu;
        Player User;

        (int x, int y) MainGameSize = (79, 25);
        (int x, int y) MenuGameSize = (100, 40);
        private int TickRate = 10;
        private bool GameStarted = false;

        public Game()
        {
            KeyInfo = new ConsoleKeyInfo?();

            GameBuffer = new GridBuffer(MainGameSize.x, MainGameSize.y);
            MenuBuffer = new GridBuffer(MenuGameSize.x, MenuGameSize.y);
            MenuRenderer = new Render((MenuGameSize.x, MenuGameSize.y), MenuBuffer);
            Renderer = new Render((MainGameSize.x, MainGameSize.y), GameBuffer);

            Timer = new Stopwatch();
            GameTextures = new Textures();

            User = new Player(GameTextures.Spaceship, GameBuffer);
            Manager = new CollisionManager(GameBuffer, User);


            Title = "=== Space Shooter ==="; 
            CursorVisible = false;

        }

        public void MainMenu()
        {
            SetWindowSize(MenuGameSize.x, MenuGameSize.y);
            SetBufferSize(MenuGameSize.x, MenuGameSize.y);

            StartMenu = new MenuBox(40, 9, (5, 26), "Menu");

            while (true)
            {

                MenuBuffer.ResetBuffer();

                MenuBuffer.DrawMenuTexture(GameTextures.ArraySpaceShooter, (4, 3), ConsoleColor.White, ConsoleColor.Black);
                MenuBuffer.DrawPlanetGradient(GameTextures.ArrayAstronaut, (58, 1));
                StartMenu.InitBox(false, MenuBuffer);
                StartMenu.InitializeMenu(Menu);
                StartMenu.RenderMenu(true, true, MenuBuffer);

                MenuRenderer.RenderBuffer();

                KeyInfo = ReadKey(true);

                if (KeyInfo != null)
                {
                    if (KeyInfo.Value.Key == ConsoleKey.UpArrow || KeyInfo.Value.Key == ConsoleKey.DownArrow)
                    {
                        StartMenu.SelectMenu(KeyInfo);
                        StartMenu.RenderMenu(true, true, MenuBuffer);
                    }
                    if (KeyInfo.Value.Key == ConsoleKey.Enter)
                    {
                        switch (StartMenu.GetIndex())
                        {
                            case 0:
                                MenuBuffer.ResetBuffer();
                                KeyInfo = null;
                                Timer.Stop();
                                MainGame();
                                break;
                            case 1:
                                WriteLine("Load Game!");
                                ReadLine();
                                break;
                            case 2:
                                WriteLine("Options!");
                                ReadLine();
                                break;
                            case 3:
                                WriteLine("Quit!");
                                ReadLine();
                                break;
                        }
                    }
                }

            }
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
