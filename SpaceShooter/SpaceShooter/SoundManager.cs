using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    class SoundManager
    {
        public SoundPlayer Select = new SoundPlayer("smw_coin.wav");
        public SoundPlayer Text = new SoundPlayer("Text.wav");
        public SoundPlayer HitorExplosion = new SoundPlayer("Explosion.wav");
        public SoundPlayer Shooting = new SoundPlayer();
        public SoundPlayer BackGround = new SoundPlayer("Music\\Background music.wav");
        public SoundPlayer BackGroundGame = new SoundPlayer("Music\\gamemusic.wav");

        public SoundManager()
        {
        }

        public void PlaySelect()
        {
            Select.LoadAsync();
            Select.Play();
        }

        public void PlayConfirm()
        {
            Select.Play();
        }

        public void PlayText()
        {
            Text.LoadAsync();
            Text.Play();
        }

        public void PlayExplosion()
        {
            HitorExplosion.Play();
        }

        public void PlayShooting()
        {
            Select.Play();
        }
    }
}
