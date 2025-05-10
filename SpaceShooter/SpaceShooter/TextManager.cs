using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Space_Shooter;

namespace Space_Shooter
{
    class TextManager
    {
        List<TextAnimator> texts = new List<TextAnimator>();

        public void AddText(string message, (int x, int y) position, ConsoleColor foreground, ConsoleColor background, int delay)
        {
            var text = new TextAnimator(message, position, foreground, background, delay);
            texts.Add(text);
        }

        public void Update(int tickcount, GridBuffer Buffer)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                var text = texts[i];
                if (!text.IsActive)
                {
                    if (i == 0 || texts[i - 1].IsFinished)
                    {
                        text.Start(tickcount);
                    }
                }
                if (text.IsActive)
                {
                    text.Update(tickcount, Buffer);
                }
            }
        }
    }
}
