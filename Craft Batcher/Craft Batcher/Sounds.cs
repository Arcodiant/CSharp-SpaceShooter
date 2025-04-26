using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Craft_Batcher
{
    class Sounds
    {
        SoundPlayer Player;
        private string FilePath;
        public Sounds(string filePath)
        {
            Player = new SoundPlayer();
            FilePath = filePath;
        }

        // play, load, sound location methods - that way I can initialize multiple objects (sounds) 
    }
}
