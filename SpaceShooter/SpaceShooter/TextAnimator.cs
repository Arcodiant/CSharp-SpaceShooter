using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Space_Shooter;

namespace Craft_Batcher
{
    class TextAnimator
    {
        public string Message;
        public (int x, int y) Position;
        public int StartTick;
        public int DelayTick;
        public bool IsActive;
        public bool IsFinished;
        private ConsoleColor Foreground;
        private ConsoleColor Background;

        SoundManager SoundManager = new SoundManager();

        public TextAnimator(string message, (int x, int y) position, ConsoleColor foreground, ConsoleColor background, int delay)
        {
            Message = message;
            Position = position;
            Foreground = foreground;
            Background = background;
            DelayTick = delay;
        }
        public void Start(int currentTick)
        {
            IsActive = true;
            StartTick = currentTick;
        }

        public void Update(int currentTick, GridBuffer Buffer)
        {
            if (!IsActive) return;

            string text;

            int ticksSinceStart = currentTick - StartTick;
            int charactersToShow = Math.Min(Message.Length, ticksSinceStart / DelayTick + 1);

            text = Message.Substring(0, charactersToShow);

            
            Buffer.DrawText(text, Position, Foreground, Background);

            if (charactersToShow == Message.Length)
            {
                IsFinished = true;
            }
        }
    }
}
