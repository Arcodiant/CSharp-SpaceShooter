using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

namespace Craft_Batcher
{
    internal class Game
    {
        ConsoleKeyInfo? KeyInfo;
        SoundPlayer MusicPlayer;
        Render Renderer;
        Stopwatch Timer;
        CollisionManager Manager;
        Random rnd = new Random();

        MenuBox MainMenuBox;
        InputBox NewGameInputBox;

        Dictionary<string, string> Menu = new Dictionary<string, string>()
        {
            {"New Game", "PERESTROIKA - Game made by DataCrab."},
            {"Load Game", "Programmed in C#."},
            {"Options", " ───────────────────────────────── "},
            {"Quit", "Thanks for Playing!"}

        };

        GridBuffer Buffer;
        Textures GameTextures;
        Player User;

        (int x, int y) MainMenuSize = (140, 40);
        (int x, int y) SingleBoxSize = (70, 20);
        (int x, int y) MainGameSize = (80, 25);
        private int TickRate = 16;

        public Game()
        {
            KeyInfo = new ConsoleKeyInfo?();
            MusicPlayer = new SoundPlayer();
            Buffer = new GridBuffer(MainGameSize.x, MainGameSize.y);
            Renderer = new Render((MainGameSize.x, MainGameSize.y), Buffer);
            Timer = new Stopwatch();

            GameTextures = new Textures();

            User = new Player(GameTextures.Spaceship, Buffer);
            Manager = new CollisionManager(Buffer, User);


            Title = "=== Craft Batcher ==="; 
            SetWindowSize(MainMenuSize.x, MainMenuSize.y);
            SetBufferSize(MainMenuSize.x, MainMenuSize.y);
            CursorVisible = false;

        }

        public void MainMenu()
        {
            // Initialization

            MainMenuBox = new MenuBox(50, 9, (5, 5), "Main Menu");
            MainMenuBox.InitBox(false);
            MainMenuBox.InitializeMenu(Menu);
            MainMenuBox.RenderMenu(true, true);

            // Textures Loading
            GameTextures.Draw((56, 0), GameTextures.IntroAstronaut, ConsoleColor.Gray, ConsoleColor.Black);
            GameTextures.Draw((52, 32), GameTextures.MenuTitle, ConsoleColor.Black, ConsoleColor.Red);
            GameTextures.Draw((5, 16), GameTextures.CCCP_Flag, ConsoleColor.Black, ConsoleColor.Yellow); 

            while (true)
            {

                KeyInfo = Console.ReadKey();
                if (KeyInfo != null)
                {
                    if (KeyInfo.Value.Key == ConsoleKey.UpArrow || KeyInfo.Value.Key == ConsoleKey.DownArrow)
                    {
                        MainMenuBox.SelectMenu(KeyInfo);
                        MainMenuBox.RenderMenu(true, true);
                    }
                    if (KeyInfo.Value.Key == ConsoleKey.Enter)
                    {
                        switch(MainMenuBox.GetIndex())
                        {
                            case 0:
                                NewGame();
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

        public void NewGame()
        {
            Clear();
            SetWindowSize(SingleBoxSize.x, SingleBoxSize.y);
            SetBufferSize(SingleBoxSize.x, SingleBoxSize.y);
            string saveName;


            NewGameInputBox = new InputBox(30, 6, (20, 5), "Create Save File");
            NewGameInputBox.InitBox(true);
            saveName = NewGameInputBox.CreateInputBox(@"Type in the save file name");

            Clear();
            SaveData.CreateSave(saveName + ".txt");

            MainGame();
        }

        public void MainGame()
        {
            SetWindowSize(MainGameSize.x, MainGameSize.y);
            SetBufferSize(MainGameSize.x, MainGameSize.y);
            CursorVisible = false;
            int tickcount = 0;

            while (true) // Basic Tick loop
            {
                Timer.Start();


                if (KeyAvailable) // Check for input
                {
                    KeyInfo = Console.ReadKey(true);
                }
                if (Timer.ElapsedMilliseconds >= TickRate) // Update each Tick
                {
                    tickcount++;

                    Manager.Update(tickcount);
                    Renderer.RenderGame(User, Manager, tickcount);




                    //if (KeyInfo != null && KeyInfo.Value.Key == ConsoleKey.A) // for input movement
                    if (KeyInfo != null)
                    {
                        if (KeyInfo.Value.Key != ConsoleKey.Spacebar)
                            User.Move(KeyInfo);
                        else if (KeyInfo.Value.Key == ConsoleKey.Spacebar)
                            User.Attack(GameTextures.Bullet, Manager);
                    }


                    KeyInfo = null; // Reset Key input stream
                    Timer.Restart();
                }

            }
        }
    }
}
