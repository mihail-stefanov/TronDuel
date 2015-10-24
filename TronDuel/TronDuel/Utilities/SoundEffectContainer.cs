namespace TronDuel.Utilities
{
    using System.Media;

    public class SoundEffectContainer
    {
        private SoundPlayer powerupSoundPlayer  = new SoundPlayer();

        public SoundEffectContainer()
        {
            LoadSounds();
        }

        private void LoadSounds()
        {
            // Loading sounds
            this.powerupSoundPlayer.SoundLocation = "powerUp.wav";
            this.powerupSoundPlayer.LoadAsync();
        }

        public void PlayPowerUp()
        {
            this.powerupSoundPlayer.Play();
        }
    }
}
