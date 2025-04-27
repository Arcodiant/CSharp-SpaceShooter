using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    class KeyBox : Box
    {
        private (int x, int y) Position;
        private int Width;
        private int Height;
        private string Title;
        public KeyBox(int width, int height, (int x, int y) position, string title) : base(width, height, position, title)
        {
            Position = position;
            Width = width;
            Height = height;
            Title = title;
        }


        public void InitializeKeyBox()
        {

        }
    }
}
