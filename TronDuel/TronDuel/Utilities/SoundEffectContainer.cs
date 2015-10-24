namespace TronDuel.Utilities
{
    using System.Media;

    public class SoundEffectContainer
    {
        private SoundPlayer powerupSoundPlayer  = new SoundPlayer();
        private SoundPlayer shotSoundPlayer = new SoundPlayer();

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
        }

        public void PlayPowerUp()
        {
            this.powerupSoundPlayer.Play();
        }

        public void PlayShot()
        {
            this.shotSoundPlayer.Play();
        }
    }
}
