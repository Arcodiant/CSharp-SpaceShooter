using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Space_Shooter;

namespace Space_Shooter
{
    internal class Animator
    {
        GridBuffer Buffer;
        public Animator(GridBuffer buffer) 
        { 
            Buffer = buffer;
        }

        public int Animation(int currentTick, int animationTick, int animationRate, (int x, int y) position, 
            List<List<char[]>> AnimationFrames, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            int ticksSinceStart = currentTick - animationTick;
            int animationFrame = ticksSinceStart / animationRate;
            if (animationFrame < AnimationFrames.Count)
            {
                Buffer.DrawCharList(AnimationFrames[animationFrame], position, foreground, background);
            }
            else
            {
                return animationFrame;
            }
            return 0;
        }
    }
}
