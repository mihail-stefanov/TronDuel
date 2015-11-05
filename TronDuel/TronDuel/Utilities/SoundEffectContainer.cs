namespace TronDuel.Utilities
{
    using System.Media;

    public class SoundEffectContainer
    {
        private SoundPlayer powerupSoundPlayer  = new SoundPlayer();
        private SoundPlayer shotSoundPlayer = new SoundPlayer();
        private SoundPlayer enemyShotSoundPlayer = new SoundPlayer();
        private SoundPlayer ammoLoadPlayer = new SoundPlayer();
        private SoundPlayer hitPlayer = new SoundPlayer();

        public SoundEffectContainer()
        {
            LoadSounds();
        }

        private void LoadSounds()
        {
            // Loading sounds
            this.powerupSoundPlayer.SoundLocation = "powerUp.wav";
            this.powerupSoundPlayer.LoadAsync();
            this.shotSoundPlayer.SoundLocation = "shot.wav";
            this.shotSoundPlayer.LoadAsync();
            this.enemyShotSoundPlayer.SoundLocation = "enemyShot.wav";
            this.enemyShotSoundPlayer.LoadAsync();
            this.ammoLoadPlayer.SoundLocation = "ammoLoad.wav";
            this.ammoLoadPlayer.LoadAsync();
            this.hitPlayer.SoundLocation = "hit.wav";
            this.hitPlayer.LoadAsync();
        }

        public void PlayPowerUp()
        {
            this.powerupSoundPlayer.Play();
        }

        public void PlayShot()
        {
            this.shotSoundPlayer.Play();
        }

        public void PlayEnemyShot()
        {
            this.enemyShotSoundPlayer.Play();
        }

        public void PlayAmmoLoad()
        {
            this.ammoLoadPlayer.Play();
        }

        public void PlayHit()
        {
            this.hitPlayer.Play();
        }
    }
}
