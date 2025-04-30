using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
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

        GameObjDraw Drawer;

        SoundManager SoundManager;

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
            Drawer = new GameObjDraw(GameBuffer);

            SoundManager = new SoundManager();

            User = new Player(GameTextures.Spaceship, GameBuffer);


            Title = "=== Space Shooter ==="; 
            CursorVisible = false;

        }

        public void MainMenu()
        {
            SetWindowSize(MenuGameSize.x, MenuGameSize.y);
            SetBufferSize(MenuGameSize.x, MenuGameSize.y);

            StartMenu = new MenuBox(40, 9, (5, 26), "Menu");
            SoundManager.BackGround.PlayLooping();

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
                    if (KeyInfo.Value.Key == ConsoleKey.Enter || KeyInfo.Value.Key == ConsoleKey.Spacebar)
                    {
                        switch (StartMenu.GetIndex())
                        {
                            case 0:
                                // Infinite Mode
                                MenuBuffer.ResetBuffer();
                                KeyInfo = null;
                                Timer.Stop();
                                MainGame(false, false, false, false, null);
                                break;
                            case 1:
                                //Journey Mode
                                MenuBuffer.ResetBuffer();
                                KeyInfo = null;
                                Timer.Stop();
                                Journey();
                                break;
                            case 2:
                                // Load Game from Journey
                                ReadLine();
                                break;
                            case 3:
                                // Options
                                ReadLine();
                                break;
                        }
                    }
                }

            }
        }

        public void MainGame(bool spawnast, bool spawncom, bool spawnpln, bool spawnblack, int? score)
        {
            SetWindowSize(MainGameSize.x, MainGameSize.y);
            SetBufferSize(MainGameSize.x, MainGameSize.y);
            CursorVisible = false;
            SoundManager.BackGroundGame.PlayLooping();

            Manager = new CollisionManager(GameBuffer, User, spawnast, spawncom, spawnpln, spawnblack);

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

                    Renderer.RenderGame(User, Manager, tickcount, fps, GameStarted, score);

                    
                    

                    //if (KeyInfo != null && KeyInfo.Value.Key == ConsoleKey.A) // for input movement
                    if (KeyInfo != null && User.Win)
                    {
                        if (User.Score > score && score != null && KeyInfo.Value.Key == ConsoleKey.Enter)
                            return;
                        if (KeyInfo.Value.Key != ConsoleKey.Spacebar && GameStarted)
                            User.Move(KeyInfo);
                        else if (KeyInfo.Value.Key == ConsoleKey.Spacebar)
                        {
                            if (!GameStarted)
                                GameStarted = true;
                            User.Attack(GameTextures.Bullet, Manager);
                        }
                    }
                    else if (KeyInfo != null && !User.Win)
                    {
                        if (KeyInfo.Value.Key == ConsoleKey.Enter)
                        {
                            KeyInfo = null; // Reset Key input stream
                            Timer.Stop();
                            User.Win = true;
                            User.HP = 10;
                            User.Score = 0;
                            User.IsDying = false;
                            User.Position = (2, 15);
                            Planet.HasSpawned = false;


                            SetWindowSize(MenuGameSize.x, MenuGameSize.y);
                            SetBufferSize(MenuGameSize.x, MenuGameSize.y);
                            SoundManager.BackGround.PlayLooping();
                            return;
                        }
                    }


                    KeyInfo = null; // Reset Key input stream
                    Timer.Restart();
                }

            }
        }

        public void Journey()
        {
            int tickcount = 0;
            TextManager part_one = new TextManager();
            part_one.AddText("Location: Outer East-Milkdromeda", (5, 31), ConsoleColor.White, ConsoleColor.Black, 4);
            part_one.AddText("Welcome to your Training, you're tasked with defending the Inhabitated Milkdromeda Galaxy", (5, 33), ConsoleColor.White, ConsoleColor.Black, 4);
            part_one.AddText("from foreign bodies.", (5, 34), ConsoleColor.White, ConsoleColor.Black, 4);
            part_one.AddText("A report came in from the Minister of Defense of a massive gravitational anomaly", (5, 35), ConsoleColor.White, ConsoleColor.Black, 4);
            part_one.AddText("the consequences of which we don't know of yet. Stay wary Comrade.", (5, 36), ConsoleColor.White, ConsoleColor.Black, 4);

            while (true)
            {
                Timer.Start();

                if (KeyAvailable) // Check for input
                {
                    KeyInfo = ReadKey(true);
                }
                if (Timer.ElapsedMilliseconds >= TickRate)
                {
                    
                    MenuBuffer.ResetBuffer();

                    MenuBuffer.DrawMenuTexture(GameTextures.ArrayAsteroid, (0, 0), ConsoleColor.Gray, ConsoleColor.Black);
                    part_one.Update(tickcount, MenuBuffer);


                    MenuBuffer.DrawText("Press ENTER to continue...", (5, 38), ConsoleColor.White, ConsoleColor.Black);
                    MenuRenderer.RenderBuffer();

                    tickcount++;

                    if (KeyInfo != null)
                    {
                        if (KeyInfo.Value.Key == ConsoleKey.Enter)
                            break;
                    }


                    KeyInfo = null; // Reset Key input stream
                    Timer.Restart();
                }
            }
            Timer.Stop();
            KeyInfo = null;
            MainGame(false, true, true, false, 150);

            SoundManager.BackGround.PlayLooping();

            SetWindowSize(MenuGameSize.x, MenuGameSize.y);
            SetBufferSize(MenuGameSize.x, MenuGameSize.y);

            tickcount = 0;
            TextManager part_two = new TextManager();
            part_two.AddText("Location: Central Outskirts of Milkdromeda", (5, 31), ConsoleColor.White, ConsoleColor.Black, 4);
            part_two.AddText("Welcome back Comrade, we've received reports of large amounts of Comets headed ", (5, 33), ConsoleColor.White, ConsoleColor.Black, 4);
            part_two.AddText("towards the Orion-Cygnus Arm.", (5, 34), ConsoleColor.White, ConsoleColor.Black, 4);
            part_two.AddText("They're faster than Asteroids, make sure to eliminate them first.", (5, 35), ConsoleColor.Red, ConsoleColor.Black, 4);
            part_two.AddText("We have no updates on the gravitational anomaly.", (5, 36), ConsoleColor.White, ConsoleColor.Black, 4);

            while (true)
            {
                Timer.Start();

                if (KeyAvailable) // Check for input
                {
                    KeyInfo = ReadKey(true);
                }
                if (Timer.ElapsedMilliseconds >= TickRate)
                {

                    MenuBuffer.ResetBuffer();

                    MenuBuffer.DrawMenuTexture(GameTextures.ArrayComet, (0, 0), ConsoleColor.Gray, ConsoleColor.Black);
                    part_two.Update(tickcount, MenuBuffer);

                    MenuBuffer.DrawText("Press ENTER to continue...", (5, 38), ConsoleColor.White, ConsoleColor.Black);
                    MenuRenderer.RenderBuffer();

                    tickcount++;

                    if (KeyInfo != null)
                    {
                        if (KeyInfo.Value.Key == ConsoleKey.Enter)
                            break;
                    }


                    KeyInfo = null; // Reset Key input stream
                    Timer.Restart();
                }
            }
            Timer.Stop();
            KeyInfo = null;
            MainGame(false, false, true, false, 350);

            SoundManager.BackGround.PlayLooping();

            SetWindowSize(MenuGameSize.x, MenuGameSize.y);
            SetBufferSize(MenuGameSize.x, MenuGameSize.y);

            tickcount = 0;
            TextManager part_three = new TextManager();
            part_three.AddText("Location: Serpenitus Cascade", (5, 31), ConsoleColor.White, ConsoleColor.Black, 4);
            part_three.AddText("Comrade, the massive gravitational anomaly has caused a huge amount of planets to be ", (5, 33), ConsoleColor.White, ConsoleColor.Black, 4);
            part_three.AddText("sent towards the Habitable zones of Milkdromeda.", (5, 34), ConsoleColor.White, ConsoleColor.Black, 4);
            part_three.AddText("They're generally slow, but very hard to get them off the trajectory.", (5, 35), ConsoleColor.Red, ConsoleColor.Black, 4);
            part_three.AddText("They also cover the Asteroids and Comets behind them.", (5, 36), ConsoleColor.Red, ConsoleColor.Black, 4);

            while (true)
            {
                Timer.Start();

                if (KeyAvailable) // Check for input
                {
                    KeyInfo = ReadKey(true);
                }
                if (Timer.ElapsedMilliseconds >= TickRate)
                {

                    MenuBuffer.ResetBuffer();

                    MenuBuffer.DrawMenuTexture(GameTextures.ArrayPlanet, (0, 0), ConsoleColor.Gray, ConsoleColor.Black);
                    part_three.Update(tickcount, MenuBuffer);


                    MenuBuffer.DrawText("Press ENTER to continue...", (5, 38), ConsoleColor.White, ConsoleColor.Black);
                    MenuRenderer.RenderBuffer();

                    tickcount++;

                    if (KeyInfo != null)
                    {
                        if (KeyInfo.Value.Key == ConsoleKey.Enter)
                            break;
                    }


                    KeyInfo = null; // Reset Key input stream
                    Timer.Restart();
                }
            }
            Timer.Stop();
            KeyInfo = null;
            MainGame(false, false, false, false, 700);

            SoundManager.BackGround.PlayLooping();

            SetWindowSize(MenuGameSize.x, MenuGameSize.y);
            SetBufferSize(MenuGameSize.x, MenuGameSize.y);

            tickcount = 0;
            TextManager part_four = new TextManager();
            part_four.AddText("Location: Habitable Milkdromeda", (5, 31), ConsoleColor.White, ConsoleColor.Black, 4);
            part_four.AddText("Comrade, we are evacuating the Planets in the -#@-' striP ", (5, 33), ConsoleColor.White, ConsoleColor.Black, 4);
            part_four.AddText("We forever are grateful for your servic-#%%", (5, 34), ConsoleColor.White, ConsoleColor.Black, 4);
            part_four.AddText("ERROR -%%# 4556, Exception: Gravitational Anomaly", (5, 35), ConsoleColor.Red, ConsoleColor.Black, 4);
            part_four.AddText("%#RFWRT--())) exception thrown in System.dll", (5, 36), ConsoleColor.Red, ConsoleColor.Black, 4);

            while (true)
            {
                Timer.Start();

                if (KeyAvailable) // Check for input
                {
                    KeyInfo = ReadKey(true);
                }
                if (Timer.ElapsedMilliseconds >= TickRate)
                {

                    MenuBuffer.ResetBuffer();

                    MenuBuffer.DrawMenuTexture(GameTextures.ArrayBlackHole, (0, 0), ConsoleColor.Gray, ConsoleColor.Black);
                    part_four.Update(tickcount, MenuBuffer);


                    MenuBuffer.DrawText("Press ENTER to continue...", (5, 38), ConsoleColor.White, ConsoleColor.Black);
                    MenuRenderer.RenderBuffer();

                    tickcount++;

                    if (KeyInfo != null)
                    {
                        if (KeyInfo.Value.Key == ConsoleKey.Enter)
                            break;
                    }

                    KeyInfo = null; // Reset Key input stream
                    Timer.Restart();
                }
            }
            Timer.Stop();
            KeyInfo = null;
            MainGame(true, true, false, true, 3000);
        }
    }
}
